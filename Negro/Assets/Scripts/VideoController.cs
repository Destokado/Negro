using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
[RequireComponent(typeof(RawImage))]
public class VideoController : MonoBehaviour
{
    [SerializeField] private VideoClip[] healthVideos;
    [SerializeField] private VideoClip[] sanityVideos;
    [SerializeField] private VideoClip[] socialStatusVideos;
    private VideoPlayer videoPlayer;
    private RawImage rawImage;

    private void Start()
    {
        rawImage = GetComponent<RawImage>();
        videoPlayer = GetComponent<VideoPlayer>();
    }

    public float ShowVideoFor(Statistic statistic)
    {
        int videoNumber = -1;
        switch (statistic.type)
        {
            case Statistic.Type.Health:
                videoNumber = Mathf.RoundToInt(Mathf.Lerp(0, healthVideos.Length-1, statistic.value/100f));
                Debug.Log("Showing video number " + videoNumber + "/"+ (healthVideos.Length-1) + " for stat " + Enum.GetName(typeof(Statistic.Type), statistic.type));
                return PlayVideo(healthVideos[videoNumber]);
            case Statistic.Type.Sanity:
                videoNumber = Mathf.RoundToInt(Mathf.Lerp(0, sanityVideos.Length-1, statistic.value/100f));
                Debug.Log("Showing video number " + videoNumber + "/"+ (sanityVideos.Length-1) + " for stat " + Enum.GetName(typeof(Statistic.Type), statistic.type));
                return PlayVideo(sanityVideos[videoNumber]);
            case Statistic.Type.SocialStatus:
                videoNumber = Mathf.RoundToInt(Mathf.Lerp(0, socialStatusVideos.Length-1, statistic.value/100f));
                Debug.Log("Showing video number " + videoNumber + "/"+ (socialStatusVideos.Length-1) + " for stat " + Enum.GetName(typeof(Statistic.Type), statistic.type));
                return PlayVideo(socialStatusVideos[videoNumber]);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private float PlayVideo(VideoClip videoClip)
    {
        rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, 1f);
        videoPlayer.clip = videoClip;
        videoPlayer.Play();
        Invoke(nameof(HideRawImage), Convert.ToSingle(videoClip.length));
        return Convert.ToSingle(videoClip.length);
    }

    private void HideRawImage()
    {
        rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, 0f);
        CancelInvoke(nameof(HideRawImage));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            HideRawImage();
    }
}
