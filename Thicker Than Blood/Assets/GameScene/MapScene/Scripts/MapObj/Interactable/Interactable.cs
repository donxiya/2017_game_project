using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enums
{
    public enum InteractableType
    {
        NPC,
        town,
        castle,
        city
    };
}

public class Interactable : MonoBehaviour {

    const float INTERACT_DIST = 1;
    public NavMeshAgent playerAgent;
    //public GameObject interactedObject;
    public new string name;
    public Enums.InteractableType interactableType;
    public int hostility;
    public string[] dialogue;
    public bool hasInteracted;
    public GameObject inspectPanel;

    public virtual void Start()
    {
        //inspectPanel.SetActive(false);
    }
    public virtual void Update()
    {
        //if (!hasInteracted && playerAgent != null && !playerAgent.pathPending) //if we have a player and have found path
        //{
            //if (Vector3.Distance(playerAgent.transform.position, transform.position) <= INTERACT_DIST)
            //{   
                //interact();
                //hasInteracted = true;
            //}
        }
    //}
    
    public virtual void inspect(bool inspecting)
    {
        if (inspecting)
        {
            inspectPanel.SetActive(true);

        } else
        {
            inspectPanel.SetActive(true);
        }
    }
    public virtual void interact()
    {
        WorldInteraction.chasing = false;
    }
    public virtual void OnTriggerEnter(Collider col)
    {
        hasInteracted = true;
    }
    public virtual void OnTriggerExit(Collider col)
    {
        hasInteracted = false;
    }
}

