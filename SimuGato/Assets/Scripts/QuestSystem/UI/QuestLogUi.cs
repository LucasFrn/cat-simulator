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
    [SerializeField]Button interactionButton;
    [SerializeField]TextMeshProUGUI interactionButtonText;
    
    private Button firstSelectedButton;
    private GameObject lastSelectedButton;
    private Quest questSelecionada;
    void OnEnable(){
        GameEventsManager.instance.questEvents.onQuestStateChange+=QuestStateChange;
    }
    void OnDisable(){
        GameEventsManager.instance.questEvents.onQuestStateChange-=QuestStateChange;
    }
    public void ShowUI(){
        contentParent.SetActive(true);
        if(lastSelectedButton != null){
            EventSystem.current.SetSelectedGameObject(lastSelectedButton);
        }
        else{
            if(firstSelectedButton != null){
                firstSelectedButton.Select();
            }
        }
    }
    public void HideUI(){
        contentParent.SetActive(false);
        lastSelectedButton = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.SetSelectedGameObject(null);
    }
    private void QuestStateChange(Quest quest){
        //adicionar botão se não tiver ainda
        QuestLogButtonUI questLogButton= scrollingList.CreateButtonIfNotExist(quest,()=>{
            SetQuestLogInfo(quest);
            SelectQuest(quest);
        });
        if(firstSelectedButton == null){
            firstSelectedButton = questLogButton.button;
        }
        questLogButton.SetState(quest.state);
        if(quest==questSelecionada){
            if(contentParent.activeInHierarchy){
                SetQuestLogInfo(quest);
            }
        }
    }
    void SetQuestLogInfo(Quest quest){
        GameEventsManager.instance.uiEvents.QuestAtualizada(quest.info.displayName,quest.GetCurrentStatusText());
        questDisplayNameText.text = quest.info.displayName;

        questStatusText.text = quest.GetFullStatusText();
        //requisitos
        questRequerimentsText.text = "";
        foreach(QuestInfoSO prerequisiteQuestInfo in quest.info.questPrerequisites){
            questRequerimentsText.text += prerequisiteQuestInfo.displayName +"\n";
        }
        //recompensas
        goldRewardsText.text=quest.info.goldReward+ " Petiscos";
        outrasRewardsText.text=quest.info.otherRewardsText;
        if(quest.info.requiresPoint){
            interactionButton.gameObject.SetActive(false);
        }
        else{
            interactionButton.gameObject.SetActive(true);
            if(quest.state.Equals(QuestState.CAN_START)){
                interactionButtonText.text = "Iniciar quest";
            }else
            if(quest.state.Equals(QuestState.CAN_FINISH)){
                interactionButtonText.text = "Terminar Quest";
            }else{
                interactionButton.gameObject.SetActive(false);
            }
        }
        
    }
    void SelectQuest(Quest quest){
        questSelecionada=quest;
        //Debug.Log($"Quest selecionada: {questSelecionada.info.displayName}");
    }
    public void OnInteractionButtonClick(){
        if(questSelecionada.state.Equals(QuestState.CAN_START)){
            GameEventsManager.instance.questEvents.StartQuest(questSelecionada.info.id);
        }
        else if(questSelecionada.state.Equals(QuestState.CAN_FINISH)){
            GameEventsManager.instance.questEvents.FinishQuest(questSelecionada.info.id);
        }
    }
    
}
