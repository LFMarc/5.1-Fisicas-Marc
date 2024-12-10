using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseRaycast : MonoBehaviour
{
    public Camera camera;
    public Rigidbody objectSelected = null;
    //Unity Callbacks
    void FixedUpdate()
    {
        RaycastHit hit;
        
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
		{
            Debug.DrawLine(camera.transform.position, hit.point, Color.blue);
		}
    }

    void Update()
    {
        // Si quieres lanzar el raycast cuando presionas el botón izquierdo del mouse, por ejemplo:
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Crear un rayo desde el punto donde se encuentra el cursor del ratón.
            RaycastHit hit; // Variable que almacenará la información del objeto impactado.

            if (Physics.Raycast(ray, out hit))
            {
                //hit.collider.gameObject.SetActive(false);
                objectSelected = hit.rigidbody;
            }
        }
    }
}
