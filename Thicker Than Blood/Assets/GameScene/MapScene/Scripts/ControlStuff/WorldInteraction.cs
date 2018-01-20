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
    float idleCounter = 0;
    GameObject objectWantToInteract;
    public NavMeshAgent playerAgent;
    List<GameObject> inspectedList = new List<GameObject>();
    Animation playerAnimation;
    public static bool chasing;
    GameObject curChasedObj;
    // Use this for initialization
    void Start()
    {
        worldInteraction = gameObject.GetComponent<WorldInteraction>();
        playerAgent = player.GetComponent<NavMeshAgent>();
        playerAgent.speed = Player.mainParty.getTravelSpeed();
        chasing = false;
        playerAnimation = player.transform.Find("Model").GetComponent<Animation>();
        
        player.transform.position = Player.mainParty.position;
        Player.mainParty.battlefieldTypes.Add(BattlefieldType.Common);
        Player.mainParty.battlefieldTypes.Add(BattlefieldType.City);
    }


    // Update is called once per frame
    void Update()
    {
        inputKeysActions();
        playerAgent.speed = Player.mainParty.getTravelSpeed();
        if (chasing)
        {
            if (curChasedObj != null)
            {
                playerAgent.destination = curChasedObj.transform.position;
            } else
            {
                chasing = false;
            }
            
        }
        Player.mainParty.position = player.transform.position;
        if (Mathf.Abs(playerAgent.destination.x - player.transform.position.x) <= .5f && Mathf.Abs(playerAgent.destination.z - player.transform.position.z) <= .5f)
        {
            idleAnimation();
            TimeSystem.pause = true;
        } else
        {

            walkingAnimation();
            TimeSystem.pause = false;
        }
        if (objectWantToInteract != null)
        {
            if (Vector3.Distance(player.transform.position, objectWantToInteract.transform.position) <= INTERACT_DIST)
            {
                objectWantToInteract.GetComponent<Interactable>().interact();
                objectWantToInteract = null;
            }
        }
    }
    void inputKeysActions()
    {
        if (Input.GetMouseButtonDown(1) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            chasing = false;
            stopEveryone(false);
            getInteraction();
        }
        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
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
    void idleAnimation()
    {
        if (!playerAnimation.IsPlaying("CleanFeather"))
        {
            playerAnimation.Play("Idle");
        }
        idleCounter += Time.deltaTime;
        if (idleCounter >= 3.0f)
        {
            idleCounter -= 3.0f;
            playerAnimation.Play("CleanFeather");
        }
    }
    void walkingAnimation()
    {
        //playerAnimation.Play("ShortGliding");
        float carriedPercentage = Player.mainParty.getInventoryWeight() / Player.mainParty.getInventoryWeightLimit();
        if (carriedPercentage <.5f) //gliding animation
        {
            if (playerAnimation.IsPlaying("Idle"))
            {
                playerAnimation.Play("Launch");
            }
            if (!playerAnimation.IsPlaying("Launch"))
            {
                playerAnimation.Play("Gliding");
            }
        } else if (carriedPercentage < .5f)
        {
            if (!playerAnimation.IsPlaying("Landing"))
            {
                playerAnimation.Play("Hop");
            }
            if (playerAnimation.IsPlaying("Gliding"))
            {
                playerAnimation.Play("Landing");
            }
        } else 
        {
            if (playerAnimation.IsPlaying("Idle")
                || playerAnimation.IsPlaying("Landing"))
            {
                playerAnimation.Play("Walk");
            }
            if (playerAnimation.IsPlaying("Gliding"))
            {
                playerAnimation.Play("Landing");
            }
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
                objectWantToInteract = null;
            }
            else if (interactedObject.tag == "Interactable Object" || interactedObject.tag == "NPC" || interactedObject.tag == "City" || interactedObject.tag == "Town") {
                //interactedObject.GetComponent<Interactable>().moveToInteraction(playerAgent);
                moveToInteraction(interactedObject);
                objectWantToInteract = interactedObject;
            }
            else
            {
                playerAgent.destination = interactionInfo.point;
                objectWantToInteract = null;
            }
        }
    }

    public virtual void moveToInteraction(GameObject interactedObj)
    {
        //interactedObj.GetComponent<Interactable>().hasInteracted = false;
        playerAgent.destination = new Vector3(interactedObj.transform.position.x, player.transform.position.y, interactedObj.transform.position.z);
        if (Vector3.Distance(playerAgent.transform.position, interactedObj.transform.position) >= 0)
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