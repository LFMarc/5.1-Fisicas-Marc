using UnityEngine;

public class CameraFollowCar : MonoBehaviour
{
    public Transform target; // El modelo del coche que la c�mara debe seguir
    public Vector3 offset = new Vector3(0, 5, -10); // Offset de la c�mara respecto al coche
    public float followSpeed = 5f; // Velocidad de seguimiento
    public float rotationSpeed = 5f; // Velocidad de ajuste de la rotaci�n

    void FixedUpdate()
    {
        if (target == null) return;

        // Calcula la posici�n deseada de la c�mara
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Calcula la rotaci�n deseada de la c�mara (mirando al coche)
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
