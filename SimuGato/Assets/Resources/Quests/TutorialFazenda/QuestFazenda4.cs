using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestFazenda4 : QuestStep
{
    int plantasRegadas = 0;
    void OnEnable(){
        GameEventsManager.instance.gardenEvents.onPlantaRegada+=PlantaRegada;
    }
    void OnDisable(){
        GameEventsManager.instance.gardenEvents.onPlantaRegada-=PlantaRegada;
    }
    protected override void SetQuestStepState(string state)
    {
        plantasRegadas = Int32.Parse(state);
    }
    void Start(){
        UpdateState();
    }
    void PlantaRegada(){
        plantasRegadas++;
        UpdateState();
        if(plantasRegadas>=5){
            FinishQuestStep();
        }
    }
    public void UpdateState(){
        string state = plantasRegadas.ToString();
        string status = "Regue as plantas para elas crescerem quando começar um novo dia! (tecla 3)\nPlantas regadas "+plantasRegadas.ToString() + " /5";
        ChangeState(state,status);
    }
}
