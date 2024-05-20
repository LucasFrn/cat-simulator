using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class FishMinigameData {
    public InventarioPeixesData inventarioPeixesData;
    public SkillTreeData skillTreeData;
    public FishMinigameData(){
        inventarioPeixesData = new InventarioPeixesData();
        skillTreeData = new SkillTreeData();
    }
}