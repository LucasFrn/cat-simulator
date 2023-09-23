using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMiniGamePao : MonoBehaviour
{
    public GameObject painelResultado;
    public Slider sliderTaxaAcerto;
    public Text textoInfo;
    void Start()
    {
        painelResultado.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DisplayResultado(){
        AtualizarInfo();
        painelResultado.SetActive(true);
    }

    public void Recomecar(){
        //Start();
        // ControllerMiniGamePao.controllerMiniGamePao.Recomecar();
        SceneManager.LoadScene(0);
    }
    void AtualizarInfo(){
        sliderTaxaAcerto.value=ControllerMiniGamePao.controllerMiniGamePao.tx_Acerto;
        int nAcertos,nPaesSpawnados,nPerfeitos,nPerdidos;
        nAcertos=ControllerMiniGamePao.controllerMiniGamePao.nAcertos;
        nPaesSpawnados=ControllerMiniGamePao.controllerMiniGamePao.nPaesSpawnados;
        nPerfeitos=ControllerMiniGamePao.controllerMiniGamePao.nPerfeitos;
        nPerdidos=ControllerMiniGamePao.controllerMiniGamePao.nPerdidos;
        textoInfo.text = string.Format("Acertos: {0}/{1}\nPerfeitos: {2}/{1}\nPerdidos: {3}/{1}",nAcertos,nPaesSpawnados,nPerfeitos,nPerdidos);
    }
}
