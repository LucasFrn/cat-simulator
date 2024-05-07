using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestData {
    public string id;
    /* EU QUEM ADICIONEI ISSO. PQ ELE GUARDA NO PLAYER PREFS, QUE FUNCIONA COMO UM DICIONARIO,
    AI ELE JÁ GUARDA TANTO A KEY QUANDO O VALOR, SENDO O VALOR ESSE SCPRIT AQUI, E EU PRECISO GUARDAR A KEY E O VALOR
    NO MESMO ARQUIVO */
    public QuestState state;
    public int questStepIndex;
    public QuestStepState[] questStepStates;

    public QuestData(string ID, QuestState state, int questStepIndex, QuestStepState[] questStepStates){
        this.id = ID;
        this.state=state;
        this.questStepIndex=questStepIndex;
        this.questStepStates = questStepStates;
    }
}