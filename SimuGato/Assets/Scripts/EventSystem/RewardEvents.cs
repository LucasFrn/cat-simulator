using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardEvents {
    public event Action<int> onGoldRewardRecived;
    public void GoldRewardRecived(int gold){
            Debug.Log($"Disparando um evento do tipo dinheiro com paramentros {gold}");
        if(onGoldRewardRecived!=null){
            onGoldRewardRecived(gold);
        }
    }
    public event Action<int> onExperienceRewardRecived;
    public void ExperienceRewardRecived(int quantidade){
            Debug.Log($"Disparando um evento do tipo experiencia com paramentros {quantidade}");
        if(onExperienceRewardRecived!=null){
            onExperienceRewardRecived(quantidade);
        }
    }
    public event Action<int,int> onSementeRewardRecived;
    public void SementeRewardRecived(int tipo,int quantidade){
        Debug.Log($"Disparando um evento do tipo semente com paramentros tipo {tipo} e quantidade {quantidade}");
        if(onSementeRewardRecived!=null){
            onSementeRewardRecived(tipo,quantidade);
        }
    }
    public event Action<int,int> onPeixeRewardRecived;
    public void PeixeRewardRecived(int tipo,int quantidade){
        Debug.Log($"Disparando um evento do tipo peixe com paramentros tipo {tipo} e quantidade {quantidade}");
        if(onPeixeRewardRecived!=null){
            onPeixeRewardRecived(tipo,quantidade);
        }
    }
    public event Action<int> onExpPescaRewardRecived;
    public void ExpPescaRewardRecived(int quantidade){
        Debug.Log($"Disparando um evento do tipo exp pesca com paramentros {quantidade}");
        if(onExpPescaRewardRecived!=null){
            onExpPescaRewardRecived(quantidade);
        }
    }
    public event Action<int> onEnergeticoRewardRecived;
    public void EnergeticoRewardRecived(int quantidade){
        Debug.Log($"Disparando um evento do tipo energetico com paramentros {quantidade}");
        if(onEnergeticoRewardRecived!=null){
            onEnergeticoRewardRecived(quantidade);
        }
    }
}