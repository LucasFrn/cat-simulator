using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ItemPeixe",menuName = "PeixeItem")]
public class PeixeItem : ScriptableObject
{
    public string nomePeixe;
    public string tamanho;
    public int valorVenda;
    public float fomeRestauradaAoComer;
    public float felicidadeAoPescar;

    public string FalaInfo(){
        string res = nomePeixe+ " " + "Medindo "+ tamanho;
        return res;
    }
}
