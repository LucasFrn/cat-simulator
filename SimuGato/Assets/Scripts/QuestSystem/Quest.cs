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
        }
        else{
            Debug.LogWarning("Tried to acess quest step data, but step index was out of range: "+
            "Quest Id= " + info.id + ", StepIndex = "+ stepIndex);
        }
    }
    public QuestData GetQuestData(){
        return new QuestData(state,currentQuestStepIndex,questStepStates);
    }
}
