using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TroopControlPanel : MonoBehaviour {
    public GameObject panel, lunge, whirlwind, execute, phalanx, charge, fire, holdSteady, rainOfArrows, quickDraw;
    public GameObject curControledTroop;
    public bool initialized;
	// Use this for initialization
	void Start () {
        
        panel.SetActive(false);
        initialized = false;
        initializeButtons();
	}
	
	// Update is called once per frame
	void Update () {
	}

    Person getInfo(GameObject troop)
    {
        return troop.GetComponent<Troop>().person;
    }

    void initializePanel()
    {
        
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
    void hidePanel()
    {
        panel.SetActive(false);
    }
    
}
