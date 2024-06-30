using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class QuestPoint : MonoBehaviour
{
    [Header("Quest")]
    [SerializeField]QuestInfoSO questInfoForPoint;
    [SerializeField]List<QuestInfoSO> listaQuests;
    [Header("Config")]
    [SerializeField]private bool startPoint = true;
    [SerializeField]private bool finishPoint = true;
    private bool playerIsNear = false;
    private string questId;
    private QuestState currentQuestState;
    private QuestIcon questIcon;
    [SerializeField]bool hasMultipleQuests;
    [SerializeField]QuestManager questManager;
    void Awake(){
        questIcon=GetComponentInChildren<QuestIcon>();
        if(!hasMultipleQuests){
            questId=questInfoForPoint.id;
        }
        else{
            questId=listaQuests[0].id;
        }
    }
    void OnEnable(){
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
        GameEventsManager.instance.inputEvents.onSubmitPressed += SubmitPressed;
    }
        void OnDisable(){
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
        GameEventsManager.instance.inputEvents.onSubmitPressed -= SubmitPressed;
    }
    void QuestStateChange(Quest quest){
        if(quest.info.id.Equals(questId)){
            currentQuestState=quest.state;
            questIcon.SetState(currentQuestState,startPoint,finishPoint);
            if(currentQuestState==QuestState.FINISHED)
                if(hasMultipleQuests)
                    FindUnfinishedQuest();
        }
    }
    void SubmitPressed(){
        if(!playerIsNear){
            return;
        }
        if(currentQuestState.Equals(QuestState.CAN_START)&&startPoint){
            GameEventsManager.instance.questEvents.StartQuest(questId);
        }
        else if(currentQuestState.Equals(QuestState.CAN_FINISH)&&finishPoint){
            GameEventsManager.instance.questEvents.FinishQuest(questId);
        }
    }

    private void OnTriggerEnter(Collider collider){
        if(collider.CompareTag("Player")){
            playerIsNear=true;
            GameEventsManager.instance.uiEvents.PainelInteracaoQuestChange(true,currentQuestState);
        }
    }
    private void OnTriggerExit(Collider collider){
        if(collider.CompareTag("Player")){
            playerIsNear=false;
            GameEventsManager.instance.uiEvents.PainelInteracaoQuestChange(false,currentQuestState);
        }
    }
    private void FindUnfinishedQuest(){
        Quest quest;
        for(int i = 0;i<listaQuests.Count;i++){
            quest = questManager.GetQuestByID(listaQuests[i].id);
            if(quest.state!=QuestState.FINISHED){
                questId=listaQuests[i].id;
                QuestStateChange(quest);
                return;
            }
        }
        //Se sairmos do loop é pq todas as quests da lista já acabaram, então n precisa fazer nada kk
    }
}
