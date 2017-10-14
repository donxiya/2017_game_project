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
        objectiveButton.onClick.AddListener(delegate () { showPanel(TabPanelType.ObjectivePanel, true); });
        factionButton.onClick.AddListener(delegate () { showPanel(TabPanelType.FactionPanel, true); });
        sapeButton.onClick.AddListener(delegate () { showPanel(TabPanelType.SAPEPanel, true); });
        ciButton.onClick.AddListener(delegate () { showPanel(TabPanelType.CIPanel, true); });
        mainGearButton.onClick.AddListener(delegate () { showPanel(TabPanelType.MainGearPanel, true); });
        secGearButton.onClick.AddListener(delegate () { showPanel(TabPanelType.SecGearPanel, true); });
        inventoryButton.onClick.AddListener(delegate () { showPanel(TabPanelType.InventoryPanel, true); });
        troopButton.onClick.AddListener(delegate () { showPanel(TabPanelType.TroopPanel, true); });
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
                if (!defaultPanel.activeSelf)
                {
                    showPanel(TabPanelType.DefaultPanel, true);
                } else
                {
                    gameObject.SetActive(false);
                    Time.timeScale = 1.0f;
                }
            }
        }

            
        

    }

    public void showPanel(TabPanelType tabPanelType, bool show)
    {
        if (tabPanelType == TabPanelType.DefaultPanel)
        {
            defaultPanel.SetActive(show);
        } else
        {
            defaultPanel.SetActive(!show);
        }
        if (tabPanelType == TabPanelType.ObjectivePanel)
        {
            animator.SetBool("objectiveShow", show);
        } else
        {
            animator.SetBool("objectiveShow", !show);
        }
        if (tabPanelType == TabPanelType.SAPEPanel)
        {
            animator.SetBool("sapeShow", show);
        } else
        {
            animator.SetBool("sapeShow", !show);
        }
        if (tabPanelType == TabPanelType.MainGearPanel)
        {
            animator.SetBool("mainGearShow", show);
        } else
        {
            animator.SetBool("mainGearShow", !show);
        }
        if (tabPanelType == TabPanelType.InventoryPanel)
        {
            animator.SetBool("inventoryShow", show);
        } else
        {
            animator.SetBool("inventoryShow", !show);
        }
        if (tabPanelType == TabPanelType.FactionPanel)
        {
            animator.SetBool("factionShow", show);
        } else
        {
            animator.SetBool("factionShow", !show);
        }
        if (tabPanelType == TabPanelType.CIPanel)
        {
            animator.SetBool("ciShow", show);
        } else
        {
            animator.SetBool("ciShow", !show);
        }
        if (tabPanelType == TabPanelType.SecGearPanel)
        {
            animator.SetBool("secGearShow", show);
        } else
        {
            animator.SetBool("secGearShow", !show);
        }
        if (tabPanelType == TabPanelType.TroopPanel)
        {
            animator.SetBool("troopShow", show);
        } else
        {
            animator.SetBool("troopShow", !show);
        }
        
    }
    

}

public enum TabPanelType
{
    DefaultPanel,
    ObjectivePanel,
    SAPEPanel,
    MainGearPanel,
    InventoryPanel,
    FactionPanel,
    CIPanel,
    SecGearPanel,
    TroopPanel
}