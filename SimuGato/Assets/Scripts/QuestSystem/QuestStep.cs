using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;
    private string questId;
    private int stepIndex;

    public void InitializeQuestStep(string questId, int stepIndex, string questStepState){
        this.questId = questId;
        this.stepIndex = stepIndex;
        if(questStepState!=null && questStepState != ""){
            SetQuestStepState(questStepState);
        }
    }
    protected void FinishQuestStep(){
        if(!isFinished){
            isFinished = true;
            GameEventsManager.instance.questEvents.AdvanceQuest(questId);
            Destroy(this.gameObject);
        }
    }
    protected void ChangeState(string newState,string status){
        GameEventsManager.instance.questEvents.QuestStepStateChange(questId,stepIndex,
        new QuestStepState(newState,status));
    }
    //Esse status é como vamos escrever no log
    //O state é como guardamos o progresso numa string

    protected abstract void SetQuestStepState(string state);
    //usado para voltar ao ponto que paramos
    //da seus corre pra receber uma string e fazer isso virar progresso
}
