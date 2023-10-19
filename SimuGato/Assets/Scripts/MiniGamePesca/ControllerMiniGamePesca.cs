using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ControllerMiniGamePesca : MonoBehaviour
{
    public static ControllerMiniGamePesca controllerMiniGamePesca;//Singleton
    public int peixesPescados;//ficara obsolete -> remover depois
    public GameObject peixe;//prefab
    //Variaveis de controle
    public bool barrinhaCompletudeLigada;
    public bool miniGameRodando;
    //Componentes visuais
    public RawImage telaMiniGame;
    public Slider barrinhaCompletude;
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
        //Adicioanr depois opção do esq fechar o minigame
    }
    public void Captura(int dificuldade){//mudar para fechar de uma vez
        //Adicionar um peixe ao jogador
        //Modificar barrinhas
        Debug.Log("Vc pescou um peixe");
        FecharMinigame();
    }
    public void Perder(){
        Debug.Log("O peixe escapou");
        FecharMinigame();
    }
    public void FecharMinigame(){
        miniGameRodando=false;
        barrinhaCompletude.gameObject.SetActive(false);
        telaMiniGame.gameObject.SetActive(false);
        barrinhaCompletudeLigada=false; 
    }
    void SpawnPeixe(){
        if(peixesPescados<3){//mudar para pescar de verdade depois
            Instantiate(peixe,transform.position,Quaternion.identity);
        }
    }
    void ComecaMiniGame(){
        miniGameRodando=true;
        barrinhaCompletudeLigada=false;
        barrinhaCompletude.gameObject.SetActive(false);
        telaMiniGame.gameObject.SetActive(true);
        Invoke("SpawnPeixe",1f);
    }
    public void DefinirMaxBarrinha(float valor){
        barrinhaCompletude.maxValue=valor;
    }
}
