using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static MainCharacter mainCharacter;
    public static MainCharacter secCharacter;
    public static MainParty mainParty;
    void Start()
    {
        initializeMainPlayers();
        initializeMainParty();
        if (BattleCentralControl.playerParty == null)
        {
            BattleCentralControl.playerParty = mainParty;
        }
        
        initializeDummy();
    }

    void initializeMainPlayers()
    {
        Stats mStats = new Stats(10, 10, 10, 10, 10, 10);
        Experience mExp = new Experience(0, 1, 5);
        mainCharacter = new MainCharacter("Nicola Da Roma", mStats, Ranking.mainChar,
            TroopType.mainCharType, Faction.mercenary, mExp);
        mainCharacter.skillTree.skillTreeInitialization();
        Experience sExp = new Experience(0, 1, 5);
        secCharacter = new MainCharacter("Rachele Sforza", mStats, Ranking.mainChar,
            TroopType.crossbowman, Faction.mercenary, sExp);
    }
    void initializeMainParty()
    {
        mainParty = new MainParty(mainCharacter, "Crimson Griffin", Faction.mercenary, 300);
        mainParty.addToParty(secCharacter);
        mainParty.addToParty(mainParty.makeGenericPerson(TroopType.halberdier, Ranking.militia));
        mainParty.addToParty(mainParty.makeGenericPerson(TroopType.cavalry, Ranking.veteran));
        mainParty.addToParty(mainParty.makeGenericPerson(TroopType.swordsman, Ranking.militia));
        mainParty.addToParty(mainParty.makeGenericPerson(TroopType.halberdier, Ranking.elite));
        mainParty.addToParty(mainParty.makeGenericPerson(TroopType.crossbowman, Ranking.elite));
        mainParty.addToParty(mainParty.makeGenericPerson(TroopType.musketeer, Ranking.elite));
        mainParty.cash = 200;
    }
    void initializeDummy()
    {
        if (BattleCentralControl.enemyParty == null)
        {
            BattleCentralControl.enemyParty = new Party("bandit", Faction.bandits, 800);
            makeParty(BattleCentralControl.enemyParty);
        }
        
    }
    void makeParty(Party npcParty)
    {
        TroopType tt = npcParty.randomTroopType(20, 20, 10, 30, 10, 10);
        Ranking rk = npcParty.randomRanking(0, 10, 10, 10);
        if (tt == TroopType.recruitType)
        {
            rk = Ranking.recruit;
        }
        Person p = npcParty.makeGenericPerson(tt, rk);
        if (npcParty.battleValue >= p.battleValue)
        {
            if (npcParty.addToParty(npcParty.makeGenericPerson(tt, rk)))
            {
                npcParty.battleValue -= p.battleValue;
            }
            if (npcParty.battleValue > 20)
            {
                makeParty(npcParty);
            }
        }
        else
        {
            if (npcParty.battleValue > 20)
            {
                makeParty(npcParty);
            }
        }

    }
}
