using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioDePeixes : MonoBehaviour
{
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
                    GameManager.Instance.fome+=peixe.fomeRestauradaAoComer;
                }break;
                case 2:{
                    GameManager.Instance.petiscos+=peixe.valorVenda;
                }break;
            }
            meusPeixes.Remove(peixe);
        }
    }

}
