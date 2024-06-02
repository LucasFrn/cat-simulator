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
        if(Input.GetKeyDown(KeyCode.Keypad3)){
            Time.timeScale=1;
        }
    }
}
//Vou anotar aqui onde tem coisas de debug pra lembrar de tirar elas depois na build final,
// ou no minimo mudar elas pra aqui dentro, ou sei la, fazer uma variavel global de permitir debug

//Dentro do ThridPersonCamera na pasta movimentação nova tem 2 trocas de camera, nas teclas G e B
//Dentro do GridController nas coisas do garden tem um opção de debug na tecla B
