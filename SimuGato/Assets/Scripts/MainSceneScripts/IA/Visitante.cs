using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Visitante : NPC
{
    private NavMeshAgent m_Agent;
    private LuzManager luz;
    public GameObject manager;
    public List<GameObject> bancos;
    public GameObject saida;

    private int index = 0;
    private bool interagindo = false;
    private bool temporizador = false;
    private bool gatoArea = false;
    private float tempoRestante = 20;
    private void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        luz = manager.GetComponent<LuzManager>();

    }
    private void FixedUpdate()
    {
        if (luz.HoraDoDia < 8 || luz.HoraDoDia > 17)
        {
            irritado = false;
            conversas = 0;
            m_Agent.SetDestination(saida.transform.position);
        }
        else
        {
            if (gatoArea)
            {
                m_Agent.SetDestination(transform.position);                
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
                        if (index < bancos.Count - 1)
                        {
                            index++;
                        }
                        else
                        {
                            index = 0;
                        }
                    }
                }
                else
                {
                    if (!interagindo)
                    {
                        m_Agent.SetDestination(bancos[index].transform.position);
                    }
                    float distancia = Vector3.Distance(transform.position, m_Agent.destination);
                    if (distancia < 3)
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
            Debug.Log("Gato");
            gatoArea = true;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Saiu gato");
            gatoArea = false;
        }
    }

}
