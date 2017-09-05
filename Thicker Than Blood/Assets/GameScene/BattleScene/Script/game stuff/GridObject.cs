using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : BattleInteractable {
    public override void cameraFocusOn()
    {
        base.cameraFocusOn();
    }
    public GameObject placeTroopOnGrid(GameObject toPlace, Vector3 pos, Quaternion rot)
    {
        return Instantiate(toPlace, pos, rot);
    }
    public bool inGrid(float x, float z)
    {
        float gridPosX = BattleCentralControl.objToGrid[gameObject].x;
        float gridPosZ = BattleCentralControl.objToGrid[gameObject].x;
        if (x > gridPosX- .5f && x < gridPosX + .5f &&
            z > gridPosZ - .5f && z < gridPosZ + .5f)
        {
            return true;
        }
        return false;
    }

    public void moveTroopToGrid(GameObject toMove)
    {

        toMove.GetComponent<PlayerTroop>().troopMoveToPlace(BattleCentralControl.objToGrid[gameObject]);
        
    }
}
