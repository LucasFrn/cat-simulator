using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class VFXGenerico : MonoBehaviour
{
    [SerializeField]ParticleSystem vfx;
    [SerializeField]GameObject efeito;
    bool typeIsPS;
    // Start is called before the first frame update
    void Start()
    {
        if(vfx==null){
            typeIsPS=false;
        }
        else{
            typeIsPS=true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider collider){
        if(collider.CompareTag("Player")){
            if(typeIsPS){
                vfx.Play();
            }
            else{
                efeito.SetActive(true);
            }
        }
    }
    void OnTriggerExit(Collider collider){
        if(collider.CompareTag("Player")){
            if(typeIsPS){
                vfx.Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear);
            }
            else{
                efeito.SetActive(false);
            }
        }
    }
}
