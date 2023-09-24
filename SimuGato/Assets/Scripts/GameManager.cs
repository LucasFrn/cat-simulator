using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int petiscos;
    public double fome, energia, higiene, felicidade, social;
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }
}
