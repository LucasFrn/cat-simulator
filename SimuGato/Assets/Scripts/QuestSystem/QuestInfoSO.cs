using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="QuestInfoSO",menuName = "ScriptableObjects/QuestInfoSO",order =1)]
public class QuestInfoSO : ScriptableObject
    {
    [field: SerializeField] public string id {get;private set;}
    
    [Header("General")]
    public string displayName;
    public bool requiresPoint;
    [Header("Requirements")]
    public int levelRequirement;
    public QuestInfoSO[] questPrerequisites;
    [Header("Steps")]
    public GameObject[] questStepPrefabs;
    [Header("Rewards")]
    public int goldReward;
    public int experienceReward;
    public string otherRewardsText;
    /* Devido a como foi feito a interpretação desse texto ele PRECISA TER O FORMATO
    x(int) semente(s) de Nomeplanta (primeira letra maiscula) & exemplo: 4 sementes de Tomate
    x(int) peixe(s) tipo NomePeixe (primeira letra maiucula)& exemplo: 1 peixe tipo Baiacu
    x(int) exp de pesca\n &
    x Energetico(s)*/
    public bool hasOtherRewards;
    public enum OtherRewardsTypes { //só como referencia
        Sementes,
        Peixes,
        ExpPesca,
        Energetico
    }

    private void OnValidate(){
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);//bruxaria desconhecida 
        #endif
    }
}