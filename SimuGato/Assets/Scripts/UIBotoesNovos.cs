using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIBotoesNovos : MonoBehaviour
{
    [SerializeField] TMP_Text quantidadeAbobora;
    [SerializeField] TMP_Text quantidadeCenoura;
    [SerializeField] TMP_Text quantidadeTomate;
    [SerializeField] TMP_Text quantidadeEnergetico;

    void OnEnable(){
        GameEventsManager.instance.uiEvents.onAtualizarQuantidadeSementes+=AtualizarQuantidadeSementes;
        GameEventsManager.instance.uiEvents.onAtualizarEnergeticos+=AtualizarEnergeticos;
    } 
    void OnDisable(){
        GameEventsManager.instance.uiEvents.onAtualizarQuantidadeSementes-=AtualizarQuantidadeSementes;
        GameEventsManager.instance.uiEvents.onAtualizarEnergeticos-=AtualizarEnergeticos;
    }
    void UpdateText(TMP_Text texto,int quantidade){
        texto.text = quantidade.ToString();
    }
    void AtualizarQuantidadeSementes(int tipoSemente,int quantidade){
        switch(tipoSemente){
            case 1:
                UpdateText(quantidadeAbobora,quantidade);
            break;
            case 2:
                UpdateText(quantidadeCenoura,quantidade);
            break;
            case 3:
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
}
        /********************************* ONDE EU PAREI **************************/
        /* ALEM DISSO TEMOS O LOG DE QUEST, DEVE SER MOLE NÉ PO, AI FAZER UMAS QUESTS AI, E DAR UM CONFERE NA PASSAGEM DO TEMPO
        E COLOCAR UMA CHAMADA PRAS PLANTAS CRESCEREM NELE, OU METER O MIGUE, E SE SOBRAR TEMPO, COLHER AS PLANTAS */