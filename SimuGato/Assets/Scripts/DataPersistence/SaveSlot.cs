using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveSlot : MonoBehaviour
{
    [Header("ProfileId")]
    [SerializeField] private string profileId = "";
    [Header("Content")]
    [SerializeField]private GameObject noDataContent;
    [SerializeField]private GameObject hasDataContent;
    [SerializeField]private TextMeshProUGUI saveSlotName;
    [SerializeField]private TextMeshProUGUI timeText;
    [Header("Delete Save Button")]
    [SerializeField]private Button deleteButton;

    public void SetData(GameData data){
        if(data==null){
            noDataContent.SetActive(true);
            hasDataContent.SetActive(false);
            deleteButton.gameObject.SetActive(false);
        }
        else
        {
            hasDataContent.SetActive(true);
            noDataContent.SetActive(false);
            deleteButton.gameObject.SetActive(true);

            saveSlotName.text = "Save "+ profileId;
            DateTime time = DateTime.FromBinary(data.lastUpdated); 
            timeText.text = time.ToString();
        }
    }
    public string GetProfileId(){
        return this.profileId;
    }
}
