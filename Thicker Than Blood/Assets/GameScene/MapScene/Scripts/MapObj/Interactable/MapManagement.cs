using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManagement : MonoBehaviour {
    public static MapManagement mapManagement;

    public GameObject[] banditSpawnPointList, franceSpawnPointList, papacySpawnPointList, empireSpawnPointList, italySpawnPointList;
    public GameObject banditTroop, frenchTroop, papalTroop, italianTroop, imperialTroop;
    public GameObject cityScape;
    public GameObject defaultCity, defaultTown;
    public GameObject pisa;
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
    const int MAXIMUM_PARTY_AMOUNT = 100;
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
        spawnPiontInitialization();
        if (SaveLoadSystem.saveType == SaveType.newGame)
        {
            cityInitialization();
            spawnsUpdate();
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
                spawnsUpdate();
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
        buildCity("Milano", defaultCity, new Vector3(135, 3, 425), 1000, 20000);
        buildCity("Torino", defaultCity, new Vector3(35, 3, 360), 1000, 20000);
        buildCity("Asti", defaultCity, new Vector3(75, 3, 350), 1000, 20000);
        buildCity("Parma", defaultCity, new Vector3(160, 3, 360), 1000, 20000);
        buildCity("Genova", defaultCity, new Vector3(115, 3, 310), 1000, 20000);
        buildCity("Modena", defaultCity, new Vector3(220, 3, 345), 1000, 20000);
        buildCity("Verona", defaultCity, new Vector3(230, 3, 425), 1000, 20000);
        buildCity("Padova", defaultCity, new Vector3(300, 3, 425), 1000, 20000);
        buildCity("Treviso", defaultCity, new Vector3(350, 3, 460), 1000, 20000);
        buildCity("Venezia", defaultCity, new Vector3(365, 3, 385), 1000, 20000);
        buildCity("Ferrara", defaultCity, new Vector3(300, 3, 365), 1000, 20000);
        buildCity("Bologna", defaultCity, new Vector3(275, 3, 315), 1000, 20000);
        buildCity("Firenze", defaultCity, new Vector3(250, 3, 240), 1000, 20000);
        buildCity("Ravenna", defaultCity, new Vector3(330, 3, 310), 1000, 20000);
        buildCity("Urbino", defaultCity, new Vector3(330, 3, 270), 1000, 20000);
        buildCity("Lucca", defaultCity, new Vector3(190, 3, 290), 1000, 20000);
        buildCity("Pisa", pisa, new Vector3(200, 3, 265), 1000, 20000);
        buildCity("Siena", defaultCity, new Vector3(275, 3, 200), 1000, 20000);
        buildCity("Grosseto", defaultCity, new Vector3(340, 3, 90), 1000, 20000);
        buildCity("Perugia", defaultCity, new Vector3(320, 3, 170), 1000, 20000);
        buildCity("Roma", defaultCity, new Vector3(400, 3, 50), 1000, 20000);
    }
    void townInitialization()
    {
        buildTown("Milano", defaultTown, new Vector3(135, 3, 425), 1000, 20000);
        buildTown("Torino", defaultTown, new Vector3(35, 3, 360), 1000, 20000);
        buildTown("Asti", defaultTown, new Vector3(75, 3, 350), 1000, 20000);
        buildTown("Parma", defaultTown, new Vector3(160, 3, 360), 1000, 20000);
        buildTown("Genova", defaultTown, new Vector3(115, 3, 310), 1000, 20000);
        buildTown("Modena", defaultTown, new Vector3(220, 3, 345), 1000, 20000);
        buildTown("Verona", defaultTown, new Vector3(230, 3, 425), 1000, 20000);
        buildTown("Padova", defaultTown, new Vector3(300, 3, 425), 1000, 20000);
        buildTown("Treviso", defaultTown, new Vector3(350, 3, 460), 1000, 20000);
        buildTown("Venezia", defaultTown, new Vector3(360, 3, 400), 1000, 20000);
        buildTown("Ferrara", defaultTown, new Vector3(300, 3, 365), 1000, 20000);
        buildTown("Bologna", defaultTown, new Vector3(275, 3, 315), 1000, 20000);
        buildTown("Firenze", defaultTown, new Vector3(250, 3, 240), 1000, 20000);
        buildTown("Ravenna", defaultTown, new Vector3(330, 3, 310), 1000, 20000);
        buildTown("Urbino", defaultTown, new Vector3(330, 3, 270), 1000, 20000);
        buildTown("Lucca", defaultTown, new Vector3(190, 3, 290), 1000, 20000);
        buildTown("Pisa", defaultTown, new Vector3(200, 3, 265), 1000, 20000);
        buildTown("Siena", defaultTown, new Vector3(275, 3, 200), 1000, 20000);
        buildTown("Grosseto", defaultTown, new Vector3(340, 3, 90), 1000, 20000);
        buildTown("Perugia", defaultTown, new Vector3(320, 3, 170), 1000, 20000);
        buildTown("Roma", defaultTown, new Vector3(400, 3, 50), 1000, 20000);
    }
    void buildCity(string cityName, GameObject obj, Vector3 location, int guardBattleValue, int cash)
    {
        var rot = new Quaternion(0, 0, 0, 0);
        var spawned = Instantiate(obj, location, rot);
        spawned.transform.SetParent(obj.transform.parent, false);
        spawned.SetActive(true);
        var spawnedCityScape = Instantiate(cityScape, location, rot);
        spawnedCityScape.transform.SetParent(obj.transform.parent, false);
        spawnedCityScape.SetActive(true);
        City spawnedCity = spawned.GetComponent<City>();
        spawnedCity.lName = cityName;
        spawnedCity.ID = cityName.Substring(0, 3).ToUpper();
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
    
    void spawnPiontInitialization()
    {
        banditSpawnPointList = GameObject.FindGameObjectsWithTag("BanditSpawnPoint");
        franceSpawnPointList = GameObject.FindGameObjectsWithTag("FranceSpawnPoint");
        papacySpawnPointList = GameObject.FindGameObjectsWithTag("PapacySpawnPoint");
        empireSpawnPointList = GameObject.FindGameObjectsWithTag("EmpireSpawnPoint");
        italySpawnPointList = GameObject.FindGameObjectsWithTag("ItalySpawnPoint");
    }
    void spawnsUpdate()
    {
        if (parties.Count <= MAXIMUM_PARTY_AMOUNT)
        {
            foreach (GameObject sp in banditSpawnPointList)
            {
                Party newParty = new Party("Bandits", Faction.bandits, Random.Range(300, 600));
                newParty.position = sp.transform.position;
                loadSingleParty(newParty);
            }
            foreach (GameObject sp in franceSpawnPointList)
            {
                Party newParty = new Party("French Troop", Faction.france, Random.Range(300, 600));
                newParty.position = sp.transform.position;
                loadSingleParty(newParty);
            }
            foreach (GameObject sp in papacySpawnPointList)
            {
                Party newParty = new Party("Papal Troop", Faction.papacy, Random.Range(300, 600));
                newParty.position = sp.transform.position;
                loadSingleParty(newParty);
            }
            foreach (GameObject sp in empireSpawnPointList)
            {
                Party newParty = new Party("Imperial Troop", Faction.empire, Random.Range(300, 600));
                newParty.position = sp.transform.position;
                loadSingleParty(newParty);
            }
            foreach (GameObject sp in italySpawnPointList)
            {
                Party newParty = new Party("Italian Troop", Faction.italy, Random.Range(300, 600));
                newParty.position = sp.transform.position;
                loadSingleParty(newParty);
            }
            foreach (City city in cities)
            {
                Party newParty = new Party("Italian Troop", Faction.italy, Random.Range(300, 600));
                newParty.belongedCity = city;
                newParty.position = city.position;
                loadSingleParty(newParty);
            }
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
            var rot = new Quaternion(0, 0, 0, 0);
            spawned = Instantiate(toSpawn, p.position + new Vector3(0, 1, 0), rot);
            spawned.transform.SetParent(toSpawn.transform.parent, false);
            spawned.GetComponent<NPC>().npcAgent.Warp(p.position);
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
    

    public static void createBattleScene(Party enemyParty, List<BattlefieldType> bt)
    {
        BattleCentralControl.enemyParty = enemyParty;
        BattleCentralControl.playerParty = Player.mainParty;
        BattleCentralControl.battlefieldTypes = bt;
        SaveLoadSystem.saveLoadSystem.tempSave();
        SceneManager.LoadScene("BattleScene");
        //Serializer.Save<MainParty>("tempPlayer", Player.mainParty);
        //SceneManager.UnloadSceneAsync("MapScene");
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
                Player.mainParty.changeMorale(Player.mainParty.partyMember.Count);
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
    public void battleSimulation(NPC npcOne, NPC npcTwo, List<BattlefieldType> bt)
    {
        if (npcOne.npcParty.battling > 0 || npcTwo.npcParty.battling > 0)
        {
            return;
        }
        npcOne.npcParty.battling = (int) (100 * npcOne.npcParty.getTaticRating());
        npcTwo.npcParty.battling = (int) (100 * npcTwo.npcParty.getTaticRating());
        float partyOneBattleValue = (float)calculateBattleValue(npcOne.npcParty, bt);
        float partyTwoBattleValue = (float)calculateBattleValue(npcTwo.npcParty, bt);
        if (partyOneBattleValue >= partyTwoBattleValue)
        {
            npcBattleResult(npcOne, npcTwo, 100f * partyOneBattleValue / (partyOneBattleValue + partyTwoBattleValue));
        }
        else if (partyOneBattleValue < partyTwoBattleValue)
        {
            npcBattleResult(npcTwo, npcOne, 100f * partyOneBattleValue / (partyOneBattleValue + partyTwoBattleValue));
        }
    }
    void npcBattleResult(NPC npcWon, NPC npcLost, float winPercent)
    {
        List<Person> toRemove = new List<Person>();
        foreach(Person p in npcWon.npcParty.partyMember)
        {
            int rand = Random.Range(0, 100);
            if (rand > winPercent)
            {
                toRemove.Add(p);
            } else
            {
                p.health = (winPercent) * p.health;
            }
        }
        foreach (Person p in toRemove)
        {
            
            npcWon.npcParty.partyMember.Remove(p);
            if (p == npcWon.npcParty.leader)
            {
                if (!npcWon.npcParty.electNewLeader())
                {
                    GameObject.Destroy(npcWon.gameObject);
                }
            }
        }
        toRemove.Clear();
        if (winPercent >= 65)
        {
            
            foreach (Item i in npcLost.npcParty.inventory)
            {
                int rand = Random.Range(0, 100);
                if (rand <= winPercent)
                {
                    npcWon.npcParty.addToInventory(i);
                }
            }
            
            for (int i = 0; i < npcLost.npcParty.partyMember.Count; i++)
            {
                npcWon.npcParty.addToInventory(ItemDataBase.dataBase.getItem("Slave"));
            }
            parties.Remove(npcLost.npcParty);
            GameObject.Destroy(npcLost.gameObject);
            npcWon.npcParty.plusPrestige(npcLost.npcParty.notoriety);
            npcWon.npcParty.plusNotoriety(npcLost.npcParty.prestige);
            npcWon.npcParty.cash += npcLost.npcParty.cash;
        } else
        {
            foreach (Person p in npcLost.npcParty.partyMember)
            {
                int rand = Random.Range(0, 100);
                if (rand > (100 - winPercent) && p != npcLost.npcParty.leader)
                {
                    toRemove.Add(p);
                }
                else
                {
                    p.health = (100 - winPercent) * p.health;
                }
            }
            foreach (Person p in toRemove)
            {

                npcLost.npcParty.partyMember.Remove(p);
                if (!npcLost.npcParty.electNewLeader())
                {
                    GameObject.Destroy(npcLost.gameObject);
                }
            }
            toRemove.Clear();
        }
        npcWon.npcParty.battling = 0;
    }

    int calculateBattleValue(Party party, List<BattlefieldType> bTypes)
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
