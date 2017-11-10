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
    public static Dictionary<Grid, GameObject> gridToObj;
    public static Dictionary<GameObject, Grid> objToGrid;
    bool groundInitialized = false;
    private void Awake()
    {
        gridToObj = new Dictionary<Grid, GameObject>();
        objToGrid = new Dictionary<GameObject, Grid>();
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
            BattleCamera.mapCenter = gridToObj[map[gridXMax / 2, gridZMax / 2]];
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
                }
            }
            BattleInteraction.curControlled = null;
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
                }
            }
            foreach(KeyValuePair<Person, GameObject> pair in BattleCentralControl.playerTroopOnField)
            {
                BattleInteraction.curControlled = pair.Value;
                break;
            }
        }


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
        foreach (Grid g in map)
        {
            for (int dx = 0; dx < 3; dx++)
            {
                for (int dz = 0; dz < 3; dz++)
                {

                    int tx = g.x - 1 + dx;
                    int tz = g.z - 1 + dz;
                    if (tx >= 0 && tx < gridXMax && tz >= 0 && tz < gridZMax)
                    {
                        Grid curG = BattleCentralControl.map[tx, tz];
                        if ((tx != g.x || tz != g.z) && !g.neighbors.Contains(curG))
                        {
                            g.neighbors.Add(curG);
                            
                        }
                        
                    }
                }
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
                obj.GetComponent<GridObject>().becomeUnseen();
                if (iz <= BattleCentralControl.playerParty.leader.getTroopPlacingRange(BattleCentralControl.gridZMax))
                {
                    obj.GetComponent<GridObject>().becomeSeen();
                }
                gridToObj.Add(map[ix, iz], obj);
                objToGrid.Add(obj, map[ix, iz]);
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
