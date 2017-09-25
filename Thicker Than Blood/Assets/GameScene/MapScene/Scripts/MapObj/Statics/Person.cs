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
    public float staminaMax;
    public float health { get; set; }
    public float healthMax;

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
        
        initialization(nameI, statsI, rk, tt, factionI, expI);
    }

    //ini gear
    public virtual void initialization(string nameI, Stats statsI, Ranking rk, TroopType tt, Faction factionI, Experience expI)
    {
        name = nameI;
        stats = statsI;
        ranking = rk;
        troopType = tt;
        faction = factionI;
        exp = expI;
        battleValue = getBattleValue();
        inBattle = false;
        stamina = getStaminaMax();
        health = getHealthMax();
    }
    public virtual float getMeleeAttackDmg()
    {
        return stats.strength * 10;
    }
    public virtual float getRangedAttackDmg()
    {
        return stats.strength * 5 + stats.perception * 5;
    }
    public virtual float getStaminaMax()
    {
        return stats.agility * 10;
    }
    public virtual float getHealthMax()
    {
        return stats.endurance * 100;
    }
    public virtual float getGuardedIncrease()
    {
        return stats.endurance;
    }
    public virtual int getTroopPlacingRange(int maxRange)
    {
        return 2 + (int) 2 * stats.intelligence * maxRange / 100;
    }
    public virtual int getTroopMaxNum()
    {
        return 2 + (int)2 * stats.charisma;
    }
    public int getBattleValue()
    {
        int result = TroopDataBase.getBattleValue(TroopDataBase.factionToString(faction) 
            + TroopDataBase.rankingToString(ranking) 
            + TroopDataBase.troopTypeToString(troopType));
        //Debug.Log(result);
        if (result == 0)
        {
            Debug.Log("troop value zero nad its name is: " + name);
        }
        return result;
    }




}


