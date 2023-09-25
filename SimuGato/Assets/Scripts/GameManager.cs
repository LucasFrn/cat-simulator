using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int petiscos = 100;
    public float fome = 50, energia = 50, higiene = 50, felicidade = 50, social = 50;
    public void Awake()
    {
        
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

    }
}
