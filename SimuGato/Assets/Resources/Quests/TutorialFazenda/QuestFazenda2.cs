using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestFazenda2 : QuestStep
{
    int terraArada = 0;
    void OnEnable(){
        GameEventsManager.instance.gardenEvents.onTerraArada+=TerraArada;
    }
    void OnDisable(){
        GameEventsManager.instance.gardenEvents.onTerraArada-=TerraArada;
    }
    protected override void SetQuestStepState(string state)
    {
        terraArada = Int32.Parse(state);
    }
    void Start(){
        UpdateState();
    }
    void TerraArada(){
        terraArada++;
        UpdateState();
        if(terraArada>=5){
            FinishQuestStep();
        }
    }
    public void UpdateState(){
        string state = terraArada.ToString();
        string status = "Are a terra com a enxada (tecla 1)\nTerras aradas "+terraArada.ToString() + " /5";
        ChangeState(state,status);
    }
    //Terras aradas "+plantasPlantadas.ToString() + " /5"
}

