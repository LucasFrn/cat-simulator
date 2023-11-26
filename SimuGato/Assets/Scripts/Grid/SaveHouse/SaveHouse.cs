using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HouseData
{
    public ObjectData[] objects;
}

public class SaveHouse : MonoBehaviour
{
    public CreateHouseObject house;
    string path;

    private void Awake()
    {
        path = Application.dataPath + "/saveHouse.txt";
    }

    public void Save()
    {
        HouseData data = new HouseData();
        PlacebleObject[] objects = FindObjectsOfType<PlacebleObject>();

        data.objects = new ObjectData[objects.Length];

        for(int i = 0; i < objects.Length; i++)
        {
            data.objects[i] = new ObjectAdapter(objects[i]);
            
        }
        string s = JsonUtility.ToJson(data, true);
        Debug.Log(s);
        File.WriteAllText(path, s);
    }

    public void Load()
    {
        PlacebleObject[] objs = FindObjectsOfType<PlacebleObject>();

        foreach (PlacebleObject p in objs)
        {
            Destroy(p.gameObject);
        }

        string s = File.ReadAllText(path);
        HouseData data = JsonUtility.FromJson<HouseData>(s);
        BuildingSystem.instance.Clear();
        for(int i = 0; i < data.objects.Length; i++)
        {       
            house.InstantiateObject(data.objects[i]);
        }
    }
}
