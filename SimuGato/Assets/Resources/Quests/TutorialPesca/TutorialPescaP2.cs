using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPescaP2 : QuestStep
{
    protected override void SetQuestStepState(string state)
    {
        //
    }
    void Start(){
        UpdateState();
    }
    void OnEnable(){
        GameEventsManager.instance.playerEvents.onPlayerTriesFishing+=WaitForComplete;
    }
    void OnDisable(){
        GameEventsManager.instance.playerEvents.onPlayerTriesFishing-=WaitForComplete;
    }
    void WaitForComplete(){
        FinishQuestStep();
    }
    
    public void UpdateState(){
        string state = "";
        string status = "Ande até a começo do lago até aparecer a opção de pescar e tente pescar"+ 
        ". Depois de um tempo vai aparecer uma exclamação, para abrir o minigame aperte o botão esq do mouse";
        ChangeState(state,status);
    }
}
