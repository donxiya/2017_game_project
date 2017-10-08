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

    public float vision;
    public float stealth;
    public float accuracy;
    public float evasion;
    public float block;
    //gear
    public GearInfo gearInfo;

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
        gearInfo = TroopDataBase.troopDataBase.getGearInfo(faction, troopType, ranking);
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


    
    public virtual float getStaminaMax()
    {
        return stats.agility * 10 * ((gearInfo.evasionRating + 10) / 10);
    }
    public virtual float getHealthMax()
    {
        return stats.endurance * 100 * ((gearInfo.armorRating + 10) / 10);
    }
    public virtual float getGuardedIncrease()
    {
        return stats.endurance * ((gearInfo.armorRating + 10) / 10);
    }
    public virtual float getArmor()
    {
        return stats.endurance * ((gearInfo.armorRating + 10) / 10);
    }
    public virtual float getBlock()
    {
        return ((stats.perception + stats.strength) / 2) * ((gearInfo.armorRating + 10) / 10);
    }
    public virtual float getEvasion()
    {
        return ((stats.perception + stats.strength) / 2) * ((gearInfo.armorRating + 10) / 10);
    }
    public virtual float getVision()
    {
        return stats.perception * ((gearInfo.visionRating + 10) / 10);
    }
    public virtual float getStealth()
    {
        return stats.agility * ((gearInfo.stealthRating + 10) / 10);
    }
    public virtual float getAccuracy()
    {
        return stats.perception * ((gearInfo.accuracyRating + 10) / 10);
    }
    public virtual float getMeleeAttackDmg()
    {
        return stats.strength * ((gearInfo.dmgRating + 10) / 10);
    }
    public virtual float getRangedAttackDmg()
    {
        return (stats.strength + stats.perception) * 5 * ((gearInfo.dmgRating + 10) / 10);
    }
    public virtual float getMobility()
    {
        return (stats.agility) * ((gearInfo.mobilityRating + 10) / 10);
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
        int result = TroopDataBase.troopDataBase.getTroopInfo(faction, troopType, ranking).battleValue;
        //Debug.Log(result);
        if (result == 0)
        {
            Debug.Log("troop value zero nad its name is: " + name);
        }
        return result;
    }




}


