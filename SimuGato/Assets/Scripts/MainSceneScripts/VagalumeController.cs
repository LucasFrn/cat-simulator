using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VagalumeController : MonoBehaviour
{
    public float startTime;
    public float endTime;
    public LuzManager luzManager;
    public GameObject vagalume;

    void Update()
    {
        if (luzManager.HoraDoDia > startTime || luzManager.HoraDoDia < endTime)
        {
            vagalume.SetActive(true);
        }
        else
        {
            vagalume.SetActive(false);
        }
    }
}
