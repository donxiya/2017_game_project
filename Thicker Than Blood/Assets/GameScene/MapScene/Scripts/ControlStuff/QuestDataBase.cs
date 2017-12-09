using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDataBase : MonoBehaviour {
    public static QuestDataBase dataBase;
    public List<Quest> quests;
    public GameObject objective;
	// Use this for initialization
	void Awake () {
        dataBase = gameObject.GetComponent<QuestDataBase>();
        objective.SetActive(false);
        initialization();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void initialization()
    {
        initializeMAIN1();
        initializeMAIN2();
    }

    void initializeMAIN1()
    {
        quests = new List<Quest>();
        Quest quest = new Quest("Chapter I", "MAIN1", true);
        GameObject newObjective = GameObject.Instantiate(objective);
        newObjective.transform.position = new Vector3(250, 3, 250);
        quest.objective = newObjective;
        quest.totalProgress = 6;
        quest.currentProgress = 3;
        quest.progressToDescription.Add(1, "Defeat enemies");
        quest.progressToDescription.Add(2, "Navigate to Genova");
        quest.progressToDescription.Add(3, "Tavern and market tutorial");
        quest.progressToDescription.Add(4, "Navigate to Milano");
        quest.progressToDescription.Add(5, "Defeat enemies");
        quest.progressToDescription.Add(6, "Quest Complete");
        quest.introduction = "I am happy";
        quests.Add(quest);
    }

    void initializeMAIN2()
    {
        quests = new List<Quest>();
        Quest quest = new Quest("Chapter 2", "MAIN2", true);
        GameObject newObjective = GameObject.Instantiate(objective);
        newObjective.transform.position = new Vector3(250, 3, 250);
        quest.objective = newObjective;
        quest.totalProgress = 3;
        quest.currentProgress = 2;
        quest.progressToDescription.Add(1, "Have 1000f");
        quest.progressToDescription.Add(2, "Pay 800f");
        quest.progressToDescription.Add(3, "Quest Complete");

        quest.introduction = "Venezia";
        quests.Add(quest);
    }

    public Quest getQuest(string id)
    {
        Quest result = null;
        foreach(Quest q in quests)
        {
            if (q.questID == id)
            {
                result = q;
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
    public GameObject objective;
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

    }
    public Quest(Quest quest)
    {
        questName = quest.questName;
        questID = quest.questID;
        unique = quest.unique;
        prerequisites = quest.prerequisites;
        progressToDescription = quest.progressToDescription;

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
    MainQuest,
    BanditHunt,

}