using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIBotoesNovos : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI quantidadeAbobora;
    [SerializeField] TextMeshProUGUI quantidadeCenoura;
    [SerializeField] TextMeshProUGUI quantidadeTomate;
    [SerializeField] TextMeshProUGUI quantidadeEnergetico;
    [SerializeField] TextMeshProUGUI displayName;
    [SerializeField] TextMeshProUGUI descricaoQuest;
    [SerializeField] GameObject[] imagensJardim;
    bool descricaoQuestLiberado;

    void OnEnable(){
        GameEventsManager.instance.uiEvents.onAtualizarQuantidadeSementes+=AtualizarQuantidadeSementes;
        GameEventsManager.instance.uiEvents.onAtualizarEnergeticos+=AtualizarEnergeticos;
        GameEventsManager.instance.uiEvents.onAtivarImagensGarden+=AtivaImagensGuia;
        GameEventsManager.instance.uiEvents.onDesativarImagensGarden+=DesativaImagensGuia;
        GameEventsManager.instance.uiEvents.onQuestAtualizada+=AtualizaQuestLog;
        GameEventsManager.instance.uiEvents.onPainelAberto+=LiberaDescricaoQuest;
    } 
    void OnDisable(){
        GameEventsManager.instance.uiEvents.onAtualizarQuantidadeSementes-=AtualizarQuantidadeSementes;
        GameEventsManager.instance.uiEvents.onAtualizarEnergeticos-=AtualizarEnergeticos;
        GameEventsManager.instance.uiEvents.onAtivarImagensGarden-=AtivaImagensGuia;
        GameEventsManager.instance.uiEvents.onDesativarImagensGarden-=DesativaImagensGuia;
        GameEventsManager.instance.uiEvents.onQuestAtualizada-=AtualizaQuestLog;
        GameEventsManager.instance.uiEvents.onPainelAberto-=LiberaDescricaoQuest;
    }
    
    void UpdateText(TextMeshProUGUI texto,int quantidade){
        texto.text = quantidade.ToString();
    }
    void AtualizarQuantidadeSementes(int tipoSemente,int quantidade){
        switch(tipoSemente){
            case 0:
                UpdateText(quantidadeAbobora,quantidade);
            break;
            case 1:
                UpdateText(quantidadeCenoura,quantidade);
            break;
            case 2:
                UpdateText(quantidadeTomate,quantidade);
            break;
        }
    }
    void AtualizarEnergeticos(int quantidade){
        UpdateText(quantidadeEnergetico,quantidade);
    }

    public void SelectTipoPlanta(int tipo){
        GameEventsManager.instance.gardenEvents.PlantaSelecionada(tipo);
    }
    void AtivaImagensGuia(bool somenteQuantidadeSementes){
        if(somenteQuantidadeSementes){
            for(int i=0;i<3;i++)
                imagensJardim[i].SetActive(true);
        }
        else{
            for(int i=0;i<imagensJardim.Length;i++)
                imagensJardim[i].SetActive(true);
        }
    }
    void DesativaImagensGuia(){
        for(int i=0;i<imagensJardim.Length;i++)
            imagensJardim[i].SetActive(false);
        
    }
    void AtualizaQuestLog(string displayName, string statusAtual){
        if(descricaoQuestLiberado){
            this.displayName.text=displayName;
            this.descricaoQuest.text=statusAtual;
        }
    }
    void LiberaDescricaoQuest(int painelAberto){
        if(painelAberto==(int)GameManager.JanelaEmFoco.Quests){
            descricaoQuestLiberado=true;
        }
    }
}