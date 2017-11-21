using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSceneUIImageDataBase : MonoBehaviour {

    public Texture2D cityDefaultImg, cityGarrisonImg, cityThreatenImg, cityMarketImg, cityHallImg, cityArmoryImg,
        cityTavernImg, cityBrothelImg, cityChurchImg, cityEncampmentImg;
    public Texture2D townDefaultImg, townGarrisonImg, townThreatenImg, townRestockImg, townRecruitImg, townInvestImg;
    public static MapSceneUIImageDataBase dataBase;

    // Use this for initialization
    void Awake () {
        dataBase = new MapSceneUIImageDataBase();
        dataBase.cityDefaultImg = cityDefaultImg;
        dataBase.cityGarrisonImg = cityGarrisonImg;
        dataBase.cityThreatenImg = cityThreatenImg;
        dataBase.cityMarketImg = cityMarketImg;
        dataBase.cityHallImg = cityHallImg;
        dataBase.cityArmoryImg = cityArmoryImg;
        dataBase.cityTavernImg = cityTavernImg;
        dataBase.cityBrothelImg = cityBrothelImg;
        dataBase.cityChurchImg = cityChurchImg;
        dataBase.cityEncampmentImg = cityEncampmentImg;
        dataBase.townDefaultImg = townDefaultImg;
        dataBase.townGarrisonImg = townGarrisonImg;
        dataBase.townThreatenImg = townThreatenImg;
        dataBase.townRestockImg = townRestockImg;
        dataBase.townRecruitImg = townRecruitImg;
        dataBase.townInvestImg = townInvestImg;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Texture2D getCityDefaultImg()
    {
        return cityDefaultImg;
    }
    public Texture2D getTownDefaultImg()
    {
        return townDefaultImg;
    }
}
