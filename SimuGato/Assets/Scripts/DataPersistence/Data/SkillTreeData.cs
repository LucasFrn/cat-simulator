using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillTreeData
{
    public int exp;
    public int level;
    public int pontosSkill;
    public bool[] skillsCompradas;
    //Existe essa variavel na skillTree, então é só copiar ela dele quando for salvas,
    //mas pra cada nova skill que for criada precisa mudar manualmente la, e aqui

    public SkillTreeData(){
        exp = 0;
        level = 0;
        pontosSkill = 0;
        skillsCompradas = new bool[5];
    }
    public SkillTreeData(int exp,int level,int pontosSkill,bool[]skillsCompradas){
        this.exp = exp;
        this.level = level;
        this.pontosSkill = pontosSkill;
        this.skillsCompradas = new bool[5];
        for(int i=0;i<skillsCompradas.Length;i++)
            this.skillsCompradas[i] = skillsCompradas[i];
    }

}
