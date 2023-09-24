using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Vase : MonoBehaviour
{
    
    public GameObject quebrado;
    private void Start()
    {
        
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<TerrainCollider>())
        {
            gameObject.GetComponent<MeshFilter>().mesh = quebrado.GetComponent<MeshFilter>().sharedMesh;
            gameObject.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y,0);
            gameObject.GetComponent<Rigidbody>().freezeRotation =  true;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
