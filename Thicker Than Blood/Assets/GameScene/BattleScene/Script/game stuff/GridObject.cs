using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : BattleInteractable {
    public bool seen = false;
    public Color originalColor;
    public MeshRenderer meshRenderer;
    public GameObject infoPanel;
    public void Start()
    {
        meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
        originalColor = meshRenderer.material.color;
        becomeUnseen();
    }
    public override void cameraFocusOn()
    {
        base.cameraFocusOn();
        infoPanel.SetActive(true);
        
        if (seen)
        {
            GridInspectPanel.unknown = false;
        } else
        {
            GridInspectPanel.unknown = true;
        }
        GridInspectPanel.grid = BattleCentralControl.objToGrid[gameObject];
    }
    public override void cameraFocusOnExit()
    {
        base.cameraFocusOnExit();
        infoPanel.SetActive(false);
        GridInspectPanel.grid = null;
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

        toMove.GetComponent<Troop>().troopMoveToPlace(BattleCentralControl.objToGrid[gameObject]);
        
    }
    public void checkTroopOnGrid(Troop troop)
    {
        BattleCentralControl.objToGrid[gameObject].checkPersonStealth(troop);
    }
    public void becomeUnseen()
    {
        meshRenderer.material.color = new Color(0f, 0f, 0f);
        seen = false;
    }
    public void becomeSeen()
    {
        meshRenderer.material.color = originalColor;
        seen = true;
    }
}
