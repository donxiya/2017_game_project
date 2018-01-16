using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCharacter : Person {

    public SkillTree skillTree;
    public MainCharacter()
    {

    }
    public MainCharacter(string nameI, Stats statsI, Ranking rk, TroopType tt, Faction factionI, Experience expI)
    {
        initialization(nameI, statsI, rk, tt, factionI, expI);
        skillTree = new SkillTree();
    }
    public override void initialization(string nameI, Stats statsI, Ranking rk, TroopType tt, Faction factionI, Experience expI)
    {
        base.initialization(nameI, statsI, rk, tt, factionI, expI);
    }
    public override void resetPerk()
    {
        base.resetPerk();
        skillTree.skillTreeInitialization();
        exp.sparedPoint += 50; //REMEMBER TO CHANGE THIS TO LEVEL
    }
    public override GearInfo getGearInfo()
    {
        
        GearInfo result = new GearInfo(1, 1, 1, 1, 1, 1, 1, 1, 1);
        if (skillTree != null)
        {
            if (troopType == TroopType.mainCharType)
            {
                if (skillTree.getPerk("M1_HELMET1").own)
                {
                    result.visionRating += 2;
                    result.armorRating += 2;
                    if (skillTree.getPerk("M1_HELMET2").own)
                    {
                        result.visionRating += 2;
                        result.armorRating += 2;
                        if (skillTree.getPerk("M1_HELMET3").own)
                        {
                            result.visionRating += 2;
                            result.armorRating += 2;
                            if (skillTree.getPerk("M1_HELMET4A").own)
                            {
                                result.visionRating += 3;
                            }
                            else if (skillTree.getPerk("M1_HELMET4B").own)
                            {
                                result.armorRating += 3;
                            }
                        }
                    }
                }
                if (skillTree.getPerk("M1_ARMOR1").own)
                {
                    result.armorRating += 2;
                    result.blockRating += 2;
                    result.evasionRating += 2;
                    if (skillTree.getPerk("M1_ARMOR2").own)
                    {
                        result.armorRating += 2;
                        result.blockRating += 2;
                        result.evasionRating += 2;
                        if (skillTree.getPerk("M1_ARMOR3").own)
                        {
                            result.armorRating += 2;
                            result.blockRating += 2;
                            result.evasionRating += 2;
                            if (skillTree.getPerk("M1_ARMOR4A").own)
                            {
                                result.armorRating += 3;
                            }
                            else if (skillTree.getPerk("M1_ARMOR4B").own)
                            {
                                result.blockRating += 3;
                            }
                            else if (skillTree.getPerk("M1_ARMOR4C").own)
                            {
                                result.evasionRating += 3;
                            }
                        }
                    }
                }
                if (skillTree.getPerk("M1_CLOTHES1").own)
                {
                    result.stealthRating += 2;
                    result.mobilityRating += 2;
                    result.evasionRating += 2;
                    result.visionRating += 2;
                    if (skillTree.getPerk("M1_CLOTHES2").own)
                    {
                        result.stealthRating += 2;
                        result.mobilityRating += 2;
                        result.evasionRating += 2;
                        result.visionRating += 2;
                        if (skillTree.getPerk("M1_CLOTHES3").own)
                        {
                            result.stealthRating += 2;
                            result.mobilityRating += 2;
                            result.evasionRating += 2;
                            result.visionRating += 2;
                            if (skillTree.getPerk("M1_CLOTHES4A").own)
                            {
                                result.stealthRating += 3;
                            }
                            else if (skillTree.getPerk("M1_CLOTHES4B").own)
                            {
                                result.mobilityRating += 3;
                            }
                            else if (skillTree.getPerk("M1_CLOTHES4C").own)
                            {
                                result.evasionRating += 3;
                            }
                            else if (skillTree.getPerk("M1_CLOTHES4D").own)
                            {
                                result.visionRating += 3;
                            }
                        }
                    }
                }
                if (skillTree.getPerk("M1_SWORD1").own)
                {
                    result.meleeDmgRating += 4;
                    result.blockRating += 2;
                    if (skillTree.getPerk("M1_SWORD2").own)
                    {
                        result.meleeDmgRating += 4;
                        result.blockRating += 2;
                        if (skillTree.getPerk("M1_SWORD3").own)
                        {
                            result.meleeDmgRating += 4;
                            result.blockRating += 2;
                            if (skillTree.getPerk("M1_SWORD4A").own)
                            {
                                result.meleeDmgRating += 7;
                            }
                            else if (skillTree.getPerk("M1_SWORD4B").own)
                            {
                                result.blockRating += 3;
                            }
                            else if (skillTree.getPerk("M1_SWORD4C").own)
                            {
                                //reduce stamina cost
                            }
                        }
                    }
                }
                if (skillTree.getPerk("M1_PISTOL1").own)
                {
                    result.rangedDmgRating += 4;
                    result.accuracyRating += 4;
                    if (skillTree.getPerk("M1_SWORD2").own)
                    {
                        result.rangedDmgRating += 4;
                        result.accuracyRating += 4;
                        if (skillTree.getPerk("M1_SWORD3").own)
                        {
                            result.rangedDmgRating += 4;
                            result.accuracyRating += 4;
                            if (skillTree.getPerk("M1_SWORD4A").own)
                            {
                                result.rangedDmgRating += 7;
                            }
                            else if (skillTree.getPerk("M1_SWORD4B").own)
                            {
                                result.accuracyRating += 7;
                            }
                        }
                    }
                }
                if (skillTree.getPerk("M1_BOOTS1").own)
                {
                    result.mobilityRating += 2;
                    result.stealthRating += 2;
                    if (skillTree.getPerk("M1_BOOTS2").own)
                    {
                        result.mobilityRating += 2;
                        result.stealthRating += 2;
                        if (skillTree.getPerk("M1_SWORD3").own)
                        {
                            result.mobilityRating += 2;
                            result.stealthRating += 2;
                            if (skillTree.getPerk("M1_SWORD4A").own)
                            {
                                result.mobilityRating += 3;
                            }
                            else if (skillTree.getPerk("M1_SWORD4B").own)
                            {
                                result.stealthRating += 3;
                            }
                        }
                    }
                }
            }
            if (troopType == TroopType.crossbowman)
            {

            }
        }
        
        return result;
    }
}

public class SkillTree
{
    public Dictionary<string, Perk> skillTreeDict;
    public SkillTree()
    {
        skillTreeInitialization();
    }

    public void skillTreeInitialization()
    {
        skillTreeDict = new Dictionary<string, Perk>();
        skillTreeDict.Add("S6A", new Perk("S6A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("S6B", new Perk("S6B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("S7A", new Perk("S7A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("S7B", new Perk("S7B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("S8A", new Perk("S8A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("S8B", new Perk("S8B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("S9A", new Perk("S9A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("S9B", new Perk("S9B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("S10A", new Perk("S10A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        
        skillTreeDict.Add("A6A", new Perk("A6A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("A6B", new Perk("A6B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("A7A", new Perk("A7A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("A7B", new Perk("A7B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("A8A", new Perk("A8A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("A8B", new Perk("A8B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("A9A", new Perk("A9A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("A9B", new Perk("A9B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("A10A", new Perk("A10A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));

        skillTreeDict.Add("P6A", new Perk("P6A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("P6B", new Perk("P6B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("P7A", new Perk("P7A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("P7B", new Perk("P7B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("P8A", new Perk("P8A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("P8B", new Perk("P8B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("P9A", new Perk("P9A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("P9B", new Perk("P9B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("P10A", new Perk("P10A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));

        skillTreeDict.Add("E6A", new Perk("E6A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("E6B", new Perk("E6B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("E7A", new Perk("E7A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("E7B", new Perk("E7B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("E8A", new Perk("E8A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("E8B", new Perk("E8B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("E9A", new Perk("E9A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("E9B", new Perk("E9B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("E10A", new Perk("E10A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));

        skillTreeDict.Add("C6A", new Perk("C6A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C6B", new Perk("C6B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C6C", new Perk("C6C", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C6D", new Perk("C6D", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C7A", new Perk("C7A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C7B", new Perk("C7B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C7C", new Perk("C7C", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C7D", new Perk("C7D", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C8A", new Perk("C8A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C8B", new Perk("C8B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C8C", new Perk("C8C", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C8D", new Perk("C8D", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C9A", new Perk("C9A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C9B", new Perk("C9B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C9C", new Perk("C9C", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C9D", new Perk("C9D", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C10A", new Perk("C10A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("C10B", new Perk("C10B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));

        skillTreeDict.Add("I6A", new Perk("I6A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I6B", new Perk("I6B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I6C", new Perk("I6C", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I6D", new Perk("I6D", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I7A", new Perk("I7A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I7B", new Perk("I7B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I7C", new Perk("I7C", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I7D", new Perk("I7D", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I8A", new Perk("I8A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I8B", new Perk("I8B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I8C", new Perk("I8C", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I8D", new Perk("I8D", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I9A", new Perk("I9A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I9B", new Perk("I9B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I9C", new Perk("I9C", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I9D", new Perk("I9D", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I10A", new Perk("I10A", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));
        skillTreeDict.Add("I10B", new Perk("I10B", false, "Merciful", "Execution cost less stamina based on S", "nothing right now"));

        skillTreeDict.Add("M1_HELMET1", new Perk("M1_HELMET1", false, "Helmet Upgrade I", "Increase vision and armor", "helmet is awesome man!"));
        skillTreeDict.Add("M1_HELMET2", new Perk("M1_HELMET2", false, "Helmet Upgrade II", "Increase vision and armor", "helmet is awesome man!"));
        skillTreeDict.Add("M1_HELMET3", new Perk("M1_HELMET3", false, "Helmet Upgrade III", "Increase vision and armor", "helmet is awesome man!"));
        skillTreeDict.Add("M1_HELMET4A", new Perk("M1_HELMET4A", false, "Helmet Upgrade IV Armor", "Increase armor", "helmet can improve ur armor yeah!"));
        skillTreeDict.Add("M1_HELMET4B", new Perk("M1_HELMET4B", false, "Helmet Upgrade IV Vision", "Increase vision", "helmet can improve ur vision yeah!"));

        skillTreeDict.Add("M1_ARMOR1", new Perk("M1_ARMOR1", false, "Armor Upgrade I", "Increase armor, block, evasion", "armor is awesome man!"));
        skillTreeDict.Add("M1_ARMOR2", new Perk("M1_ARMOR2", false, "Armor Upgrade II", "Increase armor, block, evasion", "armor is awesome man!"));
        skillTreeDict.Add("M1_ARMOR3", new Perk("M1_ARMOR3", false, "Armor Upgrade III", "Increase armor, block, evasion", "armor is awesome man!"));
        skillTreeDict.Add("M1_ARMOR4A", new Perk("M1_ARMOR4A", false, "Armor Upgrade IV Armor", "Increase armor", "armor is awesome man!"));
        skillTreeDict.Add("M1_ARMOR4B", new Perk("M1_ARMOR4B", false, "Armor Upgrade IV Block", "Increase block", "armor is awesome man!"));
        skillTreeDict.Add("M1_ARMOR4C", new Perk("M1_ARMOR4C", false, "Armor Upgrade IV Evasion", "Increase vision", "armor is awesome man!"));

        skillTreeDict.Add("M1_CLOTHES1", new Perk("M1_CLOTHES1", false, "Clothes Upgrade I", "Increase stealth, block, evasion, vision", "clothes is awesome man!"));
        skillTreeDict.Add("M1_CLOTHES2", new Perk("M1_CLOTHES2", false, "Clothes Upgrade II", "Increase stealth, block, evasion, vision", "clothes is awesome man!"));
        skillTreeDict.Add("M1_CLOTHES3", new Perk("M1_CLOTHES3", false, "Clothes Upgrade III", "Increase stealth, block, evasion, vision", "clothes is awesome man!"));
        skillTreeDict.Add("M1_CLOTHES4A", new Perk("M1_CLOTHES4A", false, "Clothes Upgrade IV Clothes", "Increase armor", "clothes is awesome man!"));
        skillTreeDict.Add("M1_CLOTHES4B", new Perk("M1_CLOTHES4B", false, "Clothes Upgrade IV Block", "Increase block", "clothes is awesome man!"));
        skillTreeDict.Add("M1_CLOTHES4C", new Perk("M1_CLOTHES4C", false, "Clothes Upgrade IV Evasion", "Increase evasion", "clothes is awesome man!"));
        skillTreeDict.Add("M1_CLOTHES4D", new Perk("M1_CLOTHES4D", false, "Clothes Upgrade IV Vision", "Increase vision", "clothes is awesome man!"));

        skillTreeDict.Add("M1_SWORD1", new Perk("M1_SWORD1", false, "Sword Upgrade I", "Increase damage, block, decrease stamina cost", "helmet is awesome man!"));
        skillTreeDict.Add("M1_SWORD2", new Perk("M1_SWORD2", false, "Sword Upgrade II", "Increase damage, block, decrease stamina cost", "helmet is awesome man!"));
        skillTreeDict.Add("M1_SWORD3", new Perk("M1_SWORD3", false, "Sword Upgrade III", "Increase damage, block, decrease stamina cost", "helmet is awesome man!"));
        skillTreeDict.Add("M1_SWORD4A", new Perk("M1_SWORD4A", false, "Sword Upgrade IV Damage", "Increase damage", "helmet can improve ur armor yeah!"));
        skillTreeDict.Add("M1_SWORD4B", new Perk("M1_SWORD4B", false, "Sword Upgrade IV Block", "Increase block", "helmet can improve ur vision yeah!"));
        skillTreeDict.Add("M1_SWORD4C", new Perk("M1_SWORD4C", false, "Sword Upgrade IV Stamina Cost", "Decrease stamina cost for lunge, whirlwind, execute", "helmet can improve ur vision yeah!"));

        skillTreeDict.Add("M1_PISTOL1", new Perk("M1_PISTOL1", false, "Pistol Upgrade I", "Increase damage, accuracy, decrease stamina cost", "helmet is awesome man!"));
        skillTreeDict.Add("M1_PISTOL2", new Perk("M1_PISTOL2", false, "Pistol Upgrade II", "Increase damage, accuracy, decrease stamina cost", "helmet is awesome man!"));
        skillTreeDict.Add("M1_PISTOL3", new Perk("M1_PISTOL3", false, "Pistol Upgrade III", "Increase damage, accuracy, decrease stamina cost", "helmet is awesome man!"));
        skillTreeDict.Add("M1_PISTOL4A", new Perk("M1_PISTOL4A", false, "Pistol Upgrade IV Damage", "Increase damage", "helmet can improve ur armor yeah!"));
        skillTreeDict.Add("M1_PISTOL4B", new Perk("M1_PISTOL4B", false, "Pistol Upgrade IV Accuracy", "Increase accuracy", "helmet can improve ur vision yeah!"));
        skillTreeDict.Add("M1_PISTOL4C", new Perk("M1_PISTOL4C", false, "Pistol Upgrade IV Stamina Cost", "Decrease stamina cost for fire and hold steady", "helmet can improve ur vision yeah!"));

        skillTreeDict.Add("M1_BOOTS1", new Perk("M1_BOOTS1", false, "Boots Upgrade I", "Increase mobility and stealth", "helmet is awesome man!"));
        skillTreeDict.Add("M1_BOOTS2", new Perk("M1_BOOTS2", false, "Boots Upgrade II", "Increase mobility and stealth", "helmet is awesome man!"));
        skillTreeDict.Add("M1_BOOTS3", new Perk("M1_BOOTS3", false, "Boots Upgrade III", "Increase mobility and stealth", "helmet is awesome man!"));
        skillTreeDict.Add("M1_BOOTS4A", new Perk("M1_BOOTS4A", false, "Boots Upgrade IV mobility", "Increase mobility", "helmet can improve ur armor yeah!"));
        skillTreeDict.Add("M1_BOOTS4B", new Perk("M1_BOOTS4B", false, "Boots Upgrade IV stealth", "Increase stealth", "helmet can improve ur vision yeah!"));


        skillTreeDict.Add("M2_HELMET1", new Perk("M2_HELMET1", false, "Helmet Upgrade I", "Increase vision and armor", "helmet is awesome man!"));
        skillTreeDict.Add("M2_HELMET2", new Perk("M2_HELMET2", false, "Helmet Upgrade II", "Increase vision and armor", "helmet is awesome man!"));
        skillTreeDict.Add("M2_HELMET3", new Perk("M2_HELMET3", false, "Helmet Upgrade III", "Increase vision and armor", "helmet is awesome man!"));
        skillTreeDict.Add("M2_HELMET4A", new Perk("M2_HELMET4A", false, "Helmet Upgrade IV Armor", "Increase armor", "helmet can improve ur armor yeah!"));
        skillTreeDict.Add("M2_HELMET4B", new Perk("M2_HELMET4B", false, "Helmet Upgrade IV Vision", "Increase vision", "helmet can improve ur vision yeah!"));

        skillTreeDict.Add("M2_ARMOR1", new Perk("M2_ARMOR1", false, "Armor Upgrade I", "Increase armor, block, evasion", "armor is awesome man!"));
        skillTreeDict.Add("M2_ARMOR2", new Perk("M2_ARMOR2", false, "Armor Upgrade II", "Increase armor, block, evasion", "armor is awesome man!"));
        skillTreeDict.Add("M2_ARMOR3", new Perk("M2_ARMOR3", false, "Armor Upgrade III", "Increase armor, block, evasion", "armor is awesome man!"));
        skillTreeDict.Add("M2_ARMOR4A", new Perk("M2_ARMOR4A", false, "Armor Upgrade IV Armor", "Increase armor", "armor is awesome man!"));
        skillTreeDict.Add("M2_ARMOR4B", new Perk("M2_ARMOR4B", false, "Armor Upgrade IV Block", "Increase block", "armor is awesome man!"));
        skillTreeDict.Add("M2_ARMOR4C", new Perk("M2_ARMOR4C", false, "Armor Upgrade IV Evasion", "Increase vision", "armor is awesome man!"));

        skillTreeDict.Add("M2_CLOTHES1", new Perk("M2_CLOTHES1", false, "Clothes Upgrade I", "Increase stealth, block, evasion, vision", "clothes is awesome man!"));
        skillTreeDict.Add("M2_CLOTHES2", new Perk("M2_CLOTHES2", false, "Clothes Upgrade II", "Increase stealth, block, evasion, vision", "clothes is awesome man!"));
        skillTreeDict.Add("M2_CLOTHES3", new Perk("M2_CLOTHES3", false, "Clothes Upgrade III", "Increase stealth, block, evasion, vision", "clothes is awesome man!"));
        skillTreeDict.Add("M2_CLOTHES4A", new Perk("M2_CLOTHES4A", false, "Clothes Upgrade IV Clothes", "Increase armor", "clothes is awesome man!"));
        skillTreeDict.Add("M2_CLOTHES4B", new Perk("M2_CLOTHES4B", false, "Clothes Upgrade IV Block", "Increase block", "clothes is awesome man!"));
        skillTreeDict.Add("M2_CLOTHES4C", new Perk("M2_CLOTHES4C", false, "Clothes Upgrade IV Evasion", "Increase evasion", "clothes is awesome man!"));
        skillTreeDict.Add("M2_CLOTHES4D", new Perk("M2_CLOTHES4D", false, "Clothes Upgrade IV Vision", "Increase vision", "clothes is awesome man!"));

        skillTreeDict.Add("M2_DAGGER1", new Perk("M2_DAGGER1", false, "Dagger Upgrade I", "Increase damage, block, decrease stamina cost", "helmet is awesome man!"));
        skillTreeDict.Add("M2_DAGGER2", new Perk("M2_DAGGER2", false, "Dagger Upgrade II", "Increase damage, block, decrease stamina cost", "helmet is awesome man!"));
        skillTreeDict.Add("M2_DAGGER3", new Perk("M2_DAGGER3", false, "Dagger Upgrade III", "Increase damage, block, decrease stamina cost", "helmet is awesome man!"));
        skillTreeDict.Add("M2_DAGGER4A", new Perk("M2_DAGGER4A", false, "Dagger Upgrade IV Damage", "Increase damage", "helmet can improve ur armor yeah!"));
        skillTreeDict.Add("M2_DAGGER4B", new Perk("M2_DAGGER4B", false, "Dagger Upgrade IV Block", "Increase block", "helmet can improve ur vision yeah!"));
        skillTreeDict.Add("M2_DAGGER4C", new Perk("M2_DAGGER4C", false, "Dagger Upgrade IV Stamina Cost", "Decrease stamina cost for lunge, whirlwind, execute", "helmet can improve ur vision yeah!"));

        skillTreeDict.Add("M2_CROSSBOW1", new Perk("M2_CROSSBOW1", false, "Crossbow Upgrade I", "Increase damage, accuracy, decrease stamina cost", "helmet is awesome man!"));
        skillTreeDict.Add("M2_CROSSBOW2", new Perk("M2_CROSSBOW2", false, "Crossbow Upgrade II", "Increase damage, accuracy, decrease stamina cost", "helmet is awesome man!"));
        skillTreeDict.Add("M2_CROSSBOW3", new Perk("M2_CROSSBOW3", false, "Crossbow Upgrade III", "Increase damage, accuracy, decrease stamina cost", "helmet is awesome man!"));
        skillTreeDict.Add("M2_CROSSBOW4A", new Perk("M2_CROSSBOW4A", false, "Crossbow Upgrade IV Damage", "Increase damage", "helmet can improve ur armor yeah!"));
        skillTreeDict.Add("M2_CROSSBOW4B", new Perk("M2_CROSSBOW4B", false, "Crossbow Upgrade IV Accuracy", "Increase accuracy", "helmet can improve ur vision yeah!"));
        skillTreeDict.Add("M2_CROSSBOW4C", new Perk("M2_CROSSBOW4C", false, "Crossbow Upgrade IV Stamina Cost", "Decrease stamina cost for fire and hold steady", "helmet can improve ur vision yeah!"));

        skillTreeDict.Add("M2_BOOTS1", new Perk("M2_BOOTS1", false, "Boots Upgrade I", "Increase mobility and stealth", "helmet is awesome man!"));
        skillTreeDict.Add("M2_BOOTS2", new Perk("M2_BOOTS2", false, "Boots Upgrade II", "Increase mobility and stealth", "helmet is awesome man!"));
        skillTreeDict.Add("M2_BOOTS3", new Perk("M2_BOOTS3", false, "Boots Upgrade III", "Increase mobility and stealth", "helmet is awesome man!"));
        skillTreeDict.Add("M2_BOOTS4A", new Perk("M2_BOOTS4A", false, "Boots Upgrade IV mobility", "Increase mobility", "helmet can improve ur armor yeah!"));
        skillTreeDict.Add("M2_BOOTS4B", new Perk("M2_BOOTS4B", false, "Boots Upgrade IV stealth", "Increase stealth", "helmet can improve ur vision yeah!"));
    }
    public Perk getPerk(string ID)
    {
        if (skillTreeDict.ContainsKey(ID))
        {
            return skillTreeDict[ID];
        }
        return null;
    }
}

public class Perk
{
    public string skillName { get; set;}
    public string description { get; set; }
    public string quote { get; set; }
    public string skillPointID { get; set; }
    public bool own { get; set; }
    public Button button { get; set; }
    public Perk(string skillPointIDI, bool ownI, string skillNameI, string descriptionI, string quoteI)
    {
        skillPointID = skillPointIDI;
        own = ownI;
        skillName = skillNameI;
        description = descriptionI;
        quote = quoteI;
    }
}
