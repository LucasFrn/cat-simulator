using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarraDoJogador : MonoBehaviour
{
    [SerializeField]float speed;
    Rigidbody rb;
    //Poderzinho
    bool skillBarrinhaMaiorComprada; //OBS: A barrinha normal tem (6,5,1)
    
    // Start is called before the first frame update
    void Start()
    {
        if(skillBarrinhaMaiorComprada){
            transform.localScale= new Vector3(6,7,1);
        }
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
    public void AtivaHabilidade(){
        transform.position = new Vector3(0,0,-0.5f);
        transform.localScale= new Vector3(6,7,1);
    }
}
