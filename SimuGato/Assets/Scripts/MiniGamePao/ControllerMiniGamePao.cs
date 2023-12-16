using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMiniGamePao : MonoBehaviour
{   
    //Controllers
    public static ControllerMiniGamePao controllerMiniGamePao;
    public UIMiniGamePao controllerUI;
    public GameObject inicioEsteira,FimEsteira;
    public GameObject paoPrefab;
    public InputBox inputBox;
    //variaveis de controle
    bool miniGameIsRunning,podeSpawnarPaes,miniGameAcabou,miniGameComecou;
    public int nPaesAtivos, nPaesSpawnados;
    //variaveis estatistica
    public int nAcertos,nErros,nPerdidos,nPerfeitos;
    public float tx_Acerto,tx_Perfeicao;
    //variaveis tempo
    float tempoMiniGame = 30f;
    public float timer;
    float delaySpawn=3,tempoEntreSpawn=1.5f;
    //Variaveis logica de dificuldade
    //public int dificuldade; 
    private float energiaGasta;
    public int petiscosGanhos;
    private float mod_dinheiro;
    public void Awake(){
        controllerMiniGamePao = this;
    } 
    // Start is called before the first frame update
    void Start()
    {
        miniGameComecou = false;
        miniGameAcabou=false;
        nPaesAtivos=0;
        nPaesSpawnados=0;
        podeSpawnarPaes=false;
        miniGameIsRunning=false;
        nAcertos=0;
        nPerdidos=0;
        nErros=0;
        nPerfeitos=0;
        timer=0;
    }

    // Update is called once per frame
    void Update()
    {
        //miniGameComecou é setado como true ao se definir a dificuldade, controla somente a logica desse loop
        //minigameIsRunning é setado ao definir a dificuldade, é usado pra controlar 2 estagios do minigame:
            // se podemos parar de spawnar paes, e se todos os paes foram consumidos
        if(miniGameComecou){
            timer+=Time.deltaTime;
            if(timer>=tempoMiniGame&&miniGameIsRunning){
                podeSpawnarPaes=false;//permite o funcionamento do proximo loop
            }                         //e pausa o spawn de paes
            if(podeSpawnarPaes==false&&nPaesAtivos<=0){     
                miniGameIsRunning=false;//fecha o loop de cima, e permite a entrada do proximo loop
            }
            if(miniGameAcabou==false&&miniGameIsRunning==false){
                //Debug.Log("MinigameAcabou");
                miniGameAcabou=true;
                TerminarMiniGame(false);
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape)&&miniGameIsRunning==true){
            TerminarMiniGame(true);
            miniGameIsRunning=false;
            podeSpawnarPaes=false;
            miniGameAcabou=true;
        }
        

    }
    void SpawnarPao(){
        if(podeSpawnarPaes){
            Instantiate(paoPrefab,inicioEsteira.transform);
            nPaesAtivos++;
            nPaesSpawnados++;
        }
    }
    void TerminarMiniGame(bool terminadoAntes){
        if(nPaesSpawnados==0){
            tx_Acerto=0;
            tx_Perfeicao=0;
            petiscosGanhos=0;
        }
        else{
            tx_Acerto=(float)nAcertos/(float)nPaesSpawnados;
            tx_Perfeicao=(float)nPerfeitos/(float)nAcertos;
        }
        if(terminadoAntes){
            petiscosGanhos = (int)(50f * tx_Acerto * mod_dinheiro);
            petiscosGanhos+= 5 *nPerfeitos;
        }
        else{
            petiscosGanhos = (int)(100f * tx_Acerto * mod_dinheiro);
            petiscosGanhos+= 5 *nPerfeitos;
        }
        if(GameManager.Instance.felicidade<=20){
            petiscosGanhos-=30;
        }
        controllerUI.DisplayResultado();
        
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
    public void SetDificuldade(int dif){
        switch(dif){
            case 1: tempoEntreSpawn = 0.8f;energiaGasta=25f;mod_dinheiro=1f; break;
            case 2: tempoEntreSpawn = 0.65f;energiaGasta=35f;mod_dinheiro=1.5f;break;
            case 3: tempoEntreSpawn = 0.65f;energiaGasta=50f;mod_dinheiro=2f;break;
            default: tempoEntreSpawn = 1f;energiaGasta=50f;mod_dinheiro=1f; break;
        }
        miniGameComecou=true;
        miniGameIsRunning=true;
        podeSpawnarPaes=true;
        controllerUI.FecharPainelDificuldade();
        inputBox.DefineCooldown(dif);
        Pao.DefineSpeed(dif);
        InvokeRepeating("SpawnarPao",delaySpawn,tempoEntreSpawn);
    }
    public void AtualizarBarrinhas(){
        GameManager.Instance.petiscos += petiscosGanhos;
        GameManager.Instance.energia -= energiaGasta;
        GameManager.Instance.fome += 30;
        GameManager.Instance.higiene -= 40;
        GameManager.Instance.felicidade -= 30;
        GameManager.Instance.social += 20;
    }
}
