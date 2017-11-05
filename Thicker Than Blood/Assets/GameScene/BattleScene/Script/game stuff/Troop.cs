using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Troop : BattleInteractable {

    
    public GameObject controlPanel, inspectPanel;
    public Person person { get; set; }
    public Grid curGrid { get; set; }

    public GameObject statusPanel, troopStaminaBar, troopHealthBar, staminaTxt, healthTxt, nameTxt;
    public GameObject visionIndicator, walkIndicator, curIndicator, lungeIndicator, whirlwindIndicator, executeIndicator, fireIndicator, guardIndicator, rainOfArrowIndicator, quickDrawIndicator;
    public GameObject seenStatus;
    public bool controlled, aiControlled, charging, holdSteadying, reachedDestination;
    public Dictionary<Person, bool> stealthCheckDict = new Dictionary<Person, bool>();
    public bool activated = false;
    public float chargeStack;
    public List<Grid> guardedGrids;
    float STATUS_BAR_HEIGHT, STATUS_BAR_WIDTH;
    bool travelCostFree = false;
    Vector3 tempDest;
    Grid destinationGrid;
    NavMeshAgent navMeshAgent;
    MeshRenderer meshRenderer;
    Color originalColor;
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
            originalColor = meshRenderer.material.color;
            hideIndicators();
            charging = holdSteadying = false;
            guardedGrids = new List<Grid>();
            stealthCheckRefresh();
            controlled = false;
            aiControlled = false;
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


            showStatus();
            lookAtCamera(statusPanel);
            if (Vector3.Distance(navMeshAgent.destination, transform.position) <= .1f) 
            {
                reachedDestination = true;
                if (curGrid.personOnGrid != null && curGrid.personOnGrid != person)
                {
                    tempDest = goNearbyGrid(getCurrentGrid());
                }
                else
                {
                    curGrid.personOnGrid = person;
                }
                chargeStack = 0;
            }
            else
            {
                reachedDestination = false;
            }
            if (destinationGrid != null && curGrid == destinationGrid)
            {
                if (person.faction == Faction.mercenary)
                {
                    BattleInteraction.inAction = false;
                } else
                {
                    BattleAIControl.inAction = false;
                }
                destinationGrid = null;
                travelCostFree = false;
            }
            
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
        base.cameraFocusOn();
        if (BattleCentralControl.playerTurn)
        {
            controlPanel.GetComponent<TroopControlPanel>().initializePanel();
            controlPanel.GetComponent<TroopControlPanel>().curControlledTroop = gameObject;

            controlled = true;
        }
        BattleInspectPanel.person = person;
        inspectPanel.SetActive(true);
    }
    public override void cameraFocusOnExit()
    {
        base.cameraFocusOnExit();
        controlPanel.SetActive(false);
        controlled = false;
        BattleInspectPanel.person = null;
        inspectPanel.SetActive(false);
        hideIndicators();
    }
    public void troopMoveToPlace(Grid grid) { 
    
        if (person.faction == Faction.mercenary) { //player case
            if (grid.personOnGrid == null)
            {
                if (person.stamina > 0)
                {
                    navMeshAgent.destination = new Vector3(grid.x, 1, grid.z);

                    BattleInteraction.inAction = true;
                    destinationGrid = grid;
                }
            }
            else
            {
                goBackToLastGrid();
            }
        } else //enemy case
        {
            if (aiControlled)
            {
                if (grid.personOnGrid == null)
                {
                    if (person.stamina > 0)
                    {
                        navMeshAgent.destination = new Vector3(grid.x, 1, grid.z);
                        BattleAIControl.inAction = true;
                        destinationGrid = grid;
                    }
                }
                else
                {
                    goBackToLastGrid();
                }
            }
        }
    }
    public void placed(Person personI, Grid curGridI)
    {
        personI.stamina = personI.getStaminaMax();
        personI.health = personI.health;
        person = personI;
        curGrid = curGridI;
        curGrid.personOnGrid = person;
        activated = true;
        gameObject.SetActive(true);
        person.troop = gameObject.GetComponent<Troop>();
        if (person.faction == Faction.mercenary)
        {
            BattleCentralControl.playerTroopOnField.Add(person, gameObject);
        } else
        {
            BattleCentralControl.enemyTroopOnField.Add(person, gameObject);
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
            if (person.faction == Faction.mercenary)
            {
                BattleInteraction.inAction = false;
            } else
            {
                BattleAIControl.inAction = false;
            }
           
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
        if (movedToNewGrid())
        {
            clearGuard();
            if (charging)
            {
                if (person.stamina < getCurrentGrid().staminaCost * 2)
                {
                    goBackToLastGrid();
                }
                else
                {
                    if (lastGrid.personOnGrid == person)
                    {
                        lastGrid.personOnGrid = null;
                    }
                    
                    person.stamina -= getCurrentGrid().getStaminaCost(person.faction) * 2;
                    if (chargeStack <= 20)
                    {
                        chargeStack += 5f/curGrid.getStaminaCost(person.faction);
                    }

                    if (curGrid.personOnGrid != null && curGrid.personOnGrid.faction != person.faction)
                    {
                        curGrid.personOnGrid.health -= .1f * person.getMeleeAttackDmg() * chargeStack;
                    }
                }
            } else
            {
                if (!travelCostFree)
                {
                    if (person.stamina < getCurrentGrid().getStaminaCost(person.faction))
                    {
                        goBackToLastGrid();
                    }
                    else
                    {
                        person.stamina -= getCurrentGrid().getStaminaCost(person.faction);
                        if (lastGrid.personOnGrid == person)
                        {
                            lastGrid.personOnGrid = null;
                        }
                    }
                }
                
            }
            
        }
    }
    public void walk()
    {
        if (!walkIndicator.activeSelf)
        {
            walkIndicator.SetActive(true);
            curIndicator = walkIndicator;
        }
        followMouse(walkIndicator);
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
    }
    public void execute()
    {
        if (!executeIndicator.activeSelf)
        {
            executeIndicator.SetActive(true);
            curIndicator = executeIndicator;
        }
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
                    BattleCentralControl.gridToObj[g].GetComponent<GridObject>().guardedByPlayer(false);
                } else
                {
                    g.playerTempStaminaCost -= person.getGuardStaminaCost();
                    BattleCentralControl.gridToObj[g].GetComponent<GridObject>().guardedByEnemy(false);
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
                } else
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
                } else
                {
                    BattleInteraction.skillMode = TroopSkill.none;
                }
                break;
            case TroopSkill.execute:
                if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit interactionInfo;
                    if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
                    {
                        GameObject interactedObject = interactionInfo.collider.gameObject.transform.parent.gameObject;
                        Troop attackedTroop = interactedObject.GetComponent<Troop>();
                        if (attackedTroop != null && person.stamina >= person.getExecutionStaminaCost()) //TODO: remove player troop later
                        {
                            if (attackedTroop.person.faction != person.faction && attackedGrid.Contains(attackedTroop.curGrid)) {
                                attackedTroop.person.health -= 5 * person.getMeleeAttackDmg();
                            }
                        } else
                        {
                            BattleInteraction.skillMode = TroopSkill.none;
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
                } else
                {
                    BattleInteraction.skillMode = TroopSkill.none;
                }
                break;
            case TroopSkill.charge:
                break;
            case TroopSkill.holdSteady:
                if (person.stamina < person.getFireStaminaCost() + person.getHoldSteadyStaminaCost())
                {
                    holdSteadying = false;
                }
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
                    } else
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
                    } else
                    {
                        BattleInteraction.skillMode = TroopSkill.none;
                    }
                }
                break;
            case TroopSkill.guard:
                if (person.stamina >= person.getGuardStaminaCost())
                {
                    foreach (Grid g in attackedGrid)
                    {
                        if (person.faction == Faction.mercenary)
                        {
                            g.enemyTempStaminaCost += person.getGuardedIncrease();
                            BattleCentralControl.gridToObj[g].GetComponent<GridObject>().guardedByPlayer(true);
                        }
                        else
                        {
                            g.playerTempStaminaCost += person.getGuardedIncrease();
                            BattleCentralControl.gridToObj[g].GetComponent<GridObject>().guardedByEnemy(true);
                        }
                        guardedGrids.Add(g);
                    }
                    person.stamina -= person.getGuardStaminaCost();
                } else
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
                } else
                {
                    BattleInteraction.skillMode = TroopSkill.none;
                }
                break;

        }
        //Debug.Log("attacked grid num: " + attackedGrid.Count);
        
    }
    public void death()
    {

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
            float rand = Random.Range(10.0f, watcher.person.getVision() + person.getStealth());
            if ( rand < watcher.person.getVision())
            {
                hidden();
            } else
            {
                revealed();
            }
            stealthCheckDict[watcher.person] = true;
        }
    }
    public void hidden()
    {
        if (person.faction == Faction.mercenary)
        {
            if (seenStatus.activeSelf)
            {
                seenStatus.SetActive(false);
            }
        }
        else
        {
            meshRenderer.material.color = new Color(0, 255, 0); //new Color(originalColor.r, originalColor.g, originalColor.b, 0.0f);
        }
    }
    public void revealed()
    {
        if (person.faction == Faction.mercenary)
        {
            if (!seenStatus.activeSelf)
            {
                seenStatus.SetActive(true);
            }
        } else
        {
            meshRenderer.material.color = originalColor;
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
            Vector3 pos = new Vector3(curGrid.x, transform.position.y, curGrid.z);
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
                Vector3 pos = new Vector3(pointedObj.transform.position.x, transform.position.y, pointedObj.transform.position.z);
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

