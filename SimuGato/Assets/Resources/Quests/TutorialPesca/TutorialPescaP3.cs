using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPescaP3 : QuestStep
{
    protected override void SetQuestStepState(string state)
    {
        //
    }
    void Start(){
        UpdateState();
    }
    void OnEnable(){
        GameEventsManager.instance.playerEvents.onPlayerVendePeixe+=WaitForComplete;
    }
    void OnDisable(){
        GameEventsManager.instance.playerEvents.onPlayerVendePeixe-=WaitForComplete;
    }
    void WaitForComplete(){
        FinishQuestStep();
    }
    
    public void UpdateState(){
        string state = "";
        string status = "Quando pegar um peixe, Aperte I para abrir seu inventario!"+
                        " Use enter para vender 1 deles";
        ChangeState(state,status);
    }
}
