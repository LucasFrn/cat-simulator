using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField]private bool disableDataPersistance = false;
    [SerializeField] private bool initializeDataIfNull= false;
    [SerializeField] private bool overrideSelectedProfileId = false;
    [SerializeField] private string testSelectedProfileId = "test";
    [Header("File Storage Config")]
    [SerializeField]private string fileName;
    [SerializeField]private bool useEncryption;
    private GameData gameData;
    private List<IDataPersistance> dataPersistanceObjects;
    private FileDataHanlder dataHanlder;
    private string selectedProfileId = "";
    public static DataPersistenceManager instance {get; private set;}
    void Awake(){
        if(instance!=null){
            //if(DebugManager.debugManager.DEBUG){
                Debug.Log("Já temos um DataPersistenceManager, então me destrui");
            //}
            Destroy(this.gameObject);
            return;
        }
        instance=this;
        DontDestroyOnLoad(this.gameObject);
        if(disableDataPersistance){
            Debug.LogWarning("Salvamento está desativado!!");
        }
        this.dataHanlder = new FileDataHanlder(Application.persistentDataPath, fileName,useEncryption);
        InitializeSelectedProfileId();
    }
    public void NewGame(){
        Debug.LogWarning("Foi criada uma nova gameData");
        this.gameData = new GameData();
    }
    public void LoadGame(){
        if(disableDataPersistance){
            return;
        }
        //TODO - Load any saved data frm a file using the data handler
        this.gameData = dataHanlder.Load(selectedProfileId);
        //if no data can be found initialize new game
        if(this.gameData==null && initializeDataIfNull == true){
            NewGame();
        }
        if(this.gameData==null){
            Debug.LogWarning("No data was found. We must create a new save first");
            return;
        }
        //TODO- push the loaded data to all scripts that need it
        foreach (IDataPersistance dataPersistanceObj in  dataPersistanceObjects){
            dataPersistanceObj.LoadData(gameData);
        }
    }
    public void SaveGame(){
        if(disableDataPersistance){
            return;
        }
        if(this.gameData==null){
            Debug.LogWarning("No data was Found, must start a new save before saving");
            return;
        }
        //pass data to other scripts so they can update ir
        foreach (IDataPersistance dataPersistanceObj in  dataPersistanceObjects){
            dataPersistanceObj.SaveData(gameData);
        }
        //timestamp the data so we know when is was last saved
        gameData.lastUpdated = System.DateTime.Now.ToBinary();

        //save data into a file using the data handler
        dataHanlder.Save(gameData,selectedProfileId);
    }
    void OnApplicationQuit(){
        SaveGame();
    }
    private List<IDataPersistance> FindAllDataPersistanceObjects(){
        IEnumerable<IDataPersistance> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>(true).OfType<IDataPersistance>();
        return new List<IDataPersistance>(dataPersistanceObjects);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        dataPersistanceObjects = FindAllDataPersistanceObjects();
        LoadGame();
    }
    void OnEnable(){
        SceneManager.sceneLoaded +=OnSceneLoaded;
    }
    void OnDisable(){
        SceneManager.sceneLoaded -=OnSceneLoaded;
    }

    public bool HasData(){
        Debug.Log(gameData);
        return gameData != null;
    }
    public Dictionary<string, GameData> GetAllProfilesGameData(){
        return dataHanlder.LoadAllProfiles();
    }
    public void ChangeSelectedProfileId(string newProfileId){
        this.selectedProfileId=newProfileId;
        LoadGame();
    }
    public void Delete(string profileId){
        dataHanlder.Delete(profileId);
        InitializeSelectedProfileId();
        LoadGame();
    }
    void InitializeSelectedProfileId(){
        this.selectedProfileId = dataHanlder.GetMostRecentProfileId();
        if(overrideSelectedProfileId){
            this.selectedProfileId=testSelectedProfileId;
            Debug.LogWarning("Demos override no profile que será loadado pelo profile: "+ testSelectedProfileId);
        }
    }
}
