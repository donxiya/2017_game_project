
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bandit : Interactable
{
    private const float DEFAULT_ALPHA = 255;
    void Start()
    {
        dialogue = new string[] { "hello", "hi" };
        interactableType = InteractableType.town;
    }
    public override void interact()
    {

    }

    public override void OnTriggerEnter(Collider col)
    {

        
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Interactable Object")
        {
            Debug.Log("Run into player");
            SceneManager.LoadScene("BattleScene");
        }
    }
    public override void OnTriggerExit(Collider col)
    {
        
    }
}
