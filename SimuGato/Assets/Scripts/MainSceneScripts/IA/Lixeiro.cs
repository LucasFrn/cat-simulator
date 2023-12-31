using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Lixeiro : NPC
{
    private NavMeshAgent m_Agent;
    private LuzManager luz;
    public GameObject manager;
    private GameObject[] lixos;
    public GameObject saida;
    public GameObject lixeira;

    private int index = 0;
    private bool interagindo = false;
    private bool temporizador = false;
    private bool gatoArea = false;
    private bool limpou = false;
    private float tempoRestante = 5;
    private void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        luz = manager.GetComponent<LuzManager>();
        lixos = GameObject.FindGameObjectsWithTag("Lixo");
        irritado = true;
    }
    private void FixedUpdate()
    {
        if (luz.HoraDoDia < 7 || luz.HoraDoDia > 18)
        {
            irritado = true;
            conversas = 0;
            limpou = false;

            m_Agent.SetDestination(saida.transform.position);
        }
        else
        {
            if (limpou)
            {
                irritado = false;
                m_Agent.SetDestination(lixeira.transform.position);
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
                        tempoRestante = 5;
                        Destroy(lixos[index]);
                        if (index < lixos.Length - 1)
                        {
                            index++;
                        }
                        else
                        {
                            limpou = true;
                        }
                    }
                }
                else
                {
                    if (!interagindo)
                    {
                        m_Agent.SetDestination(lixos[index].transform.position);
                    }
                    float distancia = Vector3.Distance(transform.position, m_Agent.destination);
                    if (distancia < 2)
                    {
                        temporizador = true;
                        interagindo = true;

                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            
            gatoArea = true;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            gatoArea = false;
        }
    }

}