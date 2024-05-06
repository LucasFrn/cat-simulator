using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AdapterGardenData:GardenData
{
    public AdapterGardenData(TileInfo tileInfo){
        hoed = tileInfo.Hoed;
        watered = tileInfo.Watered;
        planted = tileInfo.Planted;
        if(tileInfo.Planted){
            plantID=tileInfo.plantInfo.ID;
            plantStage=tileInfo.plantInfo.FaseAtual;
        }
        else{
            plantID=-1;
            plantStage=-1;
        }
    }
}
