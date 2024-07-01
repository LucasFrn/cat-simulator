using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestFazenda5 : QuestStep
{
    int plantasColhidas = 0;
    void OnEnable(){
        GameEventsManager.instance.gardenEvents.onPlantaColhida+=PlantaColhida;
    }
    void OnDisable(){
        GameEventsManager.instance.gardenEvents.onPlantaColhida-=PlantaColhida;
    }
    protected override void SetQuestStepState(string state)
    {
        plantasColhidas = Int32.Parse(state);
    }
    void Start(){
        UpdateState();
    }
    void PlantaColhida(int planta){
        plantasColhidas++;
        UpdateState();
        if(plantasColhidas>=5){
            FinishQuestStep();
        }

    }
    public void UpdateState(){
        string state = plantasColhidas.ToString();
        string status = "Cada planta tem uma barra de progresso em cima dela, depois de alguns dias a planta "+
        "vai estar pronta com a barrinha toda verde. Colha elas com tecla 4 \nPlantas colhidas"+plantasColhidas.ToString() + " /5";
        ChangeState(state,status);
    }

    
}
