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
        Selection.currentSelection = 2;
        Selection.selected = true;
        SceneManager.LoadScene("TabScene");
    }
    void factionLink()
    {
        Selection.currentSelection = 3;
        Selection.selected = true;
        SceneManager.LoadScene("TabScene");
    }
    void troopLink()
    {
        Selection.currentSelection = 4;
        Selection.selected = true;
        SceneManager.LoadScene("TabScene");
    }

    void inventoryLink()
    {
        Selection.currentSelection = 5;
        Selection.selected = true;
        SceneManager.LoadScene("TabScene");
    }
}
