using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrabalho : QuestStep
{
    protected override void SetQuestStepState(string state)
    {
        //
    }
    void OnEnable(){
        GameEventsManager.instance.playerEvents.onPlayerTrabalha+=CompleteQuest;
    }
    void OnDisable(){
        GameEventsManager.instance.playerEvents.onPlayerTrabalha-=CompleteQuest;
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
        string status = "Foi muita coise né, mas não se preocupe, se voce não quiser ter que lidar com tudo isso basta"+
        "ligar o modo casual no menu (Esc). Para terminar, vá até o ponto de onibus e trabalhe para ganhar dinheiro, lembre que"+
        " isso afeta todas as suas barrinhas, e passa 4 horas de tempo";
        ChangeState(state,status);
    }
}
