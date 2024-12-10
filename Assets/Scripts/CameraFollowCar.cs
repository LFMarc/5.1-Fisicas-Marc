using UnityEngine;

public class CameraFollowCar : MonoBehaviour
{
    public Transform target; // El modelo del coche que la cámara debe seguir
    public Vector3 offset = new Vector3(0, 5, -10); // Offset de la cámara respecto al coche
    public float followSpeed = 5f; // Velocidad de seguimiento
    public float rotationSpeed = 5f; // Velocidad de ajuste de la rotación

    void FixedUpdate()
    {
        if (target == null) return;

        // Calcula la posición deseada de la cámara
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Calcula la rotación deseada de la cámara (mirando al coche)
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
