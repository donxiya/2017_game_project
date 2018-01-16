using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Troop : BattleInteractable {

    
    public GameObject controlPanel, inspectPanel;
    public Person person { get; set; }
    public Grid curGrid { get; set; }
    public Troop lastAttacker;
    public GameObject statusPanel, troopStaminaBar, troopHealthBar, staminaTxt, healthTxt, nameTxt;
    public GameObject visionIndicator, walkIndicator, curIndicator, lungeIndicator, whirlwindIndicator, executeIndicator, fireIndicator, guardIndicator, rainOfArrowIndicator, quickDrawIndicator;
    public GameObject seenStatus;
    public bool controlled, aiControlled, charging, holdSteadying, reachedDestination, seen;
    public Dictionary<Person, bool> stealthCheckDict = new Dictionary<Person, bool>();
    public bool activated = false;
    public float chargeStack;
    public List<Grid> guardedGrids;
    public Material invisible;
    Material originalMaterial;
    float STATUS_BAR_HEIGHT, STATUS_BAR_WIDTH;
    public bool travelCostFree = false;
    bool staminaCosted = false;
    Vector3 tempDest, finalDest, safetyDest;
    Grid destinationGrid;
    NavMeshAgent navMeshAgent;
    MeshRenderer meshRenderer;
    Grid lastGrid;
    // Use this for initialization
    public void Start()
    {
        if (person != null)
        {
            STATUS_BAR_HEIGHT = troopStaminaBar.GetComponent<RawImage>().rectTransform.sizeDelta.y;
            STATUS_BAR_WIDTH = troopStaminaBar.GetComponent<RawImage>().rectTransform.sizeDelta.x;
            navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
            meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
            originalMaterial = meshRenderer.material;
            hideIndicators();
            charging = holdSteadying = false;
            guardedGrids = new List<Grid>();
            stealthCheckRefresh();
            controlled = false;
            aiControlled = false;
            seen = false;
        }
        
    }
    public void Update() {
        
        if (activated)
        {
            
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }
            visionUpdate();
            if (BattleCentralControl.playerTurn && person.faction == Faction.mercenary)
            {
                if (controlled)
                {
                    walkUpdate();
                } else
                {
                    stayOnGird();
                }
            }
            if (!BattleCentralControl.playerTurn && person.faction != Faction.mercenary)
            {
                if (aiControlled)
                {
                    walkUpdate();
                }
                else
                {
                    stayOnGird();
                }
            }
            if (person.health <= 0)
            {
                outOfHealth();
            }

            showStatus();
            lookAtCamera(statusPanel);
            
        } else
        {
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }
        }
    }
    public override void cameraFocusOn()
    {
        
        if (BattleCentralControl.playerTurn)
        {
            controlPanel.GetComponent<TroopControlPanel>().initializePanel();
            controlPanel.GetComponent<TroopControlPanel>().curControlledTroop = gameObject;

            controlled = true;
        }
        if (person.faction == Faction.mercenary || seen)
        {
            base.cameraFocusOn();
            BattleInspectPanel.person = person;
            inspectPanel.SetActive(true);
        }
        if (!BattleCentralControl.playerTurn)
        {
            base.cameraFocusOn();
        }
    }
    public override void cameraFocusOnExit()
    {
        if (BattleCentralControl.playerTurn)
        {
            controlPanel.SetActive(false);
            controlled = false;
            BattleInspectPanel.person = null;
            inspectPanel.SetActive(false);
            hideIndicators();
            BattleInteraction.skillMode = TroopSkill.walk;
        }
        base.cameraFocusOnExit();
        
        
    }
    public void troopMoveToPlace(Grid grid) {
        reachedDestination = false;
        if (person.faction == Faction.mercenary) { //player case
            if (grid != null && grid.personOnGrid == null) //
            {
                if (person.stamina > 0)
                {
                    finalDest = new Vector3(grid.x, 1, grid.z);
                    BattleInteraction.inAction = true;
                    //destinationGrid = grid;
                } else
                {
                    destinationGrid = getCurrentGrid();
                }
            }
            else
            {
                if (person.stamina > 0)
                {
                    finalDest = new Vector3(grid.x, 1, grid.z);
                    BattleInteraction.inAction = true;
                    //destinationGrid = grid;
                }
                else
                {
                    destinationGrid = getCurrentGrid();
                }
                //goBackToLastGrid();
            }
        } else //enemy case
        {
            if (aiControlled)
            {
                if (grid != null && grid.personOnGrid == null)
                {
                    if (person.stamina > 0)
                    {
                        finalDest = new Vector3(grid.x, 1, grid.z);
                        BattleAIControl.inAction = true;
                        //destinationGrid = grid;
                    }
                }
                else
                {
                    //if (person.stamina > 0)
                    //{
                    //    finalDest = new Vector3(grid.x, 1, grid.z);
                    //    BattleAIControl.inAction = true;
                    //    //destinationGrid = grid;
                    //}
                    //goBackToLastGrid();
                }
            }
        }
    }
    public void placed(Person personI, Grid curGridI)
    {
        if (curGridI.personOnGrid == null)
        {
            personI.stamina = personI.getStaminaMax();
            personI.health = personI.health;
            person = personI;
            curGrid = curGridI;
            finalDest = new Vector3(curGrid.x, 1, curGrid.z);
            tempDest = new Vector3(curGrid.x, 1, curGrid.z);
            safetyDest = new Vector3(curGrid.x, 1, curGrid.z);
            reachedDestination = true;
            curGrid.personOnGrid = person;
            activated = true;
            gameObject.SetActive(true);
            
            if (person.faction == Faction.mercenary)
            {
                BattleCentralControl.playerTroopOnField.Add(person, gameObject);
            }
            else
            {
                BattleCentralControl.enemyTroopOnField.Add(person, gameObject);
            }
            person.troop = gameObject.GetComponent<Troop>();
            EndTurnPanel.endTurnPanel.updateBattlemeter();
        }
    }

    public Grid getCurrentGrid()
    {
        return BattleCentralControl.map[Mathf.RoundToInt(gameObject.transform.position.x), Mathf.RoundToInt(gameObject.transform.position.z)];
    }

    public void goBackToLastGrid()
    {
        if (lastGrid != null)
        {
            navMeshAgent.destination = new Vector3(lastGrid.x, 1, lastGrid.z);
            charging = false;
            chargeStack = 0;
            //person.stamina += curGrid.staminaCost;
            curGrid = lastGrid;
            
           
        }
    }

    bool movedToNewGrid()
    {
        if (curGrid != getCurrentGrid())
        {
            lastGrid = curGrid;
            curGrid = getCurrentGrid();
            return true;
        }
        return false;
    }

    
    
    public void hideIndicators()
    {
        walkIndicator.SetActive(false);
        lungeIndicator.SetActive(false);
        whirlwindIndicator.SetActive(false);
        executeIndicator.SetActive(false);
        fireIndicator.SetActive(false);
        guardIndicator.SetActive(false);
        rainOfArrowIndicator.SetActive(false);
        quickDrawIndicator.SetActive(false);
    }

    public void visionUpdate()
    {
        visionIndicator.transform.localScale = new Vector3(person.getVision(), 1, person.getVision());
        if (!visionIndicator.activeSelf)
        {
            visionIndicator.SetActive(true);
        }
    }


    public void walkUpdate()
    {
        //CHECK IF AT FINAL DEST
        float finalDistance = Vector2.Distance(new Vector2(finalDest.x, finalDest.z), new Vector2(gameObject.transform.position.x, gameObject.transform.position.z));
        if (finalDistance <= 0.1f)
        {
            reachedDestination = true;
            if (person.faction == Faction.mercenary)
            {
                BattleInteraction.inAction = false;
            }
            else
            {
                aiControlled = false;
                BattleAIControl.inAction = false;
            }
        }

        float distance = Vector2.Distance(new Vector2(tempDest.x, tempDest.z), new Vector2(gameObject.transform.position.x, gameObject.transform.position.z));
        if (distance <= 0.1f) //when we arrive a grid
        {
            if (curGrid != getCurrentGrid()) //make sure new grid
            {
                if (charging)
                {
                    if (chargeStack <= 20)
                    {
                        chargeStack += 5f / curGrid.getStaminaCost(person.faction);
                    }

                    if (getCurrentGrid().personOnGrid != null && getCurrentGrid().personOnGrid.faction != person.faction)
                    {
                        getCurrentGrid().personOnGrid.health -= .1f * person.getMeleeAttackDmg() * chargeStack;
                    }
                    person.stamina -= BattleCentralControl.map[(int)tempDest.x, (int)tempDest.z].getStaminaCost(person.faction) * 2;
                } else
                {
                    if (!travelCostFree)
                    {
                        person.stamina -= BattleCentralControl.map[(int)tempDest.x, (int)tempDest.z].getStaminaCost(person.faction);
                    } else if (travelCostFree && getCurrentGrid().x == safetyDest.x && getCurrentGrid().z == safetyDest.z)
                    {
                        travelCostFree = false;
                    }
                }
                //UPDATE CUR
                if (curGrid.personOnGrid == person)
                {
                    curGrid.personOnGrid = null;
                }
                curGrid = getCurrentGrid();
                if (curGrid.personOnGrid == null)
                {
                    curGrid.personOnGrid = person;
                    safetyDest = new Vector3(curGrid.x, 1, curGrid.z);
                }
            }
            

            //GUIDANCE
            if (getCurrentGrid().x != finalDest.x || getCurrentGrid().z != finalDest.z) //if we are not at dest yet
            {
                float xDist = finalDest.x - getCurrentGrid().x;
                float zDist = finalDest.z - getCurrentGrid().z;
                if (Mathf.Abs(xDist) >= Mathf.Abs(zDist))
                {
                    tempDest.x += Mathf.Clamp(finalDest.x - getCurrentGrid().x, -1, 1);
                } else
                {
                    tempDest.z += Mathf.Clamp(finalDest.z - getCurrentGrid().z, -1, 1);
                }
                Grid nextGrid = BattleCentralControl.map[(int)tempDest.x, (int)tempDest.z];
                if ((person.stamina >= nextGrid.getStaminaCost(person.faction) && !charging)
                    || (person.stamina >= nextGrid.getStaminaCost(person.faction) * 2 && charging)) //LEAVING
                {
                    clearGuard();
                    navMeshAgent.destination = tempDest;
                    staminaCosted = false;
                } else //if stamina not enough, set final dest as temp dest  //ARRIVED
                {
                    if (getCurrentGrid().personOnGrid == null)
                    {
                        tempDest = new Vector3(getCurrentGrid().x, 1, getCurrentGrid().z);
                        finalDest = new Vector3(getCurrentGrid().x, 1, getCurrentGrid().z);
                    } else
                    {
                        tempDest = safetyDest;
                        finalDest = safetyDest;
                        travelCostFree = true;
                        charging = false;
                    }
                    navMeshAgent.destination = tempDest;
                    //lookAtVector(tempDest);
                    
                }
            } else { //ARRIVING
                if (getCurrentGrid().personOnGrid != null && getCurrentGrid().personOnGrid != person)
                {
                    finalDest = safetyDest;
                    travelCostFree = true;
                    charging = false;
                } else
                {
                    travelCostFree = false;
                    //tempDest = new Vector3(getCurrentGrid().x, 1, getCurrentGrid().z);
                    //finalDest = new Vector3(getCurrentGrid().x, 1, getCurrentGrid().z);
                    //navMeshAgent.destination = tempDest;
                    
                }
            }
            
        } else //TRAVELLING
        {
            if (curGrid !=  getCurrentGrid())
            {
                navMeshAgent.destination = tempDest;
            }
        }
        tempDest = new Vector3(getCurrentGrid().x, 1, getCurrentGrid().z);
    }
    public void walk()
    {
        if (!walkIndicator.activeSelf)
        {
            walkIndicator.SetActive(true);
            curIndicator = walkIndicator;
        }
        followMouse(walkIndicator);
        //lookAtMouse(gameObject);
    }
    public void lunge()
    {
        if (!lungeIndicator.activeSelf)
        {
            lungeIndicator.SetActive(true);
            curIndicator = lungeIndicator;
        }
        lookAtMouse(gameObject);
        lookAtMouse(lungeIndicator);
    }
    public void whirlwind()
    {
        if (!whirlwindIndicator.activeSelf)
        {
            whirlwindIndicator.SetActive(true);
            curIndicator = whirlwindIndicator;
        }
        lookAtMouse(gameObject);
    }
    public void execute()
    {
        if (!executeIndicator.activeSelf)
        {
            executeIndicator.SetActive(true);
            curIndicator = executeIndicator;
        }
        lookAtMouse(gameObject);
    }
    public void fire()
    {
        if (!fireIndicator.activeSelf)
        {
            fireIndicator.SetActive(true);
            curIndicator = fireIndicator;
        }
        lookAtMouse(fireIndicator);
        lookAtMouse(gameObject);
    }
    public void quickDraw()
    {
        if (!fireIndicator.activeSelf)
        {
            quickDrawIndicator.SetActive(true);
            curIndicator = quickDrawIndicator;
        }
        lookAtMouse(quickDrawIndicator);
        lookAtMouse(gameObject);
    }
    public void holdSteady()
    {
        holdSteadying = !holdSteadying;
    }
    public void guard()
    {
        if (!guardIndicator.activeSelf)
        {
            guardIndicator.SetActive(true);
            curIndicator = guardIndicator;
        }
    }
    public void clearGuard()
    {
        if (guardedGrids.Count > 0)
        {
            foreach(Grid g in guardedGrids)
            {
                if (person.faction == Faction.mercenary)
                {
                    g.enemyTempStaminaCost -= person.getGuardStaminaCost();
                    g.gridObject.GetComponent<GridObject>().guardedByPlayer(false, person);
                } else
                {
                    g.playerTempStaminaCost -= person.getGuardStaminaCost();
                    g.gridObject.GetComponent<GridObject>().guardedByEnemy(false, person);
                }
            }
            guardedGrids.Clear();
        }
    }
    public void rainOfArrow()
    {
        if (!rainOfArrowIndicator.activeSelf)
        {
            rainOfArrowIndicator.SetActive(true);
            curIndicator = rainOfArrowIndicator;
        }
        followMouse(rainOfArrowIndicator);
        lookAtMouse(gameObject);
    }
    public void charge()
    {
        charging = !charging;
        chargeStack = 0;
    }



    public void doSkill(List<Grid> attackedGrid, TroopSkill skillMode)
    {
        attackedGrid = sortGridByRange(attackedGrid);
        switch (skillMode)
        {
            case TroopSkill.lunge:
                if (person.stamina >= person.getLungeStaminaCost())
                {
                    foreach (Grid g in attackedGrid)
                    {
                        if (g.personOnGrid != null && g.personOnGrid.faction != person.faction)
                        {
                            g.personOnGrid.health -= person.getMeleeAttackDmg();

                        }
                    }
                    person.stamina -= person.getLungeStaminaCost();
                } else if (person.faction == Faction.mercenary)
                {
                    BattleInteraction.skillMode = TroopSkill.none;
                }
                break;
            case TroopSkill.whirlwind:
                if (person.stamina >= person.getWhirlwindStaminaCost())
                {
                    foreach (Grid g in attackedGrid)
                    {
                        if (g.personOnGrid != null && g.personOnGrid.faction != person.faction)
                        {
                            g.personOnGrid.health -= person.getMeleeAttackDmg();
                        }
                    }
                    person.stamina -= person.getWhirlwindStaminaCost();
                } else if (person.faction == Faction.mercenary)
                {
                    BattleInteraction.skillMode = TroopSkill.none;
                }
                break;
            case TroopSkill.execute:
                if (controlled)
                {
                    if (person.stamina >= person.getExecuteStaminaCost())
                    {
                        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                        {
                            Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                            RaycastHit interactionInfo;
                            if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
                            {
                                GameObject interactedObject = interactionInfo.collider.gameObject.transform.parent.gameObject;
                                Troop attackedTroop = interactedObject.GetComponent<Troop>();
                                if (attackedTroop != null && attackedTroop.seen && person.stamina >= person.getExecuteStaminaCost()) //TODO: remove player troop later
                                {
                                    if (attackedTroop.person.faction != person.faction && attackedGrid.Contains(attackedTroop.curGrid))
                                    {
                                        lookAtVector(attackedTroop.gameObject.transform.position);
                                        float lostPercentage = 1 - (attackedTroop.person.health / attackedTroop.person.getHealthMax());
                                        attackedTroop.person.health -= (lostPercentage*10 + 1) * person.getMeleeAttackDmg();
                                        person.stamina -= person.getExecuteStaminaCost();
                                    }
                                }
                                else if (person.faction == Faction.mercenary)
                                {
                                    BattleInteraction.skillMode = TroopSkill.none;
                                }
                            }
                        }
                    }
                    else
                    {
                        BattleInteraction.skillMode = TroopSkill.none;
                    }
                } else if (aiControlled)
                {
                    if (person.stamina >= person.getExecuteStaminaCost())
                    {
                        float leastHp = 0;
                        Person attackedPerson = null;
                        foreach (Grid g in attackedGrid)
                        {
                            if (g.personOnGrid != null && g.personOnGrid.faction != person.faction)
                            {
                                if (leastHp == 0)
                                {
                                    leastHp = g.personOnGrid.health;
                                    attackedPerson = g.personOnGrid;
                                }
                                else if (leastHp > g.personOnGrid.health)
                                {
                                    leastHp = g.personOnGrid.health;
                                    attackedPerson = g.personOnGrid;
                                }
                            }
                        }
                        if (leastHp != 0 && attackedPerson != null)
                        {
                            attackedPerson.health -= 5 * person.getMeleeAttackDmg();
                            person.stamina -= person.getExecuteStaminaCost();
                        }
                    }
                    
                    
                }
                
                break;
            case TroopSkill.rainOfArrows:
                if (person.stamina >= person.getRainOfArrowsStaminaCost())
                {
                    foreach (Grid g in attackedGrid)
                    {
                        if (g.personOnGrid != null)
                        {
                            g.personOnGrid.health -= person.getRangedAttackDmg();
                        }
                    }
                    person.stamina -= person.getRainOfArrowsStaminaCost();
                } else if (person.faction == Faction.mercenary)
                {
                    BattleInteraction.skillMode = TroopSkill.none;
                }
                break;
            case TroopSkill.charge:
                break;
            case TroopSkill.holdSteady:
                break;
            case TroopSkill.fire:
                if (holdSteadying)
                {
                    if (person.stamina >= person.getFireStaminaCost() + person.getHoldSteadyStaminaCost())
                    {
                        foreach (Grid g in attackedGrid)
                        {
                            if (g.personOnGrid != null && g.personOnGrid != person)
                            {
                                g.personOnGrid.health -= person.getRangedAttackDmg();
                            }
                        }
                        person.stamina -= (person.getFireStaminaCost() + person.getHoldSteadyStaminaCost());
                    } else if (person.faction == Faction.mercenary)
                    {
                        BattleInteraction.skillMode = TroopSkill.none;
                    }
                } else
                {
                    if (person.stamina >= person.getFireStaminaCost())
                    {
                        if (attackedGrid.Count == 0) 
                        {
                            Debug.Log("attack grid 0");
                        }
                        foreach (Grid g in attackedGrid)
                        {
                            if (g.personOnGrid != null && g.personOnGrid != person)
                            {
                                g.personOnGrid.health -= person.getRangedAttackDmg();
                            }
                        }
                        person.stamina -= person.getFireStaminaCost();
                    } else if (person.faction == Faction.mercenary)
                    {
                        BattleInteraction.skillMode = TroopSkill.none;
                    }
                }
                break;
            case TroopSkill.guard:
                if (person.stamina >= person.getGuardStaminaCost())
                {
                    clearGuard();
                    foreach (Grid g in attackedGrid)
                    {
                        if (person.faction == Faction.mercenary)
                        {
                            g.gridObject.GetComponent<GridObject>().guardedByPlayer(true, person);
                        }
                        else
                        {
                            g.gridObject.GetComponent<GridObject>().guardedByEnemy(true, person);
                        }
                        guardedGrids.Add(g);
                    }
                    person.stamina -= person.getGuardStaminaCost();
                } else if (person.faction == Faction.mercenary)
                {
                    BattleInteraction.skillMode = TroopSkill.none;
                }
                break;
            case TroopSkill.quickDraw:
                if (person.stamina >= person.getQuickDrawStaminaCost())
                {
                    bool blocked = false;
                    foreach (Grid g in attackedGrid)
                    {
                        if (g.personOnGrid != null && !blocked && g.personOnGrid != person)
                        {
                            g.personOnGrid.health -= person.getRangedAttackDmg();
                            blocked = true;
                        }
                    }
                    person.stamina -= person.getQuickDrawStaminaCost();
                } else if (person.faction == Faction.mercenary)
                {
                    BattleInteraction.skillMode = TroopSkill.none;
                }
                break;

        }
        
    }


    public void takingDamage(Troop attacker)
    {
        lastAttacker = attacker;
    }

    public void blocking()
    {

    }

    public void dodging()
    {

    }
    
    public void outOfHealth()
    {
        activated = false;
        curGrid.personOnGrid = null;
        Debug.Log("dead");
        if (lastAttacker != null)
        {
            lastAttacker.person.increaseExp(person.getExp());
        }
        if (Mathf.Abs(person.health) > person.getInjuredHealth() && person.ranking != Ranking.mainChar) //DEATH
        {
            if (person.faction == Faction.mercenary)
            {
                if (BattleCentralControl.playerParty.partyMember.Contains(person))
                {
                    BattleCentralControl.playerParty.partyMember.Remove(person);
                }
            }
            else
            {
                if (BattleCentralControl.enemyParty.partyMember.Contains(person))
                {
                    BattleCentralControl.enemyParty.partyMember.Remove(person);
                }
            }
        } else //INJURED
        {
            person.health = 0;
        }
        if (person.faction == Faction.mercenary)
        {
            if (BattleCentralControl.playerTroopOnField.ContainsKey(person))
            {
                BattleCentralControl.playerTroopOnField.Remove(person);
                TroopPlacing.troopPlacing.removePlacingButton(person);
            }
            BattleCentralControl.enemyParty.expToDistribute += person.getExp();
        }
        else
        {
            if (BattleCentralControl.enemyTroopOnField.ContainsKey(person))
            {
                BattleCentralControl.enemyTroopOnField.Remove(person);
            }
            BattleCentralControl.playerParty.expToDistribute += person.getExp();
        }
        EndTurnPanel.endTurnPanel.updateBattlemeter();
        GameObject.Destroy(gameObject);
    }
    public void stealthCheckRefresh()
    {
        stealthCheckDict.Clear();
        if (person.faction == Faction.mercenary)
        {
            foreach (Person p in BattleCentralControl.enemyParty.partyMember)
            {
                stealthCheckDict.Add(p, false);
            }
        } else
        {
            foreach (Person p in BattleCentralControl.playerParty.partyMember)
            {
                stealthCheckDict.Add(p, false);
            }
        }
        
        hidden();
        
    }
    public void stealthCheck(Troop watcher)
    {
        if (stealthCheckDict.ContainsKey(watcher.person) && !stealthCheckDict[watcher.person])
        {
            float distance = Vector3.Distance(watcher.gameObject.transform.position, transform.position);
            float rand = Random.Range(distance, watcher.person.getVision() + person.getStealth());
            if ( rand < watcher.person.getVision())
            {
                hidden();
            } else
            {
                revealed();
            }
            //revealed();  //revealed when enter others' vision
            stealthCheckDict[watcher.person] = true;
        }
    }
    public void hidden()
    {
        seen = false;
        if (person.faction == Faction.mercenary)
        {
            if (seenStatus.activeSelf)
            {
                seenStatus.SetActive(false);
            }
        }
        else
        {
            statusPanel.SetActive(false);
            meshRenderer.material = invisible; //new Color(originalColor.r, originalColor.g, originalColor.b, 0.0f);
        }
    }
    public void revealed()
    {
        seen = true;
        if (person.faction == Faction.mercenary)
        {
            if (!seenStatus.activeSelf)
            {
                seenStatus.SetActive(true);
            }
        } else
        {
            statusPanel.SetActive(true);
            meshRenderer.material = originalMaterial;
        }
    }


    public List<Grid> indicatedGrid()
    {
        List<Grid> result = new List<Grid>();
        if (curIndicator != null)
        {
            result = curIndicator.GetComponent<Indicator>().collided;
        }
        return result;
    }
    void stayOnGird()
    {
        if (curGrid != null)
        {
            Vector3 pos = new Vector3(getCurrentGrid().x, transform.position.y, getCurrentGrid().z);
            curGrid = getCurrentGrid();
            transform.position = Vector3.Slerp(transform.position, pos, Time.deltaTime * 1000);
        }
    }
    Vector3 goNearbyGrid(Grid g) {
        int randX = (int) Mathf.Clamp(Random.Range(-1, 2), -1, 1);
        int randZ = (int)Mathf.Clamp(Random.Range(-1, 2), -1, 1);
        while (randX == 0 && randZ == 0)
        {
            randX = (int)Mathf.Clamp(Random.Range(-1, 2), -1, 1);
            randZ = (int)Mathf.Clamp(Random.Range(-1, 2), -1, 1);
        }
        Vector3 pos = new Vector3(curGrid.x + randX, transform.position.y, curGrid.z + randZ);
        navMeshAgent.destination = pos;
        charging = false;
        chargeStack = 0;
        travelCostFree = true;
        destinationGrid = BattleCentralControl.map[curGrid.x + randX, curGrid.z + randZ];
        return pos;
    }
    void showStatus()
    {

        if (person != null)
        {
            //Debug.Log("stamina and max: " + person.health + " " + person.getHealthMax());
            troopHealthBar.GetComponent<RawImage>().rectTransform.sizeDelta = new Vector2(STATUS_BAR_WIDTH * (person.health / person.getHealthMax()), STATUS_BAR_HEIGHT);
            troopStaminaBar.GetComponent<RawImage>().rectTransform.sizeDelta = new Vector2(STATUS_BAR_WIDTH * (person.stamina / person.getStaminaMax()), STATUS_BAR_HEIGHT);
            staminaTxt.GetComponent<Text>().text = person.stamina.ToString();
            healthTxt.GetComponent<Text>().text = person.health.ToString();
            nameTxt.GetComponent<Text>().text = person.name;
        }
    }

    void lookAtVector(Vector3 pos)
    {
        Vector3 v = pos - gameObject.transform.position;
        v.x = v.z = 0.0f;
        gameObject.transform.LookAt(pos - v);
        gameObject.transform.Rotate(0, 180, 0);
    }

    void lookAtCamera(GameObject obj)
    {
        Vector3 v = Camera.main.transform.position - obj.transform.position;
        v.x = v.z = 0.0f;
        obj.transform.LookAt(Camera.main.transform.position - v);
        obj.transform.Rotate(0, 180, 0);
    }

    void lookAtMouse(GameObject obj)
    {
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;
        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
        {
            GameObject pointedObj = interactionInfo.collider.gameObject.transform.parent.gameObject;
            if (pointedObj.tag == "Grid" || pointedObj.tag == "Troop")
            {
                Vector3 v = pointedObj.transform.position - obj.transform.position;
                v.x = v.z = 0.0f;
                obj.transform.LookAt(pointedObj.transform.position - v);
                obj.transform.Rotate(0, 180, 0);
            } else
            {
                Vector3 v = interactionInfo.point - obj.transform.position;
                v.x = v.z = 0.0f;
                obj.transform.LookAt(pointedObj.transform.position - v);
                obj.transform.Rotate(0, 180, 0);
                
            }
        }
    }
    void followMouse(GameObject obj)
    {
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;
        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
        {
            GameObject pointedObj = interactionInfo.collider.gameObject.transform.parent.gameObject;
            if (pointedObj.tag == "Grid" || pointedObj.tag == "Troop")
            {
                Vector3 pos = new Vector3(pointedObj.transform.position.x, 0, pointedObj.transform.position.z);
                obj.transform.position = Vector3.Slerp(obj.transform.position, pos, Time.deltaTime * 1000);
            }
        }
    }
    List<Grid> sortGridByRange(List<Grid> gridL)
    {
        if (gridL.Count > 0)
        {
            float smallestDistance, comparingDistance;
            int smallestIndex = 0;
            Grid temp;
            for (int i = 0; i < gridL.Count - 1; i++)
            {
                smallestDistance = Vector2.Distance(new Vector2(curGrid.x, curGrid.z), new Vector2(gridL[i].x, gridL[i].z));
                smallestIndex = i;
                for (int j = i + 1; j < gridL.Count; j++)
                {
                    comparingDistance = Vector2.Distance(new Vector2(curGrid.x, curGrid.z), new Vector2(gridL[j].x, gridL[j].z));
                    if (smallestDistance >= comparingDistance)
                    {
                        smallestIndex = j;
                        smallestDistance = comparingDistance;
                    }
                }
                temp = gridL[i];
                gridL[i] = gridL[smallestIndex];
                gridL[smallestIndex] = temp;
            }
        }
        return gridL;
        
    }

}

