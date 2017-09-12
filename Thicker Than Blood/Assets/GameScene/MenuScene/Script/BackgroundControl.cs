using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundControl : MonoBehaviour {
    public Animator animator;
    bool needToSwitchState = false;
    int fromState, toState;
	// Use this for initialization
	void Start () {
        animator = transform.GetComponent<Animator>();
        animator.enabled = true; // this starts the "Idle" animation
        //Animator.r recorderMode = AnimatorRecorderMode.Record;
    }
	
	// Update is called once per frame
	void Update () {
        //animator.SetInteger("state", 5);
        if (needToSwitchState)
        {
            switchState();
        }
	}
    public void toContinue()
    {
        if (animator.GetInteger("state") < 0)
        {
            animator.SetFloat("Speed", 1.0f);
            animator.SetInteger("state", 0);
        }
        else if (animator.GetInteger("state") > 0)
        {
            animator.SetFloat("Speed", -1.0f);
            animator.SetInteger("state", 0);
        }
    }
    public void toNewGame()
    {
        if (animator.GetInteger("state") < 10)
        {
            animator.SetFloat("Speed", 1.0f);
            animator.SetInteger("state", 10);
        }
        else if (animator.GetInteger("state") > 10)
        {
            animator.SetFloat("Speed", -1.0f);
            animator.SetInteger("state", 10);
        }
    }
    public void toSaveGame()
    {
        if (animator.GetInteger("state") < 20)
        {
            animator.SetFloat("Speed", 1.0f);
            animator.SetInteger("state", 20);
        }
        else if (animator.GetInteger("state") > 20)
        {
            animator.SetFloat("Speed", -1.0f);
            animator.SetInteger("state", 20);
        }
    }
    public void toLoadGame()
    {
        if (animator.GetInteger("state") < 30)
        {
            animator.SetFloat("Speed", 1.0f);
            animator.SetInteger("state", 30);
        }
        else if (animator.GetInteger("state") > 30)
        {
            animator.SetFloat("Speed", -1.0f);
            animator.SetInteger("state", 30);
        }
    }
    public void toToturial()
    {
        if (animator.GetInteger("state") < 40)
        {
            animator.SetFloat("Speed", 1.0f);
            animator.SetInteger("state", 40);
        }
        else if (animator.GetInteger("state") > 40)
        {
            animator.SetFloat("Speed", -1.0f);
            animator.SetInteger("state", 40);
        }
    }
    public void toCredit()
    {
        if (animator.GetInteger("state") < 50)
        {
            animator.SetFloat("Speed", 1.0f);
            animator.SetInteger("state", 50);
        }
        else if (animator.GetInteger("state") > 50)
        {
            animator.SetFloat("Speed", -1.0f);
            animator.SetInteger("state", 50);
        }
    }
    public void toExit()
    {
        if (animator.GetInteger("state") < 60)
        {
            animator.SetFloat("Speed", 1.0f);
            animator.SetInteger("state", 60);
        }
        else if (animator.GetInteger("state") > 60)
        {
            animator.SetFloat("Speed", -1.0f);
            animator.SetInteger("state", 60);
        }
    }






    void changeState(int from, int to)
    {
        fromState = from;
        toState = to;
        needToSwitchState = true;
    }
    
    void switchState()
    {
        needToSwitchState = true;
        int temp = fromState;
        int toChange = 5;
        if (fromState > toState) //reverse case
        {
            toChange = -5;
        }
        if (temp != toState)
        {
            temp += 10;
            if (toChange > 0) //continue -> exit
            {
                animator.SetInteger("state", temp);
            }
            else //exit -> continue
            {
                animator.SetInteger("state", temp);
                animator.speed = -1;
            }
            
        } else
        {
            needToSwitchState = false;
        }
        
    }
}
