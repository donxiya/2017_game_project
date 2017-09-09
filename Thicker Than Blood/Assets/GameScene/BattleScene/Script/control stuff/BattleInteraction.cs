using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleInteraction : MonoBehaviour {
    const float INTERACT_DIST = 1;
    GameObject player;
    List<GameObject> inspectedList = new List<GameObject>();
    public static bool chasing;
    public static TroopSkill skillMode;
    public static GameObject curControlled;
    public static GameObject interactedObject;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("player_party");
        skillMode = TroopSkill.none;
    }


    // Update is called once per frame
    void Update()
    {
        inputKeysActions();
        if (curControlled != null)
        {
            showIndicator();
        }
        
    }
    void inputKeysActions()
    {
        if (Input.GetMouseButton(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            if (curControlled == null || skillMode == TroopSkill.none || skillMode == TroopSkill.walk)
            {
                selectObject();
            } else
            {
                curControlled.GetComponent<PlayerTroop>().makeDamage(curControlled.GetComponent<PlayerTroop>().indicatedGrid());
            }
            
        }
        if (Input.GetMouseButton(1) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            if (BattleCentralControl.playerTurn)
            {
                if (curControlled != null)
                {
                    if (skillMode == TroopSkill.none || skillMode == TroopSkill.walk)
                    {
                        skillMode = TroopSkill.walk;
                        walkToObj();
                    }
                }
            }
            
            
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (skillMode == TroopSkill.none)
            {
                SceneManager.LoadScene("MenuScene");
            } else
            {
                skillMode = TroopSkill.none;
            }
        }
    }
    void selectObject()
    {
        if (curControlled != null)
        {
            curControlled.GetComponent<PlayerTroop>().controlled = false;
            curControlled = null;
        }
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;
        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
        {
            if (interactedObject != null)
            {
                interactedObject.GetComponent<BattleInteractable>().cameraFocusOnExit();
            }
            
            interactedObject = interactionInfo.collider.gameObject.transform.parent.gameObject;
            
            if (interactedObject.tag == "EnemyTroop" || interactedObject.tag == "NeutralTroop" )
            {
                //Debug.Log("Interactable");
                interactedObject.GetComponent<BattleInteractable>().cameraFocusOn();
            } else if (interactedObject.tag == "Grid")
            {
                interactedObject.GetComponent<GridObject>().cameraFocusOn();
            } else if (interactedObject.tag == "PlayerTroop")
            {
                interactedObject.GetComponent<PlayerTroop>().cameraFocusOn();
                curControlled = interactedObject;
            }
            else
            {
                Debug.Log(interactedObject.tag);
            }
            
        }
    }

    void walkToObj()
    {
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;
        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
        {
            interactedObject = interactionInfo.collider.gameObject.transform.parent.gameObject;
            if (interactedObject.tag == "Grid")
            {
                if (skillMode == TroopSkill.walk)
                {
                    interactedObject.GetComponent<GridObject>().moveTroopToGrid(curControlled);
                }
                else
                {
                    interactedObject.GetComponent<GridObject>().cameraFocusOn();
                }
            }
        }
    }
    
    void showIndicator()
    {
        switch(skillMode)
        {
            case TroopSkill.none:
                curControlled.GetComponent<PlayerTroop>().hideIndicators();
                break;
            case TroopSkill.walk:
                curControlled.GetComponent<PlayerTroop>().hideIndicators();
                break;
            case TroopSkill.lunge:
                curControlled.GetComponent<PlayerTroop>().lunge();
                break;
        }

    }
    

}
