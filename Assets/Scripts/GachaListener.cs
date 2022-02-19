using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

//simple listener for gacha events
public class GachaListener : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onGachaSuccess += PlayVid;
        videoPlayer = this.GetComponent<VideoPlayer>();
    }

    private void PlayVid(int rarity)
    {
        videoPlayer.Play();
        Debug.Log("boop");
    }
}
