using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverColeta : IMovimentacao
{
    Vector3 dir;
    Transform _transform;
    float maxSpeed;
    Transform[] pontos;
    int index;
    bool parado;

    public MoverColeta(Transform transform,float maxSpeed,Transform[] pontos){
        _transform=transform;
        this.maxSpeed = maxSpeed;
        this.pontos = pontos;
        index = 0;
    }
    public void Mover(){
        if(parado) return;
        Vector3 dir = pontos[index].position - _transform.position;
        if(dir.magnitude<=0.1f){
            index+=1;
            if(index>=pontos.Length)
                Parar();
        }
        _transform.position+=dir.normalized*maxSpeed*Time.fixedDeltaTime;
    }
    public void Parar(){
        parado = true;
    }
    public void Voltar(){
        parado = false;
        index = 0;
    }
}
