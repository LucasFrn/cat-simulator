using System.Collections.Generic;
using UnityEngine;

public class CreateHouseObject : MonoBehaviour
{
    [SerializeField] GameObject[] houseObjects;

    Dictionary<string, GameObject> houseObjectsDictonary = new Dictionary<string, GameObject>();

    private BuildingSystem buildingSystem;

    void Start()
    {
        buildingSystem = gameObject.GetComponent<BuildingSystem>();

        foreach(GameObject objects in houseObjects)
        {
            houseObjectsDictonary.Add(objects.name, objects);
        }
    }
    public void InstantiateObject(ObjectData data)
    {
        buildingSystem.InitializeWithObject(houseObjectsDictonary[data.objectName], data.position, data.rotation, false);
    }
}
