using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class VizitanteMovimento : MonoBehaviour
{
    private NavMeshAgent m_Agent;
    private LuzManager luz;
    public GameObject manager;
    public List<GameObject> bancos;
    public GameObject saida;

    private int index = 0;
    private bool interagindo = false;
    private bool temporizador = false;
    private float tempoRestante = 20;
    private void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        luz = manager.GetComponent<LuzManager>();

    }
    private void FixedUpdate()
    {
        if (luz.HoraDoDia<8 || luz.HoraDoDia>17)
        {
            m_Agent.destination = saida.transform.position;
        }
        else
        {
            if (temporizador)
            {
                if (tempoRestante > 0)
                {
                    tempoRestante -= Time.fixedDeltaTime;
                }
                else
                {
                    temporizador = false;
                    interagindo = false;
                    tempoRestante = 20;
                }
            }
            if (!interagindo)
            {
                m_Agent.destination = bancos[index].transform.position;
            }
            float distancia = Vector3.Distance(transform.position, m_Agent.destination);
            if (distancia < 1)
            {
                if (index <= bancos.Count - 1)
                {
                    index++;
                }
                else
                {
                    index = 0;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Banco")
        {
            temporizador = true;
            interagindo = true;
        }
        else if (collision.gameObject.tag == "Player")
        {
            interagindo = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!temporizador) { interagindo = false; }
        }
    }

}
