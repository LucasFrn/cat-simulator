using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    bool painelPausaAberto,painelAudioAberto;
    public GameObject painelPausa,painelConfirmaSair,painelAudio,painelDerrota;
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
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)&&painelAudioAberto){
            AlternaPainelAudio();
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
            GameManager.Instance.janelaEmFoco=1;
        }
        else{
            painelAudio.SetActive(true);
            painelAudioAberto=true;
            GameManager.Instance.janelaEmFoco=6; 
        }
    }
    public void AbreConfirmaSair(){
        painelConfirmaSair.SetActive(true);
        GameManager.Instance.janelaEmFoco=4;
    }
    public void FechaConfirmaSair(){
        painelConfirmaSair.SetActive(false);
        GameManager.Instance.janelaEmFoco=1;
    }
    public void Sair(){
        Application.Quit();
    }

    public void Jogar() 
    {
        SceneManager.LoadScene("JogoPrincipal");

    }
    public void Perder(){
        painelDerrota.SetActive(true);
        Cursor.lockState=CursorLockMode.Confined;
        Cursor.visible=true;
    }
}
