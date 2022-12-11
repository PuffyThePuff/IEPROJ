using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPortraitHolder : MonoBehaviour
{

    [SerializeField] private GameObject red1;
    [SerializeField] private GameObject blue1;
    [SerializeField] private GameObject green1;

    [SerializeField] private GameObject red2;
    [SerializeField] private GameObject blue2;
    [SerializeField] private GameObject green2;

    [SerializeField] private Image enemySkillBar;
    [SerializeField] private Image enemySkillFill;

    public void ChangeRed(bool changeToNew)
    {
        if (changeToNew)
        {
            red1.SetActive(false);
            red2.SetActive(true);
        }
        else
        {
            red2.SetActive(false);
            red1.SetActive(true);
        }
    }

    public void ChangeBlue(bool changeToNew)
    {
        if (changeToNew)
        {
            blue1.SetActive(false);
            blue2.SetActive(true);
        }

        else
        {
            blue2.SetActive(false);
            blue1.SetActive(true);
        }
    }

    public void ChangeGreen(bool changeToNew)
    {
        if (changeToNew)
        {
            green1.SetActive(false);
            green2.SetActive(true);
        }

        else
        {
            green2.SetActive(false);
            green1.SetActive(true);
        }
    }

    public void ChangeSkillBarColor()
    {
        switch (Values.Enemy.enemyLevel)
        {
            case 1:
                enemySkillBar.gameObject.SetActive(true);
                enemySkillBar.color = new Color(0.4353f, 0.3216f, 0.3216f, 1.0f);
                enemySkillFill.color = new Color(0.7725f, 0.0784f, 0.1059f, 1.0f);
                break;
            case 2:
                enemySkillBar.gameObject.SetActive(false);
                break;
            case 3:
                enemySkillBar.gameObject.SetActive(true);
                enemySkillBar.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                enemySkillFill.color = new Color(0.5775f, 1.0f, 0.316f, 1.0f);
                break;
            default:
                enemySkillBar.gameObject.SetActive(false);
                break;
        }
    }
}