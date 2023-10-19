using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ControllerMiniGamePesca : MonoBehaviour
{
    public static ControllerMiniGamePesca controllerMiniGamePesca;
    public int peixesPescados;
    public Slider barrinhaCompletude;
    public bool barrinhaCompletudeLigada;
    public GameObject peixe;
    public bool miniGameAberto;
    void Awake(){
        controllerMiniGamePesca = this;
        miniGameAberto=false;
    }
    void Start()
    {
        
        barrinhaCompletude.gameObject.SetActive(false);
        barrinhaCompletudeLigada=false;
        Invoke("SpawnPeixe",0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Captura(int dificuldade){
        peixesPescados+=1;
        barrinhaCompletude.gameObject.SetActive(false);
        barrinhaCompletudeLigada=false;
        Invoke("SpawnPeixe",2f);
    }
    void SpawnPeixe(){
        if(peixesPescados<3){
            Instantiate(peixe,transform.position,Quaternion.identity);
        }
    }
}
