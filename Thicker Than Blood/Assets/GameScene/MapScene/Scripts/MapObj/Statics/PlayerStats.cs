using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    static public List<BaseState> player_1_stats = new List<BaseState>();

    void Start()
    {
        player_1_stats.Add(new BaseState(4, "Strength", "this is yout Strength"));
        player_1_stats[0].addStatBonus(new StatBonus(5));
        
    }
}
