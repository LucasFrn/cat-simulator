using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;

public class UIController : MonoBehaviour
{
    bool painelPausaAberto;
    public GameObject painelPausa,painelConfirmaSair;
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
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
