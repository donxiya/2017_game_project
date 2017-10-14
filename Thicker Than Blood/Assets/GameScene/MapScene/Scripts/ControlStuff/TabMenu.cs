using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabMenu : MonoBehaviour {
    public GameObject defaultPanel;
    public Button objectiveButton, factionButton, sapeButton, ciButton,
        mainGearButton, secGearButton, inventoryButton, troopButton;
    public Animator animator;
	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
        animator = gameObject.GetComponent<Animator>();
        objectiveButton.onClick.AddListener(delegate () { showObjective(true); });
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (Time.timeScale != 0.0f)
            {
                Time.timeScale = 0.0f;
            }
            if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1.0f;
                gameObject.SetActive(false);
            }
        }

            
        

    }

    public void showObjective(bool show)
    {
        defaultPanel.SetActive(!show);
        animator.SetBool("objectiveShow", show);
    }

}
