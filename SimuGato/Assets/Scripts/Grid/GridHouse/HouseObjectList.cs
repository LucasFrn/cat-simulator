using System.Collections.Generic;
using UnityEngine;

public class HouseObjectList : MonoBehaviour
{
    [SerializeField] private StoreItens[] itens;

    public static Dictionary<string, StoreItens> houseObjectList = new Dictionary<string, StoreItens>();

    void Awake()
    {
        foreach(StoreItens item in itens)
        {
            houseObjectList.Add(item._name, item);
        }
    }
}
