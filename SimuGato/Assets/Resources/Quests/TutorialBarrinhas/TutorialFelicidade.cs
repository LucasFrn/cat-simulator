using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFelicidade : QuestStep
{
    protected override void SetQuestStepState(string state)
    {
        //
    }
    void OnEnable(){
        GameEventsManager.instance.playerEvents.onPlayerBrinca+=CompleteQuest;
    }
    void OnDisable(){
        GameEventsManager.instance.playerEvents.onPlayerBrinca-=CompleteQuest;
    }
    void CompleteQuest(bool isLixo){
        if(!isLixo)
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
        string status = "A felicidade é um recurso importante, pois ao chegar a 0 você perde. Voce pode restaurar ela brincando com o "+
        "arranhador ou com o lixo. Você perde bastante felicidade ao ir trabalhar e pescar da ou tira felicidade dependendo do peixe\n" + "Brinque com o arranhador do lado da sua casa, note que brincar custa energia";
        ChangeState(state,status);
    }
}