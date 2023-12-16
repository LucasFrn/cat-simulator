using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicaController : MonoBehaviour
{
    public AudioSource audioSourceFundo;
    public AudioSource audioSourceSFX;
    public AudioClip[] fundos;
    public AudioClip[] efeitos;
    private bool diaTocando = false, noiteTocando = false;
    void Start()
    {
        audioSourceFundo.clip = fundos[0];
        audioSourceFundo.Play();
    }

    // Update is called once per frame
    public void Dia()
    {
        noiteTocando = false;
        if (diaTocando)
        {

        }
        else
        {
            audioSourceFundo.clip = fundos[0];
            audioSourceFundo.Play();
            diaTocando = true;
        }
       
    }
    public void Noite()
    {
        diaTocando = false;
        if (noiteTocando)
        {

        }
        else
        {
            audioSourceFundo.clip = fundos[1];
            audioSourceFundo.Play();
            noiteTocando = true;
        }
    }
    public void Sucesso()
    {
        audioSourceSFX.clip = efeitos[0];
        audioSourceSFX.Play();
    }
    public void Erro()
    {
        audioSourceSFX.clip = efeitos[1];
        audioSourceSFX.Play();
    }
    public void Atencao()
    {
        audioSourceSFX.clip = efeitos[2];
        audioSourceSFX.Play();
    }
}
