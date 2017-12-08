using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabMenu : MonoBehaviour {
    public static TabMenu tabMenu;
    public GameObject defaultPanel, topPanel;
    public Button objectiveButton, factionButton, sapeButton, ciButton,
        mainGearButton, secGearButton, inventoryButton, troopButton;
    public Button objectiveButtonQuick, factionButtonQuick, sapeButtonQuick, ciButtonQuick,
        mainGearButtonQuick, secGearButtonQuick, inventoryButtonQuick, troopButtonQuick, closeTabButton;
    public Animator animator;
    bool closing = true;
    // Use this for initialization
    void Start() {
        tabMenu = gameObject.GetComponent<TabMenu>();
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
        objectiveButtonQuick.onClick.AddListener(delegate () { showPanel(TabPanelType.ObjectivePanel, true); });
        factionButtonQuick.onClick.AddListener(delegate () { showPanel(TabPanelType.FactionPanel, true); });
        sapeButtonQuick.onClick.AddListener(delegate () { showPanel(TabPanelType.SAPEPanel, true); });
        ciButtonQuick.onClick.AddListener(delegate () { showPanel(TabPanelType.CIPanel, true); });
        mainGearButtonQuick.onClick.AddListener(delegate () { showPanel(TabPanelType.MainGearPanel, true); });
        secGearButtonQuick.onClick.AddListener(delegate () { showPanel(TabPanelType.SecGearPanel, true); });
        inventoryButtonQuick.onClick.AddListener(delegate () { showPanel(TabPanelType.InventoryPanel, true); });
        troopButtonQuick.onClick.AddListener(delegate () { showPanel(TabPanelType.TroopPanel, true); });
        closeTabButton.onClick.AddListener(delegate () { closeTabMenu(); });
    }
    private void OnEnable()
    {
        closing = false;
        defaultPanel.SetActive(true);
        topPanel.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            resetLayout();
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
                    //TroopManagement.troopManagement.leaveManagement();
                    //InventoryManagement.inventoryManagement.leaveManagement();
                }
            }
            if (closing)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Default"))
                {
                    gameObject.SetActive(false);
                    Time.timeScale = 1.0f;
                }
            }
        }
    }

    public void showPanel(TabPanelType tabPanelType, bool show)
    {
        animator.SetBool("troopUpgradeShow", false);
        if (tabPanelType == TabPanelType.DefaultPanel)
        {
            defaultPanel.SetActive(show);
            if (!show) {
                closing = true;
            }
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
            if (!show)
            {
                InventoryManagement.inventoryManagement.leaveManagement();
            } else
            {
                InventoryManagement.inventoryManagement.initialization();
            }
        } else
        {
            if (show && animator.GetBool("inventoryShow"))
            {
                InventoryManagement.inventoryManagement.leaveManagement();
            }
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
            if (!show && TroopManagement.troopManagement != null) //if show is false
            {
                TroopManagement.troopManagement.leaveManagement();
            }
        } else
        {
            animator.SetBool("troopShow", !show);
            if (show && TroopManagement.troopManagement != null) //if show is true
            {
                TroopManagement.troopManagement.leaveManagement();
            }
        }

    }
    public void closeTabMenu()
    {
        showPanel(TabPanelType.DefaultPanel, true);
        defaultPanel.SetActive(false);
        topPanel.SetActive(false);
        closing = true;
    }
    public void resetLayout ()
    {
        topPanel.GetComponent<HorizontalLayoutGroup>().SetLayoutHorizontal();
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