using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiteState : IState
{
    AmbienteMachine ambMachine;
    float timer = 10;// *60;
    public NoiteState(AmbienteMachine am){
        this.ambMachine=am;
    }
    public void Enter(){
        //if(DebugManager.debugManager.DEBUG)
        //    Debug.Log("Ficou de noite");
    }
    public void Exit()
    {
        //if(DebugManager.debugManager.DEBUG)
        //   Debug.Log("Fim da noite");
    }
    public void Update()
    {
        timer-=Time.deltaTime;
        if(timer<0){
            ambMachine.ChangeStateDiaNoite(new DiaState(ambMachine));
        }
    }
}
