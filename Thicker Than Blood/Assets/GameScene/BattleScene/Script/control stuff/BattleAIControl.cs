using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAIControl : MonoBehaviour {

    public static bool inAction = false;

    public bool enemyPlaced, enemyFinished, doIt;
    public Troop curControlled;
    public Vector2 mapSize;
    public int memberIndex, frontLine, midLine, backLine;
    public AIAttackMode attackMode;
    float timer;
    bool tick = false;
	// Use this for initialization
	void Start () {
        enemyPlaced = false;
        frontLine = 0;
        midLine = 0;
        backLine = 0;
        memberIndex = 0;
        enemyFinished = false;
        doIt = false;
        attackMode = AIAttackMode.neutral;
	}
	
	// Update is called once per frame
	void Update () {
		if (!enemyPlaced && BattleCentralControl.battleStart)
        {
            mapSize = new Vector2(BattleCentralControl.gridXMax, BattleCentralControl.gridZMax);
            placeEnemyOnMap();
            enemyPlaced = true;
            decideAttackMode();
        }
        if (!BattleCentralControl.playerTurn)
        {
            if (enemyFinished)
            {
                BattleCentralControl.endTurnPrep();
                BattleCentralControl.playerTurn = true;
                enemyFinished = false;
            } else
            {
                aiControl();
            }
            
        }
        timer += Time.deltaTime;
        if (timer >= 0.5f)
        {
            tick = true;
            timer -= 0.5f;
        } else
        {
            tick = false;
        }
        if (doIt && curControlled != null)
        {
            if (tick) { //curControlled.indicatedGrid().Contains(curControlled.getCurrentGrid())) {
                curControlled.doSkill(curControlled.indicatedGrid(), TroopSkill.fire);
                curControlled.hideIndicators();
                doIt = false;
            }
            
        }
	}
    void placeEnemyOnMap()
    {
        int randX = (int)Random.Range(0, BattleCentralControl.gridXMax);
        int memberInBattle = 0;
        foreach (Person unit in BattleCentralControl.enemyParty.partyMember)
        {
            if (memberInBattle <= BattleCentralControl.enemyParty.leader.getTroopMaxNum())
            {
                List<Grid> placedGrids = new List<Grid>();
                int posZ, posX;
                int zRange = BattleCentralControl.enemyParty.leader.getTroopPlacingRange(BattleCentralControl.gridZMax);
                posZ = BattleCentralControl.gridZMax;
                switch (unit.troopType)
                {
                    case TroopType.crossbowman:
                        posZ = (int)(BattleCentralControl.gridZMax - 1 - zRange / 3);
                        backLine = posZ;
                        break;
                    case TroopType.musketeer:
                        posZ = (int)(BattleCentralControl.gridZMax - 1 - zRange / 3);
                        backLine = posZ;
                        break;
                    case TroopType.swordsman:
                        posZ = (int)(BattleCentralControl.gridZMax - 1 - zRange * 2 / 3);
                        midLine = posZ;
                        break;
                    case TroopType.halberdier:
                        posZ = (int)(BattleCentralControl.gridZMax - 1 - zRange);
                        frontLine = posZ;
                        break;
                    case TroopType.cavalry:
                        posZ = (int)(BattleCentralControl.gridZMax - 1 - zRange);
                        frontLine = posZ;
                        break;
                    case TroopType.recruitType:
                        posZ = (int)(BattleCentralControl.gridZMax - 1 - zRange * 2 / 3);
                        frontLine = posZ;
                        break;
                }
                posX = randX;
                while (BattleCentralControl.map[posX, posZ].personOnGrid != null)
                {
                    if (posX >= BattleCentralControl.gridXMax - 1)
                    {
                        posX = 0;
                    }
                    posX += 1;
                }

                var pos = new Vector3(posX, 1, posZ);
                var rot = new Quaternion(0, 0, 0, 0);
                GameObject unitToPlace = TroopDataBase.troopDataBase.getTroopObject(unit.faction, unit.troopType, unit.ranking);
                GameObject gridToPlace = BattleCentralControl.gridToObj[BattleCentralControl.map[posX, posZ]];
                unitToPlace = gridToPlace.GetComponent<GridObject>().placeTroopOnGrid(unitToPlace, pos, rot);
                unitToPlace.GetComponent<Troop>().placed(unit, BattleCentralControl.map[posX, posZ]);
                memberInBattle += 1;
            }
        }
    }
    void aiControl()
    {
        if (!inAction && !doIt)
        {
            if (BattleCentralControl.enemyParty.partyMember[memberIndex].troop != null)
            {
                if (curControlled != null)
                {
                    curControlled.aiControlled = false;
                }
                curControlled = BattleCentralControl.enemyParty.partyMember[memberIndex].troop;
                curControlled.aiControlled = true;
                if (memberIndex < BattleCentralControl.enemyParty.partyMember.Count)
                {
                    memberIndex++;
                }
                else
                {
                    memberIndex = 0;
                    enemyFinished = true;
                    return;
                }
                if (curControlled != null)
                {
                    controlSingleTroop(curControlled);
                }
            }else
            {
                memberIndex = 0;
                enemyFinished = true;
                return;
            }

        }
        
        
    }
    void controlSingleTroop(Troop troop)
    {
        if (getSeen().Count > 0)
        {
            noPlayerTroopInSight(troop);
        } else
        {
            noPlayerTroopInSight(troop);
        }
    }

    void noPlayerTroopInSight(Troop troop)
    {
        fireAttack(troop, BattleCentralControl.playerParty.leader.troop);
        if (troop.person.troopType == TroopType.halberdier || troop.person.troopType == TroopType.cavalry)
        {
            //blindForward(troop, 3);
            //fireAttack(troop, BattleCentralControl.playerParty.leader.troop);
        }
        else if (troop.person.troopType == TroopType.recruitType || troop.person.troopType == TroopType.swordsman)
        {
            //blindForward(troop, 3);
            //fireAttack(troop, BattleCentralControl.playerParty.leader.troop);
        }
        else
        {
            //blindForward(troop, 3);
            //fireAttack(troop, BattleCentralControl.playerParty.leader.troop);
        }
    }
    void playerTroopInSight(Troop troop)
    {
        if (troop.person.troopType == TroopType.halberdier || troop.person.troopType == TroopType.cavalry)
        {
            blindForward(troop, 10);
        }
        else if (troop.person.troopType == TroopType.recruitType || troop.person.troopType == TroopType.swordsman)
        {
            blindForward(troop, 10);
        }
        else
        {
            blindForward(troop, 10);
        }
    }

    void blindForward(Troop troop, int distance)
    {
        //troop.curGrid.x, troop.curGrid.z - 10
        troop.troopMoveToPlace(BattleCentralControl.map[troop.getCurrentGrid().x, troop.getCurrentGrid().z - distance]);

        //BattleCentralControl.gridToObj[BattleCentralControl.map[1,1]].GetComponent<GridObject>().moveTroopToGrid(troop.gameObject);
    }

    void lungeAttack(Troop troop, Troop attacked)
    {
        if (!troop.lungeIndicator.activeSelf)
        {
            troop.lungeIndicator.SetActive(true);
        }
        Vector3 v = attacked.gameObject.transform.position - troop.gameObject.transform.position;
        v.x = v.z = 0.0f;
        troop.gameObject.transform.LookAt(attacked.gameObject.transform.position - v);
        troop.gameObject.transform.Rotate(0, 180, 0);
    }

    void fireAttack(Troop troop, Troop attacked)
    {
        while (!troop.fireIndicator.activeSelf)
        {
            troop.fireIndicator.SetActive(true);
        }
        troop.curIndicator = troop.fireIndicator;
        lookAtObject(troop.gameObject, attacked.gameObject);
        doIt = true;
        //Debug.Log(troop.fireIndicator.GetComponent<Indicator>().collided.Count);
        //troop.doSkill(troop.indicatedGrid(), TroopSkill.fire);
        //StartCoroutine(Wait(1000000000000000000000000000000000000.0f));
        //troop.fireIndicator.SetActive(false);
    }

    void lookAtObject(GameObject looker, GameObject obj)
    {
        Vector3 v = looker.transform.position - obj.transform.position;
        v.x = v.z = 0.0f;
        looker.gameObject.transform.LookAt(obj.transform.position - v);
        looker.gameObject.transform.Rotate(0, 180, 0);
    }

    List<Troop> getSeen()
    {
        List<Troop> result = new List<Troop>();
        foreach(Person unit in BattleCentralControl.playerParty.partyMember)
        {
            if (unit.troop != null && unit.troop.seenStatus && !result.Contains(unit.troop))
            {
                result.Add(unit.troop);
            }
        }
        return result;
    }

    void decideAttackMode()
    {
        int cautiousChance = 10;
        int aggressiveChance = 10;
        int neutralChance = 20;
        if (BattleCentralControl.enemyParty.leader.troopType == TroopType.musketeer
            && BattleCentralControl.enemyParty.leader.troopType == TroopType.crossbowman)
        {
            cautiousChance += 10;
            aggressiveChance -= 5;
            neutralChance -= 5;
        }
        else if (BattleCentralControl.enemyParty.leader.troopType == TroopType.swordsman
          && BattleCentralControl.enemyParty.leader.troopType == TroopType.cavalry)
        {
            cautiousChance -= 5;
            aggressiveChance += 10;
            neutralChance -= 5;
        }
        else
        {
            cautiousChance -= 5;
            aggressiveChance -= 5;
            neutralChance += 10;
        }
        int rand = Random.Range(1, cautiousChance + aggressiveChance + neutralChance);
        if (rand <= cautiousChance)
        {
            attackMode = AIAttackMode.cautious;
        }
        else if (rand > cautiousChance && rand <= aggressiveChance)
        {
            attackMode = AIAttackMode.aggressive;
        }
        else
        {
            attackMode = AIAttackMode.neutral;
        }
    }
    private IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}


public enum AIAttackMode
{
    aggressive,
    cautious,
    neutral
}