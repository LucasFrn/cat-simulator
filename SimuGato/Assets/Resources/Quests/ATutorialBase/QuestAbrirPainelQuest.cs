using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestAbrirPainelQuest : QuestStep
{
    protected override void SetQuestStepState(string state)
    {
        //
    }
    void OnEnable(){
        GameEventsManager.instance.uiEvents.onPainelAberto+=CompletaQuest;
    }
    void OnDisable(){
        GameEventsManager.instance.uiEvents.onPainelAberto-=CompletaQuest;
    }
    void CompletaQuest(int painel){
        if(painel == (int)GameManager.JanelaEmFoco.Quests){
            FinishQuestStep();
        }
    }
    void Start()
    {
        UpdateState();
    }

    // Update is called once per frame
    void UpdateState()
    {
        string state = "";
        string status = "Abra o painel com J, para iniciar a quest ou ache a exclamação no mapa e aperte enter, ou inicie por aqui";
        ChangeState(state,status);
    }
}
