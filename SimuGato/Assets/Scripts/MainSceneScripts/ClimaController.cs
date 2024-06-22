using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimaController : MonoBehaviour
{
    [SerializeField]ParticleSystem chuvaFracaVFX;
    [SerializeField]ParticleSystem chuvaForteVFX;
    [SerializeField]Transform playerTranform;
    ParticleSystem vfxTemporario;
    // Start is called before the first frame update
    void OnAwake(){
        GameEventsManager.instance.miscEvents.onFicouDia+=Chover;
    }
    void OnDisable(){
        GameEventsManager.instance.miscEvents.onFicouDia-=Chover;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.F9)){
            if(Input.GetKeyDown(KeyCode.Alpha8)){
                Chover(0);
            }
        }
        if(Input.GetKey(KeyCode.F9)){
            if(Input.GetKeyDown(KeyCode.Alpha9)){
                Chover(1);
            }
        }
        if(Input.GetKey(KeyCode.F9)){
            if(Input.GetKeyDown(KeyCode.Alpha0)){
                vfxTemporario.Stop(true,ParticleSystemStopBehavior.StopEmitting);
                Destroy(vfxTemporario);
            }
        }
    }
    void Chover(int overloadProb){
        if(vfxTemporario!=null)Destroy(vfxTemporario);
        if(overloadProb==0){
            vfxTemporario=Instantiate(chuvaFracaVFX,playerTranform);
            vfxTemporario.Play();
        }else
        if(overloadProb==1){
            vfxTemporario=Instantiate(chuvaForteVFX,playerTranform);
            vfxTemporario.Play();
        }
        else{
            int chance = Random.Range(0,11);
            if(chance==1){
                vfxTemporario=Instantiate(chuvaFracaVFX,playerTranform);
                vfxTemporario.Play();
            }
            if(chance==2){
                vfxTemporario=Instantiate(chuvaForteVFX,playerTranform);
                vfxTemporario.Play();
            }
        }
    }
}
