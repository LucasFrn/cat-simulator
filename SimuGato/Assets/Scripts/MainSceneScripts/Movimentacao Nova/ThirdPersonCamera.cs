using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField]Transform orientation;
    [SerializeField]Transform player;
    [SerializeField]Transform playerObj;
    [SerializeField]Rigidbody rb;
    [SerializeField]float rotationSpeed;
    [Header("Cameras")]
    [SerializeField]GameObject cameraPadrao;
    [SerializeField]GameObject cameraJardim;
    CameraStyle currentStyle;
    public enum CameraStyle
    {
        Basic,
        Garden
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState=CursorLockMode.Locked;
        Cursor.visible=false;
    }

    // Update is called once per frame
    void Update()
    {
        //rotate orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y,transform.position.z);
        orientation.forward=viewDir;
        //rotate player object
        float horizontalInput =  Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orientation.forward * verticalInput+ orientation.right*horizontalInput;

        if(inputDir!=Vector3.zero){
            playerObj.forward = Vector3.Slerp(playerObj.forward,inputDir.normalized,Time.deltaTime*rotationSpeed);
        }
        if(Input.GetKeyDown(KeyCode.G)){
            TrocaCamera(CameraStyle.Garden);
        }
        if(Input.GetKeyDown(KeyCode.B)){
            TrocaCamera(CameraStyle.Basic);
        }

    }
    public void TrocaCamera(CameraStyle cameraNova){
        cameraJardim.SetActive(false);
        cameraPadrao.SetActive(false);
        if(cameraNova==CameraStyle.Basic)
            cameraPadrao.SetActive(true);
        if(cameraNova==CameraStyle.Garden){
            cameraJardim.SetActive(true);
        }
    }
}
