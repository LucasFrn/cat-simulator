using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BibliotecaPlantas",menuName = "ScriptableObjects/CatalogoPlantas",order = 4)]
public class BibliotecaPlantas: ScriptableObject{
    public List<PlantaSO>catalogoPlantas = new();
}
