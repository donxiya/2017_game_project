using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TroopManagement : MonoBehaviour {
    public static TroopManagement troopManagement;
    public static bool upgradable = false;
    public GameObject selectingTroopUnitButton;
    public Text selectingTroopName, selectingTroopLevel, selectingTitle;
    public RawImage selectingMaxHealthBar, selectingHealthBar, selectingStaminaBar;
    public Text inspectTroopLevel, inspectTroopName, insepctMaxHealthNum, inspectHealthNum,
        inspectStamina, inspectS, inspectA, inspectP, inspectE, inspectTitle;
    public RawImage inspectMaxHealthBar, inspectHealthBar, inspectStaminaBar,
        inspectSBar, inspectABar, inspectPBar, inspectEBar;
    public Button addButton, removeButton, leaveButton, upgradeButton;
    public GameObject nameButton;
    public GameObject inputField;
    public GameObject currentTroopUnitButton;
    public GameObject troopUpgradePanel;
    public Text currentTroopName, currentTroopLevel, currentTitle;
    public RawImage currentMaxHealthBar, currentHealthBar, currentStaminaBar;
    public Texture2D regularTroopBackground, curSelectingBackgroundImage, curCurrentBackgroundImage;
    public GameObject resetSAPE, sPlus, aPlus, pPlus, ePlus, bars, sBar, aBar, pBar, eBar;
    
    bool inSelecting, renaming;
    GameObject curSelectingButton, curCurrentButton;
    Person curSelectingPerson, curCurrentPerson;
    public List<Person> selectingMembers, currentMembers;
    Dictionary<Person, GameObject> troopDict;
    bool initialized = false;
    float STATS_BAR_WIDTH;
    // Use this for initialization
    void Start()
    {
        troopManagement = this;
        STATS_BAR_WIDTH = inspectSBar.rectTransform.sizeDelta.y;
        
        upgradable = false;
    }
    private void OnEnable()
    {
        if (Player.mainParty != null)
        {
            initialization();
            arrangeButtons();
            selectingTroopUnitButton.SetActive(false);
            currentTroopUnitButton.SetActive(false);
            inspectUnit(curCurrentPerson);
            updateTroopUnitBackground();
            nameButton.GetComponent<Button>().interactable = false;
        }
    }
    //// Update is called once per frame
    void Update()
    {
        if (inSelecting)
        {
            if (curSelectingPerson != null)
            {
                inspectUnit(curSelectingPerson);
            }
            upgradeButton.interactable = false;
            nameButton.GetComponent<Button>().interactable = false;
            resetSAPE.GetComponent<Button>().interactable = false;
        }
        else
        {
            if (curCurrentPerson != null)
            {
                inspectUnit(curCurrentPerson);
                if (curCurrentPerson.ranking == Ranking.mainChar)
                {
                    upgradeButton.interactable = false;
                    nameButton.GetComponent<Button>().interactable = false;
                    resetSAPE.GetComponent<Button>().interactable = false;
                }
                else
                {
                    if (upgradable && !upgradeButton.interactable)
                    {
                        upgradeButton.interactable = true;
                    } else if (upgradable && upgradeButton.interactable)
                    {
                        upgradeButton.interactable = true;
                    }
                    
                    nameButton.GetComponent<Button>().interactable = true;
                    resetSAPE.GetComponent<Button>().interactable = true;
                }
                rename();
                skillPointManagement();
            }
        }
        if (currentMembers.Contains(curSelectingPerson) || curSelectingButton == null || !inSelecting)
        {
            addButton.interactable = false;
        }
        else
        {
            addButton.interactable = true;
        }
        if (selectingMembers.Contains(curCurrentPerson) || curCurrentButton == null || inSelecting || curCurrentPerson.ranking == Ranking.mainChar)
        {
            removeButton.interactable = false;
        }
        else
        {
            removeButton.interactable = true;
        }
        Canvas.ForceUpdateCanvases();
    }

    public void addToSelecting(List<Person> toAdd)
    {
        selectingMembers = toAdd;
    }

    public void upgradeCurPerson(Person unit)
    {
        curCurrentPerson = unit;
        inspectUnit(unit);
        reArrangeButtons();
    }

    void initialization()
    {
        inSelecting = false;
        selectingMembers = new List<Person>();
        curCurrentPerson = Player.mainParty.leader;
        currentMembers = Player.mainParty.partyMember;
        troopDict = new Dictionary<Person, GameObject>();
        clearButtonListener();
        addButton.GetComponent<Button>().onClick.AddListener(delegate { addToCurrent(); });
        removeButton.GetComponent<Button>().onClick.AddListener(delegate { removeFromCurrent(); });
        nameButton.GetComponent<Button>().onClick.AddListener(delegate { renaming = true; });
        resetSAPE.GetComponent<Button>().onClick.AddListener(delegate { curCurrentPerson.resetPerk(); resetLayoutGroup(); });
        sPlus.GetComponent<Button>().onClick.AddListener(delegate { curCurrentPerson.incrementS(); resetLayoutGroup(); });
        aPlus.GetComponent<Button>().onClick.AddListener(delegate { curCurrentPerson.incrementA(); resetLayoutGroup(); });
        pPlus.GetComponent<Button>().onClick.AddListener(delegate { curCurrentPerson.incrementP(); resetLayoutGroup(); });
        ePlus.GetComponent<Button>().onClick.AddListener(delegate { curCurrentPerson.incrementE(); resetLayoutGroup(); });
        sPlus.SetActive(false);
        aPlus.SetActive(false);
        pPlus.SetActive(false);
        ePlus.SetActive(false);
    }
    void clearButtonListener()
    {
        addButton.GetComponent<Button>().onClick.RemoveAllListeners();
        removeButton.GetComponent<Button>().onClick.RemoveAllListeners();
        nameButton.GetComponent<Button>().onClick.RemoveAllListeners();
        resetSAPE.GetComponent<Button>().onClick.RemoveAllListeners();
        sPlus.GetComponent<Button>().onClick.RemoveAllListeners();
        aPlus.GetComponent<Button>().onClick.RemoveAllListeners();
        pPlus.GetComponent<Button>().onClick.RemoveAllListeners();
        ePlus.GetComponent<Button>().onClick.RemoveAllListeners();
    }
    void rename()
    {
        if (renaming)
        {
            if (!inputField.activeSelf)
            {
                inputField.SetActive(true);
                inputField.GetComponent<InputField>().text = "";
                nameButton.SetActive(false);
            }
            
            if (Input.GetKeyDown(KeyCode.Return))
            {
                string newName = inputField.GetComponent<InputField>().text;
                if (newName != "")
                {
                    curCurrentPerson.setName(newName);
                }
                else
                {
                    curCurrentPerson.setDefaultName();
                }
                reArrangeButtons();
                inspectUnit(curCurrentPerson);
                renaming = false;
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                renaming = false;
            }
        }
        else
        {
            inputField.SetActive(false);
            nameButton.SetActive(true);
        }
    }
    void skillPointManagement()
    {
        if (curCurrentPerson.exp.sparedPoint > 0 && curCurrentPerson.ranking != Ranking.mainChar)
        {
            if (!sPlus.activeSelf)
            {
                sPlus.SetActive(true);
                aPlus.SetActive(true);
                pPlus.SetActive(true);
                ePlus.SetActive(true);
                resetLayoutGroup();
            }
        } else
        {
            sPlus.SetActive(false);
            aPlus.SetActive(false);
            pPlus.SetActive(false);
            ePlus.SetActive(false);
        }
    }
    void resetLayoutGroup()
    {
        bars.GetComponent<VerticalLayoutGroup>().SetLayoutVertical();
        sBar.GetComponent<HorizontalLayoutGroup>().SetLayoutHorizontal();
        aBar.GetComponent<HorizontalLayoutGroup>().SetLayoutHorizontal();
        pBar.GetComponent<HorizontalLayoutGroup>().SetLayoutHorizontal();
        eBar.GetComponent<HorizontalLayoutGroup>().SetLayoutHorizontal();
    }
    GameObject makeTroopSelectingButton(Person unit)
    {
        selectingTroopName.text = unit.name;
        selectingTroopLevel.text = unit.exp.level.ToString();
        selectingTitle.text = TroopDataBase.rankingToString(unit.ranking) + " " + TroopDataBase.troopTypeToString(unit.troopType);
        selectingMaxHealthBar.rectTransform.sizeDelta = new Vector2(unit.getHealthMax() / 2, 14);
        selectingHealthBar.rectTransform.sizeDelta = new Vector2(unit.getHealthMax() / 2 - 4, 10);
        selectingStaminaBar.rectTransform.sizeDelta = new Vector2(unit.getStaminaMax() / 2 * 5, 14);
        GameObject troopUnit = Object.Instantiate(selectingTroopUnitButton);
        troopUnit.transform.Find("TroopUnitBackground").GetComponent<RawImage>().texture
                = regularTroopBackground;
        troopUnit.transform.SetParent(selectingTroopUnitButton.transform.parent, false);
        troopUnit.GetComponent<Button>().onClick.AddListener(delegate {
            curSelectingPerson = unit;
            inSelecting = true; updateTroopUnitBackground();
        });
        troopUnit.SetActive(true);
        return troopUnit;
    }
    GameObject makeTroopCurrentButton(Person unit)
    {
        currentTroopName.text = unit.name;
        currentTroopLevel.text = unit.exp.level.ToString();
        currentTitle.text = TroopDataBase.rankingToString(unit.ranking) + " " + TroopDataBase.troopTypeToString(unit.troopType);
        currentMaxHealthBar.rectTransform.sizeDelta = new Vector2(unit.getHealthMax() / 2, 14);
        currentHealthBar.rectTransform.sizeDelta = new Vector2(unit.getHealthMax() / 2 - 4, 10);
        currentStaminaBar.rectTransform.sizeDelta = new Vector2(unit.getStaminaMax() / 2 * 5, 14);
        GameObject troopUnit = Object.Instantiate(currentTroopUnitButton);
        troopUnit.transform.Find("TroopUnitBackground").GetComponent<RawImage>().texture
                = regularTroopBackground;
        troopUnit.transform.SetParent(currentTroopUnitButton.transform.parent, false);
        troopUnit.GetComponent<Button>().onClick.AddListener(delegate {
            curCurrentPerson = unit; renaming = false;
            inSelecting = false; updateTroopUnitBackground();
        });
        troopUnit.SetActive(true);
        return troopUnit;
    }
    void inspectUnit(Person unit)
    {
        curSelectingPerson = unit;
        curSelectingButton = troopDict[curSelectingPerson];
        inspectTroopName.text = unit.name;
        inspectTroopLevel.text = unit.exp.level.ToString();
        inspectTitle.text = TroopDataBase.rankingToString(unit.ranking) + " " + TroopDataBase.troopTypeToString(unit.troopType);
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
        upgradeButton.onClick.AddListener(delegate { upgradeUnit(unit); });
    }
    void upgradeUnit(Person unit)
    {
        if (unit.ranking != Ranking.mainChar)
        {
            troopUpgradePanel.GetComponent<TroopUpgrade>().showCurPerson(unit);
        }
    }
    void addToCurrent()
    {
        int index = 0;
        if (!currentMembers.Contains(curSelectingPerson))
        {
            currentMembers.Add(curSelectingPerson);
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
            }
            else
            {
                curSelectingPerson = selectingMembers[selectingMembers.Count - 1];
            }

        }
        else
        {
            curCurrentPerson = null;
        }
        updateTroopUnitBackground();
    }
    void removeFromCurrent()
    {
        int index = 0;
        if (!selectingMembers.Contains(curCurrentPerson))
        {
            selectingMembers.Add(curSelectingPerson);
            index = currentMembers.IndexOf(curCurrentPerson);
            currentMembers.Remove(curCurrentPerson);
            reArrangeButtons();
            Object.Destroy(curCurrentButton);
        }
        if (currentMembers.Count > 0)
        {
            if (index < currentMembers.Count)
            {
                curCurrentPerson = currentMembers[index];
            }
            else
            {
                curCurrentPerson = currentMembers[currentMembers.Count - 1];
            }

        }
        else
        {
            curCurrentPerson = null;
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
            if (curCurrentPerson != null)
            {
                curCurrentButton = troopDict[curCurrentPerson];
            }
            if (curCurrentButton != null)
            {
                curCurrentButton.transform.Find("TroopUnitBackground").GetComponent<RawImage>().texture
                = curCurrentBackgroundImage;
            }
        }


    }
    void arrangeButtons()
    {
        List<Person> temp;
        if (selectingMembers.Count > 0)
        {
            temp = new List<Person>(selectingMembers);
            selectingMembers = sortList(temp);
            foreach (Person p in selectingMembers)
            {
                if (!troopDict.ContainsKey(p))
                {
                    troopDict.Add(p, makeTroopSelectingButton(p));
                }
                
            }
        }
        
        if (currentMembers.Count > 0)
        {
            temp = new List<Person>(currentMembers);
            currentMembers = sortList(temp);
            foreach (Person p in currentMembers)
            {
                if (!troopDict.ContainsKey(p))
                {
                    troopDict.Add(p, makeTroopCurrentButton(p));
                }
                
            }
        }
        
    }
    void reArrangeButtons()
    {
        List<Person> temp;
        if (selectingMembers.Count > 0)
        {
            temp = new List<Person>(selectingMembers);
            selectingMembers = sortList(temp);
        }
        temp = currentMembers;
        if (currentMembers.Count > 0)
        {
            temp = new List<Person>(currentMembers);
            currentMembers = sortList(temp);
        }
        foreach (Person p in selectingMembers)
        {
            if (troopDict.ContainsKey(p)) {
                Object.Destroy(troopDict[p]);
            }
        }
        foreach (Person p in currentMembers)
        {
            if (troopDict.ContainsKey(p)) {
                Object.Destroy(troopDict[p]);
            }
        }
        troopDict.Clear();
        foreach (Person p in selectingMembers)
        {
            if (!troopDict.ContainsKey(p))
            {
                troopDict.Add(p, makeTroopSelectingButton(p));
            }
            
        }
        foreach (Person p in currentMembers)
        {
            if (!troopDict.ContainsKey(p))
            {
                troopDict.Add(p, makeTroopCurrentButton(p));
            }
            
        }
    }
    

    public void leaveManagement()
    {
        if (selectingMembers != null)
        {
            foreach (Person p in selectingMembers)
            {
                Object.Destroy(troopDict[p]);
                troopDict.Remove(p);
            }
            selectingMembers.Clear();
            reArrangeButtons();
            Player.mainParty.partyMember = currentMembers;
        }
        upgradable = false;
    }
    List<Person> sortList(List<Person> listToSort)
    {
        listToSort.Sort(compareName);
        List<Person> temp = new List<Person>();
        foreach (Person p in listToSort)
        {
            if (p == null)
            {
                Debug.Log("p is null");
            }
            if (p.ranking == Ranking.mainChar) //here
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
        }
        else if (temp.Count == 1)
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

