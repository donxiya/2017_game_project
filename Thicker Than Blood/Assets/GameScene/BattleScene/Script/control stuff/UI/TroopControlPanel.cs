using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TroopControlPanel : MonoBehaviour {
    public GameObject panel, lunge, whirlWind, execute, enGarde, charge, fire, holdSteady, rainOfArrows, quickDraw;
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
        return troop.GetComponent<PlayerTroop>().person;
    }

    void initializePanel()
    {
        
        panel.SetActive(true);
    }
    void initializeButtons()
    {
        lunge.GetComponent<Button>().onClick.AddListener(delegate { BattleInteraction.skillMode = TroopSkill.lunge; });
        //whirlWind.GetComponent<Button>().onClick.AddListener(delegate { BattleInteraction.skillMode = TroopSkill.whirlwind; });
        //execute.GetComponent<Button>().onClick.AddListener(delegate { BattleInteraction.skillMode = TroopSkill.execute; });
        //enGarde.GetComponent<Button>().onClick.AddListener(delegate { BattleInteraction.skillMode = TroopSkill.enGarde; });
        //charge.GetComponent<Button>().onClick.AddListener(delegate { BattleInteraction.skillMode = TroopSkill.charge; });
        //fire.GetComponent<Button>().onClick.AddListener(delegate { BattleInteraction.skillMode = TroopSkill.fire; });
        //holdSteady.GetComponent<Button>().onClick.AddListener(delegate { BattleInteraction.skillMode = TroopSkill.holdSteady; });
        //rainOfArrows.GetComponent<Button>().onClick.AddListener(delegate { BattleInteraction.skillMode = TroopSkill.rainOfArrows; });
        //quickDraw.GetComponent<Button>().onClick.AddListener(delegate { BattleInteraction.skillMode = TroopSkill.quickDraw; });
    }
    void hidePanel()
    {
        panel.SetActive(false);
    }
    
}
