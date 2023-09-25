using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputBox : MonoBehaviour
{
    public Pao paoAtual;
    float PerfectionRange = 0.05f;
    float cooldownInput;
    bool aceitaInput;
    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        switch(ControllerMiniGamePao.controllerMiniGamePao.dificuldade){
            case 1: cooldownInput = .5f;break;
            case 2: cooldownInput = .3f;break;
            case 3: cooldownInput = .1f;break;
            default: cooldownInput= .1f;break;
        }
        aceitaInput=true;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(paoAtual!=null){
            dir= paoAtual.gameObject.transform.position-transform.position;
            Debug.Log(dir.ToString());
        }*/
        if(Input.GetKeyDown(KeyCode.UpArrow)&&aceitaInput){
            aceitaInput=false;
            Invoke("LiberaInput",cooldownInput);
            if(paoAtual!=null){
                if(paoAtual.teclaCorreta==KeyCode.UpArrow){
                    dir= paoAtual.gameObject.transform.position-transform.position;
                    //Debug.Log(dir.ToString());
                    Destroy(paoAtual.gameObject);
                    ControllerMiniGamePao.controllerMiniGamePao.nAcertos++;
                    if(dir.x>-PerfectionRange&&dir.x<PerfectionRange){
                        ControllerMiniGamePao.controllerMiniGamePao.nPerfeitos++;
                    }
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
                    dir= paoAtual.gameObject.transform.position-transform.position;
                    //Debug.Log(dir.ToString());
                    Destroy(paoAtual.gameObject);
                    ControllerMiniGamePao.controllerMiniGamePao.nAcertos++;
                    if(dir.x>-PerfectionRange&&dir.x<PerfectionRange){
                        ControllerMiniGamePao.controllerMiniGamePao.nPerfeitos++;
                       
                    }
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
                    dir= paoAtual.gameObject.transform.position-transform.position;
                    //Debug.Log(dir.ToString());
                    Destroy(paoAtual.gameObject);
                    ControllerMiniGamePao.controllerMiniGamePao.nAcertos++;
                    if(dir.x>-PerfectionRange&&dir.x<PerfectionRange){
                        ControllerMiniGamePao.controllerMiniGamePao.nPerfeitos++;
                    }
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
                    dir= paoAtual.gameObject.transform.position-transform.position;
                    //Debug.Log(dir.ToString());
                    Destroy(paoAtual.gameObject);
                    ControllerMiniGamePao.controllerMiniGamePao.nAcertos++;
                    if(dir.x>-PerfectionRange&&dir.x<PerfectionRange){
                        ControllerMiniGamePao.controllerMiniGamePao.nPerfeitos++;
                    }
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