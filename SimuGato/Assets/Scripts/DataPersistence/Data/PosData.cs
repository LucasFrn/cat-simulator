using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PosData
{
    public Vector3 pos;
    public Vector3 rot;

    public PosData(){
        pos = new Vector3(288.5f,16.3899994f,311.200012f);
        rot = new Vector3(0,-90,0);
    }
    public PosData(Vector3 position, Vector3 rotation){
        pos = position;
        rot = rotation;
    }
}
