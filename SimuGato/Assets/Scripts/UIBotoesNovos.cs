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
    [SerializeField] GameObject[] imagensTutorial;

    void OnEnable(){
        GameEventsManager.instance.uiEvents.onAtualizarQuantidadeSementes+=AtualizarQuantidadeSementes;
        GameEventsManager.instance.uiEvents.onAtualizarEnergeticos+=AtualizarEnergeticos;
        GameEventsManager.instance.gardenEvents.onEnterGarden+=AtivaImagensGuia;
        GameEventsManager.instance.gardenEvents.onLeaveGarden+=DesativaImagensGuia;
    } 
    void OnDisable(){
        GameEventsManager.instance.uiEvents.onAtualizarQuantidadeSementes-=AtualizarQuantidadeSementes;
        GameEventsManager.instance.uiEvents.onAtualizarEnergeticos-=AtualizarEnergeticos;
        GameEventsManager.instance.gardenEvents.onEnterGarden-=AtivaImagensGuia;
        GameEventsManager.instance.gardenEvents.onLeaveGarden-=DesativaImagensGuia;
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
    void AtivaImagensGuia(){
        for(int i=0;i<imagensTutorial.Length;i++)
            imagensTutorial[i].SetActive(true);
    }
    void DesativaImagensGuia(){
        for(int i=0;i<imagensTutorial.Length;i++)
            imagensTutorial[i].SetActive(false);
    }
}