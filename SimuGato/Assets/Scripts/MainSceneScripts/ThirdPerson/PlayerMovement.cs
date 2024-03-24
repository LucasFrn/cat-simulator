using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float moveSpeed = 3;
    float rotationSpeed = 4;
    float runningSpeed;
    float vaxis, haxis;
    public bool isJumping, isJumpingAlt, isGrounded = false;
    Vector3 movement;
    Rigidbody rb;
    Animator animator;

    void Start()
    {
        Debug.Log("Initialized: (" + this.name + ")");
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        if(animator==null){
            Debug.LogError("Personagem sem animator");
        }
    }


    void FixedUpdate()
    {
        if (!GameManager.Instance.jogoPausado)
        {
            if (GameManager.Instance.janelaEmFoco == 1)
            {
                /*  Controller Mappings */
                vaxis = Input.GetAxis("Vertical");
                haxis = Input.GetAxis("Horizontal");
                isJumping = Input.GetButton("Jump");
                isJumpingAlt = Input.GetKey(KeyCode.Joystick1Button0);

                //Simplified...
                runningSpeed = vaxis;


                if (isGrounded)
                {
                    movement = new Vector3(0, 0f, runningSpeed * 7);        // Multiplier of 8 seems to work well with Rigidbody Mass of 1.
                    movement = transform.TransformDirection(movement);      // transform correction A.K.A. "Move the way we are facing"
                }
                else
                {
                    movement *= 0.70f;                                      // Dampen the movement vector while mid-air
                }
                if(rb.velocity.magnitude<5)
                    rb.AddForce(movement.normalized*3,ForceMode.Impulse);//impurrão ver se o gato sobe moro
                rb.AddForce(movement * moveSpeed);   // Movement Force
                

                if ((isJumping || isJumpingAlt) && isGrounded)
                {
                    Debug.Log(this.ToString() + " isJumping = " + isJumping);
                    animator.SetTrigger("Pulei");
                    rb.AddForce(Vector3.up * 150);
                }



                if ((Input.GetAxis("Vertical") != 0f || Input.GetAxis("Horizontal") != 0f) && !isJumping && isGrounded)
                {
                    if (Input.GetAxis("Vertical") >= 0)
                        transform.Rotate(new Vector3(0, haxis * rotationSpeed, 0));
                    else
                        transform.Rotate(new Vector3(0, -haxis * rotationSpeed, 0));

                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Entered");
        if (collision.gameObject.CompareTag("Terra") || collision.gameObject.CompareTag("Pesca") || collision.gameObject.CompareTag("Banco")|| collision.gameObject.CompareTag("ObjetosDaCasa")||collision.gameObject.CompareTag("Garden"))
        {
            isGrounded = true;
            animator.ResetTrigger("Pulei");
        }
        
    }
    void OnCollisionStay(Collision collision){
        if(collision.gameObject.CompareTag("Terra") || collision.gameObject.CompareTag("Pesca")|| collision.gameObject.CompareTag("ObjetosDaCasa")||collision.gameObject.CompareTag("Garden")){
            isGrounded=true;
        }
    } //Podre, corrigir a maneira como o gato sabe se está no chão no futuro

    void OnCollisionExit(Collision collision)
    {
        Debug.Log("Exited");
        if (collision.gameObject.CompareTag("Terra") || collision.gameObject.CompareTag("Pesca")|| collision.gameObject.CompareTag("ObjetosDaCasa")||collision.gameObject.CompareTag("Garden"))
        {
            isGrounded = false;
            /*  Logica incorreta!
                Na borda do lago o gato colide ambos com a terra e com o lago, e ai ao sair de uma das colisoes
                ele já entrou no OnCollisionEnter da outra, e ele perde a capacidade de dar Input.
                Devido a isso foi necessario um OnCollisionStay */
        }
    }
}
