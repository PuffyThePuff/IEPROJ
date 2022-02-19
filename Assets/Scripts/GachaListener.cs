using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

//simple listener for gacha events
public class GachaListener : MonoBehaviour
{
    [SerializeField]private Sprite char1;

    private VideoPlayer videoPlayer;
    [SerializeField]private GameObject image;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onGachaSuccess += PlayVid;
        videoPlayer = this.GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += ShowSprite;
    }

    private void PlayVid(int rarity)
    {
        videoPlayer.Play();
        Debug.Log("done");
    }

    private void ShowSprite(UnityEngine.Video.VideoPlayer vp)
    {
        image.GetComponent<Image>().sprite = char1;
        image.SetActive(true);
    }
}
