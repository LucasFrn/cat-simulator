using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverPerseguir : IMovimentacao
{
    float maxSpeed;
    float maxForce;
    Rigidbody rb;
    Rigidbody rbAlvo;
    Transform alvo;
    Transform eu;
    LayerMask obstaclesLayer;

    //Variaveis aux
    Vector3 desiredVelocity;
    Vector3 steeringForce;
    Vector3 vel;
    Vector3 acceleration;
    public MoverPerseguir(float maxSpeed,float maxForce, Rigidbody rb, Transform alvo,Transform eu,LayerMask obstaclesLayer,Rigidbody rbAlvo){
        this.maxSpeed=maxSpeed;
        this.maxForce=maxForce;
        this.rb=rb;
        this.alvo=alvo;
        this.eu=eu;
        this.obstaclesLayer=obstaclesLayer;
        this.rbAlvo = rbAlvo;
    }
    public void Mover(){
        Vector3 posFut;
        RaycastHit hit;
        if(Physics.Raycast(eu.position,eu.TransformDirection(Vector3.forward),out hit,5,obstaclesLayer)){
            eu.Rotate(0,30,0,Space.Self);
            eu.Translate(Vector3.forward*maxSpeed/rb.mass*Time.deltaTime); 
        }
        else{
            float T=(alvo.position-eu.position).magnitude/50f;
            posFut=alvo.position+rbAlvo.velocity*T;
            desiredVelocity = (posFut-eu.position).normalized*maxSpeed;
            steeringForce=desiredVelocity-rb.velocity;
            steeringForce= Vector3.ClampMagnitude(steeringForce,maxForce);
            acceleration = steeringForce/rb.mass;
            vel = rb.velocity+acceleration;
            vel = Vector3.ClampMagnitude(vel,maxSpeed);
            eu.position += vel*Time.deltaTime;
            eu.LookAt(desiredVelocity);
        }
    }

    public void Parar()
    {
       //
    }

    public void Voltar()
    {
       //
    }
}
