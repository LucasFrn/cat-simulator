using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestQuestStep : QuestStep
{
    void Update(){
        if(Input.GetKeyDown(KeyCode.Keypad6)){
            FinishQuestStep();
        }
    }

    protected override void SetQuestStepState(string state)
    {
        //
    }

}
