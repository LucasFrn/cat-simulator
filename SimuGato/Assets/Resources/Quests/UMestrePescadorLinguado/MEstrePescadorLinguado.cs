using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MEstrePescadorLinguado : QuestStep
{
    int numeroLinguados;
    protected override void SetQuestStepState(string state)
    {
        if(int.TryParse(state,out numeroLinguados)){
            //
        }
        else{
            numeroLinguados=0;
        }
    }
    void OnEnable(){
        GameEventsManager.instance.playerEvents.onPlayerPescoPeixe+=CompleteQuest;
    }
    void OnDisable(){
        GameEventsManager.instance.playerEvents.onPlayerPescoPeixe-=CompleteQuest;
    }
    void CompleteQuest(int tipoPeixe){
        if(tipoPeixe==(int)TiposPeixes.Linguado){
            numeroLinguados++;
            UpdateState();
            if(numeroLinguados>=5){
                FinishQuestStep();
            }
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
        string state = numeroLinguados.ToString();
        string status = "Mostre que vc é brabo mesmo pegando 5 linguados fresquinhos então\n"+
        "Linguados pegos: " + numeroLinguados.ToString() + "/5";
        ChangeState(state,status);
    }
}