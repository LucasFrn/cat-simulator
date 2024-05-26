using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ControllerMiniGamePesca : MonoBehaviour, IDataPersistance
{
    public static ControllerMiniGamePesca controllerMiniGamePesca;//Singleton
    public PeixeItem[] peixesPossiveis;
    public InventarioDePeixes inventarioJogador;
    InventarioPeixesData inventarioPeixesLoadado;
    public BarraDoJogador minhaBarraDoJogador;
    public GameObject peixe;//prefab
    public MusicaController efeito;
    //Variaveis de controle
    public bool barrinhaCompletudeLigada;
    public bool miniGameRodando;
    //Componentes visuais
    public Text resultadoMiniGame;
    public RawImage telaMiniGame;
    public Slider barrinhaCompletude;
    public Text textoExp;
    public Text textoSkillBuy;
    public Slider barrinhaExp;
    //Outros
    int level = 0;
    GameObject peixeDaVez;
    public Player player;
    public int exp = 0;
    public int pontosSkill=0;
    //coisas de procurar o peixe
    public float tempoMaxProcura = 10f;
    bool procurandoPeixe;
    float timerProcura = 0;
    float randProcura;
    bool conseguePescar;
    public GameObject painelExclamacao;
    public GameObject painelResultado;
    [SerializeField]SkillTree minhaSkillTree;
    SkillTreeData skillTreeDataLoadada;
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
        if(inventarioJogador!=null){
            if(inventarioPeixesLoadado!=null){
                LoadInventario();
            }
        }
        if(minhaSkillTree!=null){
            if(skillTreeDataLoadada!=null){
                LoadSkillTree();
            }
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
        if(Input.GetKeyDown(KeyCode.F11)&&miniGameRodando==false){
            Captura(1);
        }
        if(Input.GetKeyDown(KeyCode.Escape)&&miniGameRodando==true){
            if(peixeDaVez!=null){
                Destroy(peixeDaVez);
            }
            Perder();
        }
        if(Input.GetKeyDown(KeyCode.F10)){
            exp+=100;
        }
        if(procurandoPeixe){
            if(timerProcura<randProcura+2f){
                timerProcura+=Time.deltaTime;
                if(timerProcura>randProcura-0.5&&conseguePescar==false){
                    conseguePescar=true;
                    //tocar audio
                    efeito.Atencao();
                    AtivaPainelExclamacao();
                }
                if(conseguePescar){
                    if(Input.GetButton("Fire1")){
                        procurandoPeixe=false;
                        ComecaMiniGame();
                    }
                }
            }
            else{
                resultadoMiniGame.text = "O peixe escapou!";
                efeito.Erro();
                resultadoMiniGame.gameObject.SetActive(true);
                painelResultado.gameObject.SetActive(true);
                Invoke("FechaResultado",2f);
                GameManager.Instance.janelaEmFoco=GameManager.JanelaEmFoco.Parque;
                GameEventsManager.instance.cameraEvents.CameraUnPause();
            }
        }
    }
    public void Captura(int dificuldade){
        //Adicionar um peixe ao jogador
        efeito.Sucesso();
        PeixeItem novoPeixe=peixesPossiveis[Random.Range(0,peixesPossiveis.Length)];
        inventarioJogador.AdicionarPeixe(novoPeixe);
        //Modificar barrinhas
        player.felicidade+=novoPeixe.felicidadeAoPescar;
        resultadoMiniGame.text = "Vc pescou um peixe, um: "+ novoPeixe.FalaInfo();
        resultadoMiniGame.gameObject.SetActive(true);
        painelResultado.gameObject.SetActive(true);
        Debug.Log("Vc pescou um peixe, um: "+ novoPeixe.FalaInfo());
        ControleExp();
        FecharMinigame();
    }
    public void Perder(){
        efeito.Erro();
        Debug.Log("O peixe escapou");
        resultadoMiniGame.text = "O peixe escapou!";
        resultadoMiniGame.gameObject.SetActive(true);
        painelResultado.gameObject.SetActive(true);
        FecharMinigame();
    }
    public void FecharMinigame(){
        miniGameRodando=false;
        barrinhaCompletude.gameObject.SetActive(false);
        telaMiniGame.gameObject.SetActive(false);
        barrinhaCompletudeLigada=false;
        GameManager.Instance.janelaEmFoco=GameManager.JanelaEmFoco.Parque;
        GameEventsManager.instance.cameraEvents.CameraUnPause();
        Invoke("FechaResultado",3f);
    }
    void SpawnPeixe(){
        if(miniGameRodando==true)
            peixeDaVez= Instantiate(peixe,transform.position,Quaternion.identity);
    }
    public void ComecaMiniGame(){
        miniGameRodando=true;
        barrinhaCompletudeLigada=false;
        barrinhaCompletude.gameObject.SetActive(false);
        telaMiniGame.gameObject.SetActive(true);
        Invoke("SpawnPeixe",1f);
        GameManager.Instance.janelaEmFoco=GameManager.JanelaEmFoco.MiniGamePesca;
        GameEventsManager.instance.cameraEvents.CameraPause();
    }
    public void DefinirMaxBarrinha(float valor){
        barrinhaCompletude.maxValue=valor;
    }
    public void FechaResultado(){
        resultadoMiniGame.gameObject.SetActive(false);
        painelResultado.gameObject.SetActive(false);
    }
    public void AtivaHabilidade(int habilidades){
        switch(habilidades){
            case 0://25% mais fome
                inventarioJogador.AtivaPowerUpFome();
            break;
            case 1://Aumenta a barra
                minhaBarraDoJogador.AtivaHabilidade();
            break;
            case 2://25% mais dinheiro
                inventarioJogador.AtivaPowerUpDinehiro();
            break;
            case 3://Pesca em metade do tempo
                Peixe.skillPescaRapidaComprada=true;
            break;
            case 4://Acha o peixe mais rapido
                tempoMaxProcura=5f;
            break;
            default: return;
        }
    }
    public void ControleExp(){//chamado ao pescar, cuida od exp e já atualiza a info na UI
        exp+=10;
        if(exp>=100){
            pontosSkill++;
            exp=0;
            level++;
        }
        UIExp();
    }
    public void UIExp(){
        barrinhaExp.value=exp;
        textoExp.text="Level... "+level.ToString();
        textoSkillBuy.text="Pontos para gastar: "+ pontosSkill.ToString();
    }
    public void ProcuraPeixe(){
        procurandoPeixe = true;
        timerProcura = 0;
        conseguePescar=false;
        if(player.higiene<=20){
            randProcura=Random.Range(1f,tempoMaxProcura*1.5f);
        }
        else{
            randProcura= Random.Range(1f,tempoMaxProcura);
        }
        GameManager.Instance.janelaEmFoco=GameManager.JanelaEmFoco.MiniGamePesca;
        GameEventsManager.instance.cameraEvents.CameraPause();
    }
    void AtivaPainelExclamacao(){
        painelExclamacao.SetActive(true);
        Invoke("DesativaPainelExclamacao",1f);
    }
    void DesativaPainelExclamacao(){
        painelExclamacao.SetActive(false);
    }

    public void LoadData(GameData data)
    {
        //LoadInventario(data.fishMinigameData.inventarioPeixesData);
        inventarioPeixesLoadado=data.inventarioPeixesData;
        //load dados skilltree
        skillTreeDataLoadada=data.skillTreeData;
    }
    public void LoadInventario(){
            int tipoPeixeInt = (int) TiposPeixes.Baiacu;
            for(int indexQuantidadePeixe=0;indexQuantidadePeixe<inventarioPeixesLoadado.quantidadePeixes.Length;indexQuantidadePeixe++){
                while(inventarioPeixesLoadado.quantidadePeixes[indexQuantidadePeixe]>0){
                    //verificar qual enum que estamos
                    PeixeItem novoPeixe = peixesPossiveis[tipoPeixeInt];
                    //Adicionar peixe (criar nova maneira de adicioanr peixe)
                    inventarioJogador.AdicionarPeixe(novoPeixe);
                    //subtrair 1 do numero de peixe
                    inventarioPeixesLoadado.quantidadePeixes[indexQuantidadePeixe]--;
                }
                tipoPeixeInt++;
            }
    }
    public void LoadSkillTree(){
        exp = skillTreeDataLoadada.exp;
        level= skillTreeDataLoadada.level;
        pontosSkill=skillTreeDataLoadada.pontosSkill;
        minhaSkillTree.AualizaComData(skillTreeDataLoadada.skillsCompradas);
        UIExp();

    }

    public void SaveData(GameData data)
    {
        InventarioPeixesData inventarioPeixesData = new InventarioPeixesData();
        inventarioJogador.SaveData(inventarioPeixesData);
        data.inventarioPeixesData=inventarioPeixesData;
        //SalvarDadosSkillTree
        SkillTreeData skillTreeData = new SkillTreeData(exp,level,pontosSkill,minhaSkillTree.GetSkillCompradas());
        data.skillTreeData=skillTreeData;
    }
}
