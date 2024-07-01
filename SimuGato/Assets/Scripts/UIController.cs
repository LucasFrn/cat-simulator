using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    bool painelPausaAberto,painelAudioAberto,painelTutorialAberto,
            painelCreditosAberto,painelQuestsAberto,painelMapaAberto;
    public GameObject painelPausa,painelConfirmaSair,painelAudio,painelDerrota,
            painelTutorial,painelCreditos,QuestsLogUI,painelMapa;
    [SerializeField]Button buttonCasual;
    [SerializeField]Sprite checkmark;
    [SerializeField]Sprite square;
    [SerializeField]Player jogador;
    QuestLogUi questLogUi;
    void Start()
    {
        
        GameManager.Instance.uiController=this;
        if(buttonCasual!=null){
            if(jogador.modoPacificoLigado){
                buttonCasual.image.sprite=checkmark;
            }
            else{
                buttonCasual.image.sprite=square;
            }
        }
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
        if(QuestsLogUI!=null){
            questLogUi=QuestsLogUI.GetComponent<QuestLogUi>();
            questLogUi.HideUI();
            painelQuestsAberto=false;
        }
        if(painelMapa!=null){
            painelMapa.SetActive(false);
            painelMapaAberto=false;
        }
    }

    // Update is called once per frame
    void Update()
    {   //Botar os inputs iguais ao que abre
        if(Input.GetKeyDown(KeyCode.Escape)&&painelAudioAberto){
            AlternaPainelAudio();
        }
        if(Input.GetKeyDown(KeyCode.Escape)&&painelTutorialAberto){
            AlternaPainelTutorial();
        }
        if(Input.GetKeyDown(KeyCode.Escape)&&painelCreditosAberto){
            AlternaPainelCreditos();
        }
        if(Input.GetKeyDown(KeyCode.J)&&GameManager.Instance.janelaEmFoco==GameManager.JanelaEmFoco.Parque){
            AlternaPainelQuests();
        }else
        if((Input.GetKeyDown(KeyCode.Escape)||Input.GetKeyDown(KeyCode.J))&&painelQuestsAberto){
            AlternaPainelQuests();
        }
        if(Input.GetKeyDown(KeyCode.M)&&GameManager.Instance.janelaEmFoco==GameManager.JanelaEmFoco.Parque)
        {
            AlternaPainelMapa();
        }else
        if((Input.GetKeyDown(KeyCode.Escape)||Input.GetKeyDown(KeyCode.M))&&painelMapaAberto){
            AlternaPainelMapa();
        }
    }
    public void AlternaPainelQuests(){
        if(painelQuestsAberto){
            questLogUi.HideUI();
            GameManager.Instance.janelaEmFoco=GameManager.JanelaEmFoco.Parque;
            GameEventsManager.instance.cameraEvents.CameraUnPause();
            painelQuestsAberto=false;
            GameEventsManager.instance.uiEvents.PainelFechado((int)GameManager.JanelaEmFoco.Quests);
            Cursor.lockState=CursorLockMode.Locked;
            Cursor.visible=false;
        }
        else{
            questLogUi.ShowUI();
            painelQuestsAberto=true;
            GameManager.Instance.janelaEmFoco=GameManager.JanelaEmFoco.Quests;
            GameEventsManager.instance.cameraEvents.CameraPause();
            GameEventsManager.instance.uiEvents.PainelAberto((int)GameManager.JanelaEmFoco.Quests);
            Cursor.lockState=CursorLockMode.Confined;
            Cursor.visible=true;
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
            GameEventsManager.instance.uiEvents.PainelFechado((int)GameManager.JanelaEmFoco.MenuAudio);
        }
        else{
            painelAudio.SetActive(true);
            painelAudioAberto=true;
            GameEventsManager.instance.uiEvents.PainelAberto((int)GameManager.JanelaEmFoco.MenuAudio);
            GameManager.Instance.janelaEmFoco=GameManager.JanelaEmFoco.MenuAudio; 
        }
    }
    public void AbreConfirmaSair(){
        painelConfirmaSair.SetActive(true);
        GameManager.Instance.janelaEmFoco=GameManager.JanelaEmFoco.ConfirmaSair;
        GameEventsManager.instance.uiEvents.PainelAberto((int)GameManager.JanelaEmFoco.ConfirmaSair);
    }
    public void FechaConfirmaSair(){
        painelConfirmaSair.SetActive(false);
        GameManager.Instance.janelaEmFoco=GameManager.JanelaEmFoco.Parque;
        GameEventsManager.instance.uiEvents.PainelFechado((int)GameManager.JanelaEmFoco.ConfirmaSair);
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
    public void DesPerder(){
        painelDerrota.SetActive(false);
        Cursor.lockState=CursorLockMode.Confined;
        Cursor.visible=false;
    }
    public void AlternaPainelTutorial(){
        if(painelTutorialAberto){
            painelTutorial.SetActive(false);
            painelTutorialAberto=false;
            GameManager.Instance.janelaEmFoco=GameManager.JanelaEmFoco.Parque;
            GameEventsManager.instance.uiEvents.PainelFechado((int)GameManager.JanelaEmFoco.Tutorial);
        }
        else{
            painelTutorial.SetActive(true);
            painelTutorialAberto=true;
            GameManager.Instance.janelaEmFoco=GameManager.JanelaEmFoco.Tutorial;
            GameEventsManager.instance.uiEvents.PainelAberto((int)GameManager.JanelaEmFoco.Tutorial); 
        }
    }
    public void AlternaPainelCreditos(){
        if(painelCreditosAberto){
            painelCreditos.SetActive(false);
            painelCreditosAberto=false;
            GameManager.Instance.janelaEmFoco=GameManager.JanelaEmFoco.Parque;
            GameEventsManager.instance.uiEvents.PainelFechado((int)GameManager.JanelaEmFoco.Creditos);
            
        }
        else{
            painelCreditos.SetActive(true);
            painelCreditosAberto=true;
            GameManager.Instance.janelaEmFoco=GameManager.JanelaEmFoco.Creditos;
            GameEventsManager.instance.uiEvents.PainelAberto((int)GameManager.JanelaEmFoco.Creditos); 
        }
    }
    public void AlternaPainelMapa(){
        if(painelMapaAberto){
            painelMapa.SetActive(false);
            painelMapaAberto=false;
            GameEventsManager.instance.cameraEvents.CameraUnPause();
            GameManager.Instance.janelaEmFoco=GameManager.JanelaEmFoco.Parque;
            GameEventsManager.instance.uiEvents.PainelFechado((int)GameManager.JanelaEmFoco.Mapa);
        }
        else{
            painelMapa.SetActive(true);
            painelMapaAberto=true;
            GameManager.Instance.janelaEmFoco=GameManager.JanelaEmFoco.Mapa;
            GameEventsManager.instance.uiEvents.PainelAberto((int)GameManager.JanelaEmFoco.Mapa);
            GameEventsManager.instance.cameraEvents.CameraPause();
        }
    }
    public void ToggleModoPacifico(){
        GameEventsManager.instance.uiEvents.ToggleCasualModeOn();
        if(jogador.modoPacificoLigado){
            buttonCasual.image.sprite=checkmark;    
        }
        else{
            buttonCasual.image.sprite=square;
        }
    }
}
