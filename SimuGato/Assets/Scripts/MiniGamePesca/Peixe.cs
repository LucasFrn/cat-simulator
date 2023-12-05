using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Peixe : MonoBehaviour
{
    [SerializeField]float speed;
    Rigidbody rb;
    //variaveis para movimentação
    public float tempoNoPonto,timerDoPonto;
    public bool chegueiNoPonto;
    public float ymax,ymin;//15 e -13
    Vector3 destino;
    public int dificuldade;
    bool isEncostando;
    public float timerPorcentagemPesca;
    float tempoPesca = 15f; //tempo necessario pra capturar o peixe
    bool podePerder;
    //Poderzinho
    public static bool skillPescaRapidaComprada;
    // Start is called before the first frame update
    void Start()
    {
        if(skillPescaRapidaComprada){
            tempoPesca=7.5f;
        }
        ControllerMiniGamePesca.controllerMiniGamePesca.DefinirMaxBarrinha(tempoPesca);
        podePerder=false;
        timerPorcentagemPesca=0;
        isEncostando=false;
        rb = GetComponent<Rigidbody>();
        if(rb==null){
            Debug.LogError("Peixe sem RB");
        }
        dificuldade=Random.Range(1,4);
        switch(dificuldade){
            case 1:{
                tempoNoPonto=5f;
                //speed=
            }break;
            case 3:{
                tempoNoPonto=3f;
            }break;
            case 2:{
                tempoNoPonto=1.5f;
            }break;
        };
        NovoDestino();
        transform.position=destino;
        timerDoPonto=.5f;
        chegueiNoPonto=true;
    }

    // Update is called once per frame
    void Update()
    {
        if(ControllerMiniGamePesca.controllerMiniGamePesca.miniGameRodando==true){
            if(isEncostando){
                if(ControllerMiniGamePesca.controllerMiniGamePesca.barrinhaCompletudeLigada==false){
                    ControllerMiniGamePesca.controllerMiniGamePesca.barrinhaCompletudeLigada=true;
                    ControllerMiniGamePesca.controllerMiniGamePesca.barrinhaCompletude.gameObject.SetActive(true);
                }
                timerPorcentagemPesca+=Time.deltaTime;
            }
            else{
                if(timerPorcentagemPesca>0)
                    timerPorcentagemPesca-=Time.deltaTime;
            }
            if(timerPorcentagemPesca>1f&&podePerder==false){
                podePerder=true;
            }
            if(timerPorcentagemPesca>tempoPesca){
                Destroy(gameObject);
                ControllerMiniGamePesca.controllerMiniGamePesca.Captura(dificuldade);
            }
            if(timerPorcentagemPesca<=0&&podePerder){
                ControllerMiniGamePesca.controllerMiniGamePesca.Perder();
                Destroy(gameObject);
            }
            ControllerMiniGamePesca.controllerMiniGamePesca.barrinhaCompletude.value=timerPorcentagemPesca;
        }
    }
    void FixedUpdate(){
        if(ControllerMiniGamePesca.controllerMiniGamePesca.miniGameRodando==true){
            if(chegueiNoPonto){
                timerDoPonto-=Time.deltaTime;
                if(timerDoPonto<0){
                    rb.velocity=Vector3.zero;
                    NovoDestino();
                    chegueiNoPonto=false;
                    timerDoPonto=tempoNoPonto;
                }
            }
            Vector3 dir = destino-transform.position;
            if(dir.magnitude<.5f){
                chegueiNoPonto=true;
            }
            if(rb.velocity.magnitude>10){
                rb.velocity/=2;
            }
            rb.AddForce(dir.normalized*speed*Time.deltaTime,ForceMode.Impulse);
        }
    }
    void NovoDestino(){
        float posy = Random.Range(ymin,ymax);
        destino=new Vector3(0,posy,-0.6f);
    }
    void OnTriggerEnter(Collider collider){
        if(collider.CompareTag("BarraPescaJogador")){
            isEncostando=true;
        }
        if(collider.CompareTag("BarreiraPesca")){
            rb.velocity=Vector3.zero;
        }
    }
    void OnTriggerExit(Collider collider){
        if(collider.CompareTag("BarraPescaJogador")){
            isEncostando=false;
        }
    }
}
