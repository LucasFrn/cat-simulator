using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreBuild : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject buttonPurchasedItem;
    [SerializeField] private Transform buttonPurchasedItemPosition;

    [SerializeField] private List<StoreItens> itens;

    void Start()
    {
        foreach(StoreItens item in itens)
        {
            GameObject buttonGameObject = Instantiate(buttonPrefab, transform);

            Button button = buttonGameObject.gameObject.GetComponent<Button>();
            button.GetComponentInChildren<Text>().text = item.name;

            button.onClick.AddListener(delegate { BuyItem(item, buttonGameObject); });
        }
    }

    void BuyItem(StoreItens item,GameObject thisGameObject)
    {
        GameObject buttonGameObject = Instantiate(buttonPurchasedItem, buttonPurchasedItemPosition);

        Button button = buttonGameObject.gameObject.GetComponent<Button>();
        button.GetComponentInChildren<Text>().text = item.name;

        button.onClick.AddListener(delegate { BuildingSystem.instance.InitializeWithObject(item.prefabIten); });

        thisGameObject.SetActive(false);
    }
}
