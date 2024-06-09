using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class StatusData
{
    

    public float fome;
    public float energia;
    public float higiene;
    public float felicidade;
    public float social;
    public float timerMorte;
    public int petiscos;
    public int nEnergeticos;

    public StatusData(){
        fome = 50;
        energia = 50;
        higiene = 50;
        felicidade = 50;
        social = 50;
        petiscos = 100;
        timerMorte=0f;
        nEnergeticos=1;
        
    }
    public StatusData(float fome, float energia, float higiene, float felicidade, float social, int petiscos, float timerMorte,int nEnergeticos){
        this.fome = fome;
        this.energia = energia;
        this.higiene = higiene;
        this.felicidade = felicidade;
        this.social = social;
        this.petiscos = petiscos;
        this.timerMorte=timerMorte;
        this.nEnergeticos=nEnergeticos;
    }
}
