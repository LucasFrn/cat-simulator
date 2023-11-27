using UnityEngine;

[CreateAssetMenu(fileName ="StoreItens")]
public class StoreItens : ScriptableObject
{
    public string _name;
    public int _cost;
    public GameObject prefabIten;
}
