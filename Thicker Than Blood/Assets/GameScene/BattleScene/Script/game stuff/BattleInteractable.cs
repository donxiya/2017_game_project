using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInteractable : MonoBehaviour {

	public virtual void cameraFocusOn ()
    {
        BattleCamera.target = gameObject;
        BattleCamera.cameraMode = CameraMode.mapObject;
    }
}
