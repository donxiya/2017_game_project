using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleCentralControl : MonoBehaviour {
    

    public static bool battleStart, playerTurn;
    public static Grid[,] map;
    public static int gridXMax, gridZMax, curRound, oldRound;
    public static MainParty playerParty;
    public static Party enemyParty;
    public static List<BattlefieldType> battlefieldTypes;
    public static Dictionary<Person, GameObject> playerTroopOnField, enemyTroopOnField;
    
    public static int playerTotal, enemyTotal;
    //ADD NEW GRID
    public GameObject flatGrass, rockAndTree, singleTree, deadTree, rockyPlain, fence;
    List<GridType> woods, farmland, mountain, grassland, hills, marsh, city, common;
    bool groundInitialized = false;
    private void Awake()
    {
        //gridToObj = new Dictionary<Grid, GameObject>();
        //objToGrid = new Dictionary<GameObject, Grid>();
        playerTroopOnField = new Dictionary<Person, GameObject>();
        enemyTroopOnField = new Dictionary<Person, GameObject>();
        if (battlefieldTypes == null || battlefieldTypes.Count == 0)
        {
            battlefieldTypes = new List<BattlefieldType>();
            battlefieldTypes.Add(BattlefieldType.Common);
        }
        
        playerTurn = true;
        battleStart = false;
    }
    // Use this for initialization
    void Start()
    {
        categorizeGridTypes();
    }

    // Update is called once per frame
    void Update()
    {
        if (!groundInitialized && playerParty != null && enemyParty != null)
        {
            gridXMax = playerParty.partyMember.Count + enemyParty.partyMember.Count;
            gridZMax = playerParty.partyMember.Count + enemyParty.partyMember.Count;
            map = new Grid[gridXMax, gridZMax];
            generateMap(gridXMax, gridZMax);
            placeOnMap(gridXMax, gridZMax);
            groundInitialization();
            groundInitialized = true;
            BattleCamera.mapCenter = map[gridXMax / 2, gridZMax / 2].gridObject;
            BattleCamera.target = BattleCamera.mapCenter;
        }
    }

    public static void endTurnPrep()
    {
        if (playerTurn)
        {
            foreach (KeyValuePair<Person, GameObject> t in BattleCentralControl.playerTroopOnField)
            {
                Troop troop = t.Value.GetComponent<Troop>();
                if (troop.activated)
                {
                    t.Key.stamina = t.Key.getStaminaMax();
                    troop.stealthCheckRefresh();
                    troop.charging = false;
                    troop.holdSteadying = false;
                }
            }
            foreach (KeyValuePair<Person, GameObject> t in BattleCentralControl.enemyTroopOnField)
            {
                Troop troop = t.Value.GetComponent<Troop>();
                if (troop.activated)
                {
                    troop.stealthCheckRefresh();
                    troop.clearGuard();
                }
            }
            if (BattleInteraction.curControlled != null)
            {
                BattleInteraction.curControlled.GetComponent<Troop>().cameraFocusOnExit();
                BattleInteraction.curControlled = null;
            }
        }
        if (!playerTurn)
        {
            foreach (KeyValuePair<Person, GameObject> t in BattleCentralControl.enemyTroopOnField)
            {
                Troop troop = t.Value.GetComponent<Troop>();
                if (troop.activated)
                {
                    t.Key.stamina = t.Key.getStaminaMax();
                    troop.stealthCheckRefresh();
                    troop.charging = false;
                    troop.holdSteadying = false;
                }
            }
            foreach (KeyValuePair<Person, GameObject> t in BattleCentralControl.playerTroopOnField)
            {
                Troop troop = t.Value.GetComponent<Troop>();
                if (troop.activated)
                {
                    troop.stealthCheckRefresh();
                    troop.clearGuard();
                }
            }
        }
    }
    public static void startTurnPrep()
    {
        if (playerTurn)
        {
            foreach (KeyValuePair<Person, GameObject> pair in BattleCentralControl.playerTroopOnField)
            {
                BattleInteraction.curControlled = pair.Value;
                BattleInteraction.curControlled.GetComponent<Troop>().cameraFocusOn();
                break;
            }
        }
        if (!playerTurn)
        {
        }
    }
    public static void endBattle()
    {
        //SaveLoadSystem.saveLoadSystem.tempSave();
        foreach(Person p in BattleCentralControl.playerParty.partyMember)
        {
            p.inBattle = false;
        }
        foreach (Person p in BattleCentralControl.enemyParty.partyMember)
        {
            p.inBattle = false;
        }
        BattleInspectPanel.person = null;
        GridInspectPanel.grid = null;
        MapManagement.parties.Remove(BattleCentralControl.enemyParty);
        SaveLoadSystem.saveLoadSystem.tempSave();
        //Debug.Log(SaveLoadSystem.saveLoadSystem.mainParty.name);
        SceneManager.LoadScene("MapScene");
        SceneManager.UnloadSceneAsync("BattleScene");
        MapManagement.mapManagement.endOfBattle(BattleCentralControl.enemyParty, EndTurnPanel.battleResult);
        
    }
    public static List<GameObject> gridInLine(GameObject start, GameObject end)
    {
        List<GameObject> result = new List<GameObject>();
        RaycastHit[] hits;
        var direction = end.transform.position - start.transform.position;
        var distance = Vector3.Distance(start.transform.position, end.transform.position);
        hits = Physics.RaycastAll(start.transform.position, direction, distance);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            result.Add(hit.transform.gameObject);
        }
        return result;
    }
    void generateMap(int x, int z)
    {
        for (int ix = 0; ix < x; ix++)
        {
            for (int iz = 0; iz < z; iz++)
            {
                mapGridDecider(ix, iz, battlefieldTypes);
            }
        }
    }
    void placeOnMap(int x, int z)
    {
        for (int ix = 0; ix < x; ix++)
        {
            for (int iz = 0; iz < z; iz++)
            {
                var pos = new Vector3(ix, 0, iz);
                var rot = new Quaternion(0, 0, 0, 0);
                var obj = Instantiate(map[ix, iz].mapSettingModel, pos, rot);
                map[ix, iz].gridObject = obj;
                obj.GetComponent<GridObject>().grid = map[ix, iz];
                obj.GetComponent<GridObject>().becomeUnseen();
                if (iz <= BattleCentralControl.playerParty.leader.getTroopPlacingRange(BattleCentralControl.gridZMax))
                {
                    obj.GetComponent<GridObject>().becomeSeen();
                }
                
                //gridToObj.Add(map[ix, iz], obj);
                //objToGrid.Add(obj, map[ix, iz]);
            }
        }
    }
    void mapGridDecider(int x, int z, List<BattlefieldType> bt)
    {
        int rand = Random.Range(0, bt.Count);
        List<GridType> gridTypesToCreate;
        switch(bt[rand])
        {
            case BattlefieldType.City:
                gridTypesToCreate = city;
                break;
            case BattlefieldType.Farmland:
                gridTypesToCreate = farmland;
                break;
            case BattlefieldType.Grassland:
                gridTypesToCreate = grassland;
                break;
            case BattlefieldType.Hills:
                gridTypesToCreate = hills;
                break;
            case BattlefieldType.Marsh:
                gridTypesToCreate = marsh;
                break;
            case BattlefieldType.Mountain:
                gridTypesToCreate = mountain;
                break;
            case BattlefieldType.Woods:
                gridTypesToCreate = woods;
                break;
            default:
                gridTypesToCreate = common;
                break;

        }
        Grid temp = makeLandscape(x, z, gridTypesToCreate);
        //ADD NEW GRID
        
        map[x, z] = temp;
    }
    Grid makeLandscape(int x, int z, List<GridType> gridTypes)
    {
        int rand = (int)Random.Range(0, gridTypes.Count);
        Grid temp = new Grid(x, z, flatGrass, GridType.flatGrass);
        switch (gridTypes[rand])
        {
            case GridType.flatGrass:
                temp = new Grid(x, z, rockAndTree, GridType.flatGrass);
                break;
            case GridType.rockAndTree:
                temp = new Grid(x, z, rockAndTree, GridType.rockAndTree);
                break;
            case GridType.deadTree:
                temp = new Grid(x, z, deadTree, GridType.deadTree);
                break;
            case GridType.singleTree:
                temp = new Grid(x, z, singleTree, GridType.singleTree);
                break;
            case GridType.rockyPlain:
                temp = new Grid(x, z, rockyPlain, GridType.rockyPlain);
                break;
            case GridType.fence:
                temp = new Grid(x, z, fence, GridType.fence);
                break;
        }
        return temp;
    }
    
    void groundInitialization()
    {
        flatGrass.SetActive(false);
        rockAndTree.SetActive(false);
        singleTree.SetActive(false);
        deadTree.SetActive(false);
        rockyPlain.SetActive(false);
        fence.SetActive(false);
        //Terrain terrain = ground.GetComponent<Terrain>();
        //ground.GetComponent<MeshRenderer>().enabled = false;
    }
    //ADD NEW GRID
    void categorizeGridTypes()
    {
        woods = new List<GridType>();
        woods.AddRange(new GridType[] { GridType.rockAndTree, GridType.singleTree });

        farmland = new List<GridType>();
        farmland.AddRange(new GridType[] { GridType.flatGrass, GridType.fence, GridType.rockAndTree, GridType.singleTree });

        mountain = new List<GridType>();
        mountain.AddRange(new GridType[] { GridType.rockAndTree, GridType.singleTree, GridType.rockyPlain });

        grassland = new List<GridType>();
        grassland.AddRange(new GridType[] { GridType.flatGrass, GridType.fence, GridType.singleTree });

        hills = new List<GridType>();
        hills.AddRange(new GridType[] { GridType.flatGrass, GridType.rockAndTree, GridType.singleTree });

        marsh = new List<GridType>();
        marsh.AddRange(new GridType[] { GridType.flatGrass});

        city = new List<GridType>();
        city.AddRange(new GridType[] { GridType.fence });

        common = new List<GridType>();
        common.AddRange(new GridType[] { GridType.flatGrass, GridType.fence, GridType.rockAndTree, GridType.singleTree, GridType.rockyPlain });
    }
}
//ADD NEW GRID
public enum GridType
{
    flatGrass,
    rockAndTree,
    singleTree,
    deadTree,
    rockyPlain,
    fence
}