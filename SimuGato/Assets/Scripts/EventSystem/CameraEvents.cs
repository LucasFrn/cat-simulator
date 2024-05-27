using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraEvents 
{
    public event Action onCameraPause;

    public void CameraPause(){
        if(onCameraPause!=null){
            onCameraPause();
        }
    }
    public event Action onCameraUnPause;

    public void CameraUnPause(){
        if(onCameraUnPause!=null){
            onCameraUnPause();
        }
    }

}
