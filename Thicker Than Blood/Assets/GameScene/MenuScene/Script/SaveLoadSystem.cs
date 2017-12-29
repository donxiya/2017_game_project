using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour {
    public static SaveLoadSystem saveLoadSystem;
    public static SaveType saveType;
    public static bool loading = false;
    public MainParty mainParty;
    public MainCharacter mainCharacter, secCharacter;
    public List<Party> parties;
    public List<LocationInfo> cities;
    public List<LocationInfo> towns;
    private static bool created = false;
    // Use this for initialization
    void Awake () {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            saveLoadSystem = this;
            if (saveType == SaveType.newGame)
            {
               createNewGame();
               //permanentLoad();
               //tempLoad();
            }
            else
            {
                tempLoad();
            }
            created = true;
        }
        else
        {
            Destroy(this.gameObject);
        }
        
        
        
        //if (!loaded) 
	}
	
	public void tempSave()
    {
        mainParty = Player.mainParty;
        mainCharacter = Player.mainCharacter;
        secCharacter = Player.secCharacter;
        parties = new List<Party>(MapManagement.parties);
        cities = new List<LocationInfo>();
        foreach (City ct in MapManagement.cities)
        {
            LocationInfo li = new LocationInfo();
            li.importLocationSave(ct);
            cities.Add(li);
        }
        towns = new List<LocationInfo>();
        foreach (City ct in MapManagement.cities)
        {
            LocationInfo li = new LocationInfo();
            li.importLocationSave(ct);
            towns.Add(li);
        }
        permanentSave();
    }
    public void tempLoad()
    {
        permanentLoad();
        Player.mainParty = mainParty;
        Player.mainCharacter = mainCharacter;
        Player.secCharacter = secCharacter;
        MapManagement.parties = new List<Party>(parties);

        //permanentLoad();
    }
    public void permanentSave()
    {
        //checkSavePath();
        string dp = "C:/Users/Zer0/game_project/IronCrown/tempSave.txt";
        SaveFile newS = new SaveFile();
        newS.importSaveFile(mainParty, parties, cities, towns);
        Serializer.Save<SaveFile>(dp, newS);
    }
    public void permanentLoad()
    {
        string dp = "C:/Users/Zer0/game_project/IronCrown/tempSave.txt";
        SaveFile saveFile = Serializer.Load<SaveFile>(dp);
        mainParty = saveFile.exportMainParty();
        foreach (Person p in mainParty.partyMember)
        {
            if (p.ranking == Ranking.mainChar)
            {
                if (p.troopType == TroopType.mainCharType)
                {
                    mainCharacter = p as MainCharacter;
                } else
                {
                    secCharacter = p as MainCharacter;
                }
            }
        }
        parties = saveFile.exportParties();
    }

    public void createNewGame()
    {
        parties = new List<Party>();
        cities = new List<LocationInfo>();
        towns = new List<LocationInfo>();
        initializePlayer();
        Player.mainParty = mainParty;
        Player.mainCharacter = mainCharacter;
        Player.secCharacter = secCharacter;
        //tempLoad();
        //saveType = SaveType.tempSave;
    }

    void initializePlayer()
    {
        Stats mStats = new Stats(10, 10, 10, 10, 10, 1);
        Experience mExp = new Experience(0, 1, 5);
        mainCharacter = new MainCharacter("Nicola Da Roma", mStats, Ranking.mainChar,
            TroopType.mainCharType, Faction.mercenary, mExp);
        Experience sExp = new Experience(0, 1, 5);
        secCharacter = new MainCharacter("Rachele Sforza", mStats, Ranking.mainChar,
            TroopType.crossbowman, Faction.mercenary, sExp);
        mainParty = new MainParty(mainCharacter, "Crimson Griffin", Faction.mercenary, 300);
        mainParty.addToParty(secCharacter);
        mainParty.addToParty(mainParty.makeGenericPerson(TroopType.halberdier, Ranking.militia));
        mainParty.addToParty(mainParty.makeGenericPerson(TroopType.cavalry, Ranking.veteran));
        mainParty.addToParty(mainParty.makeGenericPerson(TroopType.swordsman, Ranking.militia));
        mainParty.addToParty(mainParty.makeGenericPerson(TroopType.halberdier, Ranking.elite));
        mainParty.addToParty(mainParty.makeGenericPerson(TroopType.crossbowman, Ranking.elite));
        mainParty.addToParty(mainParty.makeGenericPerson(TroopType.musketeer, Ranking.elite));
        mainParty.cash = 200;
        mainParty.addToInventory(ItemDataBase.dataBase.getItem("Parchment"));
        mainParty.addToInventory(ItemDataBase.dataBase.getItem("Parchment"));
        mainParty.addToInventory(ItemDataBase.dataBase.getItem("Parchment"));
        mainParty.addToInventory(ItemDataBase.dataBase.getItem("Parchment"));
        mainParty.addToInventory(ItemDataBase.dataBase.getItem("Parchment"));
        mainParty.addToInventory(ItemDataBase.dataBase.getItem("Parchment"));
        mainParty.addToInventory(ItemDataBase.dataBase.getItem("Parchment"));
        mainParty.addToInventory(ItemDataBase.dataBase.getItem("Parchment"));
        mainParty.addToInventory(ItemDataBase.dataBase.getItem("Parchment"));
        mainParty.addToInventory(ItemDataBase.dataBase.getItem("Parchment"));
        mainParty.addToInventory(ItemDataBase.dataBase.getItem("Parchment"));
        mainParty.addToInventory(ItemDataBase.dataBase.getItem("Parchment"));
        mainParty.addToInventory(ItemDataBase.dataBase.getItem("Salt"));
        mainParty.addToInventory(ItemDataBase.dataBase.getItem("Salt"));
        mainParty.addToInventory(ItemDataBase.dataBase.getItem("Salt"));
        mainParty.addToInventory(ItemDataBase.dataBase.getItem("Parchment"));
        mainParty.addToInventory(ItemDataBase.dataBase.getItem("Salt"));
        mainParty.addToInventory(ItemDataBase.dataBase.getItem("Fur"));
        mainParty.addToInventory(ItemDataBase.dataBase.getItem("Silk Textile"));
        mainParty.morale = 60;
    }

    void checkSavePath ()
    {
        string dp = "C:/Users/Zer0/game_project/IronCrown";
        if (!Directory.Exists(dp))
        {
            //if it doesn't, create it
            Directory.CreateDirectory(dp);
            if (!File.Exists(dp + "/tempSave.txt"))
            {
                System.IO.FileStream oFileStream = null;
                oFileStream = new System.IO.FileStream(dp + "/tempSave.txt", System.IO.FileMode.Create);
            }
        }

    }


}

public enum SaveType
{
    newGame,
    tempSave
}

[System.Serializable]
public class SaveFile : System.Object
{
    MainPartySave mainPartySave;
    List<PartySave> partiesSave;
    List<LocationInfo> citySaves, townSaves;
    public SaveFile()
    {
    }
    public void importSaveFile(MainParty main, List<Party> parties, List<LocationInfo> cities, List<LocationInfo> towns)
    {
        mainPartySave = new MainPartySave();
        mainPartySave.importMainParty(main);
        partiesSave = new List<PartySave>();
        foreach (Party p in parties)
        {
            PartySave ps = new PartySave();
            ps.importParty(p);
            partiesSave.Add(ps);
        }
        citySaves = cities;
        townSaves = towns;
    }
    public MainParty exportMainParty()
    {
        return mainPartySave.exportMainParty();
    }
    public List<Party> exportParties()
    {
        List<Party> parties = new List<Party>();
        foreach (PartySave ps in partiesSave)
        {
            parties.Add(ps.exportParty());
        }
        return parties;
    }
    //public List<City> loadCities()
    //{
    //    List<City> result = new List<City>();
    //    foreach (LocationInfo li in citySaves)
    //    {
    //        result.Add(li.loadCity());
    //    }
    //    return result;
    //}
    //public List<Town> loadTowns(List<Town> towns)
    //{
    //    List<Town> result = new List<Town>();
    //    foreach (Town t in towns)
    //    {
    //        foreach (LocationInfo li in townSaves)
    //        {
    //            if (t.lName == li.lName)
    //            {
    //                result.Add(li.loadTown());
    //            }
    //        }
    //    }
    //    return result;
    //}


}

[System.Serializable]
public class PartySave : System.Object
{
    public List<PersonSave> partyMember;
    public float posX, posY, posZ;
    public string name;
    public Faction faction;
    public PersonSave leader;
    public List<ItemSave> inventory;
    public int battleValue;
    public int morale;
    public int cash;
    public Dictionary<Faction, int> factionFavors;
    public Dictionary<string, int> locationFavors;
    public int prestige, notoriety;
    public bool unique, hasShape;
    public PartySave()
    {
        partyMember = new List<PersonSave>();
        inventory = new List<ItemSave>();
        factionFavors = new Dictionary<Faction, int>();
        locationFavors = new Dictionary<string, int>();
    }
    public virtual void importParty(Party inParty)
    {
        
        foreach(Person member in inParty.partyMember)
        {
            if (member == inParty.leader)
            {
                leader = new PersonSave();
                leader.importPerson(member);
            } else
            {
                PersonSave ps = new PersonSave();
                ps.importPerson(member);
                partyMember.Add(ps);
            }
        }
        posX = inParty.position.x;
        posY = inParty.position.y;
        posZ = inParty.position.z;
        name = inParty.name;
        faction = inParty.faction;
        
        foreach (Item item in inParty.inventory)
        {
            ItemSave itemS = new ItemSave();
            itemS.importItem(item);
            inventory.Add(itemS);
        }
        battleValue = inParty.battleValue;
        morale = inParty.morale;
        cash = inParty.cash;
        factionFavors = inParty.factionFavors;
        locationFavors = inParty.locationFavors;
        prestige = inParty.prestige;
        notoriety = inParty.notoriety;
        unique = inParty.unique;
        hasShape = inParty.hasShape;
    }
    public virtual Party exportParty()
    {
        Party outParty = new Party();
        foreach (PersonSave ps in partyMember)
        {
            Person member = ps.exportPerson();
            outParty.partyMember.Add(member);
        }
        outParty.leader = leader.exportPerson();
        outParty.partyMember.Add(outParty.leader);
        outParty.position.x = posX;
        outParty.position.y = posY;
        outParty.position.z = posZ;
        outParty.name = name;
        outParty.faction = faction;
        
        foreach (ItemSave itemS in inventory)
        {
            Item item = itemS.exportItem();
            outParty.inventory.Add(item);
        }
        outParty.battleValue = battleValue;
        outParty.morale = morale;
        outParty.cash = cash;
        outParty.factionFavors = factionFavors;
        outParty.locationFavors = locationFavors;
        outParty.prestige = prestige;
        outParty.notoriety = notoriety;
        outParty.unique = unique;
        outParty.hasShape = hasShape;
        return outParty;
    }
}

[System.Serializable]
public class MainPartySave : PartySave
{
    public FactionPerkTreeSave factionPerkTreeSave;
    public List<QuestProgress> unfinishedQuestP, finishedQuestP;
    public MainCharacterSave mainOne, mainTwo;
    public MainPartySave()
    {
        partyMember = new List<PersonSave>();
        inventory = new List<ItemSave>();
        factionFavors = new Dictionary<Faction, int>();
        locationFavors = new Dictionary<string, int>();
    }
    public void importMainParty(MainParty inParty)
    {
        foreach(Person member in inParty.partyMember)
        {
            if (member.ranking == Ranking.mainChar)
            {
                if (member.troopType == TroopType.mainCharType)
                {
                    mainOne = new MainCharacterSave();
                    mainOne.importMainCharacter(member as MainCharacter);
                    //Debug.Log("saving:" + mainOne.name);
                } else
                {
                    mainTwo = new MainCharacterSave();
                    mainTwo.importMainCharacter(member as MainCharacter);
                    //Debug.Log("saving:" + mainTwo.name);
                }
            } else
            {
                PersonSave ps = new PersonSave();
                ps.importPerson(member);
                partyMember.Add(ps);
            }
        }
        posX = inParty.position.x;
        posY = inParty.position.y;
        posZ = inParty.position.z;
        name = inParty.name;
        faction = inParty.faction;

        foreach (Item item in inParty.inventory)
        {
            ItemSave itemS = new ItemSave();
            itemS.importItem(item);
            inventory.Add(itemS);
        }
        battleValue = inParty.battleValue;
        morale = inParty.morale;
        cash = inParty.cash;
        factionFavors = inParty.factionFavors;
        locationFavors = inParty.locationFavors;
        prestige = inParty.prestige;
        notoriety = inParty.notoriety;
        unique = inParty.unique;
        hasShape = inParty.hasShape;
        factionPerkTreeSave = new FactionPerkTreeSave();
        factionPerkTreeSave.importTree(inParty.factionPerkTree);
        unfinishedQuestP = new List<QuestProgress>();
        foreach (Quest q in inParty.unfinishedQuests)
        {
            QuestProgress qp = new QuestProgress();
            qp.importQuestProgress(q);
            unfinishedQuestP.Add(qp);
        }
        finishedQuestP = new List<QuestProgress>();
        foreach (Quest q in inParty.finishedQuests)
        {
            QuestProgress qp = new QuestProgress();
            qp.importQuestProgress(q);
            finishedQuestP.Add(qp);
        }
    }
    public MainParty exportMainParty()
    {
        MainParty outParty = new MainParty();
        foreach (PersonSave ps in partyMember)
        {
            Person member = ps.exportPerson();
            outParty.partyMember.Add(member);
        }
        outParty.leader = mainOne.exportMainCharacter();
        outParty.partyMember.Add(outParty.leader);
        outParty.partyMember.Add(mainTwo.exportMainCharacter());
        outParty.position.x = posX;
        outParty.position.y = posY;
        outParty.position.z = posZ;
        outParty.name = name;
        outParty.faction = faction;

        foreach (ItemSave itemS in inventory)
        {
            Item item = itemS.exportItem();
            outParty.inventory.Add(item);
        }
        outParty.battleValue = battleValue;
        outParty.morale = morale;
        outParty.cash = cash;
        outParty.factionFavors = factionFavors;
        outParty.locationFavors = locationFavors;
        outParty.prestige = prestige;
        outParty.notoriety = notoriety;
        outParty.unique = unique;
        outParty.hasShape = hasShape;
        outParty.factionPerkTree = factionPerkTreeSave.loadTree(new FactionPerkTree());
        outParty.unfinishedQuests = new List<Quest>();
        foreach (QuestProgress qp in unfinishedQuestP)
        {
            Quest q = qp.exportQuestProgress();
            //Debug.Log()
            outParty.unfinishedQuests.Add(q);
        }
        outParty.finishedQuests = new List<Quest>();
        foreach (QuestProgress qp in finishedQuestP)
        {
            Quest q = qp.exportQuestProgress();
            outParty.finishedQuests.Add(q);
        }

        return outParty;
    }
}

[System.Serializable] 
public class PersonSave : System.Object
{
    public string name;
    public Stats stats;
    public Ranking ranking;
    public TroopType troopType;
    public Faction faction;
    public Experience exp;
    public bool inBattle, renamed;
    //battleStats
    public int battleValue;
    public float health;
    

    public PersonSave()
    {

    }
    public virtual void importPerson (Person p)
    {
        name = p.name;
        stats = p.stats;
        ranking = p.ranking;
        troopType = p.troopType;
        faction = p.faction;
        exp = p.exp;
        inBattle = p.inBattle;
        renamed = p.renamed;
        battleValue = p.battleValue;
        health = p.health;
    }
    public virtual Person exportPerson()
    {
        Person outPerson = new Person();
        outPerson.name = name;
        outPerson.stats = stats;
        outPerson.ranking = ranking;
        outPerson.troopType = troopType;
        outPerson.faction = faction;
        outPerson.exp = exp;
        outPerson.inBattle = inBattle;
        outPerson.renamed = renamed;
        outPerson.battleValue = battleValue;
        outPerson.health = health;
        return outPerson;
    }
}

[System.Serializable]
public class MainCharacterSave : PersonSave
{
    SkillTreeSave skillTreeSave;
    public MainCharacterSave()
    {
        
    }
    public void importMainCharacter(MainCharacter p)
    {
        name = p.name;
        stats = p.stats;
        ranking = p.ranking;
        troopType = p.troopType;
        faction = p.faction;
        exp = p.exp;
        inBattle = p.inBattle;
        renamed = p.renamed;
        battleValue = p.battleValue;
        health = p.health;

        skillTreeSave = new SkillTreeSave();
        skillTreeSave.importTree(p.skillTree);
    }
    public MainCharacter exportMainCharacter()
    {
        MainCharacter outPerson = new MainCharacter();
        outPerson.name = name;
        outPerson.stats = stats;
        outPerson.ranking = ranking;
        outPerson.troopType = troopType;
        outPerson.faction = faction;
        outPerson.exp = exp;
        outPerson.inBattle = inBattle;
        outPerson.renamed = renamed;
        outPerson.battleValue = battleValue;
        outPerson.health = health;

        outPerson.skillTree = skillTreeSave.loadTree(new SkillTree());
        return outPerson;
    }
}

[System.Serializable]
public class ItemSave : System.Object
{
    string name;
    public ItemSave()
    {

    }
    public void importItem(Item p)
    {
        name = p.name;
    }
    public Item exportItem()
    {
        return new Item(ItemDataBase.dataBase.getItem(name));
    }
}

[System.Serializable]
public class Experience : System.Object
{
    public int exp { get; set; }
    public int level { get; set; }
    public int levelExp;
    public int sparedPoint { get; set; }
    public Experience(int expI, int levelI, int sparedPointI)
    {
        this.exp = expI;
        this.level = levelI;
        this.sparedPoint = sparedPointI;
    }
    public float getLevelExp()
    {
        return 100 * Mathf.Pow(1.1f, level);
    }
}
[System.Serializable]
public class SkillTreeSave
{
    public List<PerkSave> tree;
    public SkillTreeSave()
    {
        tree = new List<PerkSave>();
    }
    public void importTree(SkillTree st)
    {
        foreach (KeyValuePair<string, Perk> pair in st.skillTreeDict)
        {
            PerkSave newPerkSave = new PerkSave();
            newPerkSave.importPerk(pair.Value);
            tree.Add(newPerkSave);
        }
    }
    public SkillTree loadTree(SkillTree oldST)
    {
        foreach (PerkSave perkS in tree)
        {
            oldST.getPerk(perkS.id).own = perkS.own;
        }
        return oldST;
    }
    public PerkSave getPerkSave(string id)
    {
        foreach (PerkSave ps in tree)
        {
            if (ps.id == id)
            {
                return ps;
            }
        }
        return null;
    }

}
[System.Serializable]
public class FactionPerkTreeSave
{
    public List<PerkSave> tree;
    public FactionPerkTreeSave()
    {
        tree = new List<PerkSave>();
    }
    public void importTree(FactionPerkTree fpt)
    {
        foreach (KeyValuePair<string, Perk> pair in fpt.perkTreeDict)
        {
            PerkSave newPerkSave = new PerkSave();
            newPerkSave.importPerk(pair.Value);
            tree.Add(newPerkSave);
        }
    }
    public FactionPerkTree loadTree(FactionPerkTree oldFPT)
    {
        foreach (PerkSave perkS in tree)
        {
            oldFPT.getPerk(perkS.id).own = perkS.own;
        }
        return oldFPT;
    }
    public PerkSave getPerkSave (string id)
    {
        foreach(PerkSave ps in tree)
        {
            if (ps.id == id)
            {
                return ps;
            }
        }
        return null;
    }

}
[System.Serializable]
public class PerkSave
{
    public string id;
    public bool own;
    public PerkSave()
    {

    }
    public void importPerk(Perk perk)
    {
        id = perk.skillPointID;
        own = perk.own;
    }
    public Perk loadPerk(Perk oldPerk)
    {
        oldPerk.own = own;
        return oldPerk;
    }


}
[System.Serializable]
public class GearInfo : System.Object
{
    public float armorRating, blockRating, evasionRating,
        visionRating, stealthRating, accuracyRating, meleeDmgRating, rangedDmgRating, mobilityRating;
    public GearInfo(float armorR, float blockR, float evasionR, float visionR,
        float stealthR, float accuracyR, float meleeDmgR, float rangedDmgR, float mobilityR)
    {
        armorRating = armorR;
        blockRating = blockR;
        evasionRating = evasionR;
        visionRating = visionR;
        stealthRating = stealthR;
        accuracyRating = accuracyR;
        meleeDmgRating = meleeDmgR;
        rangedDmgRating = rangedDmgR;
        mobilityRating = mobilityR;
    }
}
[System.Serializable]
public class Stats
{
    //base
    public int strength { get; set; }
    public int agility { get; set; }
    public int perception { get; set; }
    public int endurance { get; set; }
    public int charisma { get; set; }
    public int intelligence { get; set; }


    public Stats(int strengthI, int agilityI, int perceptionI, int enduranceI, int charismaI, int intelligenceI)
    {
        this.strength = strengthI;
        this.agility = agilityI;
        this.perception = perceptionI;
        this.endurance = enduranceI;
        this.charisma = charismaI;
        this.intelligence = intelligenceI;
    }
}
[System.Serializable]
public class LocationInfo
{
    public string lName;
    int prosperity;
    PartySave guard, trader;
    List<ItemSave> warehouse;
    public LocationInfo ()
    {
    }
    public void importLocationSave(City city)
    {
        lName = city.lName;
        prosperity = city.prosperity;
        guard = new PartySave();
        guard.importParty(city.cityGuard);
        trader = new PartySave();
        trader.importParty(city.cityTrader);
        warehouse = new List<ItemSave>();
        foreach (Item item in city.warehouse)
        {
            ItemSave itemS = new ItemSave();
            itemS.importItem(item);
            warehouse.Add(itemS);
        }
    }
    public void importLocationSave(Town town)
    {
        lName = town.lName;
        prosperity = town.prosperity;
        guard = new PartySave();
        guard.importParty(town.townGuard);
        trader = new PartySave();
        trader.importParty(town.townTrader);
    }
    public City loadCity(City ct)
    {
        ct.lName = lName;
        ct.cityGuard = guard.exportParty();
        ct.cityGuard.belongedCity = ct;
        ct.cityTrader = trader.exportParty();
        ct.cityTrader.belongedCity = ct;
        ct.warehouse = new List<Item>();
        foreach (ItemSave itemS in warehouse)
        {
            ct.warehouse.Add(itemS.exportItem());
        }
        return ct;
    }
    public Town loadTown(Town tw)
    {
        tw.lName = lName;
        tw.townGuard = guard.exportParty();
        tw.townGuard.belongedTown = tw;
        tw.townTrader = trader.exportParty();
        tw.townTrader.belongedTown = tw;
        return tw;
    }
}
[System.Serializable]
public class QuestProgress
{
    public string questID;
    public int currentProgress;
    public bool active = false;
    public bool complete = false;
    public int stack = 0;
    public QuestProgress()
    {

    }
    public void importQuestProgress(Quest q)
    {
        questID = q.questID;
        currentProgress = q.currentProgress;
        active = q.active;
        complete = q.complete;
        stack = q.stack;
    }
    public Quest exportQuestProgress()
    {
        Quest result = QuestDataBase.dataBase.getQuest(questID);

        result.currentProgress = currentProgress;
        result.active = active;
        result.complete = complete;
        result.stack = stack;
        return result;
    }

}

[System.Serializable]
public enum Ranking
{
    mainChar,
    recruit,
    militia,
    veteran,
    elite
};
[System.Serializable]
public enum TroopType
{
    mainCharType,
    recruitType,
    crossbowman,
    musketeer,
    swordsman,
    halberdier,
    cavalry
};
[System.Serializable]
public enum Faction
{
    mercenary,
    france,
    italy,
    bandits,
    empire,
    papacy
}

[System.Serializable]
public enum TroopSkill
{
    none,
    walk,
    lunge,
    whirlwind,
    fire,
    holdSteady,
    execute,
    guard,
    charge,
    quickDraw,
    rainOfArrows
}

[System.Serializable] 
public enum BattlefieldType
{
    plain,
    forest,
    rocky,
    city,
    bushy
}