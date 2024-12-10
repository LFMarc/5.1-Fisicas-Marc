using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTunel : MonoBehaviour
{
	//Attributes
	public float force = 10;
	Rigidbody objectIn = null;

	//Unity Callbacks
	//Trigger stay example
	//private void OnTriggerStay(Collider other)
	//{
	//	objectIn = other.GetComponent<Rigidbody>().AddForce(Vector3.up * force);
	//}

	private void OnTriggerEnter(Collider other)
	{
		objectIn = other.GetComponent<Rigidbody>();
		objectIn.velocity = Vector3.zero;
	}
	private void OnTriggerExit(Collider other)
	{
		objectIn = null;
		gameObject.SetActive(false);
	}

	private void FixedUpdate()
	{
		if (objectIn != null)
			objectIn.AddForce(Vector3.up * force);
	}
}
