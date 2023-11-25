using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventarioDePeixes : MonoBehaviour
{
    public Text mensagem;
    public GameObject painelInventario;
    public bool inventarioAberto;
    PeixeItem peixeIndicador;
    public GameObject peixeNaBoca;
    public bool mochilaComprada;
    public GameObject peixePickupablePref;
    //Coisas da UI
    public Text textoNome, textoValor, textoFome;
    public GameObject setinhaVenda, setinhaComer;
    public int iteradorInventario;
    public int iteradorSetinha;
    public List<PeixeItem> meusPeixes = new List<PeixeItem>();
    // Start is called before the first frame update
    void Start()
    {
        iteradorInventario=0;
        iteradorSetinha=0;
        painelInventario.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.Instance.jogoPausado){
            if(Input.GetKeyDown(KeyCode.I)&&inventarioAberto==false&&GameManager.Instance.janelaEmFoco==1){
                painelInventario.SetActive(true);
                inventarioAberto=true;
                GameManager.Instance.janelaEmFoco=3;
                Cursor.lockState=CursorLockMode.Confined;
                Cursor.visible=true;
            }
            if(Input.GetKeyDown(KeyCode.Escape)&&inventarioAberto==true){
                painelInventario.SetActive(false);
                inventarioAberto=false;
                GameManager.Instance.janelaEmFoco=1;
                Cursor.lockState=CursorLockMode.Locked;
                Cursor.visible=false;
            }
            if(inventarioAberto){
                //parte da UI
                if(!mochilaComprada){
                    if(peixeNaBoca==null){
                        AlteraTexto("---",0,0);
                    }
                    else{
                        PeixePickupable pxpick = peixeNaBoca.GetComponent<PeixePickupable>();
                        AlteraTexto(pxpick.peixe.nomePeixe,pxpick.peixe.fomeRestauradaAoComer,pxpick.peixe.valorVenda);
                    }
                }
                else{
                    if(meusPeixes.Count==0){
                        AlteraTexto("---",0,0);
                    }
                    else{
                        peixeIndicador=meusPeixes[iteradorInventario];
                        AlteraTexto(peixeIndicador.nomePeixe,peixeIndicador.fomeRestauradaAoComer,peixeIndicador.valorVenda);
                    }
                }
                if(iteradorSetinha==0){
                    setinhaComer.SetActive(true);
                    setinhaVenda.SetActive(false);
                }
                else{
                    setinhaComer.SetActive(false);
                    setinhaVenda.SetActive(true);
                }
                //Inputs da UI
                if(Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.UpArrow)){
                    if(meusPeixes.Count>0){
                        if(iteradorInventario==0)
                            iteradorInventario=0;
                        else
                            iteradorInventario--;
                    }
                }
                if(Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.DownArrow)){
                    if(meusPeixes.Count>0){
                        if(iteradorInventario>=meusPeixes.Count-1)
                            iteradorInventario=meusPeixes.Count-1;
                        else
                            iteradorInventario++;
                    }
                }
                if(Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.LeftArrow)){
                    if(iteradorSetinha==0)
                        iteradorSetinha=0;
                    else
                        iteradorSetinha--;         
                }
                if(Input.GetKeyDown(KeyCode.D)||Input.GetKeyDown(KeyCode.RightArrow)){
                    if(iteradorSetinha==1)
                        iteradorSetinha=1;
                    else
                        iteradorSetinha++;
                }
                if(Input.GetKeyDown(KeyCode.Return)){
                    if(!mochilaComprada){
                        if(peixeNaBoca!=null){
                            PeixePickupable pxaux = peixeNaBoca.GetComponent<PeixePickupable>();
                            meusPeixes.Add(pxaux.peixe);
                            UsaPeixe(iteradorInventario,pxaux.peixe);
                            GameObject toDestroy = peixeNaBoca;
                            peixeNaBoca=null;
                            Destroy(toDestroy);
                        }
                    }
                    else{
                        if(meusPeixes.Count>0){
                            if(peixeIndicador!=null){
                                UsaPeixe(iteradorSetinha,peixeIndicador);
                                iteradorInventario=0;
                            }
                        }
                    }
                }
            }
        }
    }

    public void AdicionarPeixe(PeixeItem peixe){
        if(mochilaComprada){
            meusPeixes.Add(peixe);
        }
        else{
            Vector3 pos = ControllerMiniGamePesca.controllerMiniGamePesca.player.gameObject.transform.position;
            GameObject peixeInstanciado = Instantiate(peixePickupablePref,pos,Quaternion.identity);
            PeixePickupable peixePickupableScript = peixeInstanciado.AddComponent<PeixePickupable>();
            peixePickupableScript.peixe=peixe;
        }
    }
    public void UsaPeixe(int acao,PeixeItem peixe){
        if(meusPeixes.Contains(peixe)){
            switch(acao)
            {
                case 0:{
                    ControllerMiniGamePesca.controllerMiniGamePesca.player.fome+=peixe.fomeRestauradaAoComer;
                    mensagem.text = "Vc comeu um " + peixe.nomePeixe.ToString() + " e restaurou " + peixe.fomeRestauradaAoComer.ToString() + " de fome";
                    mensagem.gameObject.SetActive(true);
                    Invoke("FechaMensagem",3f);
                }break;
                case 1:{
                    ControllerMiniGamePesca.controllerMiniGamePesca.player.petiscos+=peixe.valorVenda;
                    mensagem.text = "Vc vendeu um " + peixe.nomePeixe.ToString() + " e ganhou " + peixe.valorVenda.ToString() + " petiscos";
                    mensagem.gameObject.SetActive(true);
                    Invoke("FechaMensagem",3f);
                }break;
            }
            meusPeixes.Remove(peixe);
        }
    }
    public void FechaMensagem(){
        mensagem.gameObject.SetActive(false);
    }
    public void AlteraTexto(string nome, float fome, int valor){
        textoNome.text=nome;
        textoFome.text=fome.ToString();
        textoValor.text=valor.ToString();
    }
    public void Ordenar(int modo){
        switch (modo){
            case 1: meusPeixes.Sort(PeixeItem.SortaValorCres()); break;
            case 2: meusPeixes.Sort(PeixeItem.SortaValorDec());break;
            case 3: meusPeixes.Sort(PeixeItem.SortaFomeCres());break;
            case 4: meusPeixes.Sort(PeixeItem.SortaFomeDec());break;
        };
    }

}
