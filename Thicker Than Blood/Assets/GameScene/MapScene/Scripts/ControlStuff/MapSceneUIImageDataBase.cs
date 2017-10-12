using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSceneUIImageDataBase : MonoBehaviour {

    public Texture2D cityDefaultImg, cityGarrisonImg, cityThreatenImg, cityMarketImg, cityHallImg, cityArmoryImg,
        cityTavernImg, cityBrothelImg, cityChurchImg;
    public static MapSceneUIImageDataBase dataBase;

    // Use this for initialization
    void Awake () {
        dataBase = new MapSceneUIImageDataBase();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
