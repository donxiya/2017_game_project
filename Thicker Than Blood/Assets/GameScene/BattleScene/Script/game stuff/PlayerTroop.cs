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
    public GameObject curIndicator, lungeIndicator;

    public Texture2D staminaBarImg, troopHealthBarImg;
    public bool controlled;
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
    }
    public void Update() {
        if (BattleCentralControl.playerTurn && controlled)
        {
            if (movedToNewGrid())
            {
                //Debug.Log("stamina: " + person.stamina);
                if (person.stamina < getCurrentGrid().staminaCost)
                {
                    goBackToLastGrid();
                } else
                {
                    
                    person.stamina -= getCurrentGrid().staminaCost;
                }
            }
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
            navMeshAgent.destination = new Vector3(grid.x, 1, grid.z);
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
        lungeIndicator.SetActive(false);
    }
    public void lunge()
    {
        lungeIndicator.SetActive(true);
        curIndicator = lungeIndicator;
        lookAtMouse(lungeIndicator);
    }

    public void makeDamage(List<Grid> attackedGrid)
    {
        //Debug.Log("attacked grid num: " + attackedGrid.Count);
        foreach (Grid g in attackedGrid)
        {
            if (g.troop != null)
            {
                attack(g.troop, BattleInteraction.skillMode);
            }
        }
    }
    public void attack(Person attacked, TroopSkill skillMode)
    {
        attacked.health -= person.getMeleeAttackDmg();
    }
    public void death()
    {

    }
    public List<Grid> indicatedGrid()
    {
        return curIndicator.GetComponent<Indicator>().collided;
    }
    void showStatus()
    {

        if (person != null)
        {
            //Debug.Log("stamina and max: " + person.health + " " + person.getHealthMax());
            troopHealthBar.GetComponent<RawImage>().rectTransform.sizeDelta = new Vector2(STATUS_BAR_WIDTH, STATUS_BAR_HEIGHT * (person.health / person.getHealthMax()));
            troopStaminaBar.GetComponent<RawImage>().rectTransform.sizeDelta = new Vector2(STATUS_BAR_WIDTH, STATUS_BAR_HEIGHT * (person.stamina / person.getStaminaMax()));
            staminaTxt.GetComponent<Text>().text = person.stamina.ToString();
            healthTxt.GetComponent<Text>().text = person.name;
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
            if (pointedObj.tag == "Grid")
            {
                Vector3 v = pointedObj.transform.position - obj.transform.position;
                v.x = v.z = 0.0f;
                obj.transform.LookAt(pointedObj.transform.position - v);
                obj.transform.Rotate(0, 180, 0);
            }
            
        }
    }

}

