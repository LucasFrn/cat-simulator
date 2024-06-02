using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPlayer : MonoBehaviour
{
    [SerializeField]ThirdPersonCamera meuScriptCamera; //Scrit da camera no Camera Handler
    [SerializeField]GameObject GridIndicator; //Grid Indicator do Garden
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
    void OnCollisionEnter(Collision colisao){
        if(colisao.collider.CompareTag("Garden")){
            meuScriptCamera?.TrocaCamera(ThirdPersonCamera.CameraStyle.Garden);
            GridIndicator?.SetActive(true);
            //meuScriptDeMovimento?.ChangeVelocity(5);
        }
    }
        void OnCollisionExit(Collision colisao){
        if(colisao.collider.CompareTag("Garden")){
            meuScriptCamera?.TrocaCamera(ThirdPersonCamera.CameraStyle.Basic);
            GridIndicator?.SetActive(false);
            meuScriptDeMovimento?.ResetMoveSpeed();
        }
    }
}
