using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "plantaExe",menuName ="ScriptableObjects/Planta",order = 2)]
public class PlantaSO:ScriptableObject {
    [field: SerializeField]
    public string Nome {get; private set;}
    [field: SerializeField]
    public int ID {get; private set;}
    [field:SerializeField]
    public int Valor {get; private set;}
    [field: SerializeField]
    public int NFases {get; private set;}
    [field: SerializeField]
    public List<GameObject> prefabs {get; private set;}
}