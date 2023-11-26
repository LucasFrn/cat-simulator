using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ItemPeixe",menuName = "PeixeItem")]
public class PeixeItem : ScriptableObject, IComparable<PeixeItem>
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
    int IComparable<PeixeItem>.CompareTo(PeixeItem obj){
        return String.Compare(this.nomePeixe,obj.nomePeixe);
    }
    public static IComparer<PeixeItem> SortaValorCres(){
        return (IComparer<PeixeItem>) new SortValorCresHelper();
    }
    public static IComparer<PeixeItem> SortaValorDec(){
        return (IComparer<PeixeItem>) new SortValorDecHelper();
    }
    public static IComparer<PeixeItem> SortaFomeCres(){
        return (IComparer<PeixeItem>) new SortFomeCresHelper();
    }
    public static IComparer<PeixeItem> SortaFomeDec(){
        return (IComparer<PeixeItem>) new SortFomeDecHelper();
    }
    private class SortValorCresHelper: IComparer<PeixeItem>{
        int IComparer<PeixeItem>.Compare(PeixeItem x, PeixeItem y){
            if(x.valorVenda>y.valorVenda)
                return 1;
            if(x.valorVenda<y.valorVenda)
                return -1;
            else
                return 0;
        }
    }
    private class SortValorDecHelper: IComparer<PeixeItem>{
        int IComparer<PeixeItem>.Compare(PeixeItem x, PeixeItem y){
            if(x.valorVenda<y.valorVenda)
                return 1;
            if(x.valorVenda>y.valorVenda)
                return -1;
            else
                return 0;
        }
    }
    private class SortFomeCresHelper: IComparer<PeixeItem>{
        int IComparer<PeixeItem>.Compare(PeixeItem x, PeixeItem y){
            if(x.fomeRestauradaAoComer>y.fomeRestauradaAoComer)
                return 1;
            if(x.fomeRestauradaAoComer<y.fomeRestauradaAoComer)
                return -1;
            else
                return 0;
        }
    }
    private class SortFomeDecHelper: IComparer<PeixeItem>{
        int IComparer<PeixeItem>.Compare(PeixeItem x, PeixeItem y){
            if(x.fomeRestauradaAoComer<y.fomeRestauradaAoComer)
                return 1;
            if(x.fomeRestauradaAoComer>y.fomeRestauradaAoComer)
                return -1;
            else
                return 0;
        }
    }
}
