using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuPanelControl : MonoBehaviour {


    private GameObject statsButton;
    private GameObject objectiveButton;
    private GameObject troopButton;
    private GameObject inventoryButton;
	// Use this for initialization
	void Start () {
        objectiveButton = GameObject.Find("Objective");
        objectiveButton.GetComponent<Button>().onClick.AddListener(delegate () { this.objectiveLink(); });
        statsButton = GameObject.Find("Faction");
        statsButton.GetComponent<Button>().onClick.AddListener(delegate () { this.factionLink(); });
        troopButton = GameObject.Find("Troop");
        troopButton.GetComponent<Button>().onClick.AddListener(delegate () { this.troopLink(); });
        inventoryButton = GameObject.Find("Inventory");
        inventoryButton.GetComponent<Button>().onClick.AddListener(delegate () { this.inventoryLink(); });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    void objectiveLink()
    {
        //TabMenu.currentSelection = 2;
        //TabMenu.selected = true;
    }
    void factionLink()
    {
        //TabMenu.currentSelection = 3;
        //TabMenu.selected = true;
    }
    void troopLink()
    {
        //TabMenu.currentSelection = 4;
        //TabMenu.selected = true;
    }

    void inventoryLink()
    {
        //TabMenu.currentSelection = 5;
        //TabMenu.selected = true;
    }
}
