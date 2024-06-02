using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoinsQuestStep : QuestStep
{
    private int coinsCollected = 0;
    private int coinsToComplete = 5;

    private void OnEnable(){
        GameEventsManager.instance.miscEvents.onCoinCollected+= CoinCollected;
    }

    private void OnDisable(){
        GameEventsManager.instance.miscEvents.onCoinCollected-= CoinCollected;
    }
    void Start(){
        UpdateState();
    }
    private void CoinCollected(){
        if(coinsCollected<coinsToComplete){
            coinsCollected++;
            UpdateState();
        }
        if (coinsCollected >= coinsToComplete){
            FinishQuestStep();
        }
    }

    public void UpdateState(){
        string state = coinsCollected.ToString();
        string status = "Coletamos "+ coinsCollected+ " / " + coinsToComplete + " moedas";
        ChangeState(state,status);
    }

    protected override void SetQuestStepState(string state)
    {
        this.coinsCollected = System.Int32.Parse(state);
        UpdateState();
    }
}
