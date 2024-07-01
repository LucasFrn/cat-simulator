using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PosData
{
    public Vector3 gatoPos;
    public PosData(){
        gatoPos = new Vector3(119.599998f,16.6000004f,437.299988f);
    }
    public PosData(Vector3 position){
        gatoPos = position;
    }
}
