using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestFazenda1 : QuestStep
{
    protected override void SetQuestStepState(string state)
    {
        //
    }
    void OnEnable(){
        GameEventsManager.instance.gardenEvents.onEnterGarden+=CompletarQuest;
    }
    void OnDisable(){
        GameEventsManager.instance.gardenEvents.onEnterGarden-=CompletarQuest;
    }
    void Start(){
        UpdateState();
    }
    void CompletarQuest(){
        FinishQuestStep();
    }
    
    public void UpdateState(){
        string state = "";
        string status = "Ir até a Fazenda (do lado da casa no minimapa)";
        ChangeState(state,status);
    }
    
}
