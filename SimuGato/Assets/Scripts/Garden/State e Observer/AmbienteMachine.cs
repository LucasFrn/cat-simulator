using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienteMachine : MonoBehaviour
{
    IState diaNoite;
    //IState estacao;
    void Start()
    {
        ChangeStateDiaNoite(new DiaState(this));
        //ChangeStateEstacao(new Primavera(this));
    }
    void Update()
    {
        diaNoite?.Update();
    }
    public void ChangeStateDiaNoite(IState novoEstado){
        diaNoite?.Exit();
        diaNoite=novoEstado;
        diaNoite?.Enter();
    }
    /*public void ChangeStateEstacao(IState novaEstacao){
        estacao?.Exit();
        estacao=novaEstacao;
        estacao?.Enter();
    }*/
}
