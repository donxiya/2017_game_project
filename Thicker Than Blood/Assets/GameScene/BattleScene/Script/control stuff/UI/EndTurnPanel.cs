using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurnPanel : MonoBehaviour {

    public GameObject endTurnButton;
	// Use this for initialization
	void Start () {
        endTurnButton.GetComponent<Button>().onClick.AddListener(
            delegate { BattleCentralControl.endTurnPrep();
                BattleCentralControl.playerTurn = false;
            });
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (BattleCentralControl.battleStart)
        {
            gameObject.SetActive(true);

            if (BattleCentralControl.playerTroopOnField.Count <= 0)
            {
                endTurnButton.GetComponent<Button>().interactable = false;
            } else
            {
                endTurnButton.GetComponent<Button>().interactable = true;
            }
        }
		if (BattleCentralControl.playerTurn && !BattleInteraction.inAction)
        {
            endTurnButton.GetComponent<Button>().enabled = true;
        }
        else{
            endTurnButton.GetComponent<Button>().enabled = false;
        }
	}
    
}
