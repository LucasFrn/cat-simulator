using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPointSelfDes : MonoBehaviour
{
    [SerializeField]QuestInfoSO questInfo;
    // Start is called before the first frame update
    void OnEnable(){
        GameEventsManager.instance.questEvents.onFinishQuest+=DestroyQuestPoint;
        GameEventsManager.instance.questEvents.onQuestStateChange+=DestroyQuestPointState;
    }
    void OnDisable(){
        GameEventsManager.instance.questEvents.onFinishQuest-=DestroyQuestPoint;
        GameEventsManager.instance.questEvents.onQuestStateChange-=DestroyQuestPointState;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void DestroyQuestPoint(string id){
        if(id==questInfo.id){
            Destroy(gameObject);
            GameEventsManager.instance.uiEvents.PainelInteracaoQuestChange(false,QuestState.FINISHED);
        }
    }
    void DestroyQuestPointState(Quest quest){
        if(quest.info.id==questInfo.id){
            if(quest.state==QuestState.FINISHED){
                Destroy(gameObject);
                GameEventsManager.instance.uiEvents.PainelInteracaoQuestChange(false,QuestState.FINISHED);
            }
        }
    }

}
