using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // La bola como target
    public float smoothSpeed = 0.125f;  // Suavidad del movimiento
    public Vector3 offset;  // Offset para mantener la distancia de la c�mara a la bola

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = new Vector3(
                target.position.x + offset.x,
                target.position.y + offset.y,
                transform.position.z);  // Mant�n el mismo Z para c�maras en perspectiva o 2D

            // Suaviza el movimiento
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Actualiza la posici�n de la c�mara
            transform.position = smoothedPosition;
        }
    }
}


