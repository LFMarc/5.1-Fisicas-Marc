using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSceneController : MonoBehaviour
{
    //Attributes
    [Range(30,80)]
    public float cannonAngle;
    [Range(100,1000)]
    public float cannonForce;
    public Transform cannon;
    public Transform cannonSpawnPoint;
    public Rigidbody cubeProyectile;
    public Rigidbody sphereProyectile;
    public CameraMouseRaycast cameraMouseRaycast;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cannon.rotation = Quaternion.Euler(cannonAngle, 0, 0);
        //Shoot Object Selected
        if (cameraMouseRaycast.objectSelected != null)
		{
            //Move object to spawn point
            cameraMouseRaycast.objectSelected.transform.position = cannonSpawnPoint.position;
            //Add Force to Selected Object
            cameraMouseRaycast.objectSelected.AddForce(cannon.up * cannonForce);

            //reset Selected Object
            cameraMouseRaycast.objectSelected = null;
        }
    }
}
