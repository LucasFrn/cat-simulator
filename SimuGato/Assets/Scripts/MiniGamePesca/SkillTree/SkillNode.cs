using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="SkillNode",menuName = "SkillPesca")]
public class SkillNode : ScriptableObject
{
    public SkillNode filhoEsq;
    public SkillNode filhoDir;
    public int cod;
    public int custoExp;
    public string Descricao;
}
