using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
    public GameObject target = null;
    public GameObject player_party = null;
    public bool orbity = false;
    private Vector3 positionOffset = Vector3.zero;
    private Vector3 positionOffsetBase = Vector3.zero;
    private int multiplier;
    private string current_mode = "TOGGLE_TO_PLAYER";
    int counter;
    private void Start()
    {
        
        multiplier = 4;
        
        positionOffsetBase = (new Vector3(0, 2, -2));
        positionOffset = multiplier * positionOffsetBase;
        player_party = GameObject.Find("player_party");
        target = player_party;
    }
	
	// Update is called once per frame
	private void FixedUpdate () {
		if (target != null)
        {
            switch (current_mode)
            {
                case "TOGGLE_TO_PLAYER" :
                    
                    transform.LookAt(target.transform);
                    transform.position = Vector3.Slerp(transform.position, target.transform.position + positionOffset, Time.deltaTime * 5f);
                    
                    zoom();
                    rotate();
                    break;
                case "FREE_MOVE":
                    freeMove();
                    //zoom();
                    break;
                case "RANDOM_MAP_OBJECT":
                    break;
                
            }
            
            
            if (Input.GetKey(KeyCode.Space) && current_mode != "TOGGLE_TO_PLAYER")
            {
                current_mode = "TOGGLE_TO_PLAYER";
                positionOffsetBase = (new Vector3(2, 2, 1));
                target = player_party;
                positionOffset = 4 * positionOffsetBase;
            }
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && current_mode != "FREE_MOVE")
            {
                current_mode = "FREE_MOVE";
            }
        }
    }
    
    private void zoom()
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
        multiplier = Mathf.Clamp(multiplier, 5, 40);
        positionOffset = multiplier * positionOffsetBase;
    }
    private void rotate()
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
        if(Input.mousePosition.x < 2) //left
        {
            positionOffsetBase = Quaternion.Euler(0, 1, 0) * positionOffsetBase;
            
        }
        if (Input.mousePosition.x > Screen.width - 2) //right
        {
            positionOffsetBase = Quaternion.Euler(0, -1, 0) * positionOffsetBase;
        }
        
        positionOffset = multiplier * positionOffsetBase;
    }


    private void freeMove () {
        float speed = 20f;
        var d = Input.GetAxis("Mouse ScrollWheel");

        //WASD
        float xAxisValue = Input.GetAxis("Horizontal");
        float zAxisValue = Input.GetAxis("Vertical");
        if (Camera.current != null)
        {
            //transform.Translate(transform.forward*Time.deltaTime*20);
            var curPos = transform.position.y;
            transform.Translate(new Vector3(xAxisValue, 0.0f, zAxisValue));
            var pos = transform.position;
            
            pos.y = Mathf.Clamp(transform.position.y, curPos, curPos);
            transform.position = pos;
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
}
