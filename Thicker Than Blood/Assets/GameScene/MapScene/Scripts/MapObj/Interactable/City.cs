using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : Interactable {

    private Material objMaterial;    // Used to store material reference.
    private Color objColor;            // Used to store color reference.
    private const float DEFAULT_ALPHA = 255;
    public override void Start()
    {
        dialogue = new string[] { "hello", "welcome" };
        interactableType = Enums.InteractableType.city;
        //objMaterial = gameObject.GetComponent<MeshRenderer>().material;
        //objColor = objMaterial.color;
    }
    public override void Update()
    {
        base.Update();
    }
    public override void interact()
    {
        DialogueSystem.Instance.addNewDialogue(name, dialogue, PanelType.city);
        DialogueSystem.Instance.createDialogue(PanelType.city);
    }

    public override void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Player")
        {
            //start dialogue
            DialogueSystem.Instance.addNewDialogue(name, dialogue, PanelType.city);
            DialogueSystem.Instance.createDialogue(PanelType.city);

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
