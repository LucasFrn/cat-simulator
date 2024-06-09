using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestFazenda3 : QuestStep
{
    int plantasPlantadas = 0;
    void OnEnable(){
        GameEventsManager.instance.gardenEvents.onPlantaPlantada+=PlantaPlantada;
    }
    void OnDisable(){
        GameEventsManager.instance.gardenEvents.onPlantaPlantada-=PlantaPlantada;
    }
    protected override void SetQuestStepState(string state)
    {
        plantasPlantadas = Int32.Parse(state);
    }
    void Start(){
        UpdateState();
        GameEventsManager.instance.rewardEvents.SementeRewardRecived(0,5);
    }
    void PlantaPlantada(){
        plantasPlantadas++;
        UpdateState();
        if(plantasPlantadas>=5){
            FinishQuestStep();
        }
    }
    public void UpdateState(){
        string state = plantasPlantadas.ToString();
        string status = "Te dei 5 sementes, plante elas! (tecla 2)\nPlantasPlantadas "+plantasPlantadas.ToString() + " /5" ;
        ChangeState(state,status);
    }
    //Terras aradas "+plantasPlantadas.ToString() + " /5"
}
