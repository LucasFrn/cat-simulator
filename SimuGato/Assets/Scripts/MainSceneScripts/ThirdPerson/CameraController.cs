using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target, camTransform;
    [Range(1, 10)]
    public float distance, sensitivityX, sensitivityY, distancePanAmount;
    [Range(-100, 100)]
    private float currentX, currentY, i, distancePan, ii;
    public bool follow;
    GameObject player;
    PlayerMovement playerController;
    Vector3 a, b;

    private float turner;
    private float looker;
    public float sensitivity = 2;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerMovement>();
        i = 0.5f;
        camTransform = transform;                                                               //  grab transform of self
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        a = new Vector3(target.position.x, target.position.y, target.position.z);
        b = a;
    }

    void Update()
    {

        if (!GameManager.Instance.jogoPausado)
        {
            if (GameManager.Instance.janelaEmFoco == GameManager.JanelaEmFoco.Parque)
            {
                turner = Input.GetAxis("Mouse X") * sensitivity;
                looker = -Input.GetAxis("Mouse Y") * sensitivity;
                if (turner != 0)
                {
                    //Code for action on mouse moving right
                    camTransform.eulerAngles += new Vector3(0, turner, 0);
                }
                if (looker != 0)
                {
                    //Code for action on mouse moving right

                    if (camTransform.eulerAngles.x >= 350 || camTransform.eulerAngles.x <= 15)
                    {
                        camTransform.eulerAngles += new Vector3(looker, 0, 0);
                    }
                    else if (transform.eulerAngles.x < 350 && camTransform.eulerAngles.x > 50)
                    {
                        camTransform.eulerAngles = new Vector3(350, camTransform.eulerAngles.y, camTransform.eulerAngles.z);
                    }
                    else { camTransform.eulerAngles = new Vector3(15, camTransform.eulerAngles.y, camTransform.eulerAngles.z); }
                }


                if (Input.GetAxis("Vertical") != 0.0f && playerController.isGrounded)   // if moving and on the ground...
                {                                                                       //       ...depth pan the camera out
                    if (i < 1)
                    {//  [ensures the float index never goes above 1...]
                        i += 0.003f * Input.GetAxis("Vertical");
                    }
                    else
                    {
                        i = 1;
                    }
                }

                if (Input.GetAxis("Vertical") == 0.0f && playerController.isGrounded)  // if not moving and on the ground...  
                {                                                                      //     ...retract the camera to the player

                    if (i > 0) i /= 1.01f;
                    else i = 0;
                }


                distance -= Input.GetAxis("Mouse ScrollWheel") * 2;

                if (distance <= 0)
                {
                    distance += 0.1f;
                }

                distancePan = /*Input.GetAxis("Vertical") * */ 2.5f * distancePanAmount;
                currentX += Input.GetAxis("Mouse X");   // + (Input.GetAxis("Horizontal 2") * 1);
                currentY += Input.GetAxis("Mouse Y");   // + (Input.GetAxis("Vertical 2") * 1);
            }
        }


    }

    private void LateUpdate()
    {
        if (!GameManager.Instance.jogoPausado)
        {
            if (GameManager.Instance.janelaEmFoco == GameManager.JanelaEmFoco.Parque)
            {
                if (follow)
                {
                    Vector3 dir = new Vector3(0, 0, distance);
                    Vector3 dir2 = new Vector3(0, 0, distance + distancePan);
                    dir = Vector3.Lerp(dir, dir2, i);
                    Quaternion rotation = Quaternion.Euler(looker, looker, 0);
                    camTransform.position = target.position + rotation * dir;
                    camTransform.position += new Vector3(0, 1.5f, 0);
                }


                a = new Vector3(target.position.x, target.position.y + 1.5f, target.position.z);

                ii += 0.01f;


                if (ii > 1.0f) //seconds
                {
                    b = new Vector3(target.position.x, target.position.y, target.position.z);
                }
                transform.LookAt(a);

                if (this.transform.position.y < player.transform.position.y + 1)
                {
                    this.transform.position = new Vector3(this.transform.position.x, player.transform.position.y + 1, this.transform.position.z);
                }
            }
        }
    }


}