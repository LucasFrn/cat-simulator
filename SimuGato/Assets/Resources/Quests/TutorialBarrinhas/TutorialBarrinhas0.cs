using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBarrinhas0 : QuestStep
{
    protected override void SetQuestStepState(string state)
    {
        //
    }
    void OnEnable(){
        GameEventsManager.instance.inputEvents.onSubmitPressed+=CompleteQuest;
    }
    void OnDisable(){
        GameEventsManager.instance.inputEvents.onSubmitPressed-=CompleteQuest;
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
        string status = "Como um jogo de sobrevivencia você tem 5 barras de vida, e todas elas afetam o jogo de alguma forma. "+
        "Se Fome ou Felicidade cairem para 0 é game over, o resto tem outros efeitos. Aperte enter para ir para a explicação de cada barrinha.";
        ChangeState(state,status);
    }
}
