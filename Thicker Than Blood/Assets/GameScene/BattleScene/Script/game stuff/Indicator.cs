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

    
}
