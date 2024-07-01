using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class TutorialPescaQuestP1 : QuestStep
{
    protected override void SetQuestStepState(string state)
    {
        //
    }
    void Start(){
        UpdateState();
    }
    void OnTriggerEnter(Collider collider){
        if(collider.CompareTag("Player")){
            FinishQuestStep();
        }
    }
    public void UpdateState(){
        string state = "";
        string status = "Existem alguns lugares onde da para pescar no lago, mas o melhor está marcado com um peixe "
        + "no seu minimapa, vá até la";
        ChangeState(state,status);
    }

}
