using System.Collections;
using System.Collections.Generic;
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
    public Transform spawnGatoNoLoad; //receber o "Spawn Do Gato" dentro do prefab da casa
    public Vector3 posGatoNoLoad = Vector3.zero;//recebe a pos ao trocar de cena
    public Quaternion rotGatoNoLoad;//recebe a rot gato ao trocar de cena
    public float HoraDoDiaAoTrocarCena = 6;
    public int janelaEmFoco;// Isso daqui é pra tecla esq fazer coisas diferentes de acordo com qual o foco do jogo
                            // Manter em 1 para quando for o parque
                            // 2 é meu minigame de pesca
                            // 3 o inventario de pesca
                            // 4 é o painel de confirmar se vai sair
                            // 5 é o minigamepao
                            // 6 é o menu audio
                            // 7 é a skilltree
                            // 8 é o tutorial
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
        Time.timeScale=1f;
        janelaEmFoco=1;//ISSO SUPOE QUE O JOGO COMEÇA NO PARQUE
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(janelaEmFoco==1){
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
        Time.timeScale=0f;
        janelaEmFoco=-1;
        uiController.Perder();
    }
}
