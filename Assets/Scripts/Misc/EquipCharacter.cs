
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EquipCharacter : MonoBehaviour
{
    public Text goldTxt = null;
    public void SelectChar1Dropdown(Dropdown dd) //equpping character 1 based on dropdown value
    {
        
        switch(dd.value)
        {
            case 0:
                Values.Player.equippedChar1 = Values.Characters.c1;
                break;

            case 1:
                Values.Player.equippedChar1 = Values.Characters.c2;
                break;

            case 2:
                Values.Player.equippedChar1 = Values.Characters.c3;
                break;

            case 3:
                Values.Player.equippedChar1 = Values.Characters.c4;
                break;
        }
        Debug.Log("equipped char 1: " + Values.Player.equippedChar1.name);
        


        //switch (dd2.value)
        //{
        //    case 0:
        //        Values.Player.equippedChar2 = Values.Characters.c1;
        //        break;

        //    case 1:
        //        Values.Player.equippedChar2 = Values.Characters.c2;
        //        break;

        //    case 2:
        //        Values.Player.equippedChar2 = Values.Characters.c3;
        //        break;

        //    case 3:
        //        Values.Player.equippedChar2 = Values.Characters.c4;
        //        break;
        //}
        //Debug.Log("equipped char 1: " + Values.Player.equippedChar1.name);

        //switch (dd3.value)
        //{
        //    case 0:
        //        Values.Player.equippedChar3 = Values.Characters.c1;
        //        break;

        //    case 1:
        //        Values.Player.equippedChar3 = Values.Characters.c2;
        //        break;

        //    case 2:
        //        Values.Player.equippedChar3 = Values.Characters.c3;
        //        break;

        //    case 3:
        //        Values.Player.equippedChar3 = Values.Characters.c4;
        //        break;
        //}

    }

    public void SelectChar2Dropdown(Dropdown dd) //equpping character 2 based on dropdown value
    {

        switch (dd.value)
        {
            case 0:
                Values.Player.equippedChar2 = Values.Characters.c1;
                break;

            case 1:
                Values.Player.equippedChar2 = Values.Characters.c2;
                break;

            case 2:
                Values.Player.equippedChar2 = Values.Characters.c3;
                break;

            case 3:
                Values.Player.equippedChar2 = Values.Characters.c4;
                break;
        }
        Debug.Log("equipped char 2: " + Values.Player.equippedChar2.name);
    }

    public void SelectChar3Dropdown(Dropdown dd) //equpping character 3 based on dropdown value
    {

        switch (dd.value)
        {
            case 0:
                Values.Player.equippedChar3 = Values.Characters.c1;
                break;

            case 1:
                Values.Player.equippedChar3 = Values.Characters.c2;
                break;

            case 2:
                Values.Player.equippedChar3 = Values.Characters.c3;
                break;

            case 3:
                Values.Player.equippedChar3 = Values.Characters.c4;
                break;
        }
        Debug.Log("equipped char 3: " + Values.Player.equippedChar3.name);
    }

    public void SelectTutorial()
    {
        Values.Enemy.maxHP = 100;
        Values.Enemy.dmg = 10;
        Values.Puzzle.isTutorial = true;
        SceneManager.LoadScene("PuzzleTest");

    }

    public void SelectLevel1()
    {
        Values.Enemy.maxHP = 100;
        Values.Enemy.dmg = 10;
        SceneManager.LoadScene("PuzzleTest");

    }

    public void SelectLevel2()
    {
        Values.Enemy.maxHP = 300;
        Values.Enemy.dmg = 30;
        SceneManager.LoadScene("PuzzleTest");


    }

    public void SelectLevel3()
    {
        Values.Enemy.maxHP = 900;
        Values.Enemy.dmg = 90;
        SceneManager.LoadScene("PuzzleTest");


    }

    public void SelectLevel4()
    {
        Values.Enemy.maxHP = 2700;
        Values.Enemy.dmg = 270;
        SceneManager.LoadScene("PuzzleTest");


    }

    public void SelectLevel5()
    {
        Values.Enemy.maxHP = 8100;
        Values.Enemy.dmg = 810;
        SceneManager.LoadScene("PuzzleTest");


    }

    private void Start()
    {
        if(goldTxt != null)
        {
            goldTxt.text = Values.Player.gold.ToString();
        }
    }

    private void Update()
    {
        if (goldTxt != null)
        {
            goldTxt.text = Values.Player.gold.ToString();
        }
    }
}
