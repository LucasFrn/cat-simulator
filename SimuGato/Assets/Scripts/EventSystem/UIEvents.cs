using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvents {
    public event Action<int> onAtualizarEnergeticos;
    public void AtualizarEnergeticos(int n){
        if(onAtualizarEnergeticos!=null){
            onAtualizarEnergeticos(n);
        }
    }
    public event Action<int,int> onAtualizarQuantidadeSementes;
    public void AtualizarQuantidadeSementes(int tipoSemente,int quantidadeSemente){
        if(onAtualizarQuantidadeSementes!=null){
            onAtualizarQuantidadeSementes(tipoSemente,quantidadeSemente);
        }
    }
    public event Action<int> onPainelAberto;
    public void PainelAberto(int painel){
        if(onPainelAberto!=null){
           onPainelAberto(painel);
        }
    }
    public event Action<int> onPainelFechado;
    public void PainelFechado(int painel){
        if(onPainelFechado!=null){
            onPainelFechado(painel);
        }
    }
    public event Action<string,string> onQuestAtualizada;
    public void QuestAtualizada(string displayName,string currentStatus){
        if(onQuestAtualizada!=null){
            onQuestAtualizada(displayName,currentStatus);
        }
    }
    public event Action<bool,QuestState> onPainelInteracaoQuestChange;
    public void PainelInteracaoQuestChange(bool ativarPainel,QuestState state){
        if(onPainelInteracaoQuestChange!=null){
            onPainelInteracaoQuestChange(ativarPainel,state);
        }
    }
}
