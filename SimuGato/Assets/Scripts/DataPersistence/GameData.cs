using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData {
    public long lastUpdated;
    public int deathCount;
    public GardenData[] gardenDatas;

    //esses valores são os valores iniciais pra quando a gente começar o jogo.
    public GameData(){
        this.deathCount=0;
        gardenDatas=new GardenData[0];
    }
}
