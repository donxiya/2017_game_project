using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NPC : Interactable {
    public NavMeshAgent npcAgent;
    public GameObject model;
    public PartyVisionIndicator partyVisionIndicator;
    public Party npcParty = null;
    int hrSCounter, hrECounter, monthSCounter, monthECounter;
    const int roamRange = 60;
    private void Awake()
    {
    }
    public override void Start()
    {
        base.Start();
        npcAgent = transform.GetComponent<NavMeshAgent>();
    }
    public override void Update()
    {
        base.Update();
        lookAtCamera(model);
        hrSCounter = TimeSystem.hour;
        if (hrSCounter != hrECounter)
        {
            if (npcParty.battling <= 1)
            {
                npcParty.battling = 0;
            }
            if (npcParty.battling > 0)
            {
                npcParty.battling -= 1;
            }
        }
        hrECounter = TimeSystem.hour;
        monthSCounter = TimeSystem.hour;
        if (monthSCounter != monthECounter)
        {
            grow();
        }
        monthECounter = TimeSystem.hour;
        
        if (npcParty != null)
        {
            npcParty.position = transform.position;
        }
        else
        {
            gameObject.SetActive(false);
        }
        if (npcParty.partyMember.Count == 0)
        {
            MapManagement.parties.Remove(npcParty);
            GameObject.Destroy(gameObject);
        }
        npcAgent.speed = npcParty.getTravelSpeed();
        if (!TimeSystem.pause)
        {
            //npcAgent = transform.GetComponent<NavMeshAgent>();
            //npcAgent.isStopped = false;
            if (npcAgent.isActiveAndEnabled)
            {
                npcAgent.destination = getRoamTarget(); //Player.mainParty.position;
            }
            else
            {
                npcAgent.Warp(transform.position);
            }

        }
        else
        {
            npcAgent.destination = transform.position;
            npcAgent.isStopped = true;
        }
        inspectPanel.GetComponent<InspectPanel>().updateTexts(npcParty);

    }
    public void FixedUpdate()
    {
        
    }
    
    public override void interact()
    {
        base.interact();
        DialogueSystem.Instance.createDialogue(PanelType.NPC, npcParty);
        //DialogueSystem.Instance.addNewDialogue(npcParty, dialogue, PanelType.NPC);
        //DialogueSystem.Instance.createDialogue(PanelType.NPC, npcParty);
    }
    public override void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Player")
        {
            //start dialogue
            //DialogueSystem.Instance.addNewDialogue(npcParty, dialogue, PanelType.NPC);
            //DialogueSystem.Instance.createDialogue(PanelType.NPC, npcParty);

        }
        if (col.gameObject.tag == "NPC" && npcParty.battling < 1)
        {
            NPC encountered = col.gameObject.GetComponent<NPC>();
            if (npcParty.factionFavors[encountered.npcParty.faction] < 0 && encountered.npcParty.battling == 0)
            {
                MapManagement.mapManagement.battleSimulation(this, col.gameObject.GetComponent<NPC>(), npcParty.battlefieldTypes);
                partyVisionIndicator.reLook();
            }
            
        }
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "NPC" && npcParty.battling < 1)
        {
            NPC encountered = col.gameObject.GetComponent<NPC>();
            if (npcParty.factionFavors[encountered.npcParty.faction] < 0 && encountered.npcParty.battling == 0)
            {
                MapManagement.mapManagement.battleSimulation(this, col.gameObject.GetComponent<NPC>(), npcParty.battlefieldTypes);
                partyVisionIndicator.reLook();
            }

        }
    }
    public override void inspect(bool inspecting)
    {
        if (inspecting)
        {
            inspectPanel.SetActive(true);

        }
        else
        {
            inspectPanel.SetActive(false);
        }
    }

    public virtual Vector3 getRoamTarget()
    {
        Vector3 result = transform.position + new Vector3(Random.Range(-roamRange, roamRange), 0, Random.Range(-roamRange, roamRange));
        Party closestEnemy = null;
        if (npcParty != null && npcParty.battling == 0)
        {

            if (getNpcInVision().Count > 0)
            {
                int hate = 0;
                foreach (Party p in getNpcInVision())
                {
                    if (!p.factionFavors.ContainsKey(npcParty.faction))
                    {
                        Debug.Log(npcParty.name + " doesnt have " + p.faction);
                    }
                    if (p.factionFavors[npcParty.faction] < 0)
                    {
                        if (closestEnemy == null || Vector3.Distance(npcParty.position, p.position) < Vector3.Distance(npcParty.position, closestEnemy.position))
                        {
                            closestEnemy = p;
                        }
                    }
                    
                    if (npcParty.factionFavors[p.faction] < hate)
                    {
                        hate = npcParty.factionFavors[p.faction];
                        if (hate < 0)
                        {
                            result = p.position;
                        }
                    }
                }
                if (closestEnemy != null && closestEnemy.getBattleValue() * .7f > npcParty.getBattleValue())
                {
                    result = 2 * npcParty.position - closestEnemy.position;
                }
            } 
        } else
        {
            result = transform.position;
        }
        //Debug.Log(result);
        return result;
    }

    public virtual void grow()
    {
        foreach (Item i in npcParty.inventory)
        {
            npcParty.cash += i.getSellingPrice();

        }
        npcParty.battleValue += npcParty.cash / 2;
        makeParty();
    }
    public List<Party> getNpcInVision()
    {
        if (npcParty != null)
        {
            partyVisionIndicator.setVisionRange(npcParty.getVisionRange());
        }
        //Debug.Log(partyVisionIndicator.getPartiesInRange().Count);
        List<Party> result = partyVisionIndicator.getPartiesInRange();
        result.Remove(npcParty);
        return result;
    }

    public virtual void makeParty()
    {

    }
    void lookAtCamera(GameObject gameObj)
    {
        Vector3 v = Camera.main.transform.position - gameObj.transform.position;
        v.x = v.z = 0.0f;
        gameObj.transform.LookAt(Camera.main.transform.position - v);
        gameObj.transform.Rotate(0, 180, 0);
    }
}
