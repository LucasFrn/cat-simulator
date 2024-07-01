using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MestrePescadorTilapia : QuestStep
{
    int numeroTilapias;
    protected override void SetQuestStepState(string state)
    {
        if(int.TryParse(state,out numeroTilapias)){
            //
        }
        else{
            numeroTilapias=0;
        }
    }
    void OnEnable(){
        GameEventsManager.instance.playerEvents.onPlayerPescoPeixe+=CompleteQuest;
    }
    void OnDisable(){
        GameEventsManager.instance.playerEvents.onPlayerPescoPeixe-=CompleteQuest;
    }
    void CompleteQuest(int tipoPeixe){
        if(tipoPeixe==(int)TiposPeixes.Tilapia){
            numeroTilapias++;
            UpdateState();
            if(numeroTilapias>=5){
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
        string state = numeroTilapias.ToString();
        string status = "Mostre que vc é brabo mesmo pegando 5 tilapias fresquinhas então\n"+
        "Tilapias pegas: " + numeroTilapias.ToString() + "/5";
        ChangeState(state,status);
    }
}
