using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    ControllerMiniGamePesca meuControllerPesca;
    //Cosias de implementeção da arvore
    int numeroHabilidades;
    public SkillNode[] minhaArvoreHabilidades;
    public bool[] skillsCompradas = new bool[5];//ESSE NUMERO É A QUANTIDADE DE HABILIDADES
    public bool[] podeComprarSkill = new bool[5];

    //Variaveis UI
    public Button[] buttonSkills;
    public GameObject painelSkillTree;
    public bool skillTreeOpen;
    void Awake(){
        numeroHabilidades=5;//NUMERO DE HALIBIDADES TOTAIS
    }

    void Start()
    {
        meuControllerPesca = ControllerMiniGamePesca.controllerMiniGamePesca;
        if(painelSkillTree!=null){
            painelSkillTree.SetActive(false);
            skillTreeOpen=false;
        }
        for(int i=0;i<2;i++){//ESSE NUMERO DEVE SER IGUAL A QUATIDADE DE "RAIZES" QUE EXISTEM
            podeComprarSkill[i]=true;
        }
        for(int i = 0;i<skillsCompradas.Length;i++){
            if(skillsCompradas[i]==true){
                LiberaParaComprar(i);
                meuControllerPesca.AtivaHabilidade(i);
            }
        }
        AtualizaInteractable();
    }

    void Update()
    {
        if(!GameManager.Instance.jogoPausado){
            if(Input.GetKeyDown(KeyCode.O)&&skillTreeOpen==false&&GameManager.Instance.janelaEmFoco==GameManager.JanelaEmFoco.Parque){
                ControllerMiniGamePesca.controllerMiniGamePesca.ControleExp(false);
                painelSkillTree.SetActive(true);
                skillTreeOpen=true;
                GameManager.Instance.janelaEmFoco=GameManager.JanelaEmFoco.SkillTree;
                GameEventsManager.instance.uiEvents.PainelAberto((int)GameManager.JanelaEmFoco.SkillTree);
                GameEventsManager.instance.cameraEvents.CameraPause();
                Cursor.lockState=CursorLockMode.Confined;
                Cursor.visible=true;
            }
            else{
                if((Input.GetKeyDown(KeyCode.Escape)||Input.GetKeyDown(KeyCode.O))&&skillTreeOpen==true){
                    painelSkillTree.SetActive(false);
                    skillTreeOpen=false;
                    GameManager.Instance.janelaEmFoco=GameManager.JanelaEmFoco.Parque;
                    GameEventsManager.instance.uiEvents.PainelFechado((int)GameManager.JanelaEmFoco.SkillTree);
                    GameEventsManager.instance.cameraEvents.CameraUnPause();
                    Cursor.lockState=CursorLockMode.Locked;
                    Cursor.visible=false;
                }
            }
        }
    }

    public void ClickComprar(int skill){
        if(skillsCompradas[skill]==true){
            return;
        }
        if(meuControllerPesca.pontosSkill<minhaArvoreHabilidades[skill].custoExp/100){
            Debug.Log("Vc n tem exp suficiente");
            return;
        }
        skillsCompradas[skill]=true;
        LiberaParaComprar(skill);
        AtualizaInteractable();
        meuControllerPesca.pontosSkill-=minhaArvoreHabilidades[skill].custoExp/100;
        meuControllerPesca.AtivaHabilidade(skill);
        meuControllerPesca.UIExp();
    }
    public void LiberaParaComprar(int skill){
        switch(skill){
            case 0: 
                podeComprarSkill[2]=true;
                podeComprarSkill[3]=true;
            break;
            case 1: 
                podeComprarSkill[3]=true;
                podeComprarSkill[4]=true;
            break;
            default: return;
        }
    }
    public void AtualizaInteractable(){
        for(int i=0;i<numeroHabilidades;i++){
            if(podeComprarSkill[i]==false)
                buttonSkills[i].interactable=false;
            else
                buttonSkills[i].interactable=true;
        }
    }
    public bool[] GetSkillCompradas(){
        bool[] newSkillsCompradas = new bool[numeroHabilidades];
        for(int i=0;i<numeroHabilidades;i++){
            newSkillsCompradas[i]=skillsCompradas[i];
        }
        return newSkillsCompradas;

    }
    public void AualizaComData(bool[]skillsJaCompradas){
        for(int i=0;i<numeroHabilidades;i++){
            skillsCompradas[i]=skillsJaCompradas[i];
        }
    }
}
