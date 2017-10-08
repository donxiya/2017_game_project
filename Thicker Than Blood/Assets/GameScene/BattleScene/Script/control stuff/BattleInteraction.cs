using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleInteraction : MonoBehaviour {
    const float INTERACT_DIST = 1;
    GameObject player;
    List<GameObject> inspectedList = new List<GameObject>();
    public static bool inAction;
    public static TroopSkill skillMode;
    public static GameObject curControlled;
    public static GameObject interactedObject;
    public static GameObject inspectedObject;
    // Use this for initialization
    void Start()
    {
        skillMode = TroopSkill.walk;
        inAction = false;
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
                curControlled.GetComponent<Troop>().doSkill(curControlled.GetComponent<Troop>().indicatedGrid(), skillMode);
                if (Input.GetMouseButtonDown(0))
                {
                    skillMode = TroopSkill.walk;
                }
            } else
            {   
                if (!inAction)
                {
                    selectObject();
                }
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
                        if (curControlled.GetComponent<Troop>().reachedDestination)
                        {
                            walkToObj();
                        }
                    } else if (skillMode == TroopSkill.none && Input.GetMouseButtonDown(1))
                    {
                        skillMode = TroopSkill.walk;
                    }
                    else
                    {
                        skillMode = TroopSkill.none;
                        curControlled.GetComponent<Troop>().hideIndicators();
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
            curControlled.GetComponent<Troop>().controlled = false;
            curControlled.GetComponent<Troop>().cameraFocusOnExit();
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
            
            if (interactedObject.tag == "Troop")
            {
                interactedObject.GetComponent<Troop>().cameraFocusOn();
                curControlled = interactedObject;
                skillMode = TroopSkill.walk;
            } else if (interactedObject.tag == "Grid")
            {
                interactedObject.GetComponent<GridObject>().cameraFocusOn();
            } else
            {
                Debug.Log(interactedObject.name);
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
            } else
            {
                Debug.Log(interactedObject.name);
            }
        }
    }
    
    void showIndicator()
    {
        switch(skillMode)
        {
            case TroopSkill.walk:
                curControlled.GetComponent<Troop>().hideIndicators();
                curControlled.GetComponent<Troop>().walk();
                break;
            case TroopSkill.lunge:
                curControlled.GetComponent<Troop>().lunge();
                break;
            case TroopSkill.whirlwind:
                curControlled.GetComponent<Troop>().whirlwind();
                break;
            case TroopSkill.execute:
                curControlled.GetComponent<Troop>().execute();
                break;
            case TroopSkill.fire:
                curControlled.GetComponent<Troop>().fire();
                break;
            case TroopSkill.holdSteady:
                curControlled.GetComponent<Troop>().holdSteady();
                break;
            case TroopSkill.quickDraw:
                curControlled.GetComponent<Troop>().quickDraw();
                break;
            case TroopSkill.rainOfArrows:
                curControlled.GetComponent<Troop>().rainOfArrow();
                break;
            case TroopSkill.guard:
                curControlled.GetComponent<Troop>().guard();
                break;
            case TroopSkill.charge:
                curControlled.GetComponent<Troop>().charge();
                break;
        }

    }
}
