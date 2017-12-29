using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManagement : MonoBehaviour {
    public static MapManagement mapManagement;
    
    public GameObject[] banditSpawnPointList, franceSpawnPointList, papacySpawnPointList, empireSpawnPointList;
    public GameObject banditTroop, frenchTroop, papalTroop, italianTroop, imperialTroop;
    public GameObject defaultCity;
    bool finishingBattle = false;
    
    public bool finishedLoading = true;
    const int spawnRange = 20;
    int monthSCounter = TimeSystem.month;
    int monthECounter = TimeSystem.month;
    public List<Party> partiesToBeRemoved = new List<Party>();
    public static List<Party> parties;
    public static List<City> cities;
    public static List<Town> towns;
    public static bool goingToLoot, playerRespawn;
    private void Awake()
    {
        finishedLoading = false;
        mapManagement = this;
        if (cities == null)
        {
            cities = new List<City>();
        }
        if (towns == null)
        {
            towns = new List<Town>();
        }
        if (parties == null)
        {
            parties = new List<Party>();
        }
        

    }
    void Start () {

        //

        SaveLoadSystem.saveType = SaveType.newGame;
        if (SaveLoadSystem.saveType == SaveType.newGame)
        {
            cityInitialization();
            banditInitialization();
            franceInitialization();

        }
        else
        {
            SaveLoadSystem.saveLoadSystem.tempLoad();
            cityInitialization();
        }
        if (!finishingBattle)
        {
            loadParties();
        }
        finishedLoading = true;
        SaveLoadSystem.saveLoadSystem.tempSave();
        SaveLoadSystem.saveType = SaveType.tempSave;
    }
	
	// Update is called once per frame
	void Update () {
        if (mapManagement != null && finishedLoading)
        {
            monthSCounter = TimeSystem.hour;
            if (monthSCounter != monthECounter)
            {
                //banditUpdate();
            }
            monthECounter = TimeSystem.hour;
            if (finishingBattle && TabMenu.tabMenu != null)
            {
                loadParties();
                finishingBattle = false;
                //TabMenu.tabMenu.showMarket(true);
            }
            if (goingToLoot && TabMenu.tabMenu != null)
            {
                InventoryManagement.managementMode = InventoryManagementMode.looting;
                TabMenu.tabMenu.showMarket(true);
                goingToLoot = false;
            }
            if (playerRespawn && WorldInteraction.worldInteraction.player != null && WorldInteraction.worldInteraction.playerAgent.enabled)
            {
                City nearestCity = getNearestCity(WorldInteraction.worldInteraction.player.transform.position);
                WorldInteraction.worldInteraction.playerAgent.Warp(new Vector3(nearestCity.position.x + 5.0f, 3.0f, nearestCity.position.z + 5.0f));
                
                nearestCity.hasInteracted = true;
                playerRespawn = false;
            }
        }

    }

    void cityInitialization()
    {
        buildCity("Milano", defaultCity, new Vector3(850/2, 3, 730/2), 1000, 20000);
        buildCity("Torino", defaultCity, new Vector3(720/2, 3, 970/2), 1000, 20000);
        buildCity("Asti", defaultCity, new Vector3(700/2, 3, 850/2), 1000, 20000);
        buildCity("Parma", defaultCity, new Vector3(610/2, 3, 740/2), 1000, 20000);
        buildCity("Genova", defaultCity, new Vector3(620/2, 3, 770/2), 1000, 20000);
        buildCity("Modena", defaultCity, new Vector3(790/2, 3, 550/2), 1000, 20000);
        buildCity("Verona", defaultCity, new Vector3(880/2, 3, 540/2), 1000, 20000);
        buildCity("Padova", defaultCity, new Vector3(850/2, 3, 400/2), 1000, 20000);
        buildCity("Treviso", defaultCity, new Vector3(940/2, 3, 270/2), 1000, 20000);
        buildCity("Venezia", defaultCity, new Vector3(860/2, 3, 200/2), 1000, 20000);
        buildCity("Ferrara", defaultCity, new Vector3(730/2, 3, 400/2), 1000, 20000);
        buildCity("Bologna", defaultCity, new Vector3(630/2, 3, 380/2), 1000, 20000);
        buildCity("Firenze", defaultCity, new Vector3(480/2, 3, 500/2), 1000, 20000);
        buildCity("Ravenna", defaultCity, new Vector3(620/2, 3, 330/2), 1000, 20000);
        buildCity("Urbino", defaultCity, new Vector3(470/2, 3, 290/2), 1000, 20000);
        buildCity("Lucca", defaultCity, new Vector3(530/2, 3, 580/2), 1000, 20000);
        buildCity("Pisa", defaultCity, new Vector3(450/2, 3, 580/2), 1000, 20000);
        buildCity("Siena", defaultCity, new Vector3(400/2, 3, 450/2), 1000, 20000);
        buildCity("Grosseto", defaultCity, new Vector3(180/2, 3, 320/2), 1000, 20000);
        buildCity("Perugia", defaultCity, new Vector3(350/2, 3, 320/2), 1000, 20000);
        buildCity("Roma", defaultCity, new Vector3(100/2, 3, 200/2), 1000, 20000);
    }
    void buildCity(string cityName, GameObject obj, Vector3 location, int guardBattleValue, int cash)
    {
        var rot = new Quaternion(0, Random.Range(0, 360), 0, 0);
        var spawned = Instantiate(obj, location, rot);
        spawned.transform.SetParent(obj.transform.parent, false);
        spawned.SetActive(true);
        City spawnedCity = spawned.GetComponent<City>();
        spawnedCity.lName = cityName;
        spawnedCity.position = location;
        Party cityGuard = new Party(cityName + " Guard", Faction.italy, guardBattleValue);
        cityGuard.hasShape = false;
        cityGuard.belongedCity = spawnedCity;
        spawned.GetComponent<City>().cityGuard = cityGuard;
        Party cityTrader = new Party(cityName + " Trader", Faction.italy, 100);
        cityTrader.hasShape = false;
        cityTrader.belongedCity = spawnedCity;
        initializeTraderInventory(cityTrader);
        cityTrader.cash = 20000;
        cityTrader.inventory = new List<Item>();
        initializeTraderInventory(cityTrader);
        spawnedCity.cityTrader = cityTrader;
        spawnedCity.warehouse = new List<Item>();
        cities.Add(spawned.GetComponent<City>());
        loadCitySave();
        //if (SaveLoadSystem.saveLoadSystem != null)
        //{
        //    loadCitySave();
        //
        //} else
        //{
            
        //}
    }
    void buildTown(string townName, GameObject obj, Vector3 location, int guardBattleValue, int cash)
    {
        var rot = new Quaternion(0, Random.Range(0, 360), 0, 0);
        var spawned = Instantiate(obj, location, rot);
        spawned.transform.SetParent(obj.transform.parent, false);
        spawned.SetActive(true);
        Town spawnedTown = spawned.GetComponent<Town>();
        Town existingTown = null; // findTown(townName);
        if (existingTown != null)
        {
            spawnedTown.townGuard = existingTown.townGuard;
            spawnedTown.townGuard.belongedTown = spawnedTown;
            spawnedTown.townTrader = existingTown.townTrader;
            spawnedTown.townTrader.belongedTown = spawnedTown;
            towns.Remove(existingTown);
            towns.Add(spawnedTown);

        }
        else
        {
            Party townGuard = new Party(townName + " Guard", Faction.italy, guardBattleValue);
            townGuard.hasShape = false;

            townGuard.belongedTown = spawned.GetComponent<Town>();
            spawned.GetComponent<Town>().townGuard = townGuard;
            Party townTrader = new Party(townName + " Trader", Faction.italy, 100);
            townTrader.hasShape = false;
            townTrader.belongedCity = spawned.GetComponent<City>();
            initializeTraderInventory(townTrader);
            townTrader.cash = 20000;
            townTrader.inventory = new List<Item>();
            initializeTraderInventory(townTrader);
            spawned.GetComponent<Town>().townTrader = townTrader;
            towns.Add(spawned.GetComponent<Town>());
        }
    }
    void initializeTraderInventory(Party cityTrader)
    {
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Parchment"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Wool"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Pottery"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Hemp"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Medicinal Liqueur"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Rose"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Majolica"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Lace"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Embroidery"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Livestock"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Bronzeware"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Leatherware"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Ale"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Wine"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Cheese"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Wheat"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Fruit"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Prosciutto"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Olive Oil"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Fish"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Salt"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Vegetable"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Honey"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Gold Ore"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Marble"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Bronze"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Silk Thread"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Alum"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Timber"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Iron Ore"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Woad"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Crossbow"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Horse"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Armor"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Fire Arm"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Weapon"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Manuscript"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Velvet"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Fur"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Amber"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Slave"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Coral"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Silk Textile"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Antique"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Glassware"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Pepper"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Clove"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Ottoman Tapestry"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("China"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Silverware"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Tanned Leather"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Intricate Gear"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Sturdy Sinew"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Fine Whetstone"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Steel Ingot"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Coal"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Saltpetre"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Sulfur"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Twine"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Iron Mail"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Beewax"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Tools"));
        cityTrader.inventory.Add(ItemDataBase.dataBase.getItem("Supplies"));
    }
    void banditInitialization()
    {
        banditSpawnPointList = GameObject.FindGameObjectsWithTag("BanditSpawnPoint");
        foreach (GameObject sp in banditSpawnPointList)
        {
            for (int i = 0; i < 2; i++)
            {
                var party = new Party("Bandits", Faction.bandits, 1000);
                party.position = sp.transform.position + new Vector3(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange)); ;
                
            }

        }
        
    }
    void franceInitialization()
    {
        franceSpawnPointList = GameObject.FindGameObjectsWithTag("FranceSpawnPoint");
        foreach (GameObject sp in franceSpawnPointList)
        {
            for (int i = 0; i < 2; i++)
            {
                var party = new Party("French Troop", Faction.france, 1000);
                party.position = sp.transform.position + new Vector3(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange)); ;

            }

        }

    }
    void italyInitialization()
    {
        foreach (City ct in cities)
        {
            for (int i = 0; i < 2; i++)
            {
                var party = new Party(ct.lName + " Troop", Faction.italy, 1000);
                party.belongedCity = ct;
                party.position = ct.gameObject.transform.position + new Vector3(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange)); ;

            }
        }
    }
    void papacyInitialization()
    {
        papacySpawnPointList = GameObject.FindGameObjectsWithTag("PapacySpawnPoint");
        foreach (GameObject sp in banditSpawnPointList)
        {
            for (int i = 0; i < 2; i++)
            {
                var party = new Party("Papal Troop", Faction.papacy, 1000);
                party.position = sp.transform.position + new Vector3(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange)); ;

            }

        }
    }
    void empireInitialization()
    {
        empireSpawnPointList = GameObject.FindGameObjectsWithTag("EmpireSpawnPoint");
        foreach (GameObject sp in banditSpawnPointList)
        {
            for (int i = 0; i < 2; i++)
            {
                var party = new Party("Imperial Troop", Faction.empire, 1000);
                party.position = sp.transform.position + new Vector3(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange)); ;

            }

        }
    }
    void banditUpdate()
    {
        banditSpawnPointList = GameObject.FindGameObjectsWithTag("BanditSpawnPoint");
        foreach (GameObject sp in banditSpawnPointList)
        {
            Party newParty = new Party("Bandits", Faction.bandits, 300);
            newParty.position = sp.transform.position;
            loadSingleParty(newParty);
        }
    }
    
    void loadParties()
    {
        foreach (Party p in parties)
        {
            loadSingleParty(p);
        }
    }
    GameObject loadSingleParty(Party p)
    {
        GameObject spawned = banditTroop;
        if (p.hasShape)
        {
            GameObject toSpawn = banditTroop;
            switch (p.faction)
            {
                case Faction.bandits:
                    toSpawn = banditTroop;
                    break;
                case Faction.empire:
                    toSpawn = imperialTroop;
                    break;
                case Faction.france:
                    toSpawn = frenchTroop;
                    break;
                case Faction.italy:
                    toSpawn = italianTroop;
                    break;
                case Faction.papacy:
                    toSpawn = papalTroop;
                    break;
            }
            var rot = new Quaternion(0, Random.Range(0, 360), 0, 0);
            spawned = Instantiate(toSpawn, p.position, rot);

            spawned.GetComponent<NPC>().npcParty = p;
            spawned.SetActive(true);
        }
        return spawned;
    }
    

    void loadCitySave()
    {
        for(int i = 0; i < cities.Count; i++)
        {
            foreach (LocationInfo li in SaveLoadSystem.saveLoadSystem.cities)
            {
                if (cities[i].lName.ToString() == li.lName.ToString())
                {
                    cities[i] = li.loadCity(cities[i]);
                }
            }

        }
    }
    

    public static void createBattleScene(Party enemyParty)
    {
        BattleCentralControl.enemyParty = enemyParty;
        BattleCentralControl.playerParty = Player.mainParty;
        SaveLoadSystem.saveLoadSystem.tempSave();
        SceneManager.LoadScene("BattleScene");
        //Serializer.Save<MainParty>("tempPlayer", Player.mainParty);
        //SceneManager.UnloadSceneAsync("MapScene");
    }

    public void battleSimulation(NPC npcOne, NPC npcTwo, BattlefieldType bt)
    {
        npcOne.battling = true;
        npcTwo.battling = true;
        int partyOneBattleValue = calculateBattleValue(npcOne.npcParty, bt);
        int partyTwoBattleValue = calculateBattleValue(npcTwo.npcParty, bt);
        if (partyOneBattleValue >= partyTwoBattleValue)
        {
            npcBattleResult(npcOne, npcTwo);
        } else if (partyOneBattleValue < partyTwoBattleValue)
        {
            npcBattleResult(npcTwo, npcOne);
        }
        npcOne.battling = false;
        npcTwo.battling = false;
    }




    public void endOfBattle(Party enemyParty, BattleResult battleResult)
    {
        SaveLoadSystem.saveLoadSystem.tempLoad();
        battleResult = BattleResult.playerWon; //TO BE REMOVED
        finishingBattle = true;
        switch (battleResult)
        {
            case BattleResult.playerWon:
                InventoryManagement.managementMode = InventoryManagementMode.looting;
                Player.mainParty.cash += enemyParty.cash;
                if (Player.mainParty.expToDistribute > 0)
                {
                    float toDistribute = (float)Player.mainParty.expToDistribute;
                    toDistribute /= Player.mainParty.partyMember.Count;
                    foreach (Person p in Player.mainParty.partyMember)
                    {
                        p.increaseExp((int)toDistribute);
                    }
                    Player.mainParty.expToDistribute = 0;
                }
                InventoryManagement.originalSelectingInventory = enemyParty.inventory;
                for (int i = 0; i < enemyParty.partyMember.Count; i ++)
                {
                    InventoryManagement.originalSelectingInventory.Add(ItemDataBase.dataBase.getItem("Slave"));
                }
                goingToLoot = true;
                parties.Remove(enemyParty);
                break;
            case BattleResult.playerUpper:
                InventoryManagement.managementMode = InventoryManagementMode.looting;
                if (Player.mainParty.expToDistribute > 0)
                {
                    float toDistribute = (float)Player.mainParty.expToDistribute;
                    toDistribute /= Player.mainParty.partyMember.Count;
                    foreach (Person p in Player.mainParty.partyMember)
                    {
                        p.increaseExp((int)toDistribute);
                    }
                    Player.mainParty.expToDistribute = 0;
                }
                InventoryManagement.originalSelectingInventory = removeHeavyItems(enemyParty);
                goingToLoot = true;
                parties.Add(enemyParty);
                break;
            case BattleResult.enemyUpper:
                
                break;
            case BattleResult.enemyWon:
                //GameObject partyObj = loadSingleParty(enemyParty);
                //partyObj.GetComponent<NPC>().hasInteracted = true;
                playerRespawn = true;
                break;
        }
        //TabMenu.tabMenu.showMarket(true);
        SaveLoadSystem.saveLoadSystem.tempSave();
        SaveLoadSystem.saveLoadSystem.tempLoad();
        parties.Clear();
        loadParties();
    }

    

    List<Item> removeHeavyItems(Party p)
    {
        List<Item> result = new List<Item>();
        for (int i = 0; i < p.inventory.Count; i ++)
        {
            if (p.inventory[i].weight == ItemWeightClass.heavy || p.inventory[i].weight == ItemWeightClass.medium)
            {
                result.Add(p.inventory[i]);
                p.inventory.Remove(p.inventory[i]);
            }
        }
        return result;
    }

    City getNearestCity(Vector3 pos)
    {
        float distance = Mathf.Infinity;
        City result = null;
        foreach (City c in cities)
        {
            if (Vector3.Distance(c.position, pos) < distance)
            {
                result = c;
                distance = Vector3.Distance(c.position, pos);
            }
        }
        return result;
    }
    void npcBattleResult(NPC npcWon, NPC npcLost)
    {
        parties.Remove(npcLost.npcParty);
        GameObject.Destroy(npcLost.gameObject);

    }

    int calculateBattleValue(Party party, BattlefieldType bType)
    {
        int result = 0;
        foreach (Person p in party.partyMember)
        {
            switch(p.troopType)
            {
                
                case TroopType.swordsman:
                case TroopType.halberdier:
                case TroopType.cavalry:
                default:
                    //Debug.Log("default case");
                    result += p.getBattleValue();
                    break;
            }
        }
        return result;
    }
}
