using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMiniGamePao : MonoBehaviour
{   
    public static ControllerMiniGamePao controllerMiniGamePao;
    public GameObject inicioEsteira,FimEsteira;
    public GameObject paoPrefab;
    bool miniGameIsRunning;
    int nPaoSpawnados;
    public int nAcertos,nErros,nPerdidos;
    float tempoMiniGame = 30f;
    float timer;
    float delaySpawn=0,tempoEntreSpawn=1.5f;
    public void Awake(){
        controllerMiniGamePao = this;
    } 
    // Start is called before the first frame update
    void Start()
    {
        miniGameIsRunning=true;
        nPaoSpawnados=0;
        nAcertos=0;
        nPerdidos=0;
        nErros=0;
        timer=0;
        InvokeRepeating("SpawnarPao",delaySpawn,tempoEntreSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        timer+=Time.deltaTime;
        if(timer>=tempoMiniGame&&miniGameIsRunning){
            miniGameIsRunning=false;
            TerminarMiniGame();
        }
        

    }
    void SpawnarPao(){
        if(miniGameIsRunning){
            Instantiate(paoPrefab,inicioEsteira.transform);
            nPaoSpawnados++;
        }
    }
    void TerminarMiniGame(){
        //Calcular taxa acerto
        //Dar display pro jogador
        //Pagar ele?
    }
}
