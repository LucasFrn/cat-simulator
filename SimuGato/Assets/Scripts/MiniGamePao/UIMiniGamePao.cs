using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMiniGamePao : MonoBehaviour
{
    public GameObject painelResultado;
    public Slider sliderTaxaAcerto;
    public Text textoInfo,textCountDown;
    public GameObject painelDificuldade;
    public Button buttonEasy,buttonMid,buttonHard;
    int energiaGastaMedia = 35, energiaGastaDificil = 50;
    bool imprimeCooldown;
    float timercooldown = 3;
    void Start()
    {
        imprimeCooldown=false;
        Cursor.lockState = CursorLockMode.None;
        
        if(GameManager.Instance.energia<energiaGastaDificil){
            buttonHard.interactable=false;
        }
        if(GameManager.Instance.energia<energiaGastaMedia){
            buttonMid.interactable=false;
        }
        painelResultado.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(imprimeCooldown&&timercooldown>0){
            timercooldown-=Time.deltaTime;
            int timerInt = (int)timercooldown;
            textCountDown.text= string.Format("{0}",timerInt+1);
            if(timercooldown<=0){
                imprimeCooldown=false;
                textCountDown.text="";
            }
        }
    }
    public void DisplayResultado(){
        AtualizarInfo();
        painelResultado.SetActive(true);
    }

    public void Sair(){
        
        //ControllerMiniGamePao.controllerMiniGamePao.Recomecar();
        //Pq que isso ta assim? n faz sentido reiniciar o minigame e sair
        ControllerMiniGamePao.controllerMiniGamePao.AtualizarBarrinhas();
        GameManager.Instance.HoraDoDiaAoTrocarCena+=4f;
        GameManager.Instance.janelaEmFoco=1;
        SceneManager.LoadScene(0);
    }
    void AtualizarInfo(){
        sliderTaxaAcerto.value=ControllerMiniGamePao.controllerMiniGamePao.tx_Acerto;
        int nAcertos,nPaesSpawnados,nPerfeitos,nPerdidos,petiscosGanhos;
        nAcertos=ControllerMiniGamePao.controllerMiniGamePao.nAcertos;
        nPaesSpawnados=ControllerMiniGamePao.controllerMiniGamePao.nPaesSpawnados;
        nPerfeitos=ControllerMiniGamePao.controllerMiniGamePao.nPerfeitos;
        nPerdidos=ControllerMiniGamePao.controllerMiniGamePao.nPerdidos;
        petiscosGanhos=ControllerMiniGamePao.controllerMiniGamePao.petiscosGanhos;
        textoInfo.text = string.Format("Acertos: {0}/{1}\nPerfeitos: {2}/{1}\nPerdidos: {3}/{1}",nAcertos,nPaesSpawnados,nPerfeitos,nPerdidos);
        textoInfo.text += string.Format("\nVocê ganhou {0} petiscos!",petiscosGanhos);
    }
    public void FecharPainelDificuldade(){
        painelDificuldade.SetActive(false);
        imprimeCooldown=true;
    }
}
