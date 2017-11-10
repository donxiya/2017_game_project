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
        if (BattleCentralControl.battleStart && !gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        
		if (BattleCentralControl.playerTurn && !BattleInteraction.inAction
            && BattleCentralControl.playerTroopOnField.Count >= 1)
        {
            if (!endTurnButton.GetComponent<Button>().interactable)
            {
                endTurnButton.GetComponent<Button>().interactable = true;
            }
        }
        else{
            if (endTurnButton.GetComponent<Button>().interactable)
            {
                endTurnButton.GetComponent<Button>().interactable = false;
            }
        }
    }
    
}
