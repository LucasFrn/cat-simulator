using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int petiscos = 100;
    public float fome = 50, energia = 50, higiene = 50, felicidade = 50, social = 50;
    //variaveis pra pausar
    public bool jogoPausado;
    public int janelaEmFoco;// Isso daqui é pra tecla esq fazer coisas diferentes de acordo com qual o foco do jogo
                            // Manter em 1 para quando for o parque
                            // 2 é meu minigame de pesca e 3 o inventario de pesca
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
        janelaEmFoco=1;//ISSO SUPOE QUE O JOGO COMEÇA NO PARQUE
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(janelaEmFoco==1){
                if(jogoPausado){
                    jogoPausado=false;
                    Time.timeScale=1f;
                }
                else{
                    jogoPausado=true;
                    Time.timeScale=1f;
                }
            }
        }
    }
}
