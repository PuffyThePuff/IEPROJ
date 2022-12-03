using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundManager : MonoBehaviour
{
    public GameObject EmptyBedroom;
    public GameObject CharacterBedroom;
    public GameObject GachaBackground;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == Values.SceneNames.BedroomScene)
            FindObjectOfType<StoryAnimations>().BackgrondChange();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
