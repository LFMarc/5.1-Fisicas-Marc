using UnityEngine;

public class BallCollisionHandler : MonoBehaviour
{
    public GameObject crossPrefab; // Arrastra aquí el objeto "Cross" en el inspector
    private CameraFollow cameraFollow; // Referencia al script de la cámara
    private bool crossSpawned = false; // Indicador para saber si ya se creó el "Cross"
    private Vector3 startPosition; // Posición inicial de la bola

    void Start()
    {
        // Encuentra el script de la cámara y almacénalo
        cameraFollow = Camera.main.GetComponent<CameraFollow>();

        // Guarda la posición inicial de la bola
        startPosition = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") && !crossSpawned)
        {
            // Deja de seguir al target
            if (cameraFollow != null)
            {
                cameraFollow.target = null;
            }

            // Crea el objeto "Cross" en el punto de colisión
            Vector3 collisionPoint = collision.contacts[0].point; // Obtiene el punto de contacto
            Vector3 spawnPosition = collisionPoint + Vector3.up * 0.1f; // Sube un poco hacia arriba
            Quaternion spawnRotation = Quaternion.Euler(0, 45, 0); // Rotación de 45 grados
            Instantiate(crossPrefab, spawnPosition, spawnRotation);

            float distanceTravelled = Vector3.Distance(startPosition, collisionPoint);

            Debug.Log("Distancia recorrida por la bola: " + distanceTravelled + " unidades.");

            crossSpawned = true;
        }
    }
}
