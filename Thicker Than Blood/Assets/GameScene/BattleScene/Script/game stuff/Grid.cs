using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid {

    public string name;
    public int x { get; set; }
    public int z { get; set; }
    public bool occupied;
    public Person personOnGrid { get; set; }
    public GameObject mapSettingModel { get; set; }
    public GridType gridType { get; set; }
    public float hideRate { get; set; }
    public float blockRate { get; set; }
    public float staminaCost { get; set; }
    public float playerTempStaminaCost { get; set; }
    public float enemyTempStaminaCost { get; set; }
    public GameObject gridObject { get; set; }
    public List<Person> guardingPersons { get; set; }
    public Grid (int x, int z, GameObject model, GridType gridType)
    {
        this.x = x;
        this.z = z;
        this.mapSettingModel = model;
        this.gridType = gridType;
        occupied = false;
        playerTempStaminaCost = 0;
        enemyTempStaminaCost = 0;
        initialization();
    }
    void initialization ()
    {
        switch (gridType)
        {
            //ADD NEW GRID
            case GridType.rockAndTree:
                name = "Rock And Tree";
                hideRate = .8f;
                blockRate = .7f;
                staminaCost = 5f;
                break;
            case GridType.flatGrass:
                name = "Flat Grass";
                hideRate = .3f;
                blockRate = .1f;
                staminaCost = 1f;
                break;
            case GridType.deadTree:
                name = "Dead Tree";
                hideRate = .3f;
                blockRate = .3f;
                staminaCost = 2f;
                break;
            case GridType.singleTree:
                name = "Single Tree";
                hideRate = .5f;
                blockRate = .3f;
                staminaCost = 2f;
                break;
            case GridType.rockyPlain:
                name = "Rocky Plain";
                hideRate = .2f;
                blockRate = .2f;
                staminaCost = 1f;
                break;
            case GridType.fence:
                name = "Fence";
                hideRate = .4f;
                blockRate = .4f;
                staminaCost = 3f;
                break;
            default:
                name = "FlatGrass";
                hideRate = .5f;
                blockRate = .3f;
                staminaCost = 2f;
                break;
        }
        playerTempStaminaCost = 0;
        enemyTempStaminaCost = 0;
        guardingPersons = new List<Person>();
    }

    public void guarded(Person person)
    {
        if (person.faction == Faction.mercenary)
        {
            enemyTempStaminaCost += person.getGuardedIncrease();
        } else
        {
            playerTempStaminaCost += person.getGuardedIncrease();
        }
        guardingPersons.Add(person);
    }
    public void unguarded(Person person)
    {
        if (person.faction == Faction.mercenary)
        {
            enemyTempStaminaCost -= person.getGuardedIncrease();
        }
        else
        {
            playerTempStaminaCost -= person.getGuardedIncrease();
        }
        guardingPersons.Remove(person);
    }
    public float getStaminaCost(Faction faction)
    {
        if (faction == Faction.mercenary)
        {
            return staminaCost + playerTempStaminaCost;
        } else
        {
            return staminaCost + enemyTempStaminaCost;
        }
        
    }
    public void checkPersonStealth(Troop watcher)
    {
        if (personOnGrid != null)
        {
            personOnGrid.troop.stealthCheck(watcher);
        }
    }
}
