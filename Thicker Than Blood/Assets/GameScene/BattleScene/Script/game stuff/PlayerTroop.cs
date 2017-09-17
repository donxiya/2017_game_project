using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerTroop : BattleInteractable {

    
    public GameObject controlPanel;
    public Person person { get; set; }
    public Grid curGrid { get; set; }

    public GameObject statusPanel, troopStaminaBar, troopHealthBar, staminaTxt, healthTxt;
    public GameObject curIndicator, lungeIndicator, whirlwindIndicator, executeIndicator, fireIndicator, phalanxIndicator, rainOfArrowIndicator, quickDrawIndicator;

    public Texture2D staminaBarImg, troopHealthBarImg;
    public bool controlled, charging, holdSteadying;
    float STATUS_BAR_HEIGHT, STATUS_BAR_WIDTH;
    NavMeshAgent navMeshAgent;
    Grid lastGrid;
    // Use this for initialization
    public void Start()
    {
        STATUS_BAR_HEIGHT = troopStaminaBar.GetComponent<RawImage>().rectTransform.sizeDelta.y;
        STATUS_BAR_WIDTH = troopStaminaBar.GetComponent<RawImage>().rectTransform.sizeDelta.x;
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        hideIndicators();
        charging = holdSteadying = false;
    }
    public void Update() {
        if (BattleCentralControl.playerTurn && controlled)
        {
            walk();
        }
        if (!controlled)
        {
            stayOnGird();
        }
        showStatus();
        lookAtCamera(statusPanel);
    }
    public override void cameraFocusOn()
    {
        base.cameraFocusOn();
        if (BattleCentralControl.playerTurn)
        {
            controlPanel.SetActive(true);
            controlPanel.GetComponent<TroopControlPanel>().curControledTroop = gameObject;
            controlled = true;
        }
    }
    public override void cameraFocusOnExit()
    {
        base.cameraFocusOnExit();
        controlPanel.SetActive(false);
        controlled = false;
    }
    public void troopMoveToPlace(Grid grid)
    {
        if (grid.troop == null)
        {
            if (person.stamina > 0)
            {
                navMeshAgent.destination = new Vector3(grid.x, 1, grid.z);
            }
        } else
        {
            goBackToLastGrid();
        }
        
    }
    public void placed(Person personI, Grid curGridI)
    {
        personI.stamina = personI.getStaminaMax();
        person = personI;
        curGrid = curGridI;
        curGrid.troop = person;
        //person.stamina = person.getStaminaMax();
    }

    public Grid getCurrentGrid()
    {
        return BattleCentralControl.map[Mathf.RoundToInt(gameObject.transform.position.x), Mathf.RoundToInt(gameObject.transform.position.z)];
    }

    public void goBackToLastGrid()
    {
        navMeshAgent.destination = new Vector3(lastGrid.x, 1, lastGrid.z);
        //person.stamina += curGrid.staminaCost;
        curGrid = lastGrid;
        
    }

    bool movedToNewGrid()
    {
        if (curGrid != getCurrentGrid())
        {
            curGrid.troop = null;
            lastGrid = curGrid;
            curGrid = getCurrentGrid();
            curGrid.troop = person;
            return true;
        }
        return false;
    }

    
    
    public void hideIndicators()
    {
        //lungeIndicator, whirlwindIndicator, executeIndicator, fireIndicator, phalanxIndicator, rainOfArrowIndicator, quickDrawIndicator;
        lungeIndicator.SetActive(false);
        whirlwindIndicator.SetActive(false);
        executeIndicator.SetActive(false);
        fireIndicator.SetActive(false);
        phalanxIndicator.SetActive(false);
        rainOfArrowIndicator.SetActive(false);
        quickDrawIndicator.SetActive(false);
    }
    public void walk()
    {
        if (movedToNewGrid())
        {
            if (charging)
            {
                if (person.stamina < getCurrentGrid().staminaCost * 2)
                {
                    goBackToLastGrid();
                }
                else
                {
                    person.stamina -= getCurrentGrid().staminaCost * 2;
                }
            } else
            {
                if (person.stamina < getCurrentGrid().staminaCost)
                {
                    goBackToLastGrid();
                }
                else
                {
                    person.stamina -= getCurrentGrid().staminaCost;
                }
            }
            
        }
    }
    public void lunge()
    {
        if (!lungeIndicator.activeSelf)
        {
            lungeIndicator.SetActive(true);
            curIndicator = lungeIndicator;
        }
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
    }
    public void quickDraw()
    {
        if (!fireIndicator.activeSelf)
        {
            quickDrawIndicator.SetActive(true);
            curIndicator = quickDrawIndicator;
        }
        lookAtMouse(quickDrawIndicator);
    }
    public void holdSteady()
    {
        holdSteadying = !holdSteadying;
    }
    public void phalanx()
    {
        if (!phalanxIndicator.activeSelf)
        {
            phalanxIndicator.SetActive(true);
            curIndicator = phalanxIndicator;
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
    }
    public void charge()
    {
        charging = !charging;
    }



    public void doSkill(List<Grid> attackedGrid, TroopSkill skillMode)
    {
        attackedGrid = sortGridByRange(attackedGrid);
        switch (skillMode)
        {
            case TroopSkill.lunge:
                foreach (Grid g in attackedGrid)
                {
                    if (g.troop != null && g.troop != person)
                    {
                        g.troop.health -= person.getMeleeAttackDmg();
                    }
                }
                break;
            case TroopSkill.whirlwind:
                foreach (Grid g in attackedGrid)
                {
                    if (g.troop != null && g.troop != person)
                    {
                        g.troop.health -= person.getMeleeAttackDmg();
                    }
                }
                break;
            
            case TroopSkill.rainOfArrows:
                foreach (Grid g in attackedGrid)
                {
                    if (g.troop != null)
                    {
                        g.troop.health -= person.getMeleeAttackDmg();
                    }
                }
                break;
            case TroopSkill.charge:
                break;
            case TroopSkill.quickDraw:
                bool blocked = false;
                foreach (Grid g in attackedGrid)
                {
                    if (g.troop != null && !blocked && g.troop != person)
                    {
                        g.troop.health -= person.getMeleeAttackDmg();
                        blocked = true;
                    }
                }
                break;

        }
        //Debug.Log("attacked grid num: " + attackedGrid.Count);
        
    }
    public void death()
    {

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

    void showStatus()
    {

        if (person != null)
        {
            //Debug.Log("stamina and max: " + person.health + " " + person.getHealthMax());
            troopHealthBar.GetComponent<RawImage>().rectTransform.sizeDelta = new Vector2(STATUS_BAR_WIDTH, STATUS_BAR_HEIGHT * (person.health / person.getHealthMax()));
            troopStaminaBar.GetComponent<RawImage>().rectTransform.sizeDelta = new Vector2(STATUS_BAR_WIDTH, STATUS_BAR_HEIGHT * (person.stamina / person.getStaminaMax()));
            staminaTxt.GetComponent<Text>().text = person.stamina.ToString();
            healthTxt.GetComponent<Text>().text = person.health.ToString();
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
            if (pointedObj.tag == "Grid" || pointedObj.tag == "PlayerTroop" || pointedObj.tag == "EnemyTroop")
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
            if (pointedObj.tag == "Grid" || pointedObj.tag == "PlayerTroop" || pointedObj.tag == "EnemyTroop")
            {
                Vector3 pos = new Vector3(pointedObj.transform.position.x, transform.position.y, pointedObj.transform.position.z);
                obj.transform.position = Vector3.Slerp(transform.position, pos, Time.deltaTime * 1000);
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

