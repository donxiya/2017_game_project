using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNPCImgDataBase : MonoBehaviour {
    public static SNPCImgDataBase dataBase;
    public Dictionary<Person, SNPCTroopInfo> snpcDict;
    //ZERO STEP
    public Texture2D ludvicoProfile, biancaProfile, girolamoProfile, pieroProfile,
        giovanniProfile, cesareProfile, captainGeneralProfile, mariaProfile, jakobProfile;
    public Texture2D ludvicoIcon, biancaIcon, girolamoIcon, pieroIcon,
        giovanniIcon, cesareIcon, captainGeneralIcon, mariaIcon, jakobIcon;
    // Use this for initialization
    void Awake () {
        dataBase = gameObject.GetComponent<SNPCImgDataBase>();
        initialization();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void initialization()
    {
        snpcDict = new Dictionary<Person, SNPCTroopInfo>();
        //FIRST STEP
        snpcDict.Add(new Person("Ludvico Sforza", new Stats(10, 9, 8, 10, 10, 10), Ranking.elite, TroopType.cavalry, Faction.italy, new Experience(0, 51, 0)), generateTroopInfo(ludvicoIcon, ludvicoProfile));
        addDescription();
    }

    void addDescription()
    {
        //SEC STEP
        getSNPCTroopInfo("Ludvico Sforza").description.Add(0, "A fucktard.");
    }

    SNPCTroopInfo generateTroopInfo(Texture2D icon, Texture2D profile)
    {
        SNPCTroopInfo result = new SNPCTroopInfo();
        result.icon = icon;
        result.profile = profile;
        result.index = 0;
        result.description = new Dictionary<int, string>();
        return result;
    }

    public SNPCTroopInfo getSNPCTroopInfo(string name)
    {
        foreach(KeyValuePair<Person, SNPCTroopInfo> pair in snpcDict)
        {
            if (name == pair.Key.name)
            {
                return pair.Value;
            }
        }
        return null;
    }
}
public class SNPCTroopInfo
{
    public GameObject model;
    public Texture2D icon;
    public Texture2D profile;
    public GearInfo gear;
    public Dictionary<int, string> description;
    public int index;
    public bool alive;
}