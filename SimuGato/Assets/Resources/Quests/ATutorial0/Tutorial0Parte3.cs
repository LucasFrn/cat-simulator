using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial0Parte3 : QuestStep
{
    protected override void SetQuestStepState(string state)
    {
       //
    }
    void OnEnable(){
        GameEventsManager.instance.uiEvents.onPainelAberto+=CompleteQuest;
    }
    void OnDisable(){
        GameEventsManager.instance.uiEvents.onPainelAberto-=CompleteQuest;
    }
    void CompleteQuest(int painel){
        if(painel==(int)GameManager.JanelaEmFoco.Mapa){
            FinishQuestStep();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdateState()
    {
        string state = "";
        string status = "Muito bom! Depois de terminar essa quest abra o seu painel de quests e inicie o tutorial barrrinhas," +
                        " os outros 2 voce precisa encontrar o ponto de inicio delas (abra o mapa e procure as !)";
        ChangeState(state,status);
    }
}
