using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CIManagement : MonoBehaviour {
    public Text charismaTxt, intelligenceTxt, sparedPoints;
    public Button charismaPlus, intelligencePlus, perceptionPlus, endurancePlus;
    public Button p6a, p6b, p7a, p7b, p8a, p8b, p9a, p9b, p10a;
    public Button a6a, a6b, a7a, a7b, a8a, a8b, a9a, a9b, a10a;
    public Button reset;
    public RawImage charismaBar, intelligenceBar, barMax;
    float MAX_BAR_HEIGHT, MAX_BAR_WIDTH;
    // Use this for initialization
    void Start () {
        MAX_BAR_HEIGHT = barMax.rectTransform.sizeDelta.y;
        MAX_BAR_WIDTH = barMax.rectTransform.sizeDelta.x;
        initialization();
    }
	
	// Update is called once per frame
	void Update () {
        showStats();
	}

    void initialization()
    {

    }

    void showStats()
    {
        charismaTxt.text = Player.mainCharacter.stats.agility.ToString();
        intelligenceTxt.text = Player.mainCharacter.stats.strength.ToString();
        charismaBar.rectTransform.sizeDelta = new Vector2(MAX_BAR_WIDTH, MAX_BAR_HEIGHT * (Mathf.Clamp(Player.mainCharacter.stats.charisma, 0, 10) / 10.0f));
        intelligenceBar.rectTransform.sizeDelta = new Vector2(MAX_BAR_WIDTH, MAX_BAR_HEIGHT * (Mathf.Clamp(Player.mainCharacter.stats.intelligence, 0, 10) / 10.0f));
        if (Player.mainCharacter.exp.sparedPoint > 0)
        {
            sparedPoints.text = "Skill points left: " + Player.mainCharacter.exp.sparedPoint;
        }
        else
        {
            sparedPoints.text = "Reset";
        }

    }

}
