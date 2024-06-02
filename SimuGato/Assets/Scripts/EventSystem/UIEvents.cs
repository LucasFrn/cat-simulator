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
            AtualizarQuantidadeSementes(tipoSemente,quantidadeSemente);
        }
    }
}
