using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents 
{
    public event Action<int> onPlayerLevelChange;
    public void PlayerLevelChange(int level){
        if(onPlayerLevelChange!=null){
            onPlayerLevelChange(level);
        }
    }
    public event Action onPlayerUsesEnergyDrink;
    public void PlayerUsesEnergyDrink(){
        if(onPlayerLevelChange!=null){
            onPlayerUsesEnergyDrink();
            
        }
    }
    public event Action onPlayerTriesFishing;
    public void PlayerTriesFishing(){
        if(onPlayerTriesFishing!=null){
            onPlayerTriesFishing();
        }
    }
    public event Action onPlayerVendePeixe;
    public void PlayerVendePeixe(){
        if(onPlayerVendePeixe!=null){
            onPlayerVendePeixe();
        }
    }
    public event Action onPlayerComePeixe;
    public void PlayerComePeixe(){
        if(onPlayerComePeixe!=null){
            onPlayerComePeixe();
        }
    }
    public event Action<bool> onPlayerBrinca;
    public void PlayerBrinca(bool isLixo){
        if(onPlayerBrinca!=null){
            onPlayerBrinca(isLixo);
        }
    }
    public event Action onPlayerBanho;
    public void PlayerBanho(){
        if(onPlayerBanho!=null){
            onPlayerBanho();
        }
    }
    public event Action onPlayerDorme;
    public void PlayerDorme(){
        if(onPlayerDorme!=null){
            onPlayerDorme();
        }
    }
    public event Action onPlayerTrabalha;
    public void PlayerTrabalha(){
        if(onPlayerTrabalha!=null){
            onPlayerTrabalha();
        }
    }
    public event Action<int> onPlayerPescoPeixe;
    public void PlayerPescouPeixe(int tipoPeixe){
        if(onPlayerPescoPeixe!=null){
            onPlayerPescoPeixe(tipoPeixe);
        }
    }
}
