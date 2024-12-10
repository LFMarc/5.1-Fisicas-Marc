using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultaScript : MonoBehaviour
{
    public Rigidbody RbPeso;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && RbPeso.isKinematic == true)
        {
            RbPeso.isKinematic = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            RbPeso.isKinematic = true;
        }
    }
}
