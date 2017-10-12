using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Selection : MonoBehaviour {

    public GameObject sapePanel;
    public GameObject ciPanel;
    public GameObject factionPanel;
    public GameObject troopPanel;
    public GameObject inventoryPanel;
    public GameObject objectivePanel;

    private static string[] selectionList;

    public static int currentSelection = 0;
    public static bool selected = false;

	// Use this for initialization
	void Start () {
        
        selectionList = new string[] {"sape", "ci", "objective", "faction", "troop", "inventory" };

        

        initializtion();
        makeSelection();
	}

    // Update is called once per frame
    void Update() {
        if (gameObject.activeSelf)
        {
            if (Time.timeScale != 0.0f)
            {
                Time.timeScale = 0.0f;
            }
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("tab"))
            {
                gameObject.SetActive(false);
            }
            

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                selectRight();
                makeSelection();
            }

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                selectLeft();
                makeSelection();
            }
        }
    }


    void makeSelection()
    {
        if (selected)
        {
            switch (selectionList[currentSelection])
            {
                case "sape":
                    sapePanel.SetActive(true);
                    ciPanel.SetActive(false);
                    factionPanel.SetActive(false);
                    objectivePanel.SetActive(false);
                    troopPanel.SetActive(false);
                    inventoryPanel.SetActive(false);
                    break;
                case "ci":
                    sapePanel.SetActive(false);
                    ciPanel.SetActive(true);
                    factionPanel.SetActive(false);
                    objectivePanel.SetActive(false);
                    troopPanel.SetActive(false);
                    inventoryPanel.SetActive(false);
                    break;
                case "faction":
                    sapePanel.SetActive(false);
                    ciPanel.SetActive(false);
                    factionPanel.SetActive(true);
                    objectivePanel.SetActive(false);
                    troopPanel.SetActive(false);
                    inventoryPanel.SetActive(false);
                    break;
                case "objective":
                    sapePanel.SetActive(false);
                    ciPanel.SetActive(false);
                    factionPanel.SetActive(false);
                    objectivePanel.SetActive(true);
                    troopPanel.SetActive(false);
                    inventoryPanel.SetActive(false);
                    break;
                case "troop":
                    sapePanel.SetActive(false);
                    ciPanel.SetActive(false);
                    factionPanel.SetActive(false);
                    objectivePanel.SetActive(false);
                    troopPanel.SetActive(true);
                    inventoryPanel.SetActive(false);
                    break;
                case "inventory":
                    sapePanel.SetActive(false);
                    ciPanel.SetActive(false);
                    factionPanel.SetActive(false);
                    objectivePanel.SetActive(false);
                    troopPanel.SetActive(false);
                    inventoryPanel.SetActive(true);
                    break;
            }
        } else
        {
            sapePanel.SetActive(false);
            ciPanel.SetActive(false);
            factionPanel.SetActive(false);
            objectivePanel.SetActive(false);
            troopPanel.SetActive(false);
            inventoryPanel.SetActive(false);
        }
    }

    void selectRight()
    {
        if (currentSelection == selectionList.Length - 1)
        {
            currentSelection = 0;
        }else
        {
            currentSelection++;
        }
    }

    void selectLeft()
    {
        if (currentSelection == 0)
        {
            currentSelection = selectionList.Length - 1;
        }
        else
        {
            currentSelection--;
        }
    }
    
    

    void initializtion()
    {
        gameObject.SetActive(false);
        sapePanel.SetActive(false);
        ciPanel.SetActive(false);
        factionPanel.SetActive(false);
        objectivePanel.SetActive(false);
        troopPanel.SetActive(false);
        inventoryPanel.SetActive(false);
    }
}
