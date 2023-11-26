using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 15.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private float turner;
    private float looker;
    public float sensitivity = 2;
    public GameObject cam;
  


    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!GameManager.Instance.jogoPausado){
            if(GameManager.Instance.janelaEmFoco==1){
                
                
                turner = Input.GetAxis("Mouse X") * sensitivity;
                looker = -Input.GetAxis("Mouse Y") * sensitivity;
                if (turner != 0)
                {
                    //Code for action on mouse moving right
                    transform.eulerAngles += new Vector3(0, turner, 0);
                }
                if (looker != 0)
                {
                    //Code for action on mouse moving right

                    if (cam.transform.eulerAngles.x >= 350 || cam.transform.eulerAngles.x <= 15)
                    {
                        cam.transform.eulerAngles += new Vector3(looker, 0, 0);
                    }
                    else if (transform.eulerAngles.x < 350 && cam.transform.eulerAngles.x > 50)
                    {
                        cam.transform.eulerAngles = new Vector3(350, cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);
                    }
                    else { cam.transform.eulerAngles = new Vector3(15, cam.transform.eulerAngles.y, cam.transform.eulerAngles.z); }
                }
                
               
            }
            //Comandos que ocorrem mesmo se a janela em foco n for a 1
        }
        //Comando que ocorrem mesmo se o jogo esiver pausado
    }
}