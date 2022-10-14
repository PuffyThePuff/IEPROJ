using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Values
{
    public struct Character
    {
        public int index;
        public string name;
        public float hp;
        public float charge;
        AffiliationType affiliation;
        public string catchPhrase;
        public enum AffiliationType
        {
            red = 0,
            pearl = 1,
            blue = 2,
            none = 3
        };

        


        public Character(int _index, string _name, float _hp, float _charge, AffiliationType _affiliation, string _catchPhrase)
        {
            index = _index;
            name = _name;
            hp = _hp;
            charge = _charge;
            affiliation = _affiliation;
            catchPhrase = _catchPhrase;
        }
    }

    
    public class Enemy
    {
        public enum SkillType
        {
            None = 0,
            Burst = 1,
            Heal = 2,
            Freeze = 3,
            Poison = 4
                
        };

        public static int enemyLevel = 1;
        public static float maxHP = 500;
        public static float dmg = 10f;
        public static float attackInterval = 1.5f;
        public static SkillType skill = SkillType.None;



    }

    public class Characters
    {
        public static Character c1 = new Character(3, "HorizontalPowerup", 1000, 100, Character.AffiliationType.red, "Take this!");  //damage
        public static Character c2 = new Character(4, "SurroundPowerup", 1000, 100, Character.AffiliationType.pearl, "By the power of ice!");    //stun
        public static Character c3 = new Character(5, "VerticalPowerup", 1000, 100, Character.AffiliationType.blue, "The gods favor us.");    //increase spawn rate
        public static Character c4 = new Character(6, "X Powerup", 1000, 100, Character.AffiliationType.blue, "");          //heal alive allies
    }

    public class Player
    {
        public static Character equippedChar1 = Characters.c1;
        public static Character equippedChar2 = Characters.c2;
        public static Character equippedChar3 = Characters.c3;
        public static int gold = 0;
        public static float basicDamage = 2;
        public static float enhancedDmaage = 15;
    }
    
    public class Puzzle
    {
        public static bool isTutorial = false;
        public static int hexBlockerCount = 3;
        public static float BlackHexDamage = 50.0f;
        public static float PainHexDamage = 10.0f;
    }
}
    

