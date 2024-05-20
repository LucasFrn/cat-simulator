using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData {
    public long lastUpdated;
    public QuestData[] questsData;
    public GardenData[] gardenDatas;
    public InventarioPeixesData inventarioPeixesData;
    public SkillTreeData skillTreeData;

    //esses valores são os valores iniciais pra quando a gente começar o jogo.
    public GameData(){
        gardenDatas=new GardenData[0];
        questsData = new QuestData[0];
        inventarioPeixesData = null;
        skillTreeData = null;
    }
}
