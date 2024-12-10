using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
	{
        transform.Rotate(0, 100 * Time.deltaTime, 0);
	}
    void FixedUpdate()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward, Color.black);
        if (Physics.Raycast(transform.position, transform.forward, out hit))
		{
            Debug.DrawLine(transform.position, hit.point, Color.red);
            print("Hit! " + hit.collider.name);
           // print("Point: " + hit.point);
            //print("Normal: " + hit.normal);
            //print("RB! " + hit.rigidbody);
		}

  //      if (Physics.SphereCast(transform.position, 1, transform.forward, out hit))
		//{
  //          print("Sphere Hit! " + hit.collider.name);
  //      }


    }
}
