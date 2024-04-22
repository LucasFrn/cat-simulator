using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaState : IState
{
    AmbienteMachine ambMachine;
    float timer = 10;// *60;
    public DiaState(AmbienteMachine am){
        this.ambMachine=am;
    }
    public void Enter()
    {
        //Canto do galo
        SubjectPlayer.instance.NotifyObserver();//Cresce as plantas
        //if(DebugManager.debugManager.DEBUG)
            //Debug.Log("Ficou de dia");
    }
    public void Exit()
    {
        //if(DebugManager.debugManager.DEBUG)
        //    //Debug.Log("Fim do dia");
    }
    public void Update()
    {
        timer-=Time.deltaTime;
        if(timer<0){
            ambMachine.ChangeStateDiaNoite(new NoiteState(ambMachine));
        }
    }
}
