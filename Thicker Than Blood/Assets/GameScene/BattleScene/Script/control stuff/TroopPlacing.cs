using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TroopPlacing : MonoBehaviour {
    public GameObject troopUnit;
    public Text troopUnitName, troopUnitLevel;
    public RawImage troopIcon, troopBackground, maxHealthBar, healthBar,
        maxStamina, stamina;
    public Texture2D unplacedBackground, placedBackground;
    public static List<Person> battleTroop;
    public Dictionary<Person, GameObject> troopDict;
    public Dictionary<Person, bool> troopPlacedDict;
    public Person placingUnit;
    public static bool pointerInPlacingPanel = false;
    public bool initialized, placing, showing;
    public Animator animator;
    private void Awake()
    {
        gameObject.SetActive(false);
        
        initialized = false;
        showing = false;
        troopDict = new Dictionary<Person, GameObject>();
        troopPlacedDict = new Dictionary<Person, bool>();
    }
    // Use this for initialization
    void Start () {
        showing = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (BattleCentralControl.battleStart && gameObject.activeSelf)
        {
            if (!initialized)
            {
                troopPlacingInitialization();
            }
            if (placing && Input.anyKeyDown)
            {
                if (Input.GetMouseButton(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    if (placeTroop(placingUnit))
                    {
                        troopDict[placingUnit].transform.Find("TroopUnitBackground").GetComponent<RawImage>().texture = placedBackground;
                    }

                }
                else
                {
                    placing = false;
                }

            }
        }
        
	}
    public void troopPlacingInitialization()
    {
        foreach (Person p in battleTroop)
        {
            addToPlacing(p);
        }
        troopUnit.SetActive(false);
        initialized = true;
    }
    void addToPlacing(Person unit)
    {
        troopUnitName.text = unit.name;
        troopUnitLevel.text = unit.exp.level.ToString();
        troopBackground.texture = unplacedBackground;
        GameObject newTroopUnit = Object.Instantiate(troopUnit);
        newTroopUnit.GetComponent<Button>().onClick.AddListener(delegate { placingTroopUnitOnClick(unit); });
        // BattleInteraction.curControlled.GetComponent<PlayerTroop>().hideIndicators();
        troopDict.Add(unit, newTroopUnit);
        troopPlacedDict.Add(unit, false);
        newTroopUnit.transform.SetParent(troopUnit.transform.parent, false);
    }

    void placingTroopUnitOnClick(Person unit)
    {
        if (!troopPlacedDict[unit]) //if havent placed on map
        {
            placingUnit = unit;
            placing = true;
        } else
        {
            //BattleCamera.target   
        }
    }
    
    bool placeTroop(Person unit)
    {
        //write wait for click
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;
        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
        {
            GameObject interactedObject = interactionInfo.collider.gameObject.transform.parent.gameObject;

            if (interactedObject.tag == "Grid" )
            {
                Grid gridInfo = BattleCentralControl.objToGrid[interactedObject];
                if (gridInfo.z <= BattleCentralControl.playerParty.leader.getTroopPlacingRange(BattleCentralControl.gridZMax))
                {
                    Info.clearInfo();
                    if (!gridInfo.occupied && !unit.inBattle)
                    {
                        var pos = new Vector3(gridInfo.x, 1.5f, gridInfo.z);
                        var rot = new Quaternion(0, 0, 0, 0);
                        GameObject unitToPlace = TroopDataBase.troopDataBase.getTroopObject(unit.faction, unit.troopType, unit.ranking);
                        if (unitToPlace == null)
                        {
                            Debug.Log(unit.name);
                        }
                        var duplicatedUnitToPlace = interactedObject.GetComponent<GridObject>().placeTroopOnGrid(unitToPlace, pos, rot);
                        duplicatedUnitToPlace.GetComponent<Troop>().placed(unit, gridInfo);
                        unit.inBattle = true;
                        placing = false;
                        return true;
                    }
                } else
                {
                    Info.displayInfo("Please click on allowed grids" + BattleCentralControl.playerParty.leader.getTroopPlacingRange(BattleCentralControl.gridZMax));
                }
                
            } else
            {
                Info.displayInfo("Please click on grids");
            }
        }
        return false;
    }
    public void showHidePanel()
    {
        showing = !showing;
        animator.SetBool("showPanel", showing);
    }
    public void pointerEnterPlacing()
    {
        pointerInPlacingPanel = true;
    }
    public void pointerExitPlacing()
    {
        pointerInPlacingPanel = false;
    }
}
