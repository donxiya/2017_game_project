using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static MainCharacter mainCharacter;
    public static MainCharacter secCharacter;
    public static MainParty mainParty;
    void Awake()
    {
        initializeMainPlayers();
        initializeMainParty();
    }

    void initializeMainPlayers()
    {
        Stats mStats = new Stats(1, 2, 3, 4, 5, 6);
        Experience mExp = new Experience(0, 1, 5);
        mainCharacter = new MainCharacter("MAIN", mStats, Ranking.mainChar,
            TroopType.mainCharType, Faction.mercenary, mExp);
        Experience sExp = new Experience(0, 1, 5);
        secCharacter = new MainCharacter("SEC", mStats, Ranking.mainChar,
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
        mainParty.addToParty(mainParty.makeGenericPerson(TroopType.halberdier, Ranking.elite));
        mainParty.addToParty(mainParty.makeGenericPerson(TroopType.halberdier, Ranking.elite));
        mainParty.addToParty(mainParty.makeGenericPerson(TroopType.halberdier, Ranking.elite));
        mainParty.addToParty(mainParty.makeGenericPerson(TroopType.halberdier, Ranking.elite));
        mainParty.addToParty(mainParty.makeGenericPerson(TroopType.halberdier, Ranking.elite));
        mainParty.cash = 200;
    }
}
