using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFome : QuestStep
{
    protected override void SetQuestStepState(string state)
    {
        //
    }
    void OnEnable(){
        GameEventsManager.instance.playerEvents.onPlayerComePeixe+=CompleteQuest;
    }
    void OnDisable(){
        GameEventsManager.instance.playerEvents.onPlayerComePeixe-=CompleteQuest;
    }
    void CompleteQuest(){
        FinishQuestStep();
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateState();
        GameEventsManager.instance.rewardEvents.PeixeRewardRecived(2,1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdateState()
    {
        string state = "";
        string status = "A fome é seu princnipal recurso, ela diminui constantemente e ao chegar a 0 você perde. Voce pode restaurar ela comendo peixes ou indo "+
        "trabalhar na fabrica\n" + "Aperte I para abrir seu inventario de pesca e coma o peixe que eu te dei";
        ChangeState(state,status);
    }
}