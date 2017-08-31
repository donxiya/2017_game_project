using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SAPEPanel : MonoBehaviour {

    public Text strengthText;
    public Text agilityText;
    public Text perceptionText;
    public Text enduranceText;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        strengthText.text = Player.mainCharacter.stats.strength.ToString();
        agilityText.text = Player.mainCharacter.stats.agility.ToString();
        perceptionText.text = Player.mainCharacter.stats.perception.ToString();
        enduranceText.text = Player.mainCharacter.stats.endurance.ToString();
    }
}
