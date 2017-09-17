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
    public static bool panelOut;
    public bool initialized, placing;
    private void Awake()
    {
        gameObject.SetActive(false);
        
        panelOut = false;
        initialized = false;
        troopDict = new Dictionary<Person, GameObject>();
        troopPlacedDict = new Dictionary<Person, bool>();
    }
    // Use this for initialization
    void Start () {
		
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

    void showPlacingPanel (bool show)
    {
        if (show)
        {
            transform.position = Vector3.Slerp(transform.position, transform.position + new Vector3(150, 0, 0), Time.deltaTime * .1f);
        } else {
            transform.position = Vector3.Slerp(transform.position, transform.position + new Vector3(-150, 0, 0), Time.deltaTime * .1f);
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
                //TODO: restrain available spots
                Grid gridInfo = BattleCentralControl.objToGrid[interactedObject];
                if (!gridInfo.occupied && !unit.inBattle)
                {
                    var pos = new Vector3(gridInfo.x, 1, gridInfo.z);
                    var rot = new Quaternion(0, 0, 0, 0);
                    GameObject unitToPlace = GameObject.Find("MainCharacter");
                    unitToPlace = interactedObject.GetComponent<GridObject>().placeTroopOnGrid(unitToPlace, pos, rot);
                    //unitToPlace.GetComponent<PlayerTroop>().person = unit;
                    //unitToPlace.GetComponent<PlayerTroop>().curGrid = gridInfo;
                    //unitToPlace.GetComponent<PlayerTroop>().person.stamina = unitToPlace.GetComponent<PlayerTroop>().person.getStaminaMax();
                    unitToPlace.GetComponent<PlayerTroop>().placed(unit, gridInfo);
                    
                    gridInfo.occupied = true;
                    unit.inBattle = true;
                    return true;
                }
                
                
            } else
            {
                Info.displayInfo("Please click on grids");
            }
        }
        return false;
    }
}
