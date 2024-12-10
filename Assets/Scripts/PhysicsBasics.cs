using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsBasics : MonoBehaviour
{
    //Attributes
    public Rigidbody playerRigidbody;
    public Rigidbody2D playerRigidbody2D;//Para juegos 2D
    public float jumpForce ;
    public float speedMove = 200;
    public Detector detector;

    // Start is called before the first frame update
    void Start()
    {
    }


    //Update for detection of Inputs
	private void Update()
	{
        print("Velocity: " + playerRigidbody.velocity);

        //Jump
        if (Input.GetKeyUp(KeyCode.Space) && detector.floorContact == true)
            playerRigidbody.AddForce(Vector3.up * jumpForce);
        //playerRigidbody2D.AddForce(Vector2.up * jumpForce); //Para juegos 2D

    //playerRigidbody.AddForce(0, 1000, 0);  //Alternative

}

// FixedUpdate for Physics
void FixedUpdate()
    {
        playerRigidbody.AddForce(Input.GetAxis("Horizontal") * speedMove , 0, Input.GetAxis("Vertical") * speedMove );
        
    }
    
}
