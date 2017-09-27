using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonColorAnimation : MonoBehaviour {
    public Animator animator;
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    public void pointerEnter()
    {
        animator.SetBool("hover", true);
    }
    public void pointerExit()
    {
        animator.SetBool("hover", false);
    }
}
