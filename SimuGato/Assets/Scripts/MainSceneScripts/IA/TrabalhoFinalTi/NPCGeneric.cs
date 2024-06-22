using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGeneric : MonoBehaviour
{
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
    Rigidbody rb;
    /* void Awake(){
        moverColeta = new MoverColeta(gameObject.transform,2,pontos);
        moverPerseguir = new MoverPerseguir(gameObject.transform,perseguido,2);
        moverWander = new MoverWander(gameObject.transform,2);
    } */
    void Update()
    {
        comportamentoMovimentacao.Mover();
    }
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        if(rb==null)Debug.LogWarning("temos um npc sem rigidbody");
        moverColeta = new MoverColeta(gameObject.transform,2,pontos);
        //moverPerseguir = new MoverPerseguir();
        //moverWander = new MoverWander(rb,100);
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
    public void TrocaMovimentacao(){
        int index = Random.Range(0,3);
        if(index==movimentoAtual)
            index= (index+1)%nMovimentacoes;
        switch(index){
            case 0: comportamentoMovimentacao=moverColeta; break;
            case 1: comportamentoMovimentacao=moverPerseguir; break;
            case 2: comportamentoMovimentacao=moverWander; break;
            default: comportamentoMovimentacao=moverColeta; break;
        };
        comportamentoMovimentacao.Voltar();
        movimentoAtual=index;
    }
}