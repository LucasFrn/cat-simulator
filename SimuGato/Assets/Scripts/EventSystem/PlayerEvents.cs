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
}
