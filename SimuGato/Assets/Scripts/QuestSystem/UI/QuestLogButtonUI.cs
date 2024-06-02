using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestLogButtonUI : MonoBehaviour, ISelectHandler
{
    public Button button {get;private set;}
    private TextMeshProUGUI buttonText;
    private UnityAction onSelectAction;
    //because we'll be instantiating the button and it may be disabled when we
    //instantiate it, we need to manually initialize anything here
    public void Initialize(string displayName,UnityAction selectAction){
        this.button = this.GetComponent<Button>();
        this.buttonText = this.GetComponentInChildren<TextMeshProUGUI>();
        this.buttonText.text = displayName;
        this.onSelectAction = selectAction;
    }
    public void OnSelect(BaseEventData eventData)
    {
        onSelectAction();
    }
    public void SetState(QuestState state){
        switch(state){
            case QuestState.REQUIREMENTS_NOT_MET:
            case QuestState.CAN_START:
                buttonText.color = Color.red;
            break;
            case QuestState.IN_PROGRESS:
            case QuestState.CAN_FINISH:
                buttonText.color = Color.yellow;
            break;
            case QuestState.FINISHED:
                buttonText.color = Color.green;
            break;
            default: Debug.LogWarning("Quest state not recognized by swtich state "+ state);
            break;

        }
    }
}
