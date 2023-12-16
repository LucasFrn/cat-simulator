using System.Collections.Generic;
using UnityEngine;

public class CreateHouseObject : MonoBehaviour
{
    [SerializeField] GameObject[] houseObjects;

    Dictionary<string, StoreItens> houseObjectsDictonary = new Dictionary<string, StoreItens>();

    [SerializeField] private BuildingSystem buildingSystem;

    void Start()
    {

       /* foreach(GameObject objects in houseObjects)
        {
            houseObjectsDictonary.Add(objects.name, objects);
        }*/
    }
    public void InstantiateObject(ObjectData data)
    {
        buildingSystem.InitializeWithObject(HouseObjectList.houseObjectList[data.objectName], data.position, data.rotation, false);
    }
}
