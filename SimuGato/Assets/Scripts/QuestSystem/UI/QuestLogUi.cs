using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuestLogUi : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]GameObject contentParent;
    [SerializeField]QuestLogScrollingList scrollingList;
    [SerializeField]TextMeshProUGUI questDisplayNameText;
    [SerializeField]TextMeshProUGUI questStatusText;
    [SerializeField]TextMeshProUGUI goldRewardsText;
    [SerializeField]TextMeshProUGUI outrasRewardsText;
    [SerializeField]TextMeshProUGUI questRequerimentsText;
    
    private Button firstSelectedButton;
    void OnEnable(){
        GameEventsManager.instance.questEvents.onQuestStateChange+=QuestStateChange;
    }
    void OnDisable(){
        GameEventsManager.instance.questEvents.onQuestStateChange-=QuestStateChange;
    }
    public void ShowUI(){
        contentParent.SetActive(true);
        if(firstSelectedButton != null){
            firstSelectedButton.Select();
        }
    }
    public void HideUI(){
        contentParent.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
    private void QuestStateChange(Quest quest){
        //adicionar botão se não tiver ainda
        QuestLogButtonUI questLogButton= scrollingList.CreateButtonIfNotExist(quest,()=>{
            SetQuestLogInfo(quest);
        });
        if(firstSelectedButton == null){
            firstSelectedButton = questLogButton.button;
        }
        questLogButton.SetState(quest.state);
    }
    void SetQuestLogInfo(Quest quest){
        questDisplayNameText.text = quest.info.displayName;

        questStatusText.text = quest.GetFullStatusText();
        //requisitos
        questRequerimentsText.text = "";
        foreach(QuestInfoSO prerequisiteQuestInfo in quest.info.questPrerequisites){
            questRequerimentsText.text += prerequisiteQuestInfo.displayName +"\n";
        }
        //recompensas
        goldRewardsText.text=quest.info.goldReward+ " Petiscos";
        outrasRewardsText.text=quest.info.otherRewards;
    }
}
