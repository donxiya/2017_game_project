using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : Person {

    public SkillTree skillTree;
    public MainCharacter(string nameI, Stats statsI, Ranking rk, TroopType tt, Faction factionI, Experience expI)
    {
        initialization(nameI, statsI, rk, tt, factionI, expI);
        skillTree = new SkillTree();

    }
    public override void initialization(string nameI, Stats statsI, Ranking rk, TroopType tt, Faction factionI, Experience expI)
    {
        base.initialization(nameI, statsI, rk, tt, factionI, expI);
    }
    public override void resetPerk()
    {
        base.resetPerk();
        skillTree.skillTreeInitialization();
        exp.sparedPoint += 50;
    }
}

public class SkillTree
{
    Dictionary<string, Perk> skillTreeDict;
    public SkillTree()
    {
    }

    public void skillTreeInitialization()
    {
        skillTreeDict = new Dictionary<string, Perk>();
        skillTreeDict.Add("S6A", new Perk("S6A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("S6B", new Perk("S6B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("S7A", new Perk("S7A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("S7B", new Perk("S7B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("S8A", new Perk("S8A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("S8B", new Perk("S8B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("S9A", new Perk("S9A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("S9B", new Perk("S9B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("S10A", new Perk("S10A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        
        skillTreeDict.Add("A6A", new Perk("A6A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("A6B", new Perk("A6B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("A7A", new Perk("A7A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("A7B", new Perk("A7B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("A8A", new Perk("A8A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("A8B", new Perk("A8B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("A9A", new Perk("A9A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("A9B", new Perk("A9B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("A10A", new Perk("A10A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));

        skillTreeDict.Add("P6A", new Perk("P6A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("P6B", new Perk("P6B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("P7A", new Perk("P7A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("P7B", new Perk("P7B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("P8A", new Perk("P8A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("P8B", new Perk("P8B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("P9A", new Perk("P9A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("P9B", new Perk("P9B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("P10A", new Perk("P10A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));

        skillTreeDict.Add("E6A", new Perk("E6A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("E6B", new Perk("E6B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("E7A", new Perk("E7A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("E7B", new Perk("E7B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("E8A", new Perk("E8A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("E8B", new Perk("E8B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("E9A", new Perk("E9A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("E9B", new Perk("E9B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("E10A", new Perk("E10A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        
    }
    public Perk getPerk(string ID)
    {
        if (skillTreeDict.ContainsKey(ID))
        {
            return skillTreeDict[ID];
        }
        return null;
    }
}

public class Perk
{
    public string skillName { get; set;}
    public string description { get; set; }
    public string quote { get; set; }
    public string skillPointID { get; set; }
    public bool own { get; set; }
    public Perk(string skillPointIDI, bool ownI, string skillNameI, string descriptionI, string quoteI)
    {
        skillPointID = skillPointIDI;
        own = ownI;
        skillName = skillNameI;
        description = descriptionI;
        quote = quoteI;
    }
}
