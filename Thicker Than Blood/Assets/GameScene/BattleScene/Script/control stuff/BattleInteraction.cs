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
    public static GameObject inspectedObject;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("player_party");
        skillMode = TroopSkill.walk;
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
        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            if (curControlled != null && skillMode != TroopSkill.walk)
            {
                curControlled.GetComponent<PlayerTroop>().doSkill(curControlled.GetComponent<PlayerTroop>().indicatedGrid(), skillMode);
                if (Input.GetMouseButtonDown(0))
                {
                    skillMode = TroopSkill.walk;
                }
            } else
            {   
                selectObject();
            }
        }
        if (Input.GetMouseButtonDown(1) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            if (BattleCentralControl.playerTurn)
            {
                if (curControlled != null)
                {
                    if (skillMode == TroopSkill.walk)
                    {
                        walkToObj();
                    } else if (skillMode == TroopSkill.none && Input.GetMouseButtonDown(1))
                    {
                        skillMode = TroopSkill.walk;
                    }
                    else
                    {
                        skillMode = TroopSkill.none;
                        curControlled.GetComponent<PlayerTroop>().hideIndicators();
                        
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (skillMode == TroopSkill.walk)
            {
                SceneManager.LoadScene("MenuScene");
            }
            else
            {
                skillMode = TroopSkill.walk;
                
            }
        }
    }
    void selectObject()
    {
        if (curControlled != null)
        {
            curControlled.GetComponent<PlayerTroop>().controlled = false;
            curControlled.GetComponent<PlayerTroop>().cameraFocusOnExit();
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
                skillMode = TroopSkill.walk;
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
            case TroopSkill.walk:
                curControlled.GetComponent<PlayerTroop>().hideIndicators();
                break;
            case TroopSkill.lunge:
                curControlled.GetComponent<PlayerTroop>().lunge();
                break;
            case TroopSkill.whirlwind:
                curControlled.GetComponent<PlayerTroop>().whirlwind();
                break;
            case TroopSkill.execute:
                curControlled.GetComponent<PlayerTroop>().execute();
                break;
            case TroopSkill.fire:
                curControlled.GetComponent<PlayerTroop>().fire();
                break;
            case TroopSkill.holdSteady:
                curControlled.GetComponent<PlayerTroop>().holdSteady();
                break;
            case TroopSkill.quickDraw:
                curControlled.GetComponent<PlayerTroop>().quickDraw();
                break;
            case TroopSkill.rainOfArrows:
                curControlled.GetComponent<PlayerTroop>().rainOfArrow();
                break;
            case TroopSkill.phalanx:
                curControlled.GetComponent<PlayerTroop>().phalanx();
                break;
            case TroopSkill.charge:
                curControlled.GetComponent<PlayerTroop>().charge();
                break;
        }

    }
    

}
