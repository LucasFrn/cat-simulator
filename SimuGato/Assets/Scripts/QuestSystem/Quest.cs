using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public QuestInfoSO info;
    public QuestState state;
    private int currentQuestStepIndex;
    private QuestStepState[] questStepStates;
    public Quest(QuestInfoSO questInfo){
        this.info = questInfo;
        this.state = QuestState.REQUIREMENTS_NOT_MET;
        this.currentQuestStepIndex = 0;
        this.questStepStates = new QuestStepState[info.questStepPrefabs.Length];
        for(int i=0;i<info.questStepPrefabs.Length;i++){
            questStepStates[i]=new QuestStepState();
        }
    }
    public Quest(QuestInfoSO questInfo, QuestState questState, int currentQuestStepIndex, QuestStepState[] questStepStates){
        this.info = questInfo;
        this.state = questState;
        this.currentQuestStepIndex=currentQuestStepIndex;
        this.questStepStates = questStepStates;

        //if the quest step states and prefabs are diffent lenght
        //something has changed during development and the saved prefab data is out of sync.
        if(this.questStepStates.Length != this.info.questStepPrefabs.Length){
            Debug.LogWarning("Quest  Step Prefabs and Quest Step States are of different lengths. This indicates something changed"+
            "with the questInfo and the saved data is out of sync. Reset you data - as this might cause issues. QuestId: "+ this.info.id);
        }

    }
    public void MoveToNexStep(){
        currentQuestStepIndex++;
    }
    public bool CurrentStepExists(){
        return ( currentQuestStepIndex<info.questStepPrefabs.Length);
    }
    public void InstantiateCurrentQuestStep(Transform parentTransform){
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();
        if(questStepPrefab!=null){
            QuestStep questStep=Object.Instantiate<GameObject>(questStepPrefab,parentTransform).GetComponent<QuestStep>();
            questStep.InitializeQuestStep(info.id,currentQuestStepIndex,questStepStates[currentQuestStepIndex].state);
        }
    }
    private GameObject GetCurrentQuestStepPrefab(){
        GameObject questStepPrefab = null;
        if(CurrentStepExists()){
            questStepPrefab = info.questStepPrefabs[currentQuestStepIndex];
        }
        else{
            Debug.LogWarning("Tried to get quest stepp prefab, but stepIndex was out of range indicating that "+
            "there's no current step: QuestId = " + info.id + ", stepIndex=" + currentQuestStepIndex);
        }
        return questStepPrefab;
    }

    public void StoreQuestStepState(QuestStepState questStepState, int stepIndex){
        if(stepIndex<questStepStates.Length){
            questStepStates[stepIndex].state=questStepState.state;
            questStepStates[stepIndex].status= questStepState.status;
        }
        else{
            Debug.LogWarning("Tried to acess quest step data, but step index was out of range: "+
            "Quest Id= " + info.id + ", StepIndex = "+ stepIndex);
        }
    }
    public QuestData GetQuestData(){
        return new QuestData(info.id,state,currentQuestStepIndex,questStepStates);
    }
    public string GetFullStatusText(){
        string fullStatus = "";
        if(state == QuestState.REQUIREMENTS_NOT_MET){
            fullStatus = "Requisitos ainda não foram atendidos para iniciar essa quest";
        }
        else if(state == QuestState.CAN_START){
            if(info.requiresPoint){
                fullStatus = "Você pode iniciar essa quest! Ache o ponto de inicio dela (Aperte Enter do lado dele)";
            }else{
                fullStatus = "Você pode iniciar essa quest!";
            }
        }
        else{
            for(int i=0;i<currentQuestStepIndex; i++){
                fullStatus += "<s>" + questStepStates[i].status + "</s>\n";
            }
            if(CurrentStepExists()){
                fullStatus += questStepStates[currentQuestStepIndex].status;
            }
            if(state== QuestState.CAN_FINISH){
                fullStatus += "Você pode entregar essa quest!";
            }
            if(state == QuestState.FINISHED){
                fullStatus += "Você já completou essa quest.";
            }
        }

        return fullStatus;
    }
    public string GetCurrentStatusText(){
        string status = "";
        if(state == QuestState.REQUIREMENTS_NOT_MET){
            status = "Requisitos ainda não foram atendidos para começar essa quest";
        }
        else if(state == QuestState.CAN_START){
            if(info.requiresPoint){
                status = "Você pode iniciar essa quest! Ache o ponto de inicio dela (Aperte Enter do lado dele)";
            }else{
                status = "Você pode iniciar essa quest!";
            }
        }
        else{
            if(CurrentStepExists()){
                status = questStepStates[currentQuestStepIndex].status;
            }
            if(state== QuestState.CAN_FINISH){
                status = "Você pode entregar essa quest!";
            }
            if(state == QuestState.FINISHED){
                status = "Você já completou essa quest.";
            }
        }
        return status;
    }
}
