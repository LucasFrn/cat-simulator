using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : MonoBehaviour,IDataPersistance
{
    [Header("Config")]
    [SerializeField]private bool loadQuestState = true;
    private Dictionary<string, Quest> questMap;
    private Dictionary<string,QuestData> questsLoadadas;

    private int currentPlayerLevel;

    /* private void Awake(){
        questMap= CreateQuestMap();
        
    } */
    void OnEnable(){
        GameEventsManager.instance.questEvents.onStartQuest+=StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest+=AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest+=FinishQuest;
        GameEventsManager.instance.playerEvents.onPlayerLevelChange += PlayerLevelChange;
        GameEventsManager.instance.questEvents.onQuestStepStateChange += QuestStepStateChange;
    }
    void OnDisable(){
        GameEventsManager.instance.questEvents.onStartQuest-=StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest-=AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest-=FinishQuest;
        GameEventsManager.instance.playerEvents.onPlayerLevelChange -= PlayerLevelChange;
        GameEventsManager.instance.questEvents.onQuestStepStateChange -= QuestStepStateChange;
    }
    void Start(){
        questMap= CreateQuestMap();
        //avisar o estado inicial da quest pra todo mundo ao iniciar
        foreach( Quest quest in questMap.Values){
            if(quest.state==QuestState.IN_PROGRESS){
                quest.InstantiateCurrentQuestStep(this.transform);
            }
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
            idToQuestMap.Add(questInfo.id,LoadQuest(questInfo));
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
                break;
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
        //Eviar eventos de recompensa, ou literalmente qualquer outra coisa kkkkk
    }
    private void QuestStepStateChange(string id,int stepIndex, QuestStepState questStepState){
        Quest quest = GetQuestByID(id);
        quest.StoreQuestStepState(questStepState, stepIndex);
        ChangeQuestState(id,quest.state);
    }
    /* private void OnApplicationQuit(){
        foreach(Quest quest in questMap.Values){
            SaveQuest(quest);
        }
    } */
    /* private void SaveQuest(Quest quest, GameData data){
        try{
            QuestData questData = quest.GetQuestData();
            //Serialize using JsonUtility, but use whatever you like, o mano recomendou JSON.NET
            string serializedData = JsonUtility.ToJson(questData);
            //saving to playerPrefs is just a quick exemple
            //PlayerPrefs.SetString(quest.info.id,serializedData);
            Debug.Log(serializedData);
        }
        catch(System.Exception e){
            Debug.Log("Failed to save quest with id "+ quest.info.id+ ": "+ e);
        }
    } */
    /* private Quest LoadQuest(QuestInfoSO questInfo){
        Quest quest = null;
        try{
            //Load quest from saved data
            if(PlayerPrefs.HasKey(questInfo.id)&&loadQuestState)//NÃO FOI IMPLEMENTADO ASSIM
            {
                string serializedData = PlayerPrefs.GetString(questInfo.id);
                QuestData questData = JsonUtility.FromJson<QuestData>(serializedData);
                quest= new Quest(questInfo,questData.state,questData.questStepIndex,questData.questStepStates);
            }
            else{
                quest = new Quest(questInfo);
            }
        }
        catch (System.Exception e){
            Debug.LogError("Failed to load quest with id: "+quest.info.id +": "+ e);
        }
        return quest;
    } */
    private Quest LoadQuest(QuestInfoSO questInfo){
        Quest quest=null;
        if(questsLoadadas.ContainsKey(questInfo.id)&&loadQuestState){
            QuestData questData = questsLoadadas[questInfo.id];
            quest = new Quest(questInfo,questData.state,questData.questStepIndex,questData.questStepStates);
        }
        else{
            quest = new Quest(questInfo);
        }
        return quest;
    }

    public void LoadData(GameData data)
    {
        Dictionary<string,QuestData> questsLoadadas = new Dictionary<string, QuestData>();
        foreach(QuestData questData in data.questsData){
            questsLoadadas.Add(questData.id,questData);
        }
        this.questsLoadadas=questsLoadadas;
    }

    public void SaveData(GameData data)
    {
        QuestData myQuest;
        QuestData[] newQuestDatas = new QuestData[questMap.Count];
        int i=0;
        foreach(Quest quest in questMap.Values){
            myQuest = quest.GetQuestData();
            newQuestDatas[i]=myQuest;
            i++;
        }
        data.questsData=newQuestDatas;
    }
}
