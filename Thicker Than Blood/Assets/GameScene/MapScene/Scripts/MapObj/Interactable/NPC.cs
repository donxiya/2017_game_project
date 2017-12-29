using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NPC : Interactable {
    public NavMeshAgent npcAgent;
    public PartyVisionIndicator partyVisionIndicator;
    public Party npcParty = null;
    public bool battling = false;
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
    }
    public void FixedUpdate()
    {
        
        if (npcParty != null)
        {
            npcParty.position = transform.position;
        } else
        {
            gameObject.SetActive(false);
        }
        npcAgent.speed = npcParty.getTravelSpeed();
        if (!TimeSystem.pause)
        {
            //npcAgent = transform.GetComponent<NavMeshAgent>();
            //npcAgent.isStopped = false;
            if (Vector3.Distance(npcAgent.destination, transform.position) <= 10)
            {
                
            }
            if (npcParty.faction == Faction.france)
            {
                
            }
            
            npcAgent.destination = getRoamTarget(); //Player.mainParty.position;
        }
        else
        {
            npcAgent.isStopped = true;
            npcAgent.destination = transform.position;
        }
        inspectPanel.GetComponent<InspectPanel>().updateTexts(npcParty);
    }
    
    public override void interact()
    {
        base.interact();
        //DialogueSystem.Instance.addNewDialogue(npcParty, dialogue, PanelType.NPC);
        //DialogueSystem.Instance.createDialogue(PanelType.NPC, npcParty);
    }
    public override void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Player")
        {
            //start dialogue
            //DialogueSystem.Instance.addNewDialogue(npcParty, dialogue, PanelType.NPC);
            DialogueSystem.Instance.createDialogue(PanelType.NPC, npcParty);

        }
        if (col.gameObject.tag == "NPC" && !battling)
        {
            NPC encountered = col.gameObject.GetComponent<NPC>();
            if (npcParty.factionFavors[encountered.npcParty.faction] < 0 && !encountered.battling)
            {
                MapManagement.mapManagement.battleSimulation(this, col.gameObject.GetComponent<NPC>(), BattlefieldType.plain);
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
        if (npcParty != null && !battling)
        {

            if (getNpcInVision().Count > 0)
            {
                int hate = 0;
                foreach (Party p in getNpcInVision())
                {
                    if (npcParty.factionFavors[p.faction] < hate)
                    {
                        hate = npcParty.factionFavors[p.faction];
                        if (hate < 0)
                        {
                            result = p.position;
                        }
                    }
                }
            } 
        }
        //Debug.Log(result);
        return result;
    }

    public virtual void grow()
    {

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
}
