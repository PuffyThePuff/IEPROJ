using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public GameObject EmptyBedroom;
    public GameObject CharacterBedroom;
    public GameObject GachaBackground;

    // Start is called before the first frame update
    void Start()
    {
        EmptyBedroom.SetActive(false);
        CharacterBedroom.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<StoryManager>().currentChapter == 0 && FindObjectOfType<StoryManager>().currentDialogue == 0)
        {
            EmptyBedroom.SetActive(false);
            CharacterBedroom.SetActive(true);
            GachaBackground.SetActive(false);
        }
        else if (FindObjectOfType<StoryManager>().currentChapter == 0 && FindObjectOfType<StoryManager>().currentDialogue == 5)
        {
            GachaBackground.SetActive(true);
            EmptyBedroom.SetActive(false);
            CharacterBedroom.SetActive(false);
        }
        else
        {
            EmptyBedroom.SetActive(true);
            CharacterBedroom.SetActive(false);
            GachaBackground.SetActive(false);
        }
    }
}
