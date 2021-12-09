using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//simple listener for gacha events
public class GachaListener : MonoBehaviour
{
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onGachaSuccess += ChangeText;
        text = this.GetComponent<Text>();
    }

    private void ChangeText(int rarity)
    {
        text.text = "Rarity: " + rarity;
    }
}
