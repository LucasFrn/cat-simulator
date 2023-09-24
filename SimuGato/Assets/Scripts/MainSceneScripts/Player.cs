using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    int petiscos;
    double fome, energia, higiene, felicidade, social;

    private GameObject itemSegurado;
    private Rigidbody seguradoRB;
    public Transform boca;
    // Start is called before the first frame update
    void Start()
    {
        petiscos = GameManager.Instance.petiscos;
        fome = GameManager.Instance.fome;
        energia = GameManager.Instance.energia;
        higiene = GameManager.Instance.higiene;
        felicidade = GameManager.Instance.felicidade;
        social = GameManager.Instance.social;
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "PontoDeOnibus")
                {
                    TransferStatus();
                    SceneManager.LoadScene(1);
                }
                else if (hit.transform.tag == "Casa")
                {
                    TransferStatus();
                    SceneManager.LoadScene(2);
                }

            }
        }
        if(Input.GetButtonDown("Fire1"))
        {
            if (itemSegurado!=null)
            {
                SoltaItem();
            }
        }


        PassaTempo();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Fisica")
        {
            felicidade += 5;
        }
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Seguravel")
        {
            felicidade += 10;
            PegaItem(collision.gameObject);
        }
    }
    void PassaTempo()
    {
        fome -= 0.0001;
        energia -= 000.1;
        higiene -= 000.1;
        felicidade -= 000.1;
        social -= 000.1;
    }

    void TransferStatus()
    {
        GameManager.Instance.petiscos = petiscos;
        GameManager.Instance.fome = fome;
        GameManager.Instance.energia = energia;
        GameManager.Instance.higiene = higiene;
        GameManager.Instance.felicidade = felicidade;
        GameManager.Instance.social = social;
    }

    private void PegaItem(GameObject item)
    {
        itemSegurado = item;
        seguradoRB = itemSegurado.GetComponent<Rigidbody>();

        seguradoRB.isKinematic = true;
        seguradoRB.useGravity = false;
        itemSegurado.transform.SetParent(boca);
        itemSegurado.transform.localPosition = Vector3.zero;
        itemSegurado.transform.localRotation = Quaternion.identity;
    }

    private void SoltaItem()
    {
        seguradoRB.isKinematic = false;
        seguradoRB.useGravity = true;
        itemSegurado.transform.SetParent(null);
        seguradoRB.AddForce(boca.right * -20f, ForceMode.Impulse);
        itemSegurado = null;
    }
}
