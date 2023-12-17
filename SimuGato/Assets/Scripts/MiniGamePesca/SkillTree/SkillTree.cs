using System.Collections;
using System.Collections.Generic;
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
    void Start()
    {
        meuControllerPesca = ControllerMiniGamePesca.controllerMiniGamePesca;
        if(painelSkillTree!=null){
            painelSkillTree.SetActive(false);
            skillTreeOpen=false;
        }
        for(int i=0;i<2;i++){//ESSE NUMERO É IGUAL A QUATIDADE DE "RAIZES" QUE EXISTEM
            podeComprarSkill[i]=true;
        }
        numeroHabilidades=5;//NUMERO DE HALIBIDADES TOTAIS
        AtualizaInteractable();
    }

    void Update()
    {
        if(!GameManager.Instance.jogoPausado){
            if(Input.GetKeyDown(KeyCode.M)&&skillTreeOpen==false&&GameManager.Instance.janelaEmFoco==1){
                painelSkillTree.SetActive(true);
                skillTreeOpen=true;
                GameManager.Instance.janelaEmFoco=7;
                Cursor.lockState=CursorLockMode.Confined;
                Cursor.visible=true;
            }
            else{
                if(Input.GetKeyDown(KeyCode.Escape)&&skillTreeOpen==true){
                    painelSkillTree.SetActive(false);
                    skillTreeOpen=false;
                    GameManager.Instance.janelaEmFoco=1;
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
}
