using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCamera : MonoBehaviour {
    public static bool startBattle = false;
    public static GameObject target, freeMoveTarget;
    public static CameraMode cameraMode = CameraMode.toggleToCenter;
    public GameObject mapCenter = null;
    public bool orbity = false;
    private bool lockCamera = false;
    private Vector3 positionOffset = Vector3.zero;
    private Vector3 positionOffsetBase = Vector3.zero;
    private int multiplier;
    private int counter;
    private void Start()
    {
        multiplier = 4;
        positionOffsetBase = (new Vector3(1, 1, 1));
        positionOffset = multiplier * positionOffsetBase;
        mapCenter.transform.position = new Vector3(BattleCentralControl.gridXMax/2, 0, BattleCentralControl.gridZMax/2);
        freeMoveTarget = mapCenter;
        freeMoveTarget.transform.position = mapCenter.transform.position;
        target = mapCenter;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (startBattle)
        {
            if (target != null)
            {
                switch (cameraMode)
                {
                    case CameraMode.toggleToCenter:
                        freeMoveTarget = mapCenter;
                        transform.LookAt(mapCenter.transform);
                        transform.position = Vector3.Slerp(transform.position, mapCenter.transform.position + positionOffset, Time.deltaTime * 1f);
                        zoom();
                        rotate();
                        break;
                    case CameraMode.freeMove:
                        transform.LookAt(freeMoveTarget.transform);
                        transform.position = Vector3.Slerp(transform.position, freeMoveTarget.transform.position + positionOffset, Time.deltaTime * 5f);
                        freeMove();
                        zoom();
                        break;
                    case CameraMode.mapObject:
                        var rotation = Quaternion.LookRotation(target.transform.position - transform.position);
                        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 1f);
                        transform.position = Vector3.Slerp(transform.position, target.transform.position + positionOffset, Time.deltaTime * 1f);
                        freeMoveTarget = getGrid(target);
                        zoom();
                        rotate();
                        break;

                }
                if (Input.GetKey(KeyCode.Space) && cameraMode != CameraMode.toggleToCenter)
                {
                    cameraMode = CameraMode.toggleToCenter;
                    positionOffsetBase = (new Vector3(1, 1, 1));
                    target = mapCenter;
                    positionOffset = 4 * positionOffsetBase;
                }
                if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && cameraMode != CameraMode.freeMove)
                {
                    cameraMode = CameraMode.freeMove;
                }
                if (Input.GetMouseButton(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && cameraMode != CameraMode.mapObject)
                {
                    cameraMode = CameraMode.mapObject;
                }
                if (Input.GetKeyDown(KeyCode.L))
                {
                    lockCamera = !lockCamera;
                }
            }

        }
    }

    private void zoom()
    {
        if (!lockCamera)
        {
            var d = Input.GetAxis("Mouse ScrollWheel");
            if (d > 0f) //scroll up
            {
                multiplier -= 2;
            }
            if (d < 0f)
            {
                multiplier += 2;
            }
            multiplier = Mathf.Clamp(multiplier, 5, 10);
            positionOffset = multiplier * positionOffsetBase;
        }
        
    }
    private void rotate()
    {
        if (!lockCamera)
        {
            if (Input.mousePosition.y > Screen.height - 2) //up, camera zoom in
            {
                counter++;
                if (counter == 4)
                {
                    multiplier--;
                    counter = 0;
                }
            }
            if (Input.mousePosition.y < 2) //down, camera zoom out
            {
                counter++;
                if (counter == 4)
                {
                    multiplier++;
                    counter = 0;
                }
            }
            if (Input.mousePosition.x > Screen.width - 2) //right
            {
                positionOffsetBase = Quaternion.Euler(0, -1, 0) * positionOffsetBase;
            }
            if (Input.mousePosition.x < 2) //left
            {
                positionOffsetBase = Quaternion.Euler(0, 1, 0) * positionOffsetBase;
            }

            positionOffset = multiplier * positionOffsetBase;
        }
        
    }
    
    private void freeMove()
    {
        float speed = 20f;
        var d = Input.GetAxis("Mouse ScrollWheel");

        //WASD
        float xAxisValue = Mathf.Clamp(Input.GetAxis("Horizontal"), 1, -1);
        float zAxisValue = Mathf.Clamp(Input.GetAxis("Vertical"), 1 , -1);
        if (Camera.current != null)
        {
            Debug.Log(xAxisValue + " " + zAxisValue);
            freeMoveTarget = getClosestGridObj(freeMoveTarget.transform.position - new Vector3(xAxisValue, 0.0f, zAxisValue));
        }


        if (d > 0f && transform.position.z < 200) //scroll up
        {
            target.transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));

        }
        if (d < 0f && transform.position.z > 4)
        {
            target.transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        }
    }
    private GameObject getClosestGridObj(Vector3 pos)
    {
        Mathf.Clamp(pos.x, 0, BattleCentralControl.gridXMax);
        Mathf.Clamp(pos.z, 0, BattleCentralControl.gridZMax);
        return BattleCentralControl.gridToObj[BattleCentralControl.map[Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.z)]];
    }
    private GameObject getGrid(GameObject obj)
    {
        if (obj.tag == "Grid")
        {
            return obj;
        } else if (obj.tag == "PlayerTroop" || obj.tag == "EnemyTroop")
        {
            return BattleCentralControl.gridToObj[obj.GetComponent<Troop>().curGrid];
        } else
        {
            return null;
        }
    }
}

public enum CameraMode
{
    toggleToCenter,
    freeMove,
    mapObject
};