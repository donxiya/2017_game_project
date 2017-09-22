using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TroopControlPanel : MonoBehaviour {
    public GameObject panel, lunge, whirlwind, execute, phalanx, charge, fire, holdSteady, rainOfArrows, quickDraw;
    public GameObject curControledTroop;
    public static bool initialized;
	// Use this for initialization
	void Start () {
        
        panel.SetActive(false);
        initialized = false;
        initializeButtons();
	}
	
	// Update is called once per frame
	void Update () {
        if (BattleInteraction.curControlled != null && !initialized)
        {
            switch (BattleInteraction.curControlled.GetComponent<Troop>().person.troopType)
            {
                case TroopType.mainCharType:
                    makeMainCharacterPanel();
                    break;
                case TroopType.crossbowman:
                    makeCrossbowmanPanel();
                    break;
                case TroopType.musketeer:
                    makeMusketeerPanel();
                    break;
                case TroopType.swordsman:
                    makeSwordsmanPanel();
                    break;
                case TroopType.halberdier:
                    makeHalberdierPanel();
                    break;
                case TroopType.cavalry:
                    makeCavalryPanel();
                    break;
            }
            initialized = true;
            
            
        }
        
	}
    void makeMainCharacterPanel()
    {
        disableAllButton();
        lunge.SetActive(true);
        whirlwind.SetActive(true);
        execute.SetActive(true);
        phalanx.SetActive(true);
        charge.SetActive(true);
        fire.SetActive(true);
        holdSteady.SetActive(true);
    }
    void makeCrossbowmanPanel()
    {
        disableAllButton();
        whirlwind.SetActive(true);
        rainOfArrows.SetActive(true);
        quickDraw.SetActive(true);
    }
    void makeMusketeerPanel()
    {
        disableAllButton();
        whirlwind.SetActive(true);
        fire.SetActive(true);
        holdSteady.SetActive(true);
    }
    void makeSwordsmanPanel()
    {
        disableAllButton();
        lunge.SetActive(true);
        whirlwind.SetActive(true);
        execute.SetActive(true);
    }
    void makeHalberdierPanel()
    {
        disableAllButton();
        lunge.SetActive(true);
        whirlwind.SetActive(true);
        phalanx.SetActive(true);
    }
    void makeCavalryPanel()
    {
        disableAllButton();
        lunge.SetActive(true);
        whirlwind.SetActive(true);
        charge.SetActive(true);
    }
    void disableAllButton()
    {
        lunge.SetActive(false);
        whirlwind.SetActive(false);
        execute.SetActive(false);
        phalanx.SetActive(false);
        charge.SetActive(false);
        fire.SetActive(false);
        holdSteady.SetActive(false);
        rainOfArrows.SetActive(false);
        quickDraw.SetActive(false);
    }

    
    Person getInfo(GameObject troop)
    {
        return troop.GetComponent<Troop>().person;
    }

    public void initializePanel()
    {
        initialized = false;
        panel.SetActive(true);
    }
    void initializeButtons()
    {
        lunge.GetComponent<Button>().onClick.AddListener(delegate { BattleInteraction.skillMode = TroopSkill.lunge; hideIndicatorsInPanel(); });
        whirlwind.GetComponent<Button>().onClick.AddListener(delegate { BattleInteraction.skillMode = TroopSkill.whirlwind; hideIndicatorsInPanel(); });
        execute.GetComponent<Button>().onClick.AddListener(delegate { BattleInteraction.skillMode = TroopSkill.execute; hideIndicatorsInPanel(); });
        phalanx.GetComponent<Button>().onClick.AddListener(delegate { BattleInteraction.skillMode = TroopSkill.phalanx; hideIndicatorsInPanel(); });
        charge.GetComponent<Button>().onClick.AddListener(delegate { BattleInteraction.curControlled.GetComponent<Troop>().charge(); hideIndicatorsInPanel(); });
        fire.GetComponent<Button>().onClick.AddListener(delegate { BattleInteraction.skillMode = TroopSkill.fire; hideIndicatorsInPanel(); });
        holdSteady.GetComponent<Button>().onClick.AddListener(delegate { BattleInteraction.curControlled.GetComponent<Troop>().holdSteady(); hideIndicatorsInPanel(); });
        rainOfArrows.GetComponent<Button>().onClick.AddListener(delegate { BattleInteraction.skillMode = TroopSkill.rainOfArrows; hideIndicatorsInPanel(); });
        quickDraw.GetComponent<Button>().onClick.AddListener(delegate { BattleInteraction.skillMode = TroopSkill.quickDraw; hideIndicatorsInPanel(); });
    }

    void hideIndicatorsInPanel()
    {
        BattleInteraction.curControlled.GetComponent<Troop>().hideIndicators();
    }
    public void hidePanel()
    {
        initialized = false;
        panel.SetActive(false);
        
    }
    
}
