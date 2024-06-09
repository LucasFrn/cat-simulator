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
        string status = "Ir até o lago (siga o peixe no minimapa)";
        ChangeState(state,status);
    }

}
