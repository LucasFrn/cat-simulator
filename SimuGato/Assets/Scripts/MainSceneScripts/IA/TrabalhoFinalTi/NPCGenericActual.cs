using System.Collections;
using System.Collections.Generic;
using UnityEngine;
   public class NPCGenericActual : MonoBehaviour
{
    //antigo
    IMovimentacao comportamentoMovimentacao;
    MoverColeta moverColeta;
    MoverWander moverWander;
    MoverPerseguir moverPerseguir;
    [SerializeField] bool isPerseguidor;
    [SerializeField] bool isWanderer;
    [SerializeField]Transform[]pontos;
    [SerializeField]Transform perseguido;
    int nMovimentacoes = 3;
    int movimentoAtual = 0;
    //novo
    [SerializeField]
    float maxSpeed;
    [SerializeField]
    float maxForce;
    [SerializeField]
    Transform alvo;
    [SerializeField]
    Rigidbody rbAlvo;
    [SerializeField]
    LayerMask obstaclesLayer;
    public Rigidbody rb;
    void Start()
    {
        //rb=GetComponent<Rigidbody>();
        if(rb==null){
            Debug.Log("Não achei meu RB");
        }
        moverColeta = new MoverColeta(gameObject.transform,2,pontos);
        moverPerseguir = new MoverPerseguir(maxSpeed,maxForce,rb,alvo,gameObject.transform,obstaclesLayer,rbAlvo);
        moverWander = new MoverWander(maxSpeed,maxForce,rb,gameObject.transform,obstaclesLayer,30);
        comportamentoMovimentacao = moverColeta;
        if(isPerseguidor){
            comportamentoMovimentacao = moverPerseguir;
            movimentoAtual=1;
        }
        if(isWanderer){
            comportamentoMovimentacao = moverWander;
            movimentoAtual = 2;
        }

    }

    // Update is called once per frame
    void Update()
    {
        comportamentoMovimentacao?.Mover();
        //if(Input.GetKeyDown(KeyCode.Return))
        //    myFSM.AdicionarEstados();
    }
}
