using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    int petiscos;
    float fome, energia, higiene, felicidade, social;

    public Slider fomeSldr;
    public Slider energiaSldr;
    public Slider higieneSldr;
    public Slider felicidadeSldr;
    public Slider socialSldr;

    private int conversa = 0;
    private int brinc = 0;
    private int quebr = 0;
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

        fomeSldr.value = fome;
        energiaSldr.value = energia;
        higieneSldr.value = higiene;
        felicidadeSldr.value = felicidade;
        socialSldr.value = social;

    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "PontoDeOnibus")
            {


                if (Input.GetKeyDown(KeyCode.E))
                {
                    TransferStatus();
                    SceneManager.LoadScene(1);
                }

            }
            if (hit.transform.tag == "Casa")
            {


                if (Input.GetKeyDown(KeyCode.E))
                {
                    TransferStatus();
                    SceneManager.LoadScene(2);
                }
            }
            if (hit.transform.tag == "Pesca")
            {


                if (Input.GetKeyDown(KeyCode.E))
                {

                }
            }
            if (hit.transform.tag == "NPC")
            {


                if (Input.GetKeyDown(KeyCode.E) && conversa < 3)
                {
                    conversa++;
                    social += 10;
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    social -= 5;
                }

            }

        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (itemSegurado != null)
            {
                SoltaItem();
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            energia = 100;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            higiene = 100;
        }

        PassaTempo();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Fisica" && quebr < 3)
        {
            felicidade += 5;
            quebr++;
        }

    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Seguravel")
        {
            if (brinc <3)
            {
                brinc++;
                felicidade += 10;
            }
            
            PegaItem(collision.gameObject);
        }
    }
    void PassaTempo()
    {
        if (fome > 100) fome = 100;
        else if (fome < 0) fome = 0;
        if (energia > 100) energia = 100;
        else if (energia < 0) energia = 0;
        if (higiene > 100) higiene = 100;
        else if (higiene < 0) higiene = 0;
        if (felicidade > 100) felicidade = 100;
        else if (felicidade < 0) felicidade = 0;
        if (social > 100) social = 100;
        else if (social < 0) social = 0;

        fome -= 0.0001f;
        fomeSldr.value = (float)fome;
        energia -= 0.0001f;
        energiaSldr.value = (float)energia;
        higiene -= 0.0001f;
        higieneSldr.value = (float)(higiene);
        felicidade -= 0.0001f;
        felicidadeSldr.value = (float)(felicidade);
        social -= 0.0001f;
        socialSldr.value = (float)(social);
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
