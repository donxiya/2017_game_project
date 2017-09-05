using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerTroop : BattleInteractable {

    
    public GameObject controlPanel;
    public Person person { get; set; }
    public Grid curGrid { get; set; }

    public GameObject troopStaminaBar, troopHealthBar, staminaTxt, healthTxt;
    public Texture2D staminaBarImg, troopHealthBarImg;
    public bool controlled;

    NavMeshAgent navMeshAgent;
    Grid lastGrid;
    // Use this for initialization
    public void Start()
    {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }
    public void Update() {
        if (BattleCentralControl.playerTurn && controlled)
        {
            if (movedToNewGrid())
            {
                Debug.Log("stamina: " + person.stamina);
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
        if (!grid.occupied)
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
            lastGrid = curGrid;
            curGrid = getCurrentGrid();
            return true;
        }
        return false;
    }

    
    void showStatus()
    {
        
        if (person != null)
        {
            //Debug.Log("stamina and max: " + person.health + " " + person.getHealthMax());
            troopHealthBar.GetComponent<RawImage>().rectTransform.sizeDelta = new Vector2(.5f, Mathf.RoundToInt(3 * (person.health / person.getHealthMax())));
            troopStaminaBar.GetComponent<RawImage>().rectTransform.sizeDelta = new Vector2(.5f, 3 * (person.stamina / person.getStaminaMax()));
            //staminaTxt.GetComponent<Text>().text = person.stamina.ToString();
        }
    }
}

