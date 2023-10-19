using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarraDoJogador : MonoBehaviour
{
    [SerializeField]float speed;
    Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        if(rb==null){
            Debug.LogError("Componente Sem Rigidbody");
        }
        speed = rb.mass*20f;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetButton("Fire1")&&ControllerMiniGamePesca.controllerMiniGamePesca.miniGameRodando==true){
            rb.AddForce(Vector3.up*speed*Time.deltaTime,ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision collision){
        if(collision.collider.CompareTag("BarreiraPesca")){
            rb.velocity=Vector3.zero;
        }
    }

}
