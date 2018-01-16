using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDataBase : MonoBehaviour {
    public static QuestDataBase dataBase;
    public List<Quest> quests;
    public GameObject objective;
    public Texture2D defaultQuestIcon;
	// Use this for initialization
	void Awake () {
        dataBase = this;
        objective.SetActive(false);
        initialization();

    }
	
	// Update is called once per frame
	void Update () {
		if (MapManagement.cities != null && MapManagement.cities.Count > 0)
        {
            initializeCityQuests();
        }
	}
    void initialization()
    {
        quests = new List<Quest>();
        initializeMAIN1();
        initializeMAIN2();
    }

    void initializeMAIN1()
    {
        Quest quest = new Quest("Chapter I", "MAIN1", true);
        quest.questType = QuestType.MAIN;
        quest.totalProgress = 6;
        quest.currentProgress = 3;
        quest.progressToDescription.Add(1, "Defeat enemies");
        quest.progressToDescription.Add(2, "Navigate to Genova");
        quest.progressToDescription.Add(3, "Tavern and market tutorial");
        quest.progressToDescription.Add(4, "Navigate to Milano");
        quest.progressToDescription.Add(5, "Defeat enemies");
        quest.progressToDescription.Add(6, "Quest Complete");
        quest.progressToLocation.Add(1, new Vector3(-1, -1, -1));
        quest.progressToLocation.Add(2, new Vector3(-1, -1, -1));
        quest.progressToLocation.Add(3, new Vector3(-1, -1, -1));
        quest.progressToLocation.Add(4, new Vector3(-1, -1, -1));
        quest.progressToLocation.Add(5, new Vector3(-1, -1, -1));
        quest.progressToLocation.Add(6, new Vector3(-1, -1, -1));
        quest.introduction = "I am happy";
        quests.Add(quest);
    }
    void initializeMAIN2()
    {
        Quest quest = new Quest("Chapter II", "MAIN2", true);
        GameObject newObjective = GameObject.Instantiate(objective);
        newObjective.transform.position = new Vector3(250, 3, 250);
        quest.questType = QuestType.MAIN;
        quest.currentProgress = 2;
        quest.progressToDescription.Add(1, "Have 1000f");
        quest.progressToDescription.Add(2, "Pay 800f");
        quest.progressToDescription.Add(3, "Quest Complete");

        quest.introduction = "Venezia";
        quests.Add(quest);
    }
    void initializeCityQuests()
    {
        foreach(City city in MapManagement.cities)
        {
            makeCityAssassinationQuest(city);
            makeCityBanditHuntQuest(city);
        }
    }
    void makeCityBanditHuntQuest(City city)
    {
        Quest quest = new Quest(city.lName + " Bandit Hunt", QuestType.HUN + city.ID, false);
        quest.questType = QuestType.HUN;
        quest.totalProgress = 2;
        quest.currentProgress = 0;
        quest.progressToDescription.Add(0, "Kill Bandits plz");
        quest.progressToDescription.Add(1, "Kill Bandits");
        quest.progressToDescription.Add(2, "Quest Complete");
        quest.progressToLocation.Add(1, new Vector3(-1, -1, -1)); //bandit location
        quest.progressToLocation.Add(2, new Vector3(-1, -1, -1));
        quest.introduction = "Bandits are cool";
        quests.Add(quest);

    }
    void makeCityAssassinationQuest(City city)
    {
        Quest quest = new Quest(city.lName + " Political Assassination", QuestType.ASN + city.ID, false);
        quest.questType = QuestType.ASN;
        quest.totalProgress = 2;
        quest.currentProgress = 0;
        quest.progressToDescription.Add(0, "Kill some dudes plz");
        quest.progressToDescription.Add(1, "Kill Bandits");
        quest.progressToDescription.Add(2, "Quest Complete");
        quest.progressToLocation.Add(1, new Vector3(-1, -1, -1)); //bandit location
        quest.progressToLocation.Add(2, new Vector3(-1, -1, -1));
        quest.introduction = "Bandits are cool";
        quests.Add(quest);

    }
    public GameObject getObjectiveIndicator()
    {
        GameObject newIndicator = Object.Instantiate(objective);
        objective.transform.SetParent(objective.transform.parent, false);
        return newIndicator;
    }
    public Quest getQuest(string id)
    {
        Quest result = null;
        foreach(Quest q in quests)
        {
            if (q.questID == id)
            {
                result = new Quest(q);
                return result;
            }
            
        }
        return result;
    }
}

public class Quest
{
    public string questName { get; set; }
    public string questID { get; set; }
    public bool unique;
    public List<string> prerequisites;
    public int totalProgress = 0;
    public int currentProgress = 0;
    public Texture2D questIcon, questProfile;
    public Dictionary<int, string> progressToDescription;
    public Dictionary<int, Vector3> progressToLocation;
    public QuestType questType;
    public float colliderSizeMultiplier = 1.0f;
    public bool active = false;
    public bool complete = false;
    public int stack = 0;
    public string introduction;
    public Quest(string name, string ID, bool uniqueI)
    {
        questName = name;
        questID = ID;
        unique = uniqueI;
        prerequisites = new List<string>();
        progressToDescription = new Dictionary<int, string>();
        progressToLocation = new Dictionary<int, Vector3>();
    }
    public Quest(Quest quest)
    {
        questName = quest.questName;
        questID = quest.questID;
        unique = quest.unique;
        prerequisites = quest.prerequisites;
        totalProgress = quest.totalProgress;
        currentProgress = quest.currentProgress;
        questIcon = quest.questIcon;
        questProfile = quest.questProfile;
        progressToDescription = quest.progressToDescription;
        progressToLocation = quest.progressToLocation;
        questType = quest.questType;
        colliderSizeMultiplier = quest.colliderSizeMultiplier;
        active = quest.active;
        complete = quest.complete;
        stack = quest.stack;
        introduction = quest.introduction;
    }
    public bool fulfilledPrerequisite(List<Quest> completedQuests)
    {
        List<string> completedIDs = new List<string>();
        foreach (Quest q in completedQuests)
        {
            completedIDs.Add(q.questID);

        }
        if (unique && prerequisites.Count > 0)
        {
            foreach (string id in prerequisites)
            {
                if (!completedIDs.Contains(id))
                {
                    return false;
                }
            }
            return true;
        } else {
            foreach (string id in prerequisites)
            {
                if (completedIDs.Contains(id))
                {
                    return false;
                }
            }
            return true;
        }
        
    }

}

public enum QuestType
{
    MAIN,
    HUN,
    ASN

}