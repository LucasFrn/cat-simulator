using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlteraPetisco : MonoBehaviour
{

    public TextMeshProUGUI txt;
    

    void Update()
    {
        txt.text = GameManager.Instance.petiscos.ToString();
    }
}
