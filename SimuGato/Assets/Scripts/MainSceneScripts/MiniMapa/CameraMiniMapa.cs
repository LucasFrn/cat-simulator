using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMiniMapa : MonoBehaviour
{
    public Transform player;
    [SerializeField]bool canZoomIn;
    [SerializeField]float minDistance;
    [SerializeField]float maxDistance;
    [SerializeField]bool isMiniMapa;
    [SerializeField]Camera thisCamera;
    [SerializeField]float factor;
    bool isOpen;
    void Start(){
        if(isMiniMapa)
            GameManager.Instance.cameraMiniMapa=this.gameObject;
        isOpen=false;
    }
    void OnEnable(){
        GameEventsManager.instance.uiEvents.onPainelAberto+=PainelOpened;
        GameEventsManager.instance.uiEvents.onPainelFechado+=PainelClosed;
    }
    void OnDisable(){
        GameEventsManager.instance.uiEvents.onPainelAberto-=PainelOpened;
        GameEventsManager.instance.uiEvents.onPainelFechado-=PainelClosed;
    }
    void Update(){
        if(canZoomIn){
            if(isOpen){
                thisCamera.orthographicSize += Input.mouseScrollDelta.y * -factor;
                if(thisCamera.orthographicSize>maxDistance){
                    thisCamera.orthographicSize=maxDistance;
                }else
                if(thisCamera.orthographicSize<minDistance){
                    thisCamera.orthographicSize=minDistance;
                }
            }
        }
    }
    void PainelOpened(int janela){
        if(janela==(int)GameManager.JanelaEmFoco.Mapa){
            isOpen=true;
        }
    }
    void PainelClosed(int janela){
        if(janela==(int)GameManager.JanelaEmFoco.Mapa){
            isOpen=false;
        }
    }
    private void LateUpdate()
    {
        Vector3 novaPosicao = player.position;  
        novaPosicao.y = transform.position.y;   
        transform.position = novaPosicao;

        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
