using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnergia : QuestStep
{
    protected override void SetQuestStepState(string state)
    {
        //
    }
    void OnEnable(){
        GameEventsManager.instance.playerEvents.onPlayerDorme+=CompleteQuest;
    }
    void OnDisable(){
        GameEventsManager.instance.playerEvents.onPlayerDorme-=CompleteQuest;
    }
    void CompleteQuest(){
        FinishQuestStep();
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
        string status = "A energia é gasta ao interagir com as coisas, como pescar e brincar, e principalmente ela te permite escolher "+
        "a dificuldade do minigame do trabalho. Se acabar sua energia você desmaia e perde 100 petiscos.\nDurma na sua cama"+
        "para restaurar a energia, e passar o tempo mais rapido";
        ChangeState(state,status);
    }
}
