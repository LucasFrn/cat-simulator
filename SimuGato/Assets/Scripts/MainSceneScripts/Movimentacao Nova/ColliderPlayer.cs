using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPlayer : MonoBehaviour
{
    [SerializeField]ThirdPersonCamera meuScriptCamera; //Scrit da camera no Camera Handler
    // Start is called before the first frame update
    PlayerMovementNovo meuScriptDeMovimento;
    void Start()
    {
        if(meuScriptCamera==null){
            Debug.LogError("Não tenho um script de controle da camera em terceira pessoa");
        }
        meuScriptDeMovimento=GetComponent<PlayerMovementNovo>();
        if(meuScriptDeMovimento==null){
            Debug.LogWarning("Não consegui acessar meu script de movimento");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider collider){
        if(collider.CompareTag("Garden")){
            meuScriptCamera?.TrocaCamera(ThirdPersonCamera.CameraStyle.Garden);
            meuScriptDeMovimento?.ChangeDrag(true);
            GameEventsManager.instance.gardenEvents.EnterGarden();
            GameEventsManager.instance.uiEvents.AtivarImagensGarden(false);
        }
    }
        void OnTriggerExit(Collider collider){
        if(collider.CompareTag("Garden")){
            meuScriptCamera?.TrocaCamera(ThirdPersonCamera.CameraStyle.Basic);
            meuScriptDeMovimento?.ChangeDrag(false);
            GameEventsManager.instance.gardenEvents.LeaveGarden();
            GameEventsManager.instance.uiEvents.DesativarImagensGarden();
        }
    }
}
