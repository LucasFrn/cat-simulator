using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputBox : MonoBehaviour
{
    public Pao paoAtual;
    float cooldownInput =1f;
    bool aceitaInput;
    // Start is called before the first frame update
    void Start()
    {
        aceitaInput=true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow)&&aceitaInput){
            aceitaInput=false;
            Invoke("LiberaInput",cooldownInput);
            if(paoAtual!=null){
                if(paoAtual.teclaCorreta==KeyCode.UpArrow){
                    Destroy(paoAtual.gameObject);
                    ControllerMiniGamePao.controllerMiniGamePao.nAcertos++;
                }
                else{
                    Destroy(paoAtual.gameObject);
                    ControllerMiniGamePao.controllerMiniGamePao.nErros++;
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)&&aceitaInput){
            aceitaInput=false;
            Invoke("LiberaInput",cooldownInput);
            if(paoAtual!=null){
                if(paoAtual.teclaCorreta==KeyCode.RightArrow){
                    Destroy(paoAtual.gameObject);
                    ControllerMiniGamePao.controllerMiniGamePao.nAcertos++;
                }
                else{
                    Destroy(paoAtual.gameObject);
                    ControllerMiniGamePao.controllerMiniGamePao.nErros++;
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow)&&aceitaInput){
            aceitaInput=false;
            Invoke("LiberaInput",cooldownInput);
            if(paoAtual!=null){
                if(paoAtual.teclaCorreta==KeyCode.LeftArrow){
                    Destroy(paoAtual.gameObject);
                    ControllerMiniGamePao.controllerMiniGamePao.nAcertos++;
                }
                else{
                    Destroy(paoAtual.gameObject);
                    ControllerMiniGamePao.controllerMiniGamePao.nErros++;
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)&&aceitaInput){
            aceitaInput=false;
            Invoke("LiberaInput",cooldownInput);
            if(paoAtual!=null){
                if(paoAtual.teclaCorreta==KeyCode.DownArrow){
                    Destroy(paoAtual.gameObject);
                    ControllerMiniGamePao.controllerMiniGamePao.nAcertos++;
                }
                else{
                    Destroy(paoAtual.gameObject);
                    ControllerMiniGamePao.controllerMiniGamePao.nErros++;
                }
            }
        }
    }
    void OnTriggerEnter(Collider colidido){
        if(colidido.CompareTag("Pao")){
            paoAtual=colidido.GetComponent<Pao>();
        }
    }
    void OnTriggerExit(){
        paoAtual=null;
    }
    void LiberaInput(){
        aceitaInput=true;
    }
}
