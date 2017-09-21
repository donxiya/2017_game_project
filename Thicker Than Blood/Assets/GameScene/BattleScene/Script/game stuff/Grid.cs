using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid {

    
    public int x { get; set; }
    public int z { get; set; }
    public bool occupied;
    public Person troop { get; set; }
    public float mark { get; set; }
    public Queue<Grid> path { get; set; }
    public List<Grid> neighbors { get; set; }
    public GameObject mapSettingModel { get; set; }
    public GridType gridType { get; set; }
    public float hideRate { get; set; }
    public float blockRate { get; set; }
    public float staminaCost { get; set; }
    public float playerTempStaminaCost { get; set; }
    public float enemyTempStaminaCost { get; set; }

    public Grid (int x, int z, GameObject model, GridType gridType)
    {
        this.x = x;
        this.z = z;
        this.mapSettingModel = model;
        this.gridType = gridType;
        occupied = false;
        mark = 0;
        initialization();
    }
    void initialization ()
    {
        switch (gridType)
        {
            case GridType.rockAndTree:
                hideRate = .8f;
                blockRate = .7f;
                staminaCost = 5f;
                break;
            case GridType.flatGrass:
                hideRate = .3f;
                blockRate = .1f;
                staminaCost = 1f;
                break;
            case GridType.tree:
                hideRate = .5f;
                blockRate = .3f;
                staminaCost = 2f;
                break;
        }
        playerTempStaminaCost = 0;
        enemyTempStaminaCost = 0;
        neighbors = new List<Grid>();
        path = new Queue<Grid>();
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
}
