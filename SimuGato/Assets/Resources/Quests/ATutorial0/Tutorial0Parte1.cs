using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial0Parte1 : QuestStep
{
    protected override void SetQuestStepState(string state)
    {
        //
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateState();
    }
    void OnEnable(){
        GameEventsManager.instance.uiEvents.onPainelAberto+=CompleteQuest;
    }
    void OnDisable(){
        GameEventsManager.instance.uiEvents.onPainelAberto-=CompleteQuest;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CompleteQuest(int painel){
        if(painel==(int)GameManager.JanelaEmFoco.Mapa){
            FinishQuestStep();
        }
    }
    void UpdateState(){
        string state = "";
        string status = "Esse é um jogo de fazendinha com sobrevivencia, onde voce pode crescer plantas, amassar paozinho, pescar, e mais" +
                        "\n O nosso mapa é bem grande, entao a primeira coisa que vc deveria fazer é abrir seu mapa com M, e dar uma olhada por ai";
        ChangeState(state,status);
    }
}
