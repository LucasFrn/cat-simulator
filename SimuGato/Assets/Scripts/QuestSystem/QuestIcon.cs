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
    [Header("Sprites")]
    [SerializeField]SpriteRenderer spriteRenderer;
    [SerializeField]Sprite requirementsNotMetToStartSprite;
    [SerializeField]Sprite canStartSprite;
    [SerializeField]Sprite requirementsNotMetToFinishSprite;
    [SerializeField]Sprite canFinishSprite;

    public void SetState(QuestState newState,bool startPoint,bool finishPoint){
        requirementsNotMetToStartIcon.SetActive(false);
        canStartIcon.SetActive(false);
        requirementsNotMetToFinishIcon.SetActive(false);
        canFinishIcon.SetActive(false);

        switch(newState){
            case QuestState.REQUIREMENTS_NOT_MET:
                if(startPoint){
                    requirementsNotMetToStartIcon.SetActive(true);
                    spriteRenderer.sprite=requirementsNotMetToStartSprite;
                }
                break;
            case QuestState.CAN_START:
                if(startPoint){
                    canStartIcon.SetActive(true); 
                    spriteRenderer.sprite=canStartSprite;
                }
            break;
            case QuestState.IN_PROGRESS:
                if(finishPoint){
                    requirementsNotMetToFinishIcon.SetActive(true);
                    spriteRenderer.sprite=requirementsNotMetToFinishSprite;
                }
            break;
            case QuestState.CAN_FINISH:
                if(finishPoint){
                    canFinishIcon.SetActive(true);
                    spriteRenderer.sprite=canFinishSprite;
                }
            break;
            case QuestState.FINISHED:spriteRenderer.gameObject.SetActive(false); break;
            default: 
                Debug.LogWarning("Quest state not recognized by switch statement for quest icon "+ newState);
            break;
        }
    }
}
