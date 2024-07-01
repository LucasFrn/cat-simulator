using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPescaP4 : QuestStep
{
    protected override void SetQuestStepState(string state)
    {
        //
    }
    void Start(){
        UpdateState();
    }
    void OnEnable(){
        GameEventsManager.instance.uiEvents.onPainelFechado+=WaitForComplete;
    }
    void OnDisable(){
        GameEventsManager.instance.uiEvents.onPainelFechado-=WaitForComplete;
    }
    void WaitForComplete(int painel){
        if(painel == (int)GameManager.JanelaEmFoco.SkillTree)
            FinishQuestStep();
    }
    
    public void UpdateState(){
        string state = "";
        string status = "Pegar um peixe te da 10 de exp Pesca, com 100 vc consegue comprar um upgrade. "
         + "Abra o menu com O e clique com o mouse para comprar um upgrade";
        ChangeState(state,status);
    }
}
