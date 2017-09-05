using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TroopSelection : MonoBehaviour {
    public GameObject battleInitializationPanel, selectingTroopUnitButton;
    public Text selectingTroopName, selectingTroopLevel;
    public RawImage selectingMaxHealthBar, selectingHealthBar, selectingStaminaBar;
    public Text inspectTroopName, inspectTroopLevel, insepctMaxHealthNum, inspectHealthNum,
        inspectStamina, inspectS, inspectA, inspectP, inspectE;
    public RawImage inspectMaxHealthBar, inspectHealthBar, inspectStaminaBar, 
        inspectSBar, inspectABar, inspectPBar, inspectEBar;
    public Button addButton, removeButton, startButton;
    public GameObject selectedTroopUnitButton;
    public Text selectedTroopName, selectedTroopLevel;
    public RawImage selectedMaxHealthBar, selectedHealthBar, selectedStaminaBar;
    public Texture2D regularTroopBackground, curSelectingBackgroundImage, curSelectedBackgroundImage;
    bool inSelecting;
    GameObject curSelectingButton, curSelectedButton;
    Person curSelectingPerson, curSelectedPerson;
    public GameObject troopPlacingPanel, endTurnPanel;
    public List<Person> selectingMembers, selectedMembers;
    Dictionary<Person, GameObject> troopDict;
    // Use this for initialization
    void Start () {
        initialization();
        arrangeButtons();
        selectingTroopUnitButton.SetActive(false);
        selectedTroopUnitButton.SetActive(false);
        inspectUnit(curSelectingPerson);
        
    }
	
	// Update is called once per frame
	void Update () {
        if (inSelecting)
        {
            if (curSelectingPerson != null)
            {
                inspectUnit(curSelectingPerson);
            }
        } else
        {
            if (curSelectedPerson != null)
            {
                inspectUnit(curSelectedPerson);
            }
        }
        if (selectedMembers.Contains(curSelectingPerson) || curSelectingButton == null || !inSelecting)
        {
            addButton.interactable = false;
        }
        else
        {
            addButton.interactable = true;
        }
        if (selectingMembers.Contains(curSelectedPerson) || curSelectedButton == null || inSelecting)
        {
            removeButton.interactable = false;
        }
        else
        {
            removeButton.interactable = true;
        }
        if (selectedMembers.Count == 0)
        {
            startButton.interactable = false;
        } else
        {
            startButton.interactable = true;
        }

    }

    void initialization()
    {
        inSelecting = true;
        curSelectingPerson = Player.mainCharacter;
        selectingMembers = Player.mainParty.partyMember;
        selectedMembers = new List<Person>();
        troopDict = new Dictionary<Person, GameObject>();
        
        addButton.GetComponent<Button>().onClick.AddListener(delegate { addToSelected(); });
        removeButton.GetComponent<Button>().onClick.AddListener(delegate { removeFromSelected(); });
        startButton.GetComponent<Button>().onClick.AddListener(delegate { startBattle(); });
    }
    GameObject makeTroopSelectingButton(Person unit)
    {
        selectingTroopName.text = unit.name;
        selectingTroopLevel.text = unit.exp.level.ToString();
        selectingMaxHealthBar.rectTransform.sizeDelta = new Vector2(unit.getHealthMax() / 2, 14);
        selectingHealthBar.rectTransform.sizeDelta = new Vector2(unit.getHealthMax() / 2 - 4, 10);
        selectingStaminaBar.rectTransform.sizeDelta = new Vector2(unit.getStaminaMax() / 2 * 5, 14);
        GameObject troopUnit = Object.Instantiate(selectingTroopUnitButton);
        troopUnit.transform.Find("TroopUnitBackground").GetComponent<RawImage>().texture
                = regularTroopBackground;
        troopUnit.transform.SetParent(selectingTroopUnitButton.transform.parent, false);
        troopUnit.GetComponent<Button>().onClick.AddListener(delegate { curSelectingPerson = unit;
            inSelecting = true; updateTroopUnitBackground(); });
        troopUnit.SetActive(true);
        return troopUnit;
    }
    GameObject makeTroopSelectedButton(Person unit)
    {
        selectedTroopName.text = unit.name;
        selectedTroopLevel.text = unit.exp.level.ToString();
        selectedMaxHealthBar.rectTransform.sizeDelta = new Vector2(unit.getHealthMax() / 2, 14);
        selectedHealthBar.rectTransform.sizeDelta = new Vector2(unit.getHealthMax() / 2 - 4, 10);
        selectedStaminaBar.rectTransform.sizeDelta = new Vector2(unit.getStaminaMax() / 2 *5, 14);
        GameObject troopUnit = Object.Instantiate(selectedTroopUnitButton);
        troopUnit.transform.Find("TroopUnitBackground").GetComponent<RawImage>().texture
                = regularTroopBackground;
        troopUnit.transform.SetParent(selectedTroopUnitButton.transform.parent, false);
        troopUnit.GetComponent<Button>().onClick.AddListener(delegate { curSelectedPerson = unit;
            inSelecting = false; updateTroopUnitBackground(); });
        troopUnit.SetActive(true);
        return troopUnit;
    }
    void inspectUnit(Person unit)
    {
        curSelectingPerson = unit;
        curSelectingButton = troopDict[curSelectingPerson];
        inspectTroopName.text = unit.name;
        inspectTroopLevel.text = unit.exp.level.ToString();
        inspectMaxHealthBar.rectTransform.sizeDelta = new Vector2(unit.getHealthMax() / 2, 14);
        inspectHealthBar.rectTransform.sizeDelta = new Vector2(unit.getHealthMax() / 2 - 5, 10);
        inspectStaminaBar.rectTransform.sizeDelta = new Vector2(unit.getStaminaMax() / 2 * 5, 14);
        inspectSBar.rectTransform.sizeDelta = new Vector2(unit.stats.strength * 5, 14);
        inspectABar.rectTransform.sizeDelta = new Vector2(unit.stats.agility * 5, 14);
        inspectPBar.rectTransform.sizeDelta = new Vector2(unit.stats.perception * 5, 14);
        inspectEBar.rectTransform.sizeDelta = new Vector2(unit.stats.endurance * 5, 14);
        inspectS.text = unit.stats.strength.ToString();
        inspectA.text = unit.stats.agility.ToString();
        inspectP.text = unit.stats.perception.ToString();
        inspectE.text = unit.stats.endurance.ToString();
    }
    void addToSelected()
    {
        int index = 0;
        if (!selectedMembers.Contains(curSelectingPerson))
        {
            selectedMembers.Add(curSelectingPerson);
            index = selectingMembers.IndexOf(curSelectingPerson);
            selectingMembers.Remove(curSelectingPerson);
            reArrangeButtons();
            Object.Destroy(curSelectingButton);
        }
        if (selectingMembers.Count > 0)
        {
            if (index < selectingMembers.Count)
            {
                curSelectingPerson = selectingMembers[index];
            } else
            {
                index -= 1;
            }
            
        } else
        {
            curSelectedPerson = null;
        }
        updateTroopUnitBackground();
    }
    void removeFromSelected()
    {
        int index = 0;
        if (!selectingMembers.Contains(curSelectedPerson))
        {
            selectingMembers.Add(curSelectingPerson);
            index = selectedMembers.IndexOf(curSelectedPerson);
            selectedMembers.Remove(curSelectedPerson);
            reArrangeButtons();
            Object.Destroy(curSelectedButton);
        }
        if (selectedMembers.Count > 0)
        {
            if (index < selectedMembers.Count)
            {
                curSelectedPerson = selectedMembers[index];
            } else
            {
                index -= 1;
            }
            
        } else
        {
            curSelectedPerson = null;
        }
        updateTroopUnitBackground();
    }
    void updateTroopUnitBackground()
    {
        foreach (KeyValuePair<Person, GameObject> button in troopDict)
        {
            button.Value.transform.Find("TroopUnitBackground").GetComponent<RawImage>().texture
                = regularTroopBackground;
        }
        if (inSelecting)
        {
            if (curSelectingPerson != null)
            {
                curSelectingButton = troopDict[curSelectingPerson];
            }
            if (curSelectingButton != null)
            {
                curSelectingButton.transform.Find("TroopUnitBackground").GetComponent<RawImage>().texture
                = curSelectingBackgroundImage;
            }
        }
        else
        {
            if (curSelectedPerson != null)
            {
                curSelectedButton = troopDict[curSelectedPerson];
            }
            if (curSelectedButton != null)
            {
                curSelectedButton.transform.Find("TroopUnitBackground").GetComponent<RawImage>().texture
                = curSelectedBackgroundImage;
            }
        }
        
        
    }
    void arrangeButtons()
    {
        List<Person> temp = selectingMembers;
        if (selectingMembers.Count > 0)
        {
            selectingMembers = sortList(temp);
        }
        foreach (Person p in selectingMembers)
        {

            troopDict.Add(p, makeTroopSelectingButton(p));
        }
    }
    void reArrangeButtons()
    {
        List<Person> temp = selectingMembers;
        if (selectingMembers.Count > 0)
        {
            selectingMembers = sortList(temp);
        }
        temp = selectedMembers;
        if (selectedMembers.Count > 0)
        {
            selectedMembers = sortList(temp);
        }
        foreach (Person p in selectingMembers)
        {
            Object.Destroy(troopDict[p]);
        }
        foreach (Person p in selectedMembers)
        {
            Object.Destroy(troopDict[p]);
        }
        troopDict.Clear();
        foreach (Person p in selectingMembers)
        {

            troopDict.Add(p, makeTroopSelectingButton(p));
        }
        foreach (Person p in selectedMembers)
        {
            troopDict.Add(p, makeTroopSelectedButton(p));
        }
    }
    void startBattle()
    {
        battleInitializationPanel.SetActive(false);
        BattleCamera.startBattle = true;
        BattleCentralControl.battleStart = true;
        TroopPlacing.battleTroop = selectedMembers;
        troopPlacingPanel.SetActive(true);
        endTurnPanel.SetActive(true);
    }
    List<Person> sortList(List<Person> listToSort)
    {
        listToSort.Sort(compareName);
        List<Person> temp = new List<Person>();
        foreach (Person p in listToSort)
        {
            if (p.troopType == TroopType.mainCharType)
            {
                temp.Add(p);
            }
        }
        if (temp.Count == 2)
        {
            listToSort.Remove(Player.mainCharacter);
            listToSort.Remove(Player.secCharacter);
            listToSort.Insert(0, Player.secCharacter);
            listToSort.Insert(0, Player.mainCharacter);
        } else if (temp.Count == 1)
        {
            listToSort.Remove(temp[0]);
            listToSort.Insert(0, temp[0]);
        }
        return listToSort;
        
    }
    int compareName(Person a, Person b)
    {
        return a.name.CompareTo(b.name);
    }
 }
