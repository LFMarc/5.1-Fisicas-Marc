using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    // Referencias a los objetos de la jerarquía
    public Transform kartModel;
    public Rigidbody rb;
    public LayerMask grassLayer; // Capa para grass
    public LayerMask obstacleLayer; // Capa para obstáculos
    public LayerMask floorLayer; // Capa para elevaciones (floor)
    public float moveSpeed = 10f;
    public float turnSpeed = 50f;
    public float raycastDistanceFront = 5f; // Distancia del raycast frontal
    public float raycastDistanceDown = 2f; // Distancia del raycast hacia abajo
    public float wheelFriction = 1f;
    public float maxWheelAngle = 20f;
    public float baseGravity = 10f; // Valor base de la gravedad
    private float currentGravity;
    public float decelerationSpeed = 5f;
    public float jumpForce = 500f; // Fuerza del salto al detectar un obstáculo
    private bool hasJumped = false; // Estado para evitar saltos múltiples continuos

    // Variables de movimiento
    private float moveInput;
    private float turnInput;

    // Estado del raycast hacia abajo
    private bool isGrounded = false;

    // Variables para controlar las ruedas
    private bool wheelsLifted = false;
    private bool isLifting = false;
    private bool isResetting = false;
    private float currentLiftAmount = 0f;

    public float wheelLiftAmount = 1.5f;
    public float liftSpeed = 2f; // Velocidad ajustada para levantar o bajar ruedas

    public Transform frontLeftWheel;
    public Transform frontRightWheel;

    // Posición inicial de las ruedas
    private Vector3 initialLeftWheelPosition;
    private Vector3 initialRightWheelPosition;

    void Start()
    {
        // Inicializar la gravedad actual con el valor base
        currentGravity = baseGravity;

        // Guardar las posiciones iniciales de las ruedas
        initialLeftWheelPosition = frontLeftWheel.localPosition;
        initialRightWheelPosition = frontRightWheel.localPosition;
    }

    void Update()
    {
        // Obtener entradas del jugador
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        // Raycast frontal
        RaycastHit hitFront;
        if (Physics.Raycast(kartModel.position, kartModel.forward, out hitFront, raycastDistanceFront))
        {
            Debug.DrawRay(kartModel.position, kartModel.forward * raycastDistanceFront, Color.green);

            // Si el raycast detecta un obstáculo en el obstacleLayer, realizar un salto
            if (((1 << hitFront.collider.gameObject.layer) & obstacleLayer) != 0 && isGrounded && !hasJumped)
            {
                Debug.Log("Obstáculo detectado. Saltando...");
                ApplyJump();
            }

            // Si detecta una elevación, levantar las ruedas
            if (((1 << hitFront.collider.gameObject.layer) & floorLayer) != 0)
            {
                Debug.Log("Se detectó una elevación. Levantando ruedas...");
                isLifting = true;
                isResetting = false; // Asegurarse de que no esté en modo de restablecimiento
            }
        }
        else
        {
            Debug.DrawRay(kartModel.position, kartModel.forward * raycastDistanceFront, Color.red);
            if (wheelsLifted)
            {
                Debug.Log("No se detecta elevación. Bajando ruedas...");
                isResetting = true;
                isLifting = false; // Asegurarse de que no esté en modo de levantamiento
            }
        }

        // Raycast hacia abajo
        RaycastHit hitDown;
        if (Physics.Raycast(kartModel.position, Vector3.down, out hitDown, raycastDistanceDown))
        {
            isGrounded = true;

            // Cambiar la gravedad si la capa es grass
            if (((1 << hitDown.collider.gameObject.layer) & grassLayer) != 0)
            {
                currentGravity = baseGravity * 2; // Gravedad duplicada
            }
            else
            {
                currentGravity = baseGravity; // Gravedad base
            }

            Debug.DrawRay(kartModel.position, Vector3.down * raycastDistanceDown, Color.blue);
        }
        else
        {
            isGrounded = false;
        }

        // Manejar el movimiento de las ruedas
        HandleWheelMovement();
    }

    void FixedUpdate()
    {
        // Aplicar movimiento solo si el vehículo detecta algo debajo
        if (isGrounded)
        {
            if (moveInput != 0)
            {
                Vector3 moveDirection = kartModel.forward * moveInput * moveSpeed;
                rb.AddForce(moveDirection, ForceMode.Force);
            }
        }

        // Desaceleración si no hay entrada de movimiento
        if (moveInput == 0)
        {
            Vector3 currentVelocity = rb.velocity;
            Vector3 deceleration = -currentVelocity.normalized * decelerationSpeed;

            if (deceleration.magnitude > currentVelocity.magnitude)
            {
                deceleration = -currentVelocity;
            }

            rb.AddForce(deceleration, ForceMode.Acceleration);
        }

        // Giro del vehículo
        float turnAmount = turnInput * turnSpeed * Time.deltaTime;
        kartModel.Rotate(0f, turnAmount, 0f);

        // Asegurar que el modelo del kart siga la posición de la esfera
        kartModel.position = rb.position;

        // Aplicar gravedad
        rb.AddForce(Vector3.down * currentGravity, ForceMode.Acceleration);
    }

    void ApplyJump()
    {
        // Aplicar fuerza hacia arriba para saltar
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        hasJumped = true;

        // Restablecer la capacidad de saltar después de un tiempo
        Invoke(nameof(ResetJump), 1f); // Ajusta el tiempo según sea necesario
    }

    private void ResetJump()
    {
        hasJumped = false;
    }

    private void HandleWheelMovement()
    {
        if (isLifting)
        {
            if (currentLiftAmount < wheelLiftAmount)
            {
                float liftStep = liftSpeed * Time.deltaTime;
                currentLiftAmount = Mathf.Min(currentLiftAmount + liftStep, wheelLiftAmount);
                MoveWheels(liftStep);
            }
            else
            {
                isLifting = false;
                wheelsLifted = true;
            }
        }

        if (isResetting)
        {
            if (currentLiftAmount > 0f)
            {
                float resetStep = liftSpeed * Time.deltaTime;
                currentLiftAmount = Mathf.Max(currentLiftAmount - resetStep, 0f);
                MoveWheels(-resetStep);
            }
            else
            {
                isResetting = false;
                wheelsLifted = false;
            }
        }
    }

    private void MoveWheels(float amount)
    {
        frontLeftWheel.localPosition = initialLeftWheelPosition + Vector3.up * currentLiftAmount;
        frontRightWheel.localPosition = initialRightWheelPosition + Vector3.up * currentLiftAmount;
    }
}