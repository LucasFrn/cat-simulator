using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    float amizade = 50;
    protected bool irritado = false;
    public bool trabalhando = false;
    protected int conversas = 0;

    public int Conversar()
    {
        conversas++;
        if (conversas > 3)
        {
            irritado = true;
        }
        if (irritado)
        {
            //TODO UI abaixar amizade
            amizade -= 5;
            return -10;
        }
        else
        {
            //TODO subir amizade
            amizade += 5;
            return 10;
        }
    }
    public virtual void Vender(GameObject gato)
    { }
}
