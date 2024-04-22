using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BibliotecaChao",menuName = "ScriptableObjects/CatalogoChao",order = 3)]
public class BibliotecaChao: ScriptableObject{
    public List<GameObject>catalogoChao = new();
}