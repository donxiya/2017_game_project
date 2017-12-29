using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInteractable : MonoBehaviour {

	public virtual void cameraFocusOn ()
    {
        if (gameObject != null)
        {
            BattleCamera.target = gameObject;
            BattleCamera.cameraMode = CameraMode.mapObject;
        }
    }
    public virtual void cameraFocusOnExit()
    {
    }
}
