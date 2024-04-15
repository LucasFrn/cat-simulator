using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubjectPlayer : MonoBehaviour
{
    public static SubjectPlayer instance;
    public List<IObserver>list;
    void Awake(){
        if(instance==null){
            instance=this;
        }
        else{
            Destroy(gameObject);
        }
        list = new List<IObserver>();
    }
    public void AddObserver(IObserver observador){
        list.Add(observador);
    }
    public void NotifyObserver(){
        foreach(IObserver obs in list){
            obs.NotifyObserver();
        }
    }
    //Colocar no Enter do state dia notify
}
