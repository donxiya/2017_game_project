using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState : MonoBehaviour {
    public List<StatBonus> baseAdditives { get; set; }
    public int baseValue { get; set; }
    public string statName { get; set; }
    public string statDescription { get; set; }
    public int finalValue { get; set; }
    
    public BaseState(int baseValue, string statName, string statDescription)
    {
        this.baseAdditives = new List<StatBonus>();
        this.baseValue = baseValue;
        this.statName = statName;
        this.statDescription = statDescription;
    }

    public void addStatBonus(StatBonus statBonus)
    {
        this.baseAdditives.Add(statBonus);
    }

    public void removeStatBonus (StatBonus statBonus)
    {
        this.baseAdditives.Remove(baseAdditives.Find(x=> x.BonusValue == statBonus.BonusValue));
    }

    public int getCalculatedValue()
    {
        this.finalValue = 0;
        this.baseAdditives.ForEach(x => this.finalValue += x.BonusValue);
        finalValue += baseValue;
        return finalValue;
    }
}
