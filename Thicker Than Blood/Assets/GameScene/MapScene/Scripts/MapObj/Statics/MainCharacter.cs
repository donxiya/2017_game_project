using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : Person {

    public MainCharacter(string nameI, Stats statsI, Ranking rk, TroopType tt, Faction factionI, Experience expI)
    {
        name = nameI;
        stats = statsI;
        ranking = rk;
        troopType = tt;
        faction = factionI;
        exp = expI;
        inBattle = false;
    }
    
}


public class SkillPoint
{
    string skillName;
    string description;
    string quote;

    public SkillPoint(string skillNameI, string descriptionI, string quoteI)
    {
        skillName = skillNameI;
        description = descriptionI;
        quote = quoteI;
    }
}
