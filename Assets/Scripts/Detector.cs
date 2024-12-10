using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
	//Atributes
	public bool floorContact;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Ball")
			print("Ejemplo de colision 2D");
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Ball")
			print("Toque!");

		if (collision.gameObject.tag == "Floor")
			floorContact = true;
	}
	private void OnCollisionExit(Collision collision)
	{
		if(collision.gameObject.tag == "Floor")
			floorContact = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Ball")
		{
			print("GOL!!");
			other.gameObject.SetActive(false);
		}
	}
}
