using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        exp.sparedPoint += 50; //REMEMBER TO CHANGE THIS TO LEVEL
    }
}

public class SkillTree
{
    Dictionary<string, Perk> skillTreeDict;
    public SkillTree()
    {
        skillTreeInitialization();
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

        skillTreeDict.Add("C6A", new Perk("C6A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C6B", new Perk("C6B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C6C", new Perk("C6C", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C6D", new Perk("C6D", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C7A", new Perk("C7A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C7B", new Perk("C7B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C7C", new Perk("C7C", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C7D", new Perk("C7D", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C8A", new Perk("C8A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C8B", new Perk("C8B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C8C", new Perk("C8C", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C8D", new Perk("C8D", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C9A", new Perk("C9A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C9B", new Perk("C9B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C9C", new Perk("C9C", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C9D", new Perk("C9D", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C10A", new Perk("C10A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C10B", new Perk("C10B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));

        skillTreeDict.Add("I6A", new Perk("I6A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I6B", new Perk("I6B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I6C", new Perk("I6C", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I6D", new Perk("I6D", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I7A", new Perk("I7A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I7B", new Perk("I7B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I7C", new Perk("I7C", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I7D", new Perk("I7D", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I8A", new Perk("I8A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I8B", new Perk("I8B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I8C", new Perk("I8C", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I8D", new Perk("I8D", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I9A", new Perk("I9A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I9B", new Perk("I9B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I9C", new Perk("I9C", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I9D", new Perk("I9D", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I10A", new Perk("I10A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I10B", new Perk("I10B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
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
    public Button button { get; set; }
    public Perk(string skillPointIDI, bool ownI, string skillNameI, string descriptionI, string quoteI)
    {
        skillPointID = skillPointIDI;
        own = ownI;
        skillName = skillNameI;
        description = descriptionI;
        quote = quoteI;
    }
}
