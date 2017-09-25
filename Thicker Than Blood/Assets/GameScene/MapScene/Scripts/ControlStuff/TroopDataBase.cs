using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopDataBase : MonoBehaviour {
    public static Dictionary<string, int> PersonBattleValue = new Dictionary<string, int>();
    public static List<Item> itemList = new List<Item>();
    public GameObject mainCharacter, secCharacter;
    public GameObject mercernaryRecruit, mercernaryMilitiaCrossbowman, mercernaryMilitiaMusketeer,
        mercernaryMilitiaSwordsman, mercernaryMilitiaHalberdier, mercernaryMilitiaCavalry;
    public GameObject mercernaryVeteranCrossbowman, mercernaryVeteranMusketeer,
        mercernaryVeteranSwordsman, mercernaryVeteranHalberdier, mercernaryVeteranCavalry;
    public GameObject mercernaryEliteCrossbowman, mercernaryEliteMusketeer,
        mercernaryEliteSwordsman, mercernaryEliteHalberdier, mercernaryEliteCavalry;

    public GameObject banditRecruit, banditMilitiaCrossbowman, banditMilitiaMusketeer,
        banditMilitiaSwordsman, banditMilitiaHalberdier, banditMilitiaCavalry;
    public GameObject banditVeteranCrossbowman, banditVeteranMusketeer,
        banditVeteranSwordsman, banditVeteranHalberdier, banditVeteranCavalry;
    public GameObject banditEliteCrossbowman, banditEliteMusketeer,
        banditEliteSwordsman, banditEliteHalberdier, banditEliteCavalry;

    public GameObject italianRecruit, italianMilitiaCrossbowman, italianMilitiaMusketeer,
        italianMilitiaSwordsman, italianMilitiaHalberdier, italianMilitiaCavalry;
    public GameObject italianVeteranCrossbowman, italianVeteranMusketeer,
        italianVeteranSwordsman, italianVeteranHalberdier, italianVeteranCavalry;
    public GameObject italianEliteCrossbowman, italianEliteMusketeer,
        italianEliteSwordsman, italianEliteHalberdier, italianEliteCavalry;

    public GameObject papalRecruit, papalMilitiaCrossbowman, papalMilitiaMusketeer,
        papalMilitiaSwordsman, papalMilitiaHalberdier, papalMilitiaCavalry;
    public GameObject papalVeteranCrossbowman, papalVeteranMusketeer,
        papalVeteranSwordsman, papalVeteranHalberdier, papalVeteranCavalry;
    public GameObject papalEliteCrossbowman, papalEliteMusketeer,
        papalEliteSwordsman, papalEliteHalberdier, papalEliteCavalry;

    public GameObject frenchRecruit, frenchMilitiaCrossbowman, frenchMilitiaMusketeer,
        frenchMilitiaSwordsman, frenchMilitiaHalberdier, frenchMilitiaCavalry;
    public GameObject frenchVeteranCrossbowman, frenchVeteranMusketeer,
        frenchVeteranSwordsman, frenchVeteranHalberdier, frenchVeteranCavalry;
    public GameObject frenchEliteCrossbowman, frenchEliteMusketeer,
        frenchEliteSwordsman, frenchEliteHalberdier, frenchEliteCavalry;

    public GameObject imperialRecruit, imperialMilitiaCrossbowman, imperialMilitiaMusketeer,
        imperialMilitiaSwordsman, imperialMilitiaHalberdier, imperialMilitiaCavalry;
    public GameObject imperialVeteranCrossbowman, imperialVeteranMusketeer,
        imperialVeteranSwordsman, imperialVeteranHalberdier, imperialVeteranCavalry;
    public GameObject imperialEliteCrossbowman, imperialEliteMusketeer,
        imperialEliteSwordsman, imperialEliteHalberdier, imperialEliteCavalry;

    // Use this for initialization
    void Awake () {
        personInitialization();
	}
	
    public static int getBattleValue(string troopName)
    {
        return 50;
        //return PersonBattleValue[troopName];
    }
    public TroopInfo getTroopInfo(Faction faction, TroopType tt, Ranking rk)
    {
        switch(faction)
        {
            case Faction.mercenary:
                return mercGetTroopInfoHelper(tt, rk);
            case Faction.bandits:
                return BanditGetTroopInfoHelper(tt, rk);
            case Faction.empire:
                return imperialGetTroopInfoHelper(tt, rk);
            case Faction.france:
                return frenchGetTroopInfoHelper(tt, rk);
            case Faction.pope:
                return papalGetTroopInfoHelper(tt, rk);
            case Faction.italy:
                return italianGetTroopInfoHelper(tt, rk);
        }
        return new TroopInfo();
    }
    public TroopInfo mercGetTroopInfoHelper(TroopType tt, Ranking rk)
    {
        TroopInfo result = new TroopInfo();
        if (rk == Ranking.mainChar)
        {
            if (tt == TroopType.mainCharType)
            {
                result.battleValue = 0;
                result.model = mainCharacter;
            } else
            {
                result.battleValue = 0;
                result.model = secCharacter;
            }
            return result;
        }
        switch (tt)
        {
            case TroopType.recruitType:
                result.battleValue = 10;
                result.model = mercernaryRecruit;
                break;
            case TroopType.crossbowman:
                switch (rk)
                {
                    case Ranking.militia:
                        result.battleValue = 20;
                        result.model = mercernaryMilitiaCrossbowman;
                        break;
                    case Ranking.veteran:
                        result.battleValue = 50;
                        result.model = mercernaryVeteranCrossbowman;
                        break;
                    case Ranking.elite:
                        result.battleValue = 150;
                        result.model = mercernaryEliteCrossbowman;
                        break;
                }
                break;
            case TroopType.musketeer:
                switch (rk)
                {
                    case Ranking.militia:
                        result.battleValue = 20;
                        result.model = mercernaryMilitiaMusketeer;
                        break;
                    case Ranking.veteran:
                        result.battleValue = 50;
                        result.model = mercernaryVeteranMusketeer;
                        break;
                    case Ranking.elite:
                        result.battleValue = 150;
                        result.model = mercernaryEliteMusketeer;
                        break;
                }
                break;
            case TroopType.swordsman:
                switch (rk)
                {
                    case Ranking.militia:
                        result.battleValue = 20;
                        result.model = mercernaryMilitiaSwordsman;
                        break;
                    case Ranking.veteran:
                        result.battleValue = 50;
                        result.model = mercernaryVeteranSwordsman;
                        break;
                    case Ranking.elite:
                        result.battleValue = 150;
                        result.model = mercernaryEliteSwordsman;
                        break;
                }
                break;
            case TroopType.halberdier:
                switch (rk)
                {
                    case Ranking.militia:
                        result.battleValue = 20;
                        result.model = mercernaryMilitiaHalberdier;
                        break;
                    case Ranking.veteran:
                        result.battleValue = 50;
                        result.model = mercernaryVeteranHalberdier;
                        break;
                    case Ranking.elite:
                        result.battleValue = 150;
                        result.model = mercernaryEliteHalberdier;
                        break;
                }
                break;
            case TroopType.cavalry:
                switch (rk)
                {
                    case Ranking.militia:
                        result.battleValue = 20;
                        result.model = mercernaryMilitiaCavalry;
                        break;
                    case Ranking.veteran:
                        result.battleValue = 50;
                        result.model = mercernaryVeteranCavalry;
                        break;
                    case Ranking.elite:
                        result.battleValue = 150;
                        result.model = mercernaryEliteCavalry;
                        break;
                }
                break;
        }
        return result;
    }
    public TroopInfo BanditGetTroopInfoHelper(TroopType tt, Ranking rk)
    {
        TroopInfo result = new TroopInfo();
        switch (tt)
        {
            case TroopType.recruitType:
                result.battleValue = 10;
                result.model = banditRecruit;
                break;
            case TroopType.crossbowman:
                switch (rk)
                {
                    case Ranking.militia:
                        result.battleValue = 20;
                        result.model = banditMilitiaCrossbowman;
                        break;
                    case Ranking.veteran:
                        result.battleValue = 50;
                        result.model = banditVeteranCrossbowman;
                        break;
                    case Ranking.elite:
                        result.battleValue = 150;
                        result.model = banditEliteCrossbowman;
                        break;
                }
                break;
            case TroopType.musketeer:
                switch (rk)
                {
                    case Ranking.militia:
                        result.battleValue = 20;
                        result.model = banditMilitiaMusketeer;
                        break;
                    case Ranking.veteran:
                        result.battleValue = 50;
                        result.model = banditVeteranMusketeer;
                        break;
                    case Ranking.elite:
                        result.battleValue = 150;
                        result.model = banditEliteMusketeer;
                        break;
                }
                break;
            case TroopType.swordsman:
                switch (rk)
                {
                    case Ranking.militia:
                        result.battleValue = 20;
                        result.model = banditMilitiaSwordsman;
                        break;
                    case Ranking.veteran:
                        result.battleValue = 50;
                        result.model = banditVeteranSwordsman;
                        break;
                    case Ranking.elite:
                        result.battleValue = 150;
                        result.model = banditEliteSwordsman;
                        break;
                }
                break;
            case TroopType.halberdier:
                switch (rk)
                {
                    case Ranking.militia:
                        result.battleValue = 20;
                        result.model = banditMilitiaHalberdier;
                        break;
                    case Ranking.veteran:
                        result.battleValue = 50;
                        result.model = banditVeteranHalberdier;
                        break;
                    case Ranking.elite:
                        result.battleValue = 150;
                        result.model = banditEliteHalberdier;
                        break;
                }
                break;
            case TroopType.cavalry:
                switch (rk)
                {
                    case Ranking.militia:
                        result.battleValue = 20;
                        result.model = banditMilitiaCavalry;
                        break;
                    case Ranking.veteran:
                        result.battleValue = 50;
                        result.model = banditVeteranCavalry;
                        break;
                    case Ranking.elite:
                        result.battleValue = 150;
                        result.model = banditEliteCavalry;
                        break;
                }
                break;
        }
        return result;
    }
    public TroopInfo italianGetTroopInfoHelper(TroopType tt, Ranking rk)
    {
        TroopInfo result = new TroopInfo();
        switch (tt)
        {
            case TroopType.recruitType:
                result.battleValue = 10;
                result.model = italianRecruit;
                break;
            case TroopType.crossbowman:
                switch (rk)
                {
                    case Ranking.militia:
                        result.battleValue = 20;
                        result.model = italianMilitiaCrossbowman;
                        break;
                    case Ranking.veteran:
                        result.battleValue = 50;
                        result.model = italianVeteranCrossbowman;
                        break;
                    case Ranking.elite:
                        result.battleValue = 150;
                        result.model = italianEliteCrossbowman;
                        break;
                }
                break;
            case TroopType.musketeer:
                switch (rk)
                {
                    case Ranking.militia:
                        result.battleValue = 20;
                        result.model = italianMilitiaMusketeer;
                        break;
                    case Ranking.veteran:
                        result.battleValue = 50;
                        result.model = italianVeteranMusketeer;
                        break;
                    case Ranking.elite:
                        result.battleValue = 150;
                        result.model = italianEliteMusketeer;
                        break;
                }
                break;
            case TroopType.swordsman:
                switch (rk)
                {
                    case Ranking.militia:
                        result.battleValue = 20;
                        result.model = italianMilitiaSwordsman;
                        break;
                    case Ranking.veteran:
                        result.battleValue = 50;
                        result.model = italianVeteranSwordsman;
                        break;
                    case Ranking.elite:
                        result.battleValue = 150;
                        result.model = italianEliteSwordsman;
                        break;
                }
                break;
            case TroopType.halberdier:
                switch (rk)
                {
                    case Ranking.militia:
                        result.battleValue = 20;
                        result.model = italianMilitiaHalberdier;
                        break;
                    case Ranking.veteran:
                        result.battleValue = 50;
                        result.model = italianVeteranHalberdier;
                        break;
                    case Ranking.elite:
                        result.battleValue = 150;
                        result.model = italianEliteHalberdier;
                        break;
                }
                break;
            case TroopType.cavalry:
                switch (rk)
                {
                    case Ranking.militia:
                        result.battleValue = 20;
                        result.model = italianMilitiaCavalry;
                        break;
                    case Ranking.veteran:
                        result.battleValue = 50;
                        result.model = italianVeteranCavalry;
                        break;
                    case Ranking.elite:
                        result.battleValue = 150;
                        result.model = italianEliteCavalry;
                        break;
                }
                break;
        }
        return result;
    }
    public TroopInfo papalGetTroopInfoHelper(TroopType tt, Ranking rk)
    {
        TroopInfo result = new TroopInfo();
        switch (tt)
        {
            case TroopType.recruitType:
                result.battleValue = 10;
                result.model = papalRecruit;
                break;
            case TroopType.crossbowman:
                switch (rk)
                {
                    case Ranking.militia:
                        result.battleValue = 20;
                        result.model = papalMilitiaCrossbowman;
                        break;
                    case Ranking.veteran:
                        result.battleValue = 50;
                        result.model = papalVeteranCrossbowman;
                        break;
                    case Ranking.elite:
                        result.battleValue = 150;
                        result.model = papalEliteCrossbowman;
                        break;
                }
                break;
            case TroopType.musketeer:
                switch (rk)
                {
                    case Ranking.militia:
                        result.battleValue = 20;
                        result.model = papalMilitiaMusketeer;
                        break;
                    case Ranking.veteran:
                        result.battleValue = 50;
                        result.model = papalVeteranMusketeer;
                        break;
                    case Ranking.elite:
                        result.battleValue = 150;
                        result.model = papalEliteMusketeer;
                        break;
                }
                break;
            case TroopType.swordsman:
                switch (rk)
                {
                    case Ranking.militia:
                        result.battleValue = 20;
                        result.model = papalMilitiaSwordsman;
                        break;
                    case Ranking.veteran:
                        result.battleValue = 50;
                        result.model = papalVeteranSwordsman;
                        break;
                    case Ranking.elite:
                        result.battleValue = 150;
                        result.model = papalEliteSwordsman;
                        break;
                }
                break;
            case TroopType.halberdier:
                switch (rk)
                {
                    case Ranking.militia:
                        result.battleValue = 20;
                        result.model = papalMilitiaHalberdier;
                        break;
                    case Ranking.veteran:
                        result.battleValue = 50;
                        result.model = papalVeteranHalberdier;
                        break;
                    case Ranking.elite:
                        result.battleValue = 150;
                        result.model = papalEliteHalberdier;
                        break;
                }
                break;
            case TroopType.cavalry:
                switch (rk)
                {
                    case Ranking.militia:
                        result.battleValue = 20;
                        result.model = papalMilitiaCavalry;
                        break;
                    case Ranking.veteran:
                        result.battleValue = 50;
                        result.model = papalVeteranCavalry;
                        break;
                    case Ranking.elite:
                        result.battleValue = 150;
                        result.model = papalEliteCavalry;
                        break;
                }
                break;
        }
        return result;
    }
    public TroopInfo frenchGetTroopInfoHelper(TroopType tt, Ranking rk)
    {
        TroopInfo result = new TroopInfo();
        switch (tt)
        {
            case TroopType.recruitType:
                result.battleValue = 10;
                result.model = frenchRecruit;
                break;
            case TroopType.crossbowman:
                switch (rk)
                {
                    case Ranking.militia:
                        result.battleValue = 20;
                        result.model = frenchMilitiaCrossbowman;
                        break;
                    case Ranking.veteran:
                        result.battleValue = 50;
                        result.model = frenchVeteranCrossbowman;
                        break;
                    case Ranking.elite:
                        result.battleValue = 150;
                        result.model = frenchEliteCrossbowman;
                        break;
                }
                break;
            case TroopType.musketeer:
                switch (rk)
                {
                    case Ranking.militia:
                        result.battleValue = 20;
                        result.model = frenchMilitiaMusketeer;
                        break;
                    case Ranking.veteran:
                        result.battleValue = 50;
                        result.model = frenchVeteranMusketeer;
                        break;
                    case Ranking.elite:
                        result.battleValue = 150;
                        result.model = frenchEliteMusketeer;
                        break;
                }
                break;
            case TroopType.swordsman:
                switch (rk)
                {
                    case Ranking.militia:
                        result.battleValue = 20;
                        result.model = frenchMilitiaSwordsman;
                        break;
                    case Ranking.veteran:
                        result.battleValue = 50;
                        result.model = frenchVeteranSwordsman;
                        break;
                    case Ranking.elite:
                        result.battleValue = 150;
                        result.model = frenchEliteSwordsman;
                        break;
                }
                break;
            case TroopType.halberdier:
                switch (rk)
                {
                    case Ranking.militia:
                        result.battleValue = 20;
                        result.model = frenchMilitiaHalberdier;
                        break;
                    case Ranking.veteran:
                        result.battleValue = 50;
                        result.model = frenchVeteranHalberdier;
                        break;
                    case Ranking.elite:
                        result.battleValue = 150;
                        result.model = frenchEliteHalberdier;
                        break;
                }
                break;
            case TroopType.cavalry:
                switch (rk)
                {
                    case Ranking.militia:
                        result.battleValue = 20;
                        result.model = frenchMilitiaCavalry;
                        break;
                    case Ranking.veteran:
                        result.battleValue = 50;
                        result.model = frenchVeteranCavalry;
                        break;
                    case Ranking.elite:
                        result.battleValue = 150;
                        result.model = frenchEliteCavalry;
                        break;
                }
                break;
        }
        return result;
    }
    public TroopInfo imperialGetTroopInfoHelper(TroopType tt, Ranking rk)
    {
        TroopInfo result = new TroopInfo();
        switch (tt)
        {
            case TroopType.recruitType:
                result.battleValue = 10;
                result.model = imperialRecruit;
                break;
            case TroopType.crossbowman:
                switch (rk)
                {
                    case Ranking.militia:
                        result.battleValue = 20;
                        result.model = imperialMilitiaCrossbowman;
                        break;
                    case Ranking.veteran:
                        result.battleValue = 50;
                        result.model = imperialVeteranCrossbowman;
                        break;
                    case Ranking.elite:
                        result.battleValue = 150;
                        result.model = imperialEliteCrossbowman;
                        break;
                }
                break;
            case TroopType.musketeer:
                switch (rk)
                {
                    case Ranking.militia:
                        result.battleValue = 20;
                        result.model = imperialMilitiaMusketeer;
                        break;
                    case Ranking.veteran:
                        result.battleValue = 50;
                        result.model = imperialVeteranMusketeer;
                        break;
                    case Ranking.elite:
                        result.battleValue = 150;
                        result.model = imperialEliteMusketeer;
                        break;
                }
                break;
            case TroopType.swordsman:
                switch (rk)
                {
                    case Ranking.militia:
                        result.battleValue = 20;
                        result.model = imperialMilitiaSwordsman;
                        break;
                    case Ranking.veteran:
                        result.battleValue = 50;
                        result.model = imperialVeteranSwordsman;
                        break;
                    case Ranking.elite:
                        result.battleValue = 150;
                        result.model = imperialEliteSwordsman;
                        break;
                }
                break;
            case TroopType.halberdier:
                switch (rk)
                {
                    case Ranking.militia:
                        result.battleValue = 20;
                        result.model = imperialMilitiaHalberdier;
                        break;
                    case Ranking.veteran:
                        result.battleValue = 50;
                        result.model = imperialVeteranHalberdier;
                        break;
                    case Ranking.elite:
                        result.battleValue = 150;
                        result.model = imperialEliteHalberdier;
                        break;
                }
                break;
            case TroopType.cavalry:
                switch (rk)
                {
                    case Ranking.militia:
                        result.battleValue = 20;
                        result.model = imperialMilitiaCavalry;
                        break;
                    case Ranking.veteran:
                        result.battleValue = 50;
                        result.model = imperialVeteranCavalry;
                        break;
                    case Ranking.elite:
                        result.battleValue = 150;
                        result.model = imperialEliteCavalry;
                        break;
                }
                break;
        }
        return result;
    }

    public GameObject getTroopObject(Faction f, TroopType tt, Ranking rk)
    {
        return getTroopInfo(f, tt, rk).model;
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
    guard,
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

public class TroopInfo{
    public int battleValue;
    public GameObject model;
 }