using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInteraction : MonoBehaviour {
    const float INTERACT_DIST = 1;
    GameObject player;
    List<GameObject> inspectedList = new List<GameObject>();
    public static bool chasing;
    public static TroopSkill skillMode;
    public static GameObject curControlled;
    GameObject curChasedObj;
    
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("player_party");
        chasing = false;
        skillMode = TroopSkill.none;
    }


    // Update is called once per frame
    void Update()
    {
        inputKeysActions();
        if (chasing)
        {
        }
    }
    void inputKeysActions()
    {
        if (Input.GetMouseButton(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            BattleCentralControl.hideCurShowing();
            selectObject();
        }
        if (Input.GetMouseButton(1) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            chasing = false;
            
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
        }
    }
    void selectObject()
    {
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;
        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
        {
            GameObject interactedObject = interactionInfo.collider.gameObject.transform.parent.gameObject;
            
            if (interactedObject.tag == "EnemyTroop" || interactedObject.tag == "NeutralTroop" )
            {
                //Debug.Log("Interactable");
                interactedObject.GetComponent<BattleInteractable>().cameraFocusOn();
            } else if (interactedObject.tag == "Grid")
            {
                if (skillMode == TroopSkill.walk)
                {
                    if (BattleCentralControl.objToGrid[interactedObject].mark >= 0)
                    {
                        interactedObject.GetComponent<GridObject>().moveTroopToGrid(curControlled);
                    } else
                    {

                    }
                } else
                {
                    interactedObject.GetComponent<GridObject>().cameraFocusOn();
                }
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
    
}
