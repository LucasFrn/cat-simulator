using UnityEngine;

[CreateAssetMenu(fileName ="StoreItens")]
public class StoreItens : ScriptableObject
{
    public string Name;
    public int Cost;
    public GameObject prefabIten;
}
