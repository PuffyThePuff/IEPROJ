
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EquipCharacter : MonoBehaviour
{
    public Text goldTxt = null;
    public Dropdown[] dropdowns;
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
        Values.Enemy.enemyLevel = 0;
        Values.Enemy.maxHP = 100;
        Values.Enemy.dmg = 10;
        Values.Puzzle.isTutorial = true;
        SceneManager.LoadScene("Puzzle");

    }

    public void SelectLevel1()
    {
        if(dropdowns[0].value != dropdowns[1].value && dropdowns[0].value != dropdowns[2].value && dropdowns[1].value != dropdowns[2].value)
        {
            

        }

        else
        {
            Debug.Log("Cannot use a piece more than once");
        }

        Values.Enemy.enemyLevel = 1;
        Values.Enemy.maxHP = 100;
        Values.Enemy.dmg = 100;
        Values.Enemy.attackInterval = 1.6f;
        Values.Puzzle.isTutorial = false;
        SceneManager.LoadScene("Puzzle");
        AudioManager.Instance.Play("BattleBGM");
    }

    public void SelectLevel2()
    {
        if (dropdowns[0].value != dropdowns[1].value && dropdowns[0].value != dropdowns[2].value && dropdowns[1].value != dropdowns[2].value)
        {
            

        }

        else
        {
            Debug.Log("Cannot use a piece more than once");
        }

        Values.Enemy.enemyLevel = 2;
        Values.Enemy.maxHP = 200;
        Values.Enemy.dmg = 100;
        Values.Enemy.attackInterval = 1.4f;
        Values.Puzzle.isTutorial = false;
        Values.Enemy.skill = Values.Enemy.SkillType.Freeze;
        SceneManager.LoadScene("Puzzle");
        AudioManager.Instance.Play("BattleBGM");
    }

    public void SelectLevel3()
    {
        if (dropdowns[0].value != dropdowns[1].value && dropdowns[0].value != dropdowns[2].value && dropdowns[1].value != dropdowns[2].value)
        {
            

        }

        else
        {
            Debug.Log("Cannot use a piece more than once");
        }

        Values.Enemy.enemyLevel = 3;
        Values.Enemy.maxHP = 400;
        Values.Enemy.dmg = 100;
        Values.Enemy.attackInterval = 1.2f;
        Values.Puzzle.isTutorial = false;
        SceneManager.LoadScene("Puzzle");
        AudioManager.Instance.Play("BattleBGM");
    }

    public void SelectLevel4()
    {
        if (dropdowns[0].value != dropdowns[1].value && dropdowns[0].value != dropdowns[2].value && dropdowns[1].value != dropdowns[2].value)
        {
           
        }

        else
        {
            Debug.Log("Cannot use a piece more than once");
        }

        Values.Enemy.enemyLevel = 4;
        Values.Enemy.maxHP = 2700;
        Values.Enemy.dmg = 100;
        Values.Enemy.attackInterval = 1.0f;
        Values.Puzzle.isTutorial = false;
        SceneManager.LoadScene("Puzzle");
        AudioManager.Instance.Play("BattleBGM");
    }

    public void SelectLevel5()
    {
        if (dropdowns[0].value != dropdowns[1].value && dropdowns[0].value != dropdowns[2].value && dropdowns[1].value != dropdowns[2].value)
        {
            

        }


        else
        {
            Debug.Log("Cannot use a piece more than once");
        }
        Values.Enemy.enemyLevel = 4;
        Values.Enemy.maxHP = 8100;
        Values.Enemy.dmg = 100;
        Values.Enemy.attackInterval = 1.1f;
        Values.Puzzle.isTutorial = false;
        SceneManager.LoadScene("Puzzle");
        AudioManager.Instance.Play("BattleBGM");
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
