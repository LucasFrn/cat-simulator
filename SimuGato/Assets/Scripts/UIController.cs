using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    bool painelPausaAberto,painelAudioAberto,painelTutorialAberto,painelCreditosAberto;
    public GameObject painelPausa,painelConfirmaSair,painelAudio,painelDerrota,painelTutorial,painelCreditos;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.uiController=this;
        if(painelPausa!=null){
            painelPausa.SetActive(false);
            painelPausaAberto=false;
        }
        if(painelConfirmaSair!=null){
            painelConfirmaSair.SetActive(false);
        }
        if(painelAudio!=null){
            painelAudio.SetActive(false);
            painelAudioAberto=false;
        }
        if(painelDerrota!=null){
            painelDerrota.SetActive(false);
        }
        if(painelTutorial!=null){
            painelTutorial.SetActive(false);
            painelTutorialAberto=false;
        }
        if(painelCreditos!=null){
            painelCreditos.SetActive(false);
            painelCreditosAberto=false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)&&painelAudioAberto){
            AlternaPainelAudio();
        }
        if(Input.GetKeyDown(KeyCode.Escape)&&painelTutorialAberto){
            AlternaPainelTutorial();
        }
        if(Input.GetKeyDown(KeyCode.Escape)&&painelCreditosAberto){
            AlternaPainelCreditos();
        }
    }
    public void AlternaPainelPausa(){
        if(painelPausaAberto){
            painelPausa.SetActive(false);
            painelPausaAberto=false;
            Cursor.lockState=CursorLockMode.Locked;
            Cursor.visible=false;
        }
        else{
            painelPausa.SetActive(true);
            painelPausaAberto=true;
            Cursor.lockState=CursorLockMode.Confined;
            Cursor.visible=true;
        }
    }
    public void AlternaPainelAudio(){
        if(painelAudioAberto){
            painelAudio.SetActive(false);
            painelAudioAberto=false;
            GameManager.Instance.janelaEmFoco=GameManager.JanelaEmFoco.Parque;
        }
        else{
            painelAudio.SetActive(true);
            painelAudioAberto=true;
            GameManager.Instance.janelaEmFoco=GameManager.JanelaEmFoco.MenuAudio; 
        }
    }
    public void AbreConfirmaSair(){
        painelConfirmaSair.SetActive(true);
        GameManager.Instance.janelaEmFoco=GameManager.JanelaEmFoco.ConfirmaSair;
    }
    public void FechaConfirmaSair(){
        painelConfirmaSair.SetActive(false);
        GameManager.Instance.janelaEmFoco=GameManager.JanelaEmFoco.Parque;
    }
    public void VoltarAoMenuPrincipal(){
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadScene("MainMenu");
    }
    public void Sair(){
        Application.Quit();
    }

    /* public void Jogar() NÃO É MAIS USADO
    {
        SceneManager.LoadScene("Intro");

    } */
    public void Perder(){
        painelDerrota.SetActive(true);
        Cursor.lockState=CursorLockMode.Confined;
        Cursor.visible=true;
    }
    public void AlternaPainelTutorial(){
        if(painelTutorialAberto){
            painelTutorial.SetActive(false);
            painelTutorialAberto=false;
            GameManager.Instance.janelaEmFoco=GameManager.JanelaEmFoco.Parque;
        }
        else{
            painelTutorial.SetActive(true);
            painelTutorialAberto=true;
            GameManager.Instance.janelaEmFoco=GameManager.JanelaEmFoco.Tutorial; 
        }
    }
    public void AlternaPainelCreditos(){
        if(painelCreditosAberto){
            painelCreditos.SetActive(false);
            painelCreditosAberto=false;
            GameManager.Instance.janelaEmFoco=GameManager.JanelaEmFoco.Parque;
        }
        else{
            painelCreditos.SetActive(true);
            painelCreditosAberto=true;
            GameManager.Instance.janelaEmFoco=GameManager.JanelaEmFoco.Creditos; 
        }
    }
}
