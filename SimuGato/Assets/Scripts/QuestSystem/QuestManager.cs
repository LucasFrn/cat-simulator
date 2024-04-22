using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> questMap;

    private int currentPlayerLevel;

    private void Awake(){
        questMap= CreateQuestMap();
        
    }
    void OnEnable(){
        GameEventsManager.instance.questEvents.onStartQuest+=StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest+=AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest+=FinishQuest;
        GameEventsManager.instance.playerEvents.onPlayerLevelChange += PlayerLevelChange;
    }
    void OnDisable(){
        GameEventsManager.instance.questEvents.onStartQuest-=StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest-=AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest-=FinishQuest;
        GameEventsManager.instance.playerEvents.onPlayerLevelChange -= PlayerLevelChange;
    }
    void Start(){
        //avisar o estado inicial da quest pra todo mundo ao iniciar
        foreach( Quest quest in questMap.Values){
            GameEventsManager.instance.questEvents.QuestStateChange(quest);
        }
    }

    private Dictionary<string, Quest> CreateQuestMap(){
        QuestInfoSO[] allQuests= Resources.LoadAll<QuestInfoSO>("Quests");
        Dictionary<string,Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach(QuestInfoSO questInfo in allQuests){
            if(idToQuestMap.ContainsKey(questInfo.id)){
                Debug.LogWarning("Duplicate ID found when creating quest map: "+ questInfo.id);
            }
            idToQuestMap.Add(questInfo.id,new Quest(questInfo));
        }
        return idToQuestMap;
    }
    private Quest GetQuestByID(string id){
        Quest quest = questMap[id];
        if(quest==null){
            Debug.LogError("Id not found in the Quest Map"+ id);
        }
        return quest;
    }
    void PlayerLevelChange(int level){
        currentPlayerLevel = level;
    }
    private void ChangeQuestState(string id, QuestState state){
        Quest quest = GetQuestByID(id);
        quest.state = state;
        GameEventsManager.instance.questEvents.QuestStateChange(quest);
    }
    private bool CheckRequirementsMet(Quest quest){
        bool meetsRequirements = true;
        if(currentPlayerLevel<quest.info.levelRequirement){
            meetsRequirements=false;
        }
        foreach(QuestInfoSO prerequisiteQuestInfo in quest.info.questPrerequisites){
            if((GetQuestByID(prerequisiteQuestInfo.id)).state != QuestState.FINISHED){
                meetsRequirements=false;
            }
        }
        return meetsRequirements;
    }

    void Update(){
        foreach(Quest quest in questMap.Values){
            if(quest.state==QuestState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(quest)){
                ChangeQuestState(quest.info.id,QuestState.CAN_START);
            }
        }
    }

    private void StartQuest(string id){
        Quest quest=GetQuestByID(id);
        quest.InstantiateCurrentQuestStep(this.transform);
        ChangeQuestState(quest.info.id,QuestState.IN_PROGRESS);
    }
    private void AdvanceQuest(string id){
        Quest quest = GetQuestByID(id);
        quest.MoveToNexStep();
        if(quest.CurrentStepExists()){
            quest.InstantiateCurrentQuestStep(this.transform);
        }
        else{//se não tem mais quest steps quer dizer que acabou a quest
            ChangeQuestState(quest.info.id,QuestState.CAN_FINISH);
        }
    }
    private void FinishQuest(string id){
        Quest quest = GetQuestByID(id);
        ClaimRewards(quest);
        ChangeQuestState(quest.info.id,QuestState.FINISHED);
    }
    private void ClaimRewards(Quest quest){

    }
}
