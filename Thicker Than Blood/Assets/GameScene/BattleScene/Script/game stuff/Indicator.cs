using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : BattleInteractable {
    public List<Grid> collided;
    private void Start()
    {
        collided = new List<Grid>();
    }
    private void Update()
    {

        //Debug.Log("collided: " + collided.Count);
    }
    public override void cameraFocusOn()
    {
        //base.cameraFocusOn();
    }

    
}
