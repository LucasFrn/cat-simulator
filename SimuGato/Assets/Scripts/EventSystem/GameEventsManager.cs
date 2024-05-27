using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance {get; private set;}

    public MiscEvents miscEvents;
    public QuestEvents questEvents;
    public InputEvents inputEvents;
    public PlayerEvents playerEvents;
    public CameraEvents cameraEvents;

    private void Awake(){
        if(instance!=null){
            Debug.LogError("Mais de um Game Events Manager existe");
        }
        instance = this;
        miscEvents = new MiscEvents();
        questEvents = new QuestEvents();
        inputEvents = new InputEvents();
        playerEvents = new PlayerEvents();
        cameraEvents = new CameraEvents();
    }
}