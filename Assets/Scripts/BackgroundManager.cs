using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public GameObject EmptyBedroom;
    public GameObject CharacterBedroom;

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
        }
        else
        {
            EmptyBedroom.SetActive(true);
            CharacterBedroom.SetActive(false);
        }
    }
}
