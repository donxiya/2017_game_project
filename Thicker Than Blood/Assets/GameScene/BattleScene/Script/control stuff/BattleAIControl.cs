using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAIControl : MonoBehaviour {

    public static bool inAction = false;

    public bool enemyPlaced;
    public static bool enemyFinished;
    public Troop curControlled;
    public Vector2 mapSize, troopCenter;
    public int memberIndex, frontLine, midLine, backLine;
    public AIAttackMode attackMode;
    float timer;
    bool waited = false;
    bool tick = false;
    bool turnInitialized = false;
    bool scouted = false;
    Queue<AIAction> actionQueue = new Queue<AIAction>();
    List<Person> halberdiers, swordsmen, cavalries, crossbowmen, musketeer;
    int halberdierIndex, swordsmenIndex, cavalriesIndex, crossbowmenIndex, musketeerIndex;
    public static AIAction curAIAction;
	// Use this for initialization
	void Start () {
        enemyPlaced = false;
        frontLine = 0;
        midLine = 0;
        backLine = 0;
        memberIndex = 0;
        enemyFinished = false;
        attackMode = AIAttackMode.neutral;
        halberdiers = new List<Person>();
        swordsmen = new List<Person>();
        cavalries = new List<Person>();
        crossbowmen = new List<Person>();
        musketeer = new List<Person>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!enemyPlaced && BattleCentralControl.battleStart)
        {
            mapSize = new Vector2(BattleCentralControl.gridXMax, BattleCentralControl.gridZMax);
            placeEnemyOnMap();
            enemyPlaced = true;
            decideAttackMode();
        } //occur on battle start

        //if (actionQueue.Count != 0)
        //{
        //    Debug.Log(actionQueue.Count + " " + inAction);
        //}
        //if (curAIAction != null)
        //{
        //    Debug.Log(curAIAction.troop.name + " " + curAIAction.skillMode + " " + inAction);
        //}
        //if (inAction)
        //{
        //    Debug.Log("cord: " + curAIAction.troop.transform.position.x + " " + curAIAction.troop.transform.position.z);
        //    Debug.Log("cord grid: " + curAIAction.troop.getCurrentGrid().x + " " + curAIAction.troop.getCurrentGrid().z);
        //}
        

        if (!BattleCentralControl.playerTurn)
        {
            if (enemyFinished)
            {
                BattleCentralControl.endTurnPrep();
                enemyFinished = false;
                curAIAction = null;
                turnInitialized = false;
                scouted = false;
                BattleCentralControl.playerTurn = true;
                return;
            } else
            {
                if (!turnInitialized)
                {
                    categorizeTroop();
                    actionQueue.Clear();
                    turnInitialized = true;
                }
                aiControl();
                doAction();
            }
            
        } //occur on player finish their moves
        

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
    void doAction()
    {
        if (actionQueue.Count != 0 && !inAction)
        {

            if (curAIAction == null) //read first action
            {
                curAIAction = actionQueue.Dequeue();
            }
            else if (curAIAction != null && curAIAction.finished) //read next action only if finished the last one
            {
                curAIAction = actionQueue.Dequeue();
            }

        }
        if (!inAction && curAIAction != null && !curAIAction.finished) //if we have an action to do
        {
            if (curAIAction.skillMode == TroopSkill.none)
            {
                enemyFinished = true;
                actionQueue.Clear();
            }
            curAIAction.doAIAction(); //do the first half

            Debug.Log("here");
            if (!waited) //reset clock
            {
                timer = 0;
                tick = false;
                waited = true;
            }
            // wait for a bit
            clockTick();
            if (tick)
            {

                curAIAction.finishAIAction();
                waited = false;
                curAIAction.finished = true;

            }
            Debug.Log("here finish");

        }
    }
    void aiControl()
    {
        //Debug.Log("aic");
        if (!scouted)
        {
           // Debug.Log("aic1");
            scout();
            scouted = true;
        }
        


    }

    void scout()
    {
        int xOffset = (int)mapSize.x / (cavalries.Count + swordsmen.Count + 1);
        int index = 0;
        foreach (Person unit in cavalries)
        {
            int posX = (int) Mathf.Clamp(xOffset * index, 0, mapSize.x);
            index++;
            if (unit.troop != null)
            {
                
                actionQueue.Enqueue(new AIAction(unit.troop, TroopSkill.walk, BattleCentralControl.map[unit.troop.getCurrentGrid().x, unit.troop.getCurrentGrid().z - 10]));
                actionQueue.Enqueue(new AIAction(unit.troop, TroopSkill.fire, BattleCentralControl.playerParty.leader.troop));
            }
        }
        foreach (Person unit in swordsmen)
        {
            int posX = (int)Mathf.Clamp(xOffset * index, 0, mapSize.x);
            index++;
            if (unit.troop != null)
            {
                actionQueue.Enqueue(new AIAction(unit.troop, TroopSkill.walk, BattleCentralControl.map[unit.troop.getCurrentGrid().x, unit.troop.getCurrentGrid().z - 10]));
                actionQueue.Enqueue(new AIAction(unit.troop, TroopSkill.fire, BattleCentralControl.playerParty.leader.troop));
            }
        }
        foreach (Person unit in halberdiers)
        {
            if (unit.troop != null)
            {
                //actionQueue.Enqueue(new AIAction(unit.troop, TroopSkill.walk, BattleCentralControl.map[unit.troop.getCurrentGrid().x, unit.troop.getCurrentGrid().z - 3]));
                //actionQueue.Enqueue(new AIAction(unit.troop, TroopSkill.fire, BattleCentralControl.playerParty.leader.troop));
            }
        }
        actionQueue.Enqueue(new AIAction(BattleCentralControl.enemyParty.leader.troop, TroopSkill.none));
        

    }
    

    void blindForward(Troop troop, int distance)
    {
        //troop.curGrid.x, troop.curGrid.z - 10
        troop.troopMoveToPlace(BattleCentralControl.map[troop.getCurrentGrid().x, troop.getCurrentGrid().z - distance]);
        //doIt = false;
        //BattleCentralControl.gridToObj[BattleCentralControl.map[1,1]].GetComponent<GridObject>().moveTroopToGrid(troop.gameObject);
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

    void categorizeTroop()
    {
        halberdiers.Clear();
        cavalries.Clear();
        swordsmen.Clear();
        crossbowmen.Clear();
        musketeer.Clear();

        foreach(Person p in BattleCentralControl.enemyParty.partyMember)
        {
            if (p.troop != null && p != BattleCentralControl.enemyParty.leader)
            {
                switch (p.troopType)
                {
                    case TroopType.halberdier:
                        halberdiers.Add(p);
                        break;
                    case TroopType.cavalry:
                        cavalries.Add(p);
                        break;
                    case TroopType.swordsman:
                        //swordsmen.Add(p);
                        break;
                    case TroopType.crossbowman:
                        crossbowmen.Add(p);
                        break;
                    case TroopType.musketeer:
                        musketeer.Add(p);
                        break;
                    case TroopType.recruitType:
                        swordsmen.Add(p);
                        break;
                }
            }
            if (p != BattleCentralControl.enemyParty.leader)
            {

            }
        }
        halberdiers = sortListByGridX(halberdiers);
        cavalries = sortListByGridX(cavalries);
        swordsmen = sortListByGridX(swordsmen);
        musketeer = sortListByGridX(musketeer);
        crossbowmen = sortListByGridX(crossbowmen);
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


    void clockTick()
    {
        timer += Time.deltaTime;
        if (timer >= 0.5f)
        {
            tick = true;
            timer -= 0.5f;
        }
        else
        {
            tick = false;
        }
    }
    List<Person> sortListByGridX(List<Person> personL)
    {
        if (personL.Count > 0)
        {
            float smallestX, comparingX;
            int smallestIndex = 0;
            Person temp;
            for (int i = 0; i < personL.Count - 1; i++)
            {
                smallestX = personL[i].troop.getCurrentGrid().x;
                smallestIndex = i;
                for (int j = i + 1; j < personL.Count; j++)
                {
                    comparingX = personL[j].troop.getCurrentGrid().x;
                    if (smallestX >= comparingX)
                    {
                        smallestIndex = j;
                        smallestX = comparingX;
                    }
                }
                temp = personL[i];
                personL[i] = personL[smallestIndex];
                personL[smallestIndex] = temp;
            }
        }
        return personL;
    }
}

public class AIAction {
    public Troop troop, target;
    public TroopSkill skillMode;
    public Grid destination;
    public bool finished;
    public AIAction(Troop troopI, TroopSkill skillModeI)
    {
        troop = troopI;
        skillMode = skillModeI;
    }
    public AIAction(Troop troopI, TroopSkill skillModeI, Troop targetI)
    {
        troop = troopI;
        skillMode = skillModeI;
        target = targetI;
    }
    public AIAction(Troop troopI, TroopSkill skillModeI, Grid destinationI)
    {
        troop = troopI;
        skillMode = skillModeI;
        destination = destinationI;
    }
    public void doAIAction()
    {
        troop.aiControlled = true;
        switch(skillMode)
        {
            case TroopSkill.none:
                BattleAIControl.enemyFinished = true;
                break;
            case TroopSkill.walk:
                //Debug.Log(destination.x + " " + destination.z);
                troop.troopMoveToPlace(destination);
                break;
            case TroopSkill.lunge:
                break;
            case TroopSkill.whirlwind:
                break;
            case TroopSkill.execute:
                break;
            case TroopSkill.guard:
                break;
            case TroopSkill.holdSteady:
                break;
            case TroopSkill.fire:
                fireAttack(troop, target);
                break;
            case TroopSkill.quickDraw:
                break;
            case TroopSkill.rainOfArrows:
                break;
        }
    }
    public void finishAIAction()
    {
        switch (skillMode)
        {
            case TroopSkill.none:
                break;
            case TroopSkill.walk:
                break;
            case TroopSkill.lunge:
                break;
            case TroopSkill.whirlwind:
                break;
            case TroopSkill.execute:
                break;
            case TroopSkill.guard:
                break;
            case TroopSkill.holdSteady:
                break;
            case TroopSkill.fire:
                troop.doSkill(troop.indicatedGrid(), TroopSkill.fire);
                troop.hideIndicators();
                troop.aiControlled = false;
                break;
            case TroopSkill.quickDraw:
                break;
            case TroopSkill.rainOfArrows:
                break;
        }
        
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
        if (!troop.fireIndicator.activeSelf)
        {
            troop.fireIndicator.SetActive(true);
        }
        troop.curIndicator = troop.fireIndicator;
        lookAtObject(troop.gameObject, attacked.gameObject);
    }

    void lookAtObject(GameObject looker, GameObject obj)
    {
        Vector3 v = looker.transform.position - obj.transform.position;
        v.x = v.z = 0.0f;
        looker.gameObject.transform.LookAt(obj.transform.position - v);
        looker.gameObject.transform.Rotate(0, 180, 0);
    }

}
public enum AIAttackMode
{
    aggressive,
    cautious,
    neutral
}