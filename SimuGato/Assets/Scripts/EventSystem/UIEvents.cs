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
}
