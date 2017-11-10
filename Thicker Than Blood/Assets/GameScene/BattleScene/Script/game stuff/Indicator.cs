using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : BattleInteractable {
    public List<Grid> collided = new List<Grid>();
    private void Start()
    {

    }
    
    private void OnEnable()
    {
        collided.Clear();
    }
    public override void cameraFocusOn()
    {
        //base.cameraFocusOn();
    }
    public void goToIndicatedGrid(GameObject troop) //only works for walk indicator
    {
        Grid curGrid = BattleCentralControl.map[Mathf.RoundToInt(gameObject.transform.position.x), Mathf.RoundToInt(gameObject.transform.position.z)];
        troop.GetComponent<Troop>().troopMoveToPlace(curGrid);
    }
    
}
