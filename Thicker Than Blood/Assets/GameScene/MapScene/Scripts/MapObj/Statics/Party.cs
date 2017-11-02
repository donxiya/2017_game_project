using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party {
    const float maxTravelSpeed = 8.0f;
    public string name { get; set; }
    public Faction faction { get; set; }
    public Person leader { get; set; }
    public List<Person> partyMember { get; set; }
    public List<Item> inventory { get; set; }
    public int battleValue { get; set; }
    public int morale { get; set;}
    public int cash { get; set; }

    public int prestige, notoriety;
    public int partySize, partySizeLimit;
    public float travelSpeed, visionRange;
    public float taticRating, convinceRating;
    public float inventoryWeightLimit, inventoryWeight;
    public bool unique;

    protected Party()
    {

    }
    public Party(Person leaderI, string nameI, Faction factionI, int battleValueI)
    {
        unique = true;
        leader = leaderI;
        name = nameI;
        faction = factionI;
        partyMember = new List<Person>();
        inventory = new List<Item>();
        partySize = 0;
        addToParty(leader);
        battleValue = battleValueI;
        PartyInitialization();
    }
    public Party(string nameI, Faction factionI, int battleValueI) //generic parties
    {
        unique = false;
        name = nameI;
        faction = factionI;
        leader = makeGenericPerson(randomTroopType(0, 20, 20, 20, 20, 20), randomRanking(0, 10, 10, 10));
        leader.stats.charisma = Random.Range(3,7);
        leader.stats.intelligence = Random.Range(3, 7);
        leader.exp.level += (leader.stats.intelligence + leader.stats.charisma - 2);
        partyMember = new List<Person>();
        inventory = new List<Item>();
        partySize = 0;
        addToParty(leader);
        battleValue = battleValueI;
        PartyInitialization();
    }

    public virtual void PartyInitialization()
    {
        prestige = 0;
        notoriety = 0;
        partySizeLimit = getPartySizeLimit();
        addToInventory(new Item("Supply", 5, "Pisa", "this is supply"));
    }

    public bool addToParty(Person member)
    {
        if (partySize < getPartySizeLimit())
        {
            partyMember.Add(member);
            partySize++;
            return true;
        }
        return false;
    }
    public bool removeFromParty(Person member)
    {
        if (partyMember.Remove(member))
        {
            partySize -= 1;
            return true;
        }
        return false;
    }
    public bool addToInventory(Item item)
    {
        if (inventoryWeight < inventoryWeightLimit)
        {
            inventory.Add(item);
            inventoryWeight += item.weight;
            return true;
        }
        return false;
    }
    public bool removeFromInventory(Item item)
    {
        if (inventory.Remove(item))
        {
            inventoryWeight -= item.weight;
            return true;
        }
        return false;
    }
    public virtual int getPrestige()
    {
        return prestige;
    }
    public virtual void plusPrestige(int toAdd)
    {
        prestige += toAdd;
    }
    public virtual int getNotoriety()
    {
        return notoriety;
    }
    public virtual void plusNotoriety(int toAdd)
    {
        notoriety += toAdd;
    }
    public virtual int getPartySizeLimit()
    {
        return leader.stats.charisma * 4 + 5;
    }
    public virtual float getTravelSpeed()
    {
        float travelSpeed = maxTravelSpeed * (getAverage().agility / 10.0f) - 0.1f * (partySize + .1f * getInventoryWeight());
        Mathf.Clamp(travelSpeed, 1, 10);
        return travelSpeed;
    }
    public virtual float getVisionRange()
    {
        return (leader.stats.perception + getHighest().perception) * 4.0f;
    }
    public virtual float getTaticRating()
    {
        return (leader.stats.intelligence + getHighest().intelligence) * .005f;
    }
    public virtual float getConvinceRating()
    {
        return (leader.stats.charisma + getHighest().charisma) * .005f;
    }
    public virtual float getInventoryWeightLimit()
    {
        return (getAverage().strength + getAverage().endurance) * 10.0f;
    }
    public virtual int getBattleValue()
    {
        int curBattleValue = 0;
        foreach (Person p in partyMember)
        {
            curBattleValue += TroopDataBase.troopDataBase.getBattleValue(p.faction, p.troopType, p.ranking);
        }
        return curBattleValue;
    }
    public virtual int getInventoryValue()
    {
        int result = 0;
        foreach (Item item in inventory)
        {
            result += item.value;
        }
        return result;
    }
    public virtual float getInventoryWeight()
    {
        float result = 5;
        /**if (inventory.Count > 0)
        {
            foreach (Item item in inventory)
            {
                result += item.weight;
            }
        }**/
        return result;
    }
    public int getAverageLevel()
    {
        int result = 0;
        foreach (Person p in partyMember)
        {
            result += p.exp.level;
        }
        return result / partySize;

    }
    public Stats getAverage()
    {
        Stats result = new Stats(0, 0, 0, 0, 0, 0);
        foreach( Person p in partyMember)
        {
            result.strength += p.stats.strength;
            result.agility += p.stats.agility;
            result.perception += p.stats.perception;
            result.endurance += p.stats.endurance;
            result.charisma += p.stats.charisma;
            result.intelligence += p.stats.intelligence;
        }
        result.strength = result.strength / partySize;
        result.agility = result.agility / partySize;
        result.perception = result.perception / partySize;
        result.endurance = result.endurance / partySize;
        result.charisma = result.charisma / partySize;
        result.intelligence = result.intelligence / partySize;
        return result;
    }
    public Stats getHighest()
    {
        Stats result = leader.stats;
        foreach (Person p in partyMember)
        {
            if (p.stats.strength > result.strength)
            {
                result.strength = p.stats.strength;
            }
            if (p.stats.agility > result.agility)
            {
                result.agility = p.stats.agility;
            }
            if (p.stats.perception > result.perception)
            {
                result.perception = p.stats.perception;
            }
            if (p.stats.endurance > result.endurance)
            {
                result.agility = p.stats.agility;
            }
            if (p.stats.charisma > result.charisma)
            {
                result.charisma = p.stats.charisma;
            }
            if (p.stats.intelligence > result.intelligence)
            {
                result.intelligence = p.stats.intelligence;
            }
        }
        return result;
    }
    public Person makeGenericPerson(TroopType tt, Ranking rk)
    {
        string memberName = TroopDataBase.rankingToString(rk) + " " + TroopDataBase.troopTypeToString(tt);
        Stats gStats = new Stats(1, 1, 1, 1, 1, 1);
        int s = 1;
        int a = 1;
        int p = 1;
        int e = 1;
        if (rk == Ranking.recruit)
        {
            s = Random.Range(1, 2);
            a = Random.Range(1, 2);
            p = Random.Range(1, 2);
            e = Random.Range(1, 2);
            
        } else if (rk == Ranking.militia)
        {
            s = Random.Range(3, 5);
            a = Random.Range(3, 5);
            p = Random.Range(3, 5);
            e = Random.Range(3, 5);
        } else if (rk == Ranking.veteran)
        {
            s = Random.Range(5, 7);
            a = Random.Range(5, 7);
            p = Random.Range(5, 7);
            e = Random.Range(5, 7);
        } else if (rk == Ranking.elite)
        {
            s = Random.Range(7, 8);
            a = Random.Range(7, 8);
            p = Random.Range(7, 8);
            e = Random.Range(7, 8);
        }
        gStats = new Stats(s, a, p, e, 0, 0);
        int level = gStats.strength + gStats.agility + gStats.perception + gStats.endurance - 4;
        Experience gExp = new Experience(0, level, 0);
        Ranking gRk = rk;
        TroopType gTt = tt;
        Faction gF = faction;
        Person per = new Person(memberName, gStats, gRk, gTt, gF, gExp);
        per.name = TroopDataBase.rankingToString(gRk) + " " + TroopDataBase.troopTypeToString(gTt);
        return per;
    }
    public TroopType randomTroopType(int recruitC, int crossC, int musketC, int swordC, int halbC, int cavC)
    {
        int r = Random.Range(1, recruitC + crossC + musketC + swordC + halbC + cavC);
        int curC = 0;
        if (r <= recruitC)
        {
            return TroopType.recruitType;
        }
        curC += recruitC;
        if (curC < r && r <= curC + crossC)
        {
            return TroopType.crossbowman;
        }
        curC += crossC;
        if (curC < r && r <= curC + musketC)
        {
            return TroopType.musketeer;
        }
        curC += musketC;
        if (curC < r && r <= curC + swordC)
        {
            return TroopType.swordsman;
        }
        curC += swordC;
        if (curC < r && r <= curC + halbC)
        {
            return TroopType.halberdier;
        }
        curC += halbC;
        if (curC < r && r <= curC + cavC)
        {
            return TroopType.cavalry;
        }
        return TroopType.recruitType;
    }
    public Ranking randomRanking(int recruitC, int militiaC, int veteranC, int eliteC)
    {
        int curC = 0;
        int r = Random.Range(1, recruitC + militiaC + veteranC + eliteC);
        if (r <= recruitC)
        {
            return Ranking.recruit;
        }
        curC += recruitC;
        if (curC < r && r <= curC + militiaC)
        {
            
            return Ranking.militia;
        }
        curC += militiaC;
        if (curC < r && r <= curC + veteranC)
        {
            return Ranking.veteran;
        }
        curC += veteranC;
        if (curC < r && r <= curC + eliteC)
        {
            return Ranking.elite;
        }
        return Ranking.recruit;
    }
    
}



