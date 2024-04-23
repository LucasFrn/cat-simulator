using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public bool DEBUG;
    public static DebugManager debugManager;
    void Awake(){
        if(debugManager==null){
            debugManager=this;
        }
        else{
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Keypad1)){
            Debug.Log("O debug magico dita que foi pega uma moeda");
            GameEventsManager.instance.miscEvents.coinCollected();
        }
        if(Input.GetKeyDown(KeyCode.Keypad7)){
            Debug.Log("O debug magico dita que foi apertado Submit");
            GameEventsManager.instance.inputEvents.SubmitPressed();
        }
        if(Input.GetKeyDown(KeyCode.Keypad2)){
            Debug.Log("O debug magico dita que o jogador está nivel 1");
            GameEventsManager.instance.playerEvents.PlayerLevelChange(1);
        }
    }
}
