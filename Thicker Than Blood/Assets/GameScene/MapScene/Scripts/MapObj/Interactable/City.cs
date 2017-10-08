using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : Interactable {
    public Party cityGuard;
    private Material objMaterial;    // Used to store material reference.
    private Color objColor;            // Used to store color reference.
    private const float DEFAULT_ALPHA = 255;
    public override void Start()
    {
        dialogue = new string[] { "hello", "welcome" };
        interactableType = Enums.InteractableType.city;
        cityGuard = new Party(name + " Guard", Faction.italy, 800);
        //objMaterial = gameObject.GetComponent<MeshRenderer>().material;
        //objColor = objMaterial.color;
    }
    public override void Update()
    {
        base.Update();
    }
    public override void interact()
    {
        DialogueSystem.Instance.addNewDialogue(cityGuard, dialogue, PanelType.city);
        DialogueSystem.Instance.createDialogue(PanelType.city, cityGuard);
    }

    public override void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Player")
        {
            interact();
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
}
