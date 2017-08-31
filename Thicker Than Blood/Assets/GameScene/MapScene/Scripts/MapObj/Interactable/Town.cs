using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;


public class Town : Interactable
{
    private Material objMaterial;    // Used to store material reference.
    private Color objColor;            // Used to store color reference.
    private const float DEFAULT_ALPHA = 255;
    public override void Start()
    {
        base.Start();
        dialogue = new string[] { "hello", "hi" };
        interactableType = Enums.InteractableType.town;
        objMaterial = gameObject.GetComponent<MeshRenderer>().material;
        objColor = objMaterial.color;
    }
    public override void interact()
    {
        //start dialogue
        DialogueSystem.Instance.addNewDialogue(name, dialogue, "town");
        DialogueSystem.Instance.createDialogue("town");
    }

    public override void OnTriggerEnter(Collider col)
    {
        
        if (col.gameObject.tag == "Player")
        {
            //to indicate obj interacting
            //objMaterial.color = new Color(0, objColor.g, objColor.b, objColor.a/100);
            //gameObject.GetComponent<MeshRenderer>().material = objMaterial;
            DialogueSystem.Instance.addNewDialogue(name, dialogue, "town");
            DialogueSystem.Instance.createDialogue("town");
        }
    }
    public override void OnTriggerExit(Collider col)
    {
        /**if (col.gameObject.tag == "Player")
        {
            objMaterial.color = new Color(objColor.r, objColor.g, objColor.b, DEFAULT_ALPHA);
            gameObject.GetComponent<MeshRenderer>().material = objMaterial;
            DialogueSystem.Instance.closeDialogue("town");
        }**/
    }
}