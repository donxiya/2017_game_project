using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Town : Interactable
{
    public string lName;
    public Party townGuard, townTrader;
    public int prosperity;
    private Material objMaterial;    // Used to store material reference.
    private Color objColor;            // Used to store color reference.
    private const float DEFAULT_ALPHA = 255;
    public override void Start()
    {
        base.Start();
        dialogue = new string[] { "hello", "hi" };
        interactableType = InteractableType.town;
        objMaterial = gameObject.GetComponentInChildren<MeshRenderer>().material;
        objColor = objMaterial.color;
        //townGuard.belongedTown = this;
    }
    public override void interact()
    {
        //start dialogue
        DialogueSystem.Instance.createDialogue(PanelType.town, townGuard);
    }

    public override void OnTriggerEnter(Collider col)
    {
        
        if (col.gameObject.tag == "Player")
        {
            interact();
            //to indicate obj interacting
            //objMaterial.color = new Color(0, objColor.g, objColor.b, objColor.a/100);
            //gameObject.GetComponent<MeshRenderer>().material = objMaterial;
            //DialogueSystem.Instance.addNewDialogue(townGuard, dialogue, PanelType.town);
            //DialogueSystem.Instance.createDialogue(PanelType.town, townGuard);
        }
    }
    public override void OnTriggerExit(Collider col)
    {
        /**if (col.gameObject.tag == "Player")
        {
            objMaterial.color = new Color(objColor.r, objColor.g, objColor.b, DEFAULT_ALPHA);
            gameObject.GetComponent<MeshRenderer>().material = objMaterial;
            DialogueSystem.Instance.closeDialogue(PanelType.town);
        }**/
    }

    void initializeTraderInventory()
    {
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Parchment"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Wool"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Pottery"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Hemp"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Medicinal Liqueur"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Rose"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Majolica"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Lace"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Embroidery"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Livestock"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Bronzeware"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Leatherware"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Ale"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Wine"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Cheese"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Wheat"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Fruit"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Prosciutto"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Olive Oil"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Fish"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Salt"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Vegetable"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Honey"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Gold Ore"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Marble"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Bronze"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Silk Thread"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Alum"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Timber"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Iron Ore"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Woad"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Crossbow"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Horse"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Armor"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Fire Arm"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Weapon"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Manuscript"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Velvet"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Fur"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Amber"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Slave"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Coral"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Silk Textile"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Antique"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Glassware"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Pepper"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Clove"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Ottoman Tapestry"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("China"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Silverware"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Tanned Leather"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Intricate Gear"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Sturdy Sinew"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Fine Whetstone"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Steel Ingot"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Coal"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Saltpetre"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Sulfur"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Twine"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Iron Mail"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Beewax"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Tools"));
        townTrader.inventory.Add(ItemDataBase.dataBase.getItem("Supplies"));
    }

    public List<Item> getTradeInventory()
    {
        return townTrader.inventory;
    }

    public string[] getGarrisonInfo()
    {
        string[] result = new string[] { "hello", "welcome" };

        return result;
    }
}