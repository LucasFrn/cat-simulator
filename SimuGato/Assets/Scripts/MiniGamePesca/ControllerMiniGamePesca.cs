using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ControllerMiniGamePesca : MonoBehaviour
{
    public static ControllerMiniGamePesca controllerMiniGamePesca;//Singleton
    public PeixeItem[] peixesPossiveis;
    public InventarioDePeixes inventarioJogador;
    public GameObject peixe;//prefab
    //Variaveis de controle
    public bool barrinhaCompletudeLigada;
    public bool miniGameRodando;
    //Componentes visuais
    public RawImage telaMiniGame;
    public Slider barrinhaCompletude;
    GameObject peixeDaVez;
    void Awake(){
        controllerMiniGamePesca = this;
        miniGameRodando=false;
    }
    void Start()
    {
        
        if(miniGameRodando==false){  
            barrinhaCompletude.gameObject.SetActive(false);
            telaMiniGame.gameObject.SetActive(false);
            barrinhaCompletudeLigada=false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Input magico de debug para começar o jogo
        //substituir pela logica de pescar
        if(Input.GetKeyDown(KeyCode.F12)&&miniGameRodando==false){
            ComecaMiniGame();
        }
        if(Input.GetKeyDown(KeyCode.Escape)&&miniGameRodando==true){
            if(peixeDaVez!=null){
                Destroy(peixeDaVez);
            }
            Perder();
        }
    }
    public void Captura(int dificuldade){
        //Adicionar um peixe ao jogador
        PeixeItem novoPeixe=peixesPossiveis[Random.Range(0,peixesPossiveis.Length)];
        inventarioJogador.meusPeixes.Add(novoPeixe);
        //Modificar barrinhas
        GameManager.Instance.energia-=5f;//Mover isso para a logica de jogar a linha depois
        GameManager.Instance.felicidade+=novoPeixe.felicidadeAoPescar;
        Debug.Log("Vc pescou um peixe, um: "+ novoPeixe.FalaInfo());
        FecharMinigame();
    }
    public void Perder(){
        GameManager.Instance.energia-=5f;
        Debug.Log("O peixe escapou");
        FecharMinigame();
    }
    public void FecharMinigame(){
        miniGameRodando=false;
        barrinhaCompletude.gameObject.SetActive(false);
        telaMiniGame.gameObject.SetActive(false);
        barrinhaCompletudeLigada=false;
        GameManager.Instance.janelaEmFoco=1;
    }
    void SpawnPeixe(){
        peixeDaVez= Instantiate(peixe,transform.position,Quaternion.identity);
    }
    void ComecaMiniGame(){
        miniGameRodando=true;
        barrinhaCompletudeLigada=false;
        barrinhaCompletude.gameObject.SetActive(false);
        telaMiniGame.gameObject.SetActive(true);
        Invoke("SpawnPeixe",1f);
        GameManager.Instance.janelaEmFoco=2;
    }
    public void DefinirMaxBarrinha(float valor){
        barrinhaCompletude.maxValue=valor;
    }
}
