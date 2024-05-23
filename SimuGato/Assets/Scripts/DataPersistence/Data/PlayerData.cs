using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData
{
    public Vector3 pos;
    public Vector3 rot;

    public float fome;
    public float energia;
    public float higiene;
    public float felicidade;
    public float social;
    public float petiscos;

    public PlayerData(){
        pos = new Vector3(288.5f,16.3899994f,311.200012f);
        rot = new Vector3(0,-90,0);
        fome = 50;
        energia = 50;
        higiene = 50;
        felicidade = 50;
        social = 50;
        petiscos = 100;
    }
}
