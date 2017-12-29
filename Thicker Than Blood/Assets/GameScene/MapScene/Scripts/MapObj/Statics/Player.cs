using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static MainCharacter mainCharacter;
    public static MainCharacter secCharacter;
    public static MainParty mainParty;
    private void Awake()
    {
        //if (Serializer.HasKey("tempPlayer"))
        //{
        //    Debug.Log("here");
        //    mainParty = Serializer.Load<MainParty>("tempPlayer");
        //} else
        //{
        //    Debug.Log("cant find");
        //}
        
        
    }
    void Start()
    {
        //should be removable
        //if (BattleCentralControl.playerParty == null)
        //{
        //    BattleCentralControl.playerParty = SaveLoadSystem.saveLoadSystem.mainParty;
        //}
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
