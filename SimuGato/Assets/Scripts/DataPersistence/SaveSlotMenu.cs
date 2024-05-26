using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlotMenu : MonoBehaviour
{
    [Header("Menu Navegation")]
    [SerializeField] private MainMenu mainMenu;
    private SaveSlot[] saveSlots;
    private bool isLoadingGame = false;

    private void Awake(){
        saveSlots = this.GetComponentsInChildren<SaveSlot>();
    }
    public void OnBackClick(){
        mainMenu.ActivateMenu();
        DeactivateMenu();
    }
    public void ActivateMenu(bool isLoadingGame){
        this.gameObject.SetActive(true);
        this.isLoadingGame=isLoadingGame;
        Dictionary<String,GameData> profilesGameData = DataPersistenceManager.instance.GetAllProfilesGameData();

        foreach( SaveSlot saveSlot in saveSlots){
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileId(), out profileData);
            saveSlot.SetData(profileData);
            Button button = saveSlot.GetComponent<Button>();
            if(button!=null){
                if(profileData == null && isLoadingGame){
                    button.interactable=false;
                }
                else{
                    button.interactable=true;
                }
            }
        }
    }
    public void DeactivateMenu(){
        gameObject.SetActive(false);
    }
    public void OnSaveSlotClicled(SaveSlot saveSlot){
        DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());
        if(!isLoadingGame){
            DataPersistenceManager.instance.NewGame();
        }
        DataPersistenceManager.instance.SaveGame();
        if(isLoadingGame){
            SceneManager.LoadScene("JogoPrincipal");
        }
        else
            SceneManager.LoadScene("JogoPrincipal");
            //coloca aqui a troca de cena para a cutscene
    }
    public void OnDeleteClick(SaveSlot saveSlot){
        DataPersistenceManager.instance.Delete(saveSlot.GetProfileId());
        ActivateMenu(isLoadingGame);
    }
}
