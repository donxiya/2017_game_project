using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : BattleInteractable {
    public Grid gridInfo;
    public override void cameraFocusOn()
    {
        base.cameraFocusOn();
    }
    public GameObject placeTroopOnGrid(GameObject toPlace, Vector3 pos, Quaternion rot)
    {
        return Instantiate(toPlace, pos, rot);
    }
    public void moveTroopToGrid(GameObject toMove)
    {
        Debug.Log(BattleCentralControl.objToGrid[gameObject].path.Count);
        toMove.GetComponent<PlayerTroop>().troopMoveToPlace(BattleCentralControl.objToGrid[gameObject].path.Dequeue());
        /**while (BattleCentralControl.objToGrid[gameObject].path.Count != 0)
        {
            toMove.GetComponent<PlayerTroop>().troopMoveToPlace(BattleCentralControl.objToGrid[gameObject].path.Dequeue());
        }**/
    }
}
