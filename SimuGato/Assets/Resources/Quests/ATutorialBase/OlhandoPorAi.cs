using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OlhandoPorAi : QuestStep
{
    protected override void SetQuestStepState(string state)
    {
        //
    }
    void OnEnable(){
        GameEventsManager.instance.playerEvents.onPlayerUsesEnergyDrink+=CompletaQuest;
    }
    void OnDisable(){
        GameEventsManager.instance.playerEvents.onPlayerUsesEnergyDrink-=CompletaQuest;
    }
    void CompletaQuest(){
        FinishQuestStep();
    }
    void Start()
    {
        UpdateState();
    }

    // Update is called once per frame
    void UpdateState()
    {
        string state = "";
        string status = "Mova por ai com W,A,S,D, olhe com o mouse, pule com espaço\n"
            + "Para andar mais rápido use o energético (tecla k), você pode comprar mais deles. Use um energético!";
        ChangeState(state,status);
    }
}
