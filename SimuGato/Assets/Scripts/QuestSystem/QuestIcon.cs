using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestIcon : MonoBehaviour
{
    [Header("Icons")]
    [SerializeField] GameObject requirementsNotMetToStartIcon;
    [SerializeField] GameObject canStartIcon;
    [SerializeField] GameObject requirementsNotMetToFinishIcon;
    [SerializeField] GameObject canFinishIcon;

    public void SetState(QuestState newState,bool startPoint,bool finishPoint){
        requirementsNotMetToStartIcon.SetActive(false);
        canStartIcon.SetActive(false);
        requirementsNotMetToFinishIcon.SetActive(false);
        canFinishIcon.SetActive(false);

        switch(newState){
            case QuestState.REQUIREMENTS_NOT_MET:
                if(startPoint) requirementsNotMetToStartIcon.SetActive(true);
                break;
            case QuestState.CAN_START:
                if(startPoint) canStartIcon.SetActive(true); 
            break;
            case QuestState.IN_PROGRESS:
                if(finishPoint) requirementsNotMetToFinishIcon.SetActive(true); 
            break;
            case QuestState.CAN_FINISH:
                if(finishPoint) canFinishIcon.SetActive(true); 
            break;
            case QuestState.FINISHED: break;
            default: 
                Debug.LogWarning("Quest state not recognized by switch statement for quest icon "+ newState);
            break;
        }
    }
}
