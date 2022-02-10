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
        

        public Character(int _index, string _name, float _hp, float _charge)
        {
            index = _index;
            name = _name;
            hp = _hp;
            charge = _charge;
        }
    }

    
    public class Enemy
    {
        public enum SkillType
        {
            None = 0,
            Burst = 1,
            Heal = 2,
            Freeze = 3
                
        };

        public static float maxHP = 500;
        public static float dmg = 10f;
        public static SkillType skill = SkillType.None;



    }

    public class Characters
    {
        public static Character c1 = new Character(3, "HorizontalPowerup", 1000, 100);  //damage
        public static Character c2 = new Character(4, "SurroundPowerup", 1000, 100);    //stun
        public static Character c3 = new Character(5, "VerticalPowerup", 1000, 100);    //increase spawn rate
        public static Character c4 = new Character(6, "X Powerup", 1000, 100);          //heal alive allies
    }

    public class Player
    {
        public static Character equippedChar1 = Characters.c1;
        public static Character equippedChar2 = Characters.c1;
        public static Character equippedChar3 = Characters.c1;
        public static int gold = 0;
        public static float basicDamage = 10;
        public static float enhancedDmaage = 60;
    }

    public class Puzzle
    {
        public static bool isTutorial = false;
    }
}
    

