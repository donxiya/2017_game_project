using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour {
    public static Dictionary<string, int> PersonBattleValue = new Dictionary<string, int>();
    public static List<Item> itemList = new List<Item>();
    // Use this for initialization
    void Awake () {
        personInitialization();
	}
	
    public static int getBattleValue(string troopName)
    {
        return 50;
        //return PersonBattleValue[troopName];
    }

    public static string troopTypeToString(TroopType tt)
    {
        switch (tt)
        {
            case TroopType.recruitType:
                return "Recruit";
            case TroopType.crossbowman:
                return "Crossbowman";
            case TroopType.musketeer:
                return "Musketeer";
            case TroopType.swordsman:
                return "Swordsman";
            case TroopType.halberdier:
                return "Halberdier";
            case TroopType.cavalry:
                return "Cavalry";
        }
        return "Recruit";
    }
    public static string rankingToString(Ranking rk)
    {
        switch (rk)
        {
            case Ranking.recruit:
                return "";
            case Ranking.militia:
                return "Militia";
            case Ranking.veteran:
                return "Veteran";
            case Ranking.elite:
                return "Elite";
        }
        return "";
    }
    public static string factionToString(Faction f)
    {
        switch (f)
        {
            case Faction.mercenary:
                return "Mercenary";
            case Faction.bandits:
                return "Bandit";
            case Faction.italy:
                return "Italian";
            case Faction.pope:
                return "Papal";
            case Faction.france:
                return "French";
            case Faction.empire:
                return "Imperial";
        }
        return "";
    }
    void personInitialization()
    {
        //MERC
        PersonBattleValue.Add("MercenaryRecruit", 10);
        PersonBattleValue.Add("MercenaryMilitiaCrossbowman", 30);
        PersonBattleValue.Add("MercenaryMilitiaMusketeer", 50);
        PersonBattleValue.Add("MercenaryMilitiaSwordsman", 20);
        PersonBattleValue.Add("MercenaryMilitiaHalberdier", 30);
        PersonBattleValue.Add("MercenaryMilitiaCavalry", 60);
        PersonBattleValue.Add("MercenaryVeteranCrossbowman", 30);
        PersonBattleValue.Add("MercenaryVeteranMusketeer", 30);
        PersonBattleValue.Add("MercenaryVeteranSwordsman", 30);
        PersonBattleValue.Add("MercenaryVeteranHalberdier", 30);
        PersonBattleValue.Add("MercenaryVeteranCavalry", 30);
        PersonBattleValue.Add("MercenaryEliteCrossbowman", 30);
        PersonBattleValue.Add("MercenaryEliteMusketeer", 30);
        PersonBattleValue.Add("MercenaryEliteSwordsman", 30);
        PersonBattleValue.Add("MercenaryEliteHalberdier", 30);
        PersonBattleValue.Add("MercenaryEliteCavalry", 30);
        //BANDIT
        PersonBattleValue.Add("BanditRecruit", 10);
        PersonBattleValue.Add("BanditMilitiaCrossbowman", 30);
        PersonBattleValue.Add("BanditMilitiaMusketeer", 30);
        PersonBattleValue.Add("BanditMilitiaSwordsman", 30);
        PersonBattleValue.Add("BanditMilitiaHalberdier", 30);
        PersonBattleValue.Add("BanditMilitiaCavalry", 30);
        PersonBattleValue.Add("BanditVeteranCrossbowman", 30);
        PersonBattleValue.Add("BanditVeteranMusketeer", 30);
        PersonBattleValue.Add("BanditVeteranSwordsman", 30);
        PersonBattleValue.Add("BanditVeteranHalberdier", 30);
        PersonBattleValue.Add("BanditVeteranCavalry", 30);
        PersonBattleValue.Add("BanditEliteCrossbowman", 30);
        PersonBattleValue.Add("BanditEliteMusketeer", 30);
        PersonBattleValue.Add("BanditEliteSwordsman", 30);
        PersonBattleValue.Add("BanditEliteHalberdier", 30);
        PersonBattleValue.Add("BanditEliteCavalry", 30);
        //Italian
        PersonBattleValue.Add("ItalianRecruit", 10);
        PersonBattleValue.Add("ItalianMilitiaCrossbowman", 30);
        PersonBattleValue.Add("ItalianMilitiaMusketeer", 30);
        PersonBattleValue.Add("ItalianMilitiaSwordsman", 30);
        PersonBattleValue.Add("ItalianMilitiaHalberdier", 30);
        PersonBattleValue.Add("ItalianMilitiaCavalry", 30);
        PersonBattleValue.Add("ItalianVeteranCrossbowman", 30);
        PersonBattleValue.Add("ItalianVeteranMusketeer", 30);
        PersonBattleValue.Add("ItalianVeteranSwordsman", 30);
        PersonBattleValue.Add("ItalianVeteranHalberdier", 30);
        PersonBattleValue.Add("ItalianVeteranCavalry", 30);
        PersonBattleValue.Add("ItalianEliteCrossbowman", 30);
        PersonBattleValue.Add("ItalianEliteMusketeer", 30);
        PersonBattleValue.Add("ItalianEliteSwordsman", 30);
        PersonBattleValue.Add("ItalianEliteHalberdier", 30);
        PersonBattleValue.Add("ItalianEliteCavalry", 30);
        //PAPAL
        PersonBattleValue.Add("PapalRecruit", 10);
        PersonBattleValue.Add("PapalMilitiaCrossbowman", 30);
        PersonBattleValue.Add("PapalMilitiaMusketeer", 30);
        PersonBattleValue.Add("PapalMilitiaSwordsman", 30);
        PersonBattleValue.Add("PapalMilitiaHalberdier", 30);
        PersonBattleValue.Add("PapalMilitiaCavalry", 30);
        PersonBattleValue.Add("PapalVeteranCrossbowman", 30);
        PersonBattleValue.Add("PapalVeteranMusketeer", 30);
        PersonBattleValue.Add("PapalVeteranSwordsman", 30);
        PersonBattleValue.Add("PapalVeteranHalberdier", 30);
        PersonBattleValue.Add("PapalVeteranCavalry", 30);
        PersonBattleValue.Add("PapalEliteCrossbowman", 30);
        PersonBattleValue.Add("PapalEliteMusketeer", 30);
        PersonBattleValue.Add("PapalEliteSwordsman", 30);
        PersonBattleValue.Add("PapalEliteHalberdier", 30);
        PersonBattleValue.Add("PapalEliteCavalry", 30);
        //FRANCE
        PersonBattleValue.Add("FrenchRecruit", 10);
        PersonBattleValue.Add("FrenchMilitiaCrossbowman", 30);
        PersonBattleValue.Add("FrenchMilitiaMusketeer", 30);
        PersonBattleValue.Add("FrenchMilitiaSwordsman", 30);
        PersonBattleValue.Add("FrenchMilitiaHalberdier", 30);
        PersonBattleValue.Add("FrenchMilitiaCavalry", 30);
        PersonBattleValue.Add("FrenchVeteranCrossbowman", 30);
        PersonBattleValue.Add("FrenchVeteranMusketeer", 30);
        PersonBattleValue.Add("FrenchVeteranSwordsman", 30);
        PersonBattleValue.Add("FrenchVeteranHalberdier", 30);
        PersonBattleValue.Add("FrenchVeteranCavalry", 30);
        PersonBattleValue.Add("FrenchEliteCrossbowman", 30);
        PersonBattleValue.Add("FrenchEliteMusketeer", 30);
        PersonBattleValue.Add("FrenchEliteSwordsman", 30);
        PersonBattleValue.Add("FrenchEliteHalberdier", 30);
        PersonBattleValue.Add("FrenchEliteCavalry", 30);
        //IMPERIAL
        PersonBattleValue.Add("ImperialRecruit", 10);
        PersonBattleValue.Add("ImperialMilitiaCrossbowman", 30);
        PersonBattleValue.Add("ImperialMilitiaMusketeer", 30);
        PersonBattleValue.Add("ImperialMilitiaSwordsman", 30);
        PersonBattleValue.Add("ImperialMilitiaHalberdier", 30);
        PersonBattleValue.Add("ImperialMilitiaCavalry", 30);
        PersonBattleValue.Add("ImperialVeteranCrossbowman", 30);
        PersonBattleValue.Add("ImperialVeteranMusketeer", 30);
        PersonBattleValue.Add("ImperialVeteranSwordsman", 30);
        PersonBattleValue.Add("ImperialVeteranHalberdier", 30);
        PersonBattleValue.Add("ImperialVeteranCavalry", 30);
        PersonBattleValue.Add("ImperialEliteCrossbowman", 30);
        PersonBattleValue.Add("ImperialEliteMusketeer", 30);
        PersonBattleValue.Add("ImperialEliteSwordsman", 30);
        PersonBattleValue.Add("ImperialEliteHalberdier", 30);
        PersonBattleValue.Add("ImperialEliteCavalry", 30);

    }
    void itemInitialization()
    {

    }
}

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
public class Experience
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
public enum Ranking
{
    mainChar,
    recruit,
    militia,
    veteran,
    elite
};
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
public enum Faction
{
    mercenary,
    france,
    italy,
    bandits,
    empire,
    pope
}
public enum GridType
{
    rockAndTree,
    flatGrass,
    tree
}

public enum TroopSkill {
    none,
    walk,
    lunge,
    whirlwind,
    fire,
    holdSteady,
    execute,
    phalanx,
    charge,
    quickDraw,
    rainOfArrows
}
public class Item
{
    public string name;
    public int value;
    public float weight;
    public string description;
    public string city;
    public Item(string nameI, int valueI, string cityI, string descriptionI)
    {
        name = nameI;
        value = valueI;
        city = cityI;
        description = descriptionI;
    }
}