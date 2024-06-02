using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class QuestLogScrollingList : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]GameObject contentParent;
    [Header("Quest log button")]
    [SerializeField]GameObject questLogButtonPrefab;

    Dictionary<string,QuestLogButtonUI> idToButtonMap = new Dictionary<string, QuestLogButtonUI>();
    public QuestLogButtonUI CreateButtonIfNotExist(Quest quest,UnityAction selectAction){
        QuestLogButtonUI questLogButton = null;
        if(!idToButtonMap.ContainsKey(quest.info.id)){
            questLogButton = InstantiateQuestLogButton(quest,selectAction);
        }
        else{
            questLogButton = idToButtonMap[quest.info.id];
        }
        return questLogButton;
    }
    QuestLogButtonUI InstantiateQuestLogButton(Quest quest, UnityAction selectAction){
        QuestLogButtonUI questLogButton = Instantiate(questLogButtonPrefab,
            contentParent.transform).GetComponent<QuestLogButtonUI>();
        questLogButton.gameObject.name = quest.info.id + "_button";
        questLogButton.Initialize(quest.info.displayName,selectAction);
        idToButtonMap[quest.info.id]=questLogButton;
        return questLogButton;
    }
}
