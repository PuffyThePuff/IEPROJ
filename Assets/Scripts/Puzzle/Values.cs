using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Values
{
    public struct Character
    {
        public float hp;

        public Character(float _hp)
        {
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
        public static Character c1 = new Character(100);
        public static Character c2 = new Character(100);
        public static Character c3 = new Character(50);
    }

    public class Player
    {
        public static Character equippedChar1 = Characters.c1;
        public static Character equippedChar2 = Characters.c2;
        public static Character equippedChar3 = Characters.c3;
    }
}
    

