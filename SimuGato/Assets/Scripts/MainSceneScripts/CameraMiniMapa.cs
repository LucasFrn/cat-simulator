using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMiniMapa : MonoBehaviour
{
    public Transform player;

    private void LateUpdate()
    {
        Vector3 novaPosicao = player.position;  
        novaPosicao.y = transform.position.y;   
        transform.position = novaPosicao;

        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
