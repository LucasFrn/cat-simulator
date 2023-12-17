using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreBuild : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private GameObject buttonPrefab;

    void Start()
    {
        Dictionary<string, StoreItens> objects = HouseObjectList.houseObjectList;

        foreach (StoreItens item in objects.Values)
        {
            GameObject buttonGameObject = Instantiate(buttonPrefab, transform);

            Button button = buttonGameObject.gameObject.GetComponent<Button>();
            button.GetComponentInChildren<Text>().text = item._name;

            button.onClick.AddListener(delegate
            { BuildingSystem.instance.InitializeWithObject(item, new Vector3(0, 1, 0), Vector3.zero, true); });
        }


       /* foreach(StoreItens item in itens)
        {
            GameObject buttonGameObject = Instantiate(buttonPrefab, transform);

            Button button = buttonGameObject.gameObject.GetComponent<Button>();
            button.GetComponentInChildren<Text>().text = item.name;

            button.onClick.AddListener(delegate { BuildingSystem.instance.InitializeWithObject(item.prefabItem,item._cost, new Vector3(0, 1, 0), Vector3.zero, true); });
        }*/
    }
}
