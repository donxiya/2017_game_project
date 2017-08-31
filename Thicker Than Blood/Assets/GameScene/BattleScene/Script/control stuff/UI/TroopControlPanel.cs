using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TroopControlPanel : MonoBehaviour {
    public GameObject panel, walkButton, rainOfArrows, quickDraw,
        flay;
    public GameObject curControledTroop;
    public bool initialized;
	// Use this for initialization
	void Start () {
        walkButton.GetComponent<Button>().onClick.AddListener(delegate { walk(); });
        panel.SetActive(false);
        initialized = false;
	}
	
	// Update is called once per frame
	void Update () {
        /**if (curControledTroop != null)
        {
            if (!panel.activeSelf && !initialized)
            {
                switch (getInfo(curControledTroop).troopType)
                {
                    case TroopType.mainCharType:
                        initializeMain();
                        break;
                    case TroopType.recruitType:
                        break;

                }
                initialized = true;
            }
        }
        
        if (panel.activeSelf && initialized)
        {
            initialized = false;
        }**/
	}

    Person getInfo(GameObject troop)
    {
        Debug.Log("info " + troop.GetComponent<PlayerTroop>().person.name);
        return troop.GetComponent<PlayerTroop>().person;
    }

    void initializePanel()
    {
        
        panel.SetActive(true);
    }
    void hidePanel()
    {
        panel.SetActive(false);
    }

    void walk()
    {
        curControledTroop.GetComponent<PlayerTroop>().showMoveRange();
        BattleInteraction.skillMode = TroopSkill.walk;
    }
}
