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

    void SelectTipoPlanta(int tipo){
        /********************************* ONDE EU PAREI **************************/
        /* ESSA FUNÇÃO SERÁ COLOCADA NOS BOTOES DE SELECIONAR PLANTAS, ELA DEVE PASSAR UM INT DO TIPO DE PLANTA
        E AI ENVIAR UM EVENTO (QUE VC PRECISA CRIAR) PARA O GRIDMANAGER SE INSCREVER, ONDE ELE DEVE RECEBER ESSE INT AQUI
        E FAZER UM CAST PRA ENUM DO TIPO PLANTA E COLOCAR NA PLANTA SELECIONADA, PRA AI VC FAZER A LOGICA DE PLANTAR NÃO COM
        A PLANTA EM UM INDICE SELECIONADO, E SIM COM A PLANTA GUARDADA NO PLANTA SELECIONADA*/
        /* ALEM DISSO TEMOS O LOG DE QUEST, DEVE SER MOLE NÉ PO, AI FAZER UMAS QUESTS AI, E DAR UM CONFERE NA PASSAGEM DO TEMPO
        E COLOCAR UMA CHAMADA PRAS PLANTAS CRESCEREM NELE, OU METER O MIGUE, E SE SOBRAR TEMPO, COLHER AS PLANTAS */
    }
}
