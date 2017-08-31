using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person {


    public string name { get; set; }
    public Stats stats { get; set; }
    public Ranking ranking { get; set; }
    public TroopType troopType { get; set; }
    public Faction faction { get; set; }
    public Experience exp { get; set; }
    public bool inBattle;

    //battleStats
    public int battleValue;
    public float attackDmg;
    public float stamina { get; set; }
    public float staminaMax { get; set; }
    public float health;
    public float maxHealth;

    public float visionRate;
    public float hideRate;
    public float aimRate;
    public float dodgeRate;
    //gear
    public float gearVision;
    public float gearHide;
    public float gearAttack;
    public float gearProtect;

    protected Person()
    {

    }

    public Person(string nameI, Stats statsI, Ranking rk, TroopType tt, Faction factionI, Experience expI)
    {
        name = nameI;
        stats = statsI;
        ranking = rk;
        troopType = tt;
        faction = factionI;
        exp = expI;
        initialization();
        attackDmg = 10 * stats.strength * gearAttack;
        battleValue = getBattleValue();
        inBattle = false;
    }

    //ini gear
    public void initialization()
    {

    }
    public virtual float getAttackDmg()
    {
        return stats.strength * 10;
    }
    public virtual float getStaminaMax()
    {
        return stats.agility * 10;
    }
    public virtual float getHealthMax()
    {
        return stats.endurance * 100;
    }
    public int getBattleValue()
    {
        int result = DataBase.getBattleValue(DataBase.factionToString(faction) 
            + DataBase.rankingToString(ranking) 
            + DataBase.troopTypeToString(troopType));
        //Debug.Log(result);
        if (result == 0)
        {
            Debug.Log("troop value zero nad its name is: " + name);
        }
        return result;
    }




}

public class Item
{
    public string name;
    public int value;
    public float weight;
    public string description;
    public string city;
    public Item(string nameI, int valueI, string cityI, string descriptionI)
    {
        name = nameI;
        value = valueI;
        city = cityI;
        description = descriptionI;
    }
}
