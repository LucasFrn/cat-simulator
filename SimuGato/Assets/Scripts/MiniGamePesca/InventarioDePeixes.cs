using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventarioDePeixes : MonoBehaviour
{
    public Text mensagem;
    public List<PeixeItem> meusPeixes = new List<PeixeItem>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J)){
            Debug.Log(meusPeixes.ToString());
        }
        if(Input.GetKeyDown(KeyCode.K)){
            if(meusPeixes.Count>0)
                UsaPeixe(1,meusPeixes[0]);
        }
        if(Input.GetKeyDown(KeyCode.L)){
            if(meusPeixes.Count>0)
                UsaPeixe(2,meusPeixes[0]);
        }
    }

    public void AdicionarPeixe(PeixeItem peixe){
        meusPeixes.Add(peixe);
    }
    public void UsaPeixe(int acao,PeixeItem peixe){
        if(meusPeixes.Contains(peixe)){
            switch(acao)
            {
                case 1:{
                    ControllerMiniGamePesca.controllerMiniGamePesca.player.fome+=peixe.fomeRestauradaAoComer;
                    mensagem.text = "Vc comeu um " + peixe.nomePeixe.ToString() + " e restaurou " + peixe.fomeRestauradaAoComer.ToString() + " de fome";
                    mensagem.gameObject.SetActive(true);
                    Invoke("FechaMensagem",3f);
                }break;
                case 2:{
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

}
