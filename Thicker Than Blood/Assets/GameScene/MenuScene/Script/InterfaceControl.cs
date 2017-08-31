using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterfaceControl : MonoBehaviour {
    private GameObject mainMenuPanel;
    private GameObject saveLoadPanel;
    private GameObject tutorialPanel;

    //main panel buttons
    private GameObject continueButton;
    private GameObject newGameButton;
    private GameObject saveGameButton;
    private GameObject loadGameButton;
    private GameObject tutorialButton;
    private GameObject exitGameButton;

    //save load buttons
    private GameObject saveLoadReturnButton;

    //tutorial buttons
    private GameObject tutorialReturnButton;

    // Use this for initialization
    void Start () {
        initializion();
        
        

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void continueOnClicked()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MapScene");
    }

    void newGameOnClicked()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MapScene");
    }

    void saveGameOnClicked()
    {
        string save_load = "save";
        saveLoadInitializtion(save_load);
    }

    void loadGameOnClicked()
    {
        string save_load = "load";
        saveLoadInitializtion(save_load);
    }

    void initializion ()
    {
        mainMenuPanel = GameObject.Find("Main_Menu_Panel");
        saveLoadPanel = GameObject.Find("Save_Load_Panel");
        tutorialPanel = GameObject.Find("Tutorial_Panel");

        mainMenuPanel.SetActive(true);
        saveLoadPanel.SetActive(false);
        tutorialPanel.SetActive(false);

        continueButton = GameObject.Find("continue_button");
        continueButton.GetComponent<Button>().onClick.AddListener(delegate () { this.continueOnClicked(); });
        newGameButton = GameObject.Find("new_game_button");
        newGameButton.GetComponent<Button>().onClick.AddListener(delegate () { this.newGameOnClicked(); });
        saveGameButton = GameObject.Find("save_game_button");
        saveGameButton.GetComponent<Button>().onClick.AddListener(delegate () { this.saveGameOnClicked(); });
        loadGameButton = GameObject.Find("load_game_button");
        loadGameButton.GetComponent<Button>().onClick.AddListener(delegate () { this.loadGameOnClicked(); });
        tutorialButton = GameObject.Find("tutorial_button");
        tutorialButton.GetComponent<Button>().onClick.AddListener(delegate () { this.tutorialInitializtion(); });
        exitGameButton = GameObject.Find("tutorial_button");
        exitGameButton.GetComponent<Button>().onClick.AddListener(delegate () { this.exitGame(); });
    }

    void navi(GameObject curPanel, GameObject toPanel)
    {
        curPanel.SetActive(false);
        toPanel.SetActive(true);
    }

    void saveLoadInitializtion (string save_load)
    {
        navi(mainMenuPanel, saveLoadPanel);

        saveLoadReturnButton = GameObject.Find("save_load_return_button");
        saveLoadReturnButton.GetComponent<Button>().onClick.AddListener(delegate () { this.navi(saveLoadPanel, mainMenuPanel); });
    }

    void tutorialInitializtion()
    {
        navi(mainMenuPanel, tutorialPanel);

        tutorialReturnButton = GameObject.Find("tutorial_return_button");
        tutorialReturnButton.GetComponent<Button>().onClick.AddListener(delegate () { this.navi(tutorialPanel, mainMenuPanel); });
    }

    void exitGame()
    {
        Application.Quit();
    }
}
