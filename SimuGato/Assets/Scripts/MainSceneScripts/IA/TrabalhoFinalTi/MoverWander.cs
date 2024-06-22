using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverWander : IMovimentacao
{
    float maxSpeed;
    float maxForce;
    Rigidbody rb;
    Vector3 alvo;
    Transform eu;
    LayerMask obstaclesLayer;

    //Variaveis aux
    Vector3 desiredVelocity;
    Vector3 steeringForce;
    Vector3 vel;
    Vector3 acceleration;
    float timer,tempo;
    public MoverWander(float maxSpeed,float maxForce, Rigidbody rb,Transform eu,LayerMask obstaclesLayer,float tempo){
        this.maxSpeed=maxSpeed;
        this.maxForce=maxForce;
        this.rb=rb;
        this.eu=eu;
        this.obstaclesLayer=obstaclesLayer;
        this.tempo=tempo;
        timer=0;
    }
    public void Mover(){
        if(timer>0){
            timer-=Time.deltaTime;
        }
        else{
            NovoAlvo();
            timer=tempo;
        }
        RaycastHit hit;
        if(Physics.Raycast(eu.position,eu.TransformDirection(Vector3.forward),out hit,5,obstaclesLayer)){
            eu.Rotate(0,30,0,Space.Self);
            eu.Translate(Vector3.forward*maxSpeed/rb.mass*Time.deltaTime); 
        }
        else{
            desiredVelocity = (alvo-eu.position).normalized*maxSpeed;
            steeringForce=desiredVelocity-rb.velocity;
            steeringForce= Vector3.ClampMagnitude(steeringForce,maxForce);
            acceleration = steeringForce/rb.mass;
            vel = rb.velocity+acceleration;
            vel = Vector3.ClampMagnitude(vel,maxSpeed);
            eu.position += vel*Time.deltaTime;
            
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

    void NovoAlvo(){
        alvo= GameManager.Instance.RandPointInSpace();
    }
}
