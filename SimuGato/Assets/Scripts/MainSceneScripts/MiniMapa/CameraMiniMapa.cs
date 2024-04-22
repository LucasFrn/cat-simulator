using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMiniMapa : MonoBehaviour
{
    public Transform player;
    void Start(){
        GameManager.Instance.cameraMiniMapa=this.gameObject;
    }
    private void LateUpdate()
    {
        Vector3 novaPosicao = player.position;  
        novaPosicao.y = transform.position.y;   
        transform.position = novaPosicao;

        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
