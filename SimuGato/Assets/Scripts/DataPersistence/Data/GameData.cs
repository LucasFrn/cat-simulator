using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData {
    public long lastUpdated;
    public int [] quantidadeSementes;
    public float horaDoDia;
    public QuestData[] questsData;
    public GardenData[] gardenDatas;
    public InventarioPeixesData inventarioPeixesData;
    public SkillTreeData skillTreeData;
    public StatusData statusData;
    public PosData posData;
    public CameraData cameraData;

    //esses valores são os valores iniciais pra quando a gente começar o jogo.
    public GameData(){
        gardenDatas=new GardenData[0];
        questsData = new QuestData[0];
        horaDoDia=6;
        statusData = new StatusData();
        inventarioPeixesData = null;
        skillTreeData = null;
        posData = new PosData();
        cameraData = null;
        quantidadeSementes=null;
    }
}
