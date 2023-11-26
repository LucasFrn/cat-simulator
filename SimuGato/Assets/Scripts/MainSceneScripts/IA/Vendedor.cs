using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Vendedor : NPC
{
    private NavMeshAgent m_Agent;
    private LuzManager luz;
    public GameObject manager;
    public GameObject banca;
    public GameObject saida;


    private bool gatoArea = false;
    private void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        luz = manager.GetComponent<LuzManager>();

    }
    private void FixedUpdate()
    {
        if (luz.HoraDoDia < 6 || luz.HoraDoDia > 17)
        {
            trabalhando = false;
            irritado = false;
            conversas = 0;
            m_Agent.SetDestination(saida.transform.position);
            if (gatoArea)
            {
                m_Agent.SetDestination(transform.position);
            }
        }
        else
        {
            trabalhando = true;
            m_Agent.SetDestination(banca.transform.position);

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

    public override void Vender(GameObject gameObject)
    {
        Player gato = gameObject.GetComponent<Player>();
        if (gato.petiscos > 10)
        {
            Debug.Log(gato.petiscos);
            gato.petiscos -= 10;
            Debug.Log(gato.petiscos);
        }
    }

}
