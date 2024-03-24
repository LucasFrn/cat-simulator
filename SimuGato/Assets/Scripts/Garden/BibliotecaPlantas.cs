using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BibliotecaPlantas",menuName = "CatalogoPlantas",order = 2)]
public class BibliotecaPlantas: ScriptableObject{
    public List<PlantaSO>catalogoPlantas = new();
}
