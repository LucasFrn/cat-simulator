using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventarioPeixesData{
    //Toda vez que for adicinado um novo peixe vc precisa vir aqui adicionar o peixe na lista  
    /* 
    public int nBaiacu;
    public int nLinguado;
    public int nPacu;
    public int nPiranha;
    public int nTilapia; 
    */
    public int []quantidadePeixes;

    public InventarioPeixesData(){
        quantidadePeixes = new int[Enum.GetNames(typeof(TiposPeixes)).Length];
    }
}
