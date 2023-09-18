using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMiniGamePao : MonoBehaviour
{   
    public static ControllerMiniGamePao controllerMiniGamePao;
    public UIMiniGamePao controllerUI;
    public GameObject inicioEsteira,FimEsteira;
    public GameObject paoPrefab;
    bool miniGameIsRunning,podeSpawnarPaes,miniGameAcabou;
    public int nPaesAtivos, nPaesSpawnados;
    public int nAcertos,nErros,nPerdidos,nPerfeitos;
    public float tx_Acerto,tx_Perfeicao;
    float tempoMiniGame = 30f;
    float timer;
    float delaySpawn=0,tempoEntreSpawn=1.5f;
    public int dificuldade;
    public void Awake(){
        controllerMiniGamePao = this;
    } 
    // Start is called before the first frame update
    void Start()
    {
        switch(dificuldade){
            case 1: tempoEntreSpawn = 1f; break;
            case 2: tempoEntreSpawn = 0.8f;break;
            case 3: tempoEntreSpawn = 0.5f;break;
            default: tempoEntreSpawn= 0.5f;break;
        }
        miniGameAcabou=false;
        nPaesAtivos=0;
        nPaesSpawnados=0;
        podeSpawnarPaes=true;
        miniGameIsRunning=true;
        nAcertos=0;
        nPerdidos=0;
        nErros=0;
        nPerfeitos=0;
        timer=0;
        InvokeRepeating("SpawnarPao",delaySpawn,tempoEntreSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        timer+=Time.deltaTime;
        if(timer>=tempoMiniGame&&miniGameIsRunning){
            podeSpawnarPaes=false;
        }
        if(podeSpawnarPaes==false&&nPaesAtivos<=0){
            miniGameIsRunning=false;
        }
        if(miniGameAcabou==false&&miniGameIsRunning==false){
            Debug.Log("MinigameAcabou");
            miniGameAcabou=true;
            TerminarMiniGame();
        }
        

    }
    void SpawnarPao(){
        if(podeSpawnarPaes){
            Instantiate(paoPrefab,inicioEsteira.transform);
            nPaesAtivos++;
            nPaesSpawnados++;
        }
    }
    void TerminarMiniGame(){
        tx_Acerto=(float)nAcertos/(float)nPaesSpawnados;
        tx_Perfeicao=(float)nPerfeitos/(float)nAcertos;
        controllerUI.DisplayResultado();
        //valor a ser recebido = alguma logica
        //salario * tx_Acerto * mod_dificuldade
        //Pagar o jogador
    }
    public void Recomecar(){
        miniGameAcabou=false;
        nPaesAtivos=0;
        nPaesSpawnados=0;
        podeSpawnarPaes=true;
        miniGameIsRunning=true;
        nAcertos=0;
        nPerdidos=0;
        nErros=0;
        nPerfeitos=0;
        timer=0;
    }
}
