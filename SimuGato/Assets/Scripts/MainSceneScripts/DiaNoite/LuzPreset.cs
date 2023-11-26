using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Luz Preset",menuName ="Scriptables/Luz Preset", order = 1)]
public class LuzPreset : ScriptableObject
{
    public Gradient CorAmbiente;
    public Gradient CorDirecional;
    public Gradient CorFog;

    
    
}
