using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : Interactable {
    public string lName;
    public string ID;
    public Party cityGuard, cityTrader;
    private Material objMaterial;    // Used to store material reference.
    private Color objColor;            // Used to store color reference.
    private const float DEFAULT_ALPHA = 255;
    public int prosperity, encampmentPrice;
    public bool encampmentAvailable = false;
    public Vector3 position;
    public List<Item> warehouse;
    public override void Start()
    {
        
        //objMaterial = gameObject.GetComponent<MeshRenderer>().material;
        //objColor = objMaterial.color;
        if (MapManagement.mapManagement == null)
        {
            gameObject.SetActive(false);
        }
    }



    public override void Update()
    {
        base.Update();
    }
    public override void interact()
    {
        base.interact();
        DialogueSystem.Instance.createDialogue(PanelType.city, cityGuard);
    }

    public override void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Player")
        {
            //interact();
            //start dialogue
            //DialogueSystem.Instance.addNewDialogue(name, dialogue, PanelType.city);
            //DialogueSystem.Instance.createDialogue(PanelType.city);

        }
    }
    /**public override void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Interactable Object")
        {
            Debug.Log("exit coliided");
            objMaterial.color = new Color(objColor.r, objColor.g, objColor.b, DEFAULT_ALPHA);
            gameObject.GetComponent<MeshRenderer>().material = objMaterial;
            DialogueSystem.Instance.closeDialogue(PanelType.city);
        }
    }**/
    
    

    public int getEncampmentPrice()
    {
        return encampmentPrice;
    }
    public List<Item> getTradeInventory()
    {
        return cityTrader.inventory;
    }
    public List<Item> getWarehouseInventory()
    {
        return warehouse;
    }
    public string[] getGarrisonInfo()
    {
        string[] result = new string[] { "hello", "welcome" };

        return result;
    }
    public List<Quest> getavailableQuests()
    {
        List<Quest> quests = new List<Quest>();
        if (!checkPlayerQuestID(QuestType.ASN + ID))
        {
            quests.Add(QuestDataBase.dataBase.getQuest(QuestType.ASN + ID));
        }
        if (!checkPlayerQuestID(QuestType.HUN + ID))
        {
            quests.Add(QuestDataBase.dataBase.getQuest(QuestType.HUN + ID));
        }

        return quests;
    }
    bool checkPlayerQuestID(string id)
    {
        foreach (Quest q in Player.mainParty.unfinishedQuests)
        {
            if (q.questID == id)
            {
                return true;
            }
        }
        return false;
    }
}
