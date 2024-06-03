using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardsHendler {
    public static void GiveRewards(Quest quest){
        GameEventsManager.instance.rewardEvents.GoldRewardRecived(quest.info.goldReward);
        GameEventsManager.instance.rewardEvents.ExperienceRewardRecived(quest.info.experienceReward);
        if(quest.info.hasOtherRewards){
            string[]substrings = quest.info.otherRewardsText.Split('&');
            foreach(string s in substrings){
                Debug.Log(s);
                if(s.Contains("semente")){
                    int tipoSemente =0;
                    int quantidade;
                    string[]novasSubstrings = s.Split(' ',System.StringSplitOptions.RemoveEmptyEntries);
                    int.TryParse(novasSubstrings[0],out quantidade);
                    if(s.Contains("Abobora")){
                        tipoSemente = 0;
                    } else
                    if(s.Contains("Cenoura")){
                        tipoSemente = 1;
                    } else
                    if(s.Contains("Tomate")){
                        tipoSemente = 2;
                    }
                    GameEventsManager.instance.rewardEvents.SementeRewardRecived(tipoSemente,quantidade);
                }else
                if(s.Contains("peixe")){
                    int quantidade,tipo=0;
                    string[]novasSubstrings = s.Split(' ',System.StringSplitOptions.RemoveEmptyEntries);
                    int.TryParse(novasSubstrings[0],out quantidade);
                    if(s.Contains("Baiacu")){
                        tipo = 0;
                    }else
                    if(s.Contains("Linguado")){
                        tipo = 1;
                    }else
                    if(s.Contains("Pacu")){
                        tipo = 2;
                    }else
                    if(s.Contains("Piranha")){
                        tipo = 3;
                    }else
                    if(s.Contains("Tilapia")){
                        tipo = 4;
                    }
                    GameEventsManager.instance.rewardEvents.PeixeRewardRecived(tipo,quantidade);
                }else
                if(s.Contains("exp")){
                    int quantidade;
                    string[]novasSubstrings = s.Split(' ',System.StringSplitOptions.RemoveEmptyEntries);
                    int.TryParse(novasSubstrings[0],out quantidade);
                    GameEventsManager.instance.rewardEvents.ExpPescaRewardRecived(quantidade);
                }else
                if(s.Contains("Energetico")){
                    int quantidade;
                    string[]novasSubstrings = s.Split(' ',System.StringSplitOptions.RemoveEmptyEntries);
                    int.TryParse(novasSubstrings[0],out quantidade);
                    GameEventsManager.instance.rewardEvents.EnergeticoRewardRecived(quantidade);
                }
            }
        }
    }

}
