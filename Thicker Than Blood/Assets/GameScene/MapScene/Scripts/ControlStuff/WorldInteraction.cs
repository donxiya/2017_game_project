using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class WorldInteraction : MonoBehaviour
{
    public static WorldInteraction worldInteraction;
    public GameObject player;
    public GameObject tabCanvas;
    const float INTERACT_DIST = 1;
    public NavMeshAgent playerAgent;
    List<GameObject> inspectedList = new List<GameObject>();
    public static bool chasing;
    GameObject curChasedObj;
    // Use this for initialization
    void Start()
    {
        worldInteraction = gameObject.GetComponent<WorldInteraction>();
        playerAgent = player.GetComponent<NavMeshAgent>();
        playerAgent.speed = Player.mainParty.getTravelSpeed();
        chasing = false;

        
        player.transform.position = Player.mainParty.position;
    }


    // Update is called once per frame
    void Update()
    {
        inputKeysActions();
        
        if (chasing)
        {
            playerAgent.destination = curChasedObj.transform.position;
        }
        Player.mainParty.position = player.transform.position;
        if (playerAgent.destination.x == player.transform.position.x && playerAgent.destination.z == player.transform.position.z)
        {
            TimeSystem.pause = true;
        } else
        {
            TimeSystem.pause = false;
        }
    }
    void inputKeysActions()
    {
        if (Input.GetMouseButton(1) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            chasing = false;
            stopEveryone(false);
            getInteraction();
        }
        if (Input.GetMouseButton(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            chasing = false;
            inspect();
        }
        if (Input.GetKeyDown("tab"))
        {
            disInspect();
            tabCanvas.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (tabCanvas.activeSelf)
            {
                disInspect();
            } else
            {
                SceneManager.LoadScene("MenuScene");
            }
        }
        if (Input.GetKey(KeyCode.Space))
        {
            TimeSystem.pause = true;
            stopEveryone(true);
        }
    }

    public void stopEveryone(bool stop)
    {
        if (!stop)
        {
            disInspect();
        }
        
        playerAgent.isStopped = stop;
        TimeSystem.pause = stop;
        GameObject[] npcObj = GameObject.FindGameObjectsWithTag("NPC");
        foreach (GameObject npc in npcObj)
        {
            npc.GetComponent<NPC>().npcAgent.isStopped = stop;
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
                playerAgent.destination = interactionInfo.point;
            }
        }
    }

    public virtual void moveToInteraction(GameObject interactedObj)
    {
        //interactedObj.GetComponent<Interactable>().hasInteracted = false;
        playerAgent.destination = new Vector3(interactedObj.transform.position.x, player.transform.position.y, interactedObj.transform.position.z);
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
                Debug.Log("h");
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