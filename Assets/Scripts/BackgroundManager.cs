using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    public GameObject EmptyBedroom;
    public GameObject CharacterBedroom;
    public GameObject GachaBackground;
    public GameObject InGameBackground;
    public GameObject CaveBackground;
    public GameObject BedYuukiBackground;

    public GameObject NagiSpawn;
    public GameObject ChessSpawn;
    public GameObject SakuraSpawn;

    public Text FinalText;

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
