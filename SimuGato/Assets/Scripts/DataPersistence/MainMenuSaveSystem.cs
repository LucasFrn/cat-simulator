using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    [Header("Menu Navegation")]
    [SerializeField] private SaveSlotMenu saveSlotMenu;
    [SerializeField] Button newGameButton;
    [SerializeField] Button continueGameButton;
    [SerializeField] Button loadGameButton;
    void Start(){
        ActivateButtonsDependingOnData();
    }
    public void NewGameButton(){
        saveSlotMenu.ActivateMenu(false);
        DeactivateMenu();
    }
    public void LoadGameButton(){
        saveSlotMenu.ActivateMenu(true);
        DeactivateMenu();
    }
    public void ContinueButton(){
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadScene("JogoPrincipal");
    }
    public void DeactivateMenu(){
        gameObject.SetActive(false);
    }
    public void ActivateMenu(){
        gameObject.SetActive(true);
        ActivateButtonsDependingOnData();
    }
    public void ActivateButtonsDependingOnData(){
            Debug.Log($"temos data? {DataPersistenceManager.instance.HasData()}");
        if(!DataPersistenceManager.instance.HasData()){
            continueGameButton.interactable = false;
            loadGameButton.interactable = false;
        }
    }
}
