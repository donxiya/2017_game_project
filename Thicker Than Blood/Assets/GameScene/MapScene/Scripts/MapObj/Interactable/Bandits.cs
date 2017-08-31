using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bandits : NPC {
    public GameObject spawnPoint;
    const int roamRange = 60;
    // Use this for initialization
    public override void Start () {
        base.Start();
        dialogue = new string[] { "hello", "i will kill u" };
        name = "Bandits";
        npcParty = new Party(name, Faction.bandits, 300);
        npcParty.prestige = 0;
        npcParty.notoriety = 80;
        makeParty();
        roam();
    }

    // Update is called once per frame
    public override void Update () {
        base.Update();
		if (!TimeSystem.pause)
        {
            npcAgent = transform.GetComponent<NavMeshAgent>();
            npcAgent.isStopped = false;
            if (Vector3.Distance(npcAgent.destination,transform.position) <= 10)
            {
                roam();
            }
        } else
        {
            npcAgent.isStopped = true;
        }
        inspectPanel.GetComponent<InspectPanel>().updateTexts(npcParty);
	}
    
    public override void roam()
    {
        if (spawnPoint != null)
        {
            if (Vector3.Distance(transform.position, spawnPoint.transform.position) >= roamRange)
            {
                npcAgent.destination = spawnPoint.transform.position;
            }
            else
            {
                Vector3 randomV = new Vector3(Random.Range(-roamRange, roamRange), 0, Random.Range(-roamRange, roamRange));
                npcAgent.destination = spawnPoint.transform.position + randomV;
            }

        }
        else
        {
            Vector3 randomV = new Vector3(Random.Range(-roamRange, roamRange), 0, Random.Range(-roamRange, roamRange));
            npcAgent.destination = transform.position + randomV;
        }
        
    }
    public void setSpawnPoint(GameObject sp)
    {
        spawnPoint = sp;
    }
    public override void grow()
    {

    }
    public void makeParty()
    {
        TroopType tt = npcParty.randomTroopType(20, 20, 10, 30, 10, 10);
        Ranking rk = npcParty.randomRanking(0, 10, 10, 10);
        if (tt == TroopType.recruitType)
        {
            rk = Ranking.recruit;
        }
        Person p = npcParty.makeGenericPerson(tt, rk);
        if (npcParty.battleValue >= p.battleValue)
        {
            if (npcParty.addToParty(npcParty.makeGenericPerson(tt, rk)))
            {
                npcParty.battleValue -= p.battleValue;
                npcParty.curBattleValue += p.battleValue;
            }
            if (npcParty.battleValue > 20)
            {
                makeParty();
            }
        } else
        {
            if (npcParty.battleValue > 20)
            {
                makeParty();
            }
        }
        
    }
}
