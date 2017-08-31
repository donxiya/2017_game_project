using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class WorldInteraction : MonoBehaviour
{
    const float INTERACT_DIST = 1;
    NavMeshAgent playerAgent;
    GameObject player;
    List<GameObject> inspectedList = new List<GameObject>();
    public static bool chasing;
    GameObject curChasedObj;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("player_party");
        playerAgent = player.GetComponent<NavMeshAgent>();
        chasing = false;
    }


    // Update is called once per frame
    void Update()
    {
        inputKeysActions();
        playerAgent.speed = Player.mainParty.getTravelSpeed();
        if (chasing)
        {
            playerAgent.destination = curChasedObj.transform.position;
        }
    }
    void inputKeysActions()
    {
        if (Input.GetMouseButton(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            chasing = false;
            disInspect();
            playerAgent.isStopped = false;
            getInteraction();
        }
        if (Input.GetMouseButton(1) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            chasing = false;
            inspect();
        }
        if (Input.GetKeyDown("tab"))
        {
            disInspect();
            SceneManager.LoadScene("TabScene");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            disInspect();
            SceneManager.LoadScene("MenuScene");
        }
        if (Input.GetKey(KeyCode.Space))
        {
            disInspect();
            playerAgent.isStopped = true;
        }
    }
    void getInteraction()
    {
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;
        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
        {
            GameObject interactedObject = interactionInfo.collider.gameObject;
            if (interactedObject.tag == "Plain")
            {
                playerAgent.destination = interactionInfo.point;
                
            }
            else if (interactedObject.tag == "Interactable Object" || interactedObject.tag == "NPC") {
                //interactedObject.GetComponent<Interactable>().moveToInteraction(playerAgent);
                moveToInteraction(interactedObject);
            }
            else
            {
                Debug.Log("cannot walk there");
            }
        }
    }

    public virtual void moveToInteraction(GameObject interactedObj)
    {

        interactedObj.GetComponent<Interactable>().hasInteracted = false;
        playerAgent.speed = Player.mainParty.getTravelSpeed();
        playerAgent.destination = this.transform.position;
        if (Vector3.Distance(playerAgent.transform.position, interactedObj.transform.position) >= INTERACT_DIST)
        {
            chasing = true;
            curChasedObj = interactedObj;
        }

    }

    void inspect()
    {
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;
        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
        {
            GameObject interactedObject = interactionInfo.collider.gameObject;
            if (interactedObject.tag == "Plain")
            {
            }
            else if (interactedObject.tag == "Interactable Object")
            {
            }
            else if (interactedObject.tag == "NPC")
            {
                interactedObject.GetComponent<Interactable>().inspect(true);
                if (interactedObject != null && !inspectedList.Contains(interactedObject))
                {
                    inspectedList.Add(interactedObject);
                }
            }
            else
            {
                Debug.Log("cannot walk there");
            }
        }
    }
    void disInspect()
    {
        
        if (inspectedList.Count > 0)
        {
            for(int i = 0; i < inspectedList.Count; i++)
            {
                if (inspectedList[i] != null)
                {
                    inspectedList[i].GetComponent<Interactable>().inspect(false);
                    inspectedList.RemoveAt(i);
                }
            }
        }
    }

}