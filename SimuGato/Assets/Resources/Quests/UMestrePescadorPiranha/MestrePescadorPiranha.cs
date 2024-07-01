using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MestrePescadorPiranha : QuestStep
{
    int numeroPiranhas;
    protected override void SetQuestStepState(string state)
    {
        if(int.TryParse(state,out numeroPiranhas)){
            //
        }
        else{
            numeroPiranhas=0;
        }
    }
    void OnEnable(){
        GameEventsManager.instance.playerEvents.onPlayerPescoPeixe+=CompleteQuest;
    }
    void OnDisable(){
        GameEventsManager.instance.playerEvents.onPlayerPescoPeixe-=CompleteQuest;
    }
    void CompleteQuest(int tipoPeixe){
        if(tipoPeixe==(int)TiposPeixes.Piranha){
            numeroPiranhas++;
            UpdateState();
            if(numeroPiranhas>=5){
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
        string state = numeroPiranhas.ToString();
        string status = "Mostre que vc é brabo mesmo pegando 5 piranhas fresquinhas então\n"+
        "Piranhas pegas: " + numeroPiranhas.ToString() + "/5";
        ChangeState(state,status);
    }
}