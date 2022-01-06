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
        

        public Character(int _index, string _name, float _hp)
        {
            index = _index;
            name = _name;
            hp = _hp;
        }
    }
    public class Enemy
    {
        public static float maxHP = 500;
        public static float dmg = 10f;
    }

    public class Characters
    {
        public static Character c1 = new Character(3, "HorizontalPowerup", 1000);
        public static Character c2 = new Character(4, "SurroundPowerup", 1000);
        public static Character c3 = new Character(5, "VerticalPowerup", 1000);
        public static Character c4 = new Character(6, "X Powerup", 1000);
    }

    public class Player
    {
        public static Character equippedChar1 = Characters.c1;
        public static Character equippedChar2 = Characters.c1;
        public static Character equippedChar3 = Characters.c1;
        public static int gold = 0;
    }

    public class Puzzle
    {
        public static bool isTutorial = false;
    }
}
    

