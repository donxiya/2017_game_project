using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleCentralControl : MonoBehaviour {
    

    public static bool battleStart, playerTurn;
    public static Grid[,] map;
    public static int gridXMax, gridZMax, curRound, oldRound;
    public static MainParty playerParty;
    public static Dictionary<Person, GameObject> playerTroopOnField, enemyTroopOnField;
    public static Party enemyParty;
    public static int playerTotal, enemyTotal;
    //public static Dictionary<Grid, GameObject> gridToObj;
    //public static Dictionary<GameObject, Grid> objToGrid;
    bool groundInitialized = false;
    private void Awake()
    {
        //gridToObj = new Dictionary<Grid, GameObject>();
        //objToGrid = new Dictionary<GameObject, Grid>();
        playerTroopOnField = new Dictionary<Person, GameObject>();
        enemyTroopOnField = new Dictionary<Person, GameObject>();
        playerTurn = true;
        battleStart = false;
    }
    // Use this for initialization
    void Start()
    {
        
        

    }

    // Update is called once per frame
    void Update()
    {
        if (!groundInitialized && playerParty != null && enemyParty != null)
        {
            gridXMax = playerParty.partySize + enemyParty.partySize;
            gridZMax = playerParty.partySize + enemyParty.partySize;
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
                mapGridDecider(ix, iz);
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
    GameObject mapGridDecider(int x, int z)
    {
        int rand = (int) Random.Range(1, 4);
        GameObject model;
        if (rand == 1)
        {
            model = GameObject.Find("tree");
            var temp = new Grid(x, z, model, GridType.tree);
            map[x, z] = temp;
        }
        else if (rand == 2)
        {
            model = GameObject.Find("rockAndTree");
            var temp = new Grid(x, z, model, GridType.rockAndTree);
            map[x, z] = temp;
        }
        else
        {
            model = GameObject.Find("flatGrass");
            var temp = new Grid(x, z, model, GridType.flatGrass);
            map[x, z] = temp;
        }
        return model;
    }
    void groundInitialization()
    {
        GameObject.Find("flatGrass").SetActive(false);
        GameObject.Find("rockAndTree").SetActive(false);
        GameObject.Find("tree").SetActive(false);
        //Terrain terrain = ground.GetComponent<Terrain>();
        //ground.GetComponent<MeshRenderer>().enabled = false;
    }
}

public enum GridType
{
    rockAndTree,
    flatGrass,
    tree
}