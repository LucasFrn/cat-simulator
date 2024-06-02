using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenEvents {
    public event Action onEnterGarden;
    public void EnterGarden(){
        if(onEnterGarden!=null){
            onEnterGarden();
        }
    }
    public event Action onLeaveGarden;
    public void LeaveGarden(){
        if(onLeaveGarden!=null){
            onLeaveGarden();
        }
    }
}
