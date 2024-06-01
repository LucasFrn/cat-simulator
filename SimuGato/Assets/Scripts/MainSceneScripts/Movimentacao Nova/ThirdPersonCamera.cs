using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour,IDataPersistance
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
    CinemachineFreeLook freelookPadrao;
    CinemachineFreeLook freelookJardim;
    CameraData dataLoadada;

    public enum CameraStyle
    {
        Basic,
        Garden
    }
    void OnEnable(){
        GameEventsManager.instance.cameraEvents.onCameraPause+=pauseCinemachineInputs;
        GameEventsManager.instance.cameraEvents.onCameraUnPause+=returnCinemachineInputs;
    }
    void OnDisable(){
        GameEventsManager.instance.cameraEvents.onCameraPause-=pauseCinemachineInputs;
        GameEventsManager.instance.cameraEvents.onCameraUnPause-=returnCinemachineInputs;
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState=CursorLockMode.Locked;
        Cursor.visible=false;
        freelookJardim = cameraJardim.GetComponent<CinemachineFreeLook>();
        freelookPadrao = cameraPadrao.GetComponent<CinemachineFreeLook>();
        LoadSavedData();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.Instance.jogoPausado){
            if(GameManager.Instance.janelaEmFoco==GameManager.JanelaEmFoco.Parque){
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
            }
        }
        //DEBUG CONTROL
        if(Input.GetKeyDown(KeyCode.G)){
            TrocaCamera(CameraStyle.Garden);
        }
        if(Input.GetKeyDown(KeyCode.B)){
            TrocaCamera(CameraStyle.Basic);
        }
        if(Input.GetKeyDown(KeyCode.P)){
            pauseCinemachineInputs();
        }
        if(Input.GetKeyDown(KeyCode.LeftBracket)){
            returnCinemachineInputs();
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
    public void pauseCinemachineInputs(){
        freelookJardim.m_XAxis.m_MaxSpeed=0;
        freelookJardim.m_YAxis.m_MaxSpeed=0;
        freelookPadrao.m_XAxis.m_MaxSpeed=0;
        freelookPadrao.m_YAxis.m_MaxSpeed=0;
    }
    public void returnCinemachineInputs(){
        freelookJardim.m_XAxis.m_MaxSpeed=300;
        freelookJardim.m_YAxis.m_MaxSpeed=2;
        freelookPadrao.m_XAxis.m_MaxSpeed=300;
        freelookPadrao.m_YAxis.m_MaxSpeed=2;
    }

    public void LoadData(GameData data)
    {
       dataLoadada=data.cameraData;
    }

    public void SaveData(GameData data)
    {
        CameraData newCameraData = new CameraData();
        newCameraData.cameraPos = transform.position;
        newCameraData.cameraRot = transform.rotation.eulerAngles;
        newCameraData.orientationRotation = orientation.rotation.eulerAngles;
        data.cameraData=newCameraData;
    }
    void LoadSavedData(){
        orientation.rotation=Quaternion.Euler(dataLoadada.orientationRotation);
        freelookPadrao.ForceCameraPosition(dataLoadada.cameraPos,Quaternion.Euler(dataLoadada.cameraRot));
    }
}
