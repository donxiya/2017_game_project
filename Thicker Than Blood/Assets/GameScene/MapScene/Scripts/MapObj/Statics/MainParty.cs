using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainParty : Party {
    List<Perk> PerkList;
    public MainParty(Person leaderI, string nameI, Faction factionI, int battleValueI)
    {
        unique = true;
        leader = leaderI;
        name = nameI;
        faction = factionI;
        partyMember = new List<Person>();
        addToParty(leader);
        battleValue = battleValueI;
    }
    public override void PartyInitialization()
    {
        prestige = 0;
        notoriety = 0;
        travelSpeed = getTravelSpeed();
        partySizeLimit = getPartySizeLimit();
    }
    public override void plusPrestige(int toAdd)
    {
        base.plusPrestige(toAdd);
    }
    public override int getPrestige()
    {
        return base.getPrestige();
    }
    public override void plusNotoriety(int toAdd)
    {
        base.plusNotoriety(toAdd);
    }
    public override int getPartySizeLimit()
    {
        return base.getPartySizeLimit();
    }
    public override float getTravelSpeed()
    {
        return base.getTravelSpeed() + 4.0f;
    }
    public override float getVisionRange()
    {
        return base.getVisionRange();
    }
    public override float getTaticRating()
    {
        return base.getTaticRating();
    }
    public override float getConvinceRating()
    {
        return base.getConvinceRating();
    }
    public override float getInventoryWeightLimit()
    {
        return base.getInventoryWeightLimit();
    }
    public void initializePerks()
    {
        //PerkList.Add(new Perk());
    }
    
}

