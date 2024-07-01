using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHigiene : QuestStep
{
    bool tomouBanho;
    bool brincouLixo;
    protected override void SetQuestStepState(string state)
    {
        string[] valores = state.Split();
        if(valores[0]=="false")tomouBanho=false;
        else tomouBanho=true;
        if(valores[1]=="false")brincouLixo=false;
        else brincouLixo=true;
    }
    void OnEnable(){
        GameEventsManager.instance.playerEvents.onPlayerBrinca+=LigaLixo;
        GameEventsManager.instance.playerEvents.onPlayerBanho+=LigaBanho;
    }
    void OnDisable(){
        GameEventsManager.instance.playerEvents.onPlayerBrinca-=LigaLixo;
        GameEventsManager.instance.playerEvents.onPlayerBanho-=LigaBanho;
    }
    void CheckCompleteQuest(){
        if(tomouBanho&&brincouLixo){
            FinishQuestStep();
        }
            
    }
    void LigaLixo(bool isLixo){
        if(isLixo){
            brincouLixo=true;
            UpdateState();
            CheckCompleteQuest();
        }
    }
    void LigaBanho(){
        tomouBanho=true;
        UpdateState();
        CheckCompleteQuest();
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateState();
    }

    // Update is called once per frame
    void UpdateState()
    {
        string state= "false false";
        if(tomouBanho&&brincouLixo) state = "true true";
        else if(tomouBanho) state = "true false";
        else if(brincouLixo) state = "false true";
        else state = "false false";
        string status = "A higiene afeta principalmente a pesca, tentar pescar com higiene baixa faz demorar o dobro "+
        "para achar um peixe. Voce perde ela ao brincar com o lixo e ao trabalhar, e recupera tomando banho. Aperte B para tomar banho\n"+
        "Banho tomado: "+ GetOKoufalta(true) + " Brincou com o lixo: " + GetOKoufalta(false);
        ChangeState(state,status);
    }
    string GetOKoufalta(bool banho){
        if(banho){
            if(tomouBanho){
                return "OK";
            }
            else{
                return "falta";
            }
        }
        else{
            if(brincouLixo){
                return "OK";
            }
            else{
                return "falta";
            }
        }
    }
}