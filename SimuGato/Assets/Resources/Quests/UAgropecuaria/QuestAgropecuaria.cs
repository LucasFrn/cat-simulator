using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestAgropecuaria : QuestStep
{
    int numeroPlantas;
    protected override void SetQuestStepState(string state)
    {
        if(int.TryParse(state,out numeroPlantas)){
            //
        }
        else{
            numeroPlantas=0;
        }
    }
    void OnEnable(){
        GameEventsManager.instance.gardenEvents.onPlantaColhida+=CompleteQuest;
    }
    void OnDisable(){
        GameEventsManager.instance.gardenEvents.onPlantaColhida-=CompleteQuest;
    }
    void CompleteQuest(int valorPlanta){
        numeroPlantas++;
        UpdateState();
        if(numeroPlantas>=20){
            FinishQuestStep();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdateState()
    {
        string state = numeroPlantas.ToString();
        string status = "Vi que oce tem dedo verde, mas se vc quer fazer dinheiro de verdade, venda 20 plantas. "+
        "Ae, vc consegue comprar mais sementes nas maquinas de vender semente, ta no seu mapa\n"+
        "Plantas colhidas: " + numeroPlantas.ToString() + "/20";
        ChangeState(state,status);
    }
}
