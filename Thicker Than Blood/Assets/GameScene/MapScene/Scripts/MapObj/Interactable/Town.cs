using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Town : Interactable
{
    public Party townGuard;
    private Material objMaterial;    // Used to store material reference.
    private Color objColor;            // Used to store color reference.
    private const float DEFAULT_ALPHA = 255;
    public override void Start()
    {
        base.Start();
        dialogue = new string[] { "hello", "hi" };
        interactableType = InteractableType.town;
        objMaterial = gameObject.GetComponent<MeshRenderer>().material;
        objColor = objMaterial.color;
        townGuard = new Party("townGuard", Faction.italy, 100);
        townGuard.belongedTown = this;
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

    public List<Item> getTradeInventory()
    {
        List<Item> result = new List<Item>();
        return result;
    }

    public string[] getGarrisonInfo()
    {
        string[] result = new string[] { "hello", "welcome" };

        return result;
    }
}