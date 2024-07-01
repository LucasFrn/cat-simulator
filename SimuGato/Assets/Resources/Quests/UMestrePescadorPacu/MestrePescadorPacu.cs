using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MestrePescadorPacu : QuestStep
{
    int numeroPacus;
    protected override void SetQuestStepState(string state)
    {
        if(int.TryParse(state,out numeroPacus)){
            //
        }
        else{
            numeroPacus=0;
        }
    }
    void OnEnable(){
        GameEventsManager.instance.playerEvents.onPlayerPescoPeixe+=CompleteQuest;
    }
    void OnDisable(){
        GameEventsManager.instance.playerEvents.onPlayerPescoPeixe-=CompleteQuest;
    }
    void CompleteQuest(int tipoPeixe){
        if(tipoPeixe==(int)TiposPeixes.Pacu){
            numeroPacus++;
            UpdateState();
            if(numeroPacus>=5){
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
        string state = numeroPacus.ToString();
        string status = "Mostre que vc é brabo mesmo pegando 5 pacus fresquinhos então\n"+
        "Pacus pegos: " + numeroPacus.ToString() + "/5";
        ChangeState(state,status);
    }
}