using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PosData
{
    public Vector3 gatoPos;
    public PosData(){
        gatoPos = new Vector3(288.5f,16.3899994f,311.200012f);
    }
    public PosData(Vector3 position){
        gatoPos = position;
    }
}
