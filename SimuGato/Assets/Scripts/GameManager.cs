using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public UIController uiController;
    public int petiscos = 100;
    public float fome = 50, energia = 50, higiene = 50, felicidade = 50, social = 50;
    //variaveis pra pausar
    public bool jogoPausado;
    public GameObject cameraMiniMapa;
    public float minimapaTamanho = 39;
    //public Transform spawnGatoNoLoad; //receber o "Spawn Do Gato" dentro do prefab da casa
    //public Vector3 posGatoNoLoad = Vector3.zero;//recebe a pos ao trocar de cena
    //public Quaternion rotGatoNoLoad;//recebe a rot gato ao trocar de cena
    //public float HoraDoDiaAoTrocarCena = 6;
    [SerializeField]public TimeSpan GanhoTempoAoTrocarCena = new TimeSpan(0,0,0);
    public bool overrideSaveToGameManager = false;
    public JanelaEmFoco janelaEmFoco;
    //public bool casualModeOn;
    bool jogoPerdido;
    public enum JanelaEmFoco{// Isso daqui é pra tecla esq fazer coisas diferentes de acordo com qual o foco do jogo
        Parque,
        MiniGamePesca,
        InventarioPesca,
        ConfirmaSair,
        MiniGamePao,
        MenuAudio,
        SkillTree,
        Tutorial,
        Creditos,
        Quests,
        Nula,
        Mapa
    }
                            // Manter em 1 para quando for o parque
                            // 2 é meu minigame de pesca
                            // 3 o inventario de pesca
                            // 4 é o painel de confirmar se vai sair
                            // 5 é o minigamepao
                            // 6 é o menu audio
                            // 7 é a skilltree
                            // 8 é o tutorial
                            // 9 é os creditos
    public void Awake()
    {
        
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

    }
    public void Start(){
        ResetCoisasManagerParaJogar();//ISSO SUPOE QUE O JOGO COMEÇA NO PARQUE
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(janelaEmFoco==JanelaEmFoco.Parque){
                if(jogoPausado){
                    uiController.AlternaPainelPausa();
                    jogoPausado=false;
                    Time.timeScale=1f;
                }
                else{
                    uiController.AlternaPainelPausa();
                    jogoPausado=true;
                    Time.timeScale=0f;
                }
            }
        }
    }
    public void Perder(){
        jogoPerdido=true;
        Time.timeScale=0f;
        janelaEmFoco=JanelaEmFoco.Nula;
        uiController.Perder();
    }
    public void ResetCoisasManagerParaJogar(){
        Time.timeScale=1;
        jogoPausado=false;
        janelaEmFoco=JanelaEmFoco.Parque;
    }
    public Vector3 RandPointInSpace(){
        float x = UnityEngine.Random.Range(100,600);
        float y = UnityEngine.Random.Range(15,19);
        float z = UnityEngine.Random.Range(100,600);
        Vector3 vet = new Vector3(x,y,z);
        return vet;
    }
    public void Unlose(){
        if(jogoPerdido){
            jogoPerdido=false;
            Time.timeScale=1f;
            janelaEmFoco=JanelaEmFoco.Parque;
            uiController.DesPerder();
        }
    }
    /* public void ToggleModoPacifico(){//chamar de casual no jogo final
        if(!casualModeOn){    
            casualModeOn=true;
            if(jogoPerdido){
                jogoPerdido=false;
                Time.timeScale=1f;
                janelaEmFoco=JanelaEmFoco.Parque;
                uiController.DesPerder();
            }
        }
        else{
            casualModeOn=false;
        }
    } */
}
