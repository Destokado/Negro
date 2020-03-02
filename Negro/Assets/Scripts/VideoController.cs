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
    [SerializeField] private VideoClip[] healthVideosHighSocialStatus;
    [SerializeField] private VideoClip[] healthVideosLowSocialStatus;
    [SerializeField] private VideoClip[] sanityVideosHighSocialStatus;
    [SerializeField] private VideoClip[] sanityVideosLowSocialStatus;
    [SerializeField] private VideoClip[] socialStatusVideos;
    private VideoPlayer videoPlayer;
    private RawImage rawImage;

    private void Start()
    {
        rawImage = GetComponent<RawImage>();
        videoPlayer = GetComponent<VideoPlayer>();
    }

    public float ShowVideoFor(Statistic statistic, bool highSocialStatus)
    {
        int videoNumber = -1;

        if (statistic.type == Statistic.Type.SocialStatus)
        {
            videoNumber = Mathf.RoundToInt(Mathf.Lerp(0, socialStatusVideos.Length-1, statistic.value/100f));
            Debug.Log("Showing video number " + videoNumber + "/"+ (socialStatusVideos.Length-1) + " for stat " + Enum.GetName(typeof(Statistic.Type), statistic.type));
            return PlayVideo(socialStatusVideos[videoNumber]);
        }

        if (!highSocialStatus)
        {
            switch (statistic.type)
            {
                case Statistic.Type.Health:
                    videoNumber = Mathf.RoundToInt(Mathf.Lerp(0, healthVideosLowSocialStatus.Length-1, statistic.value/100f));
                    Debug.Log("Showing video number for low social status " + videoNumber + "/"+ (healthVideosLowSocialStatus.Length-1) + " for stat " + Enum.GetName(typeof(Statistic.Type), statistic.type));
                    return PlayVideo(healthVideosLowSocialStatus[videoNumber]);
                case Statistic.Type.Sanity:
                    videoNumber = Mathf.RoundToInt(Mathf.Lerp(0, sanityVideosLowSocialStatus.Length-1, statistic.value/100f));
                    Debug.Log("Showing video number for low social status " + videoNumber + "/"+ (sanityVideosLowSocialStatus.Length-1) + " for stat " + Enum.GetName(typeof(Statistic.Type), statistic.type));
                    return PlayVideo(sanityVideosLowSocialStatus[videoNumber]);
            }
        }
        else
        {
            switch (statistic.type)
            {
                case Statistic.Type.Health:
                    videoNumber = Mathf.RoundToInt(Mathf.Lerp(0, healthVideosHighSocialStatus.Length-1, statistic.value/100f));
                    Debug.Log("Showing video number for high social status " + videoNumber + "/"+ (healthVideosHighSocialStatus.Length-1) + " for stat " + Enum.GetName(typeof(Statistic.Type), statistic.type));
                    return PlayVideo(healthVideosHighSocialStatus[videoNumber]);
                case Statistic.Type.Sanity:
                    videoNumber = Mathf.RoundToInt(Mathf.Lerp(0, sanityVideosHighSocialStatus.Length-1, statistic.value/100f));
                    Debug.Log("Showing video number for high social status " + videoNumber + "/"+ (sanityVideosHighSocialStatus.Length-1) + " for stat " + Enum.GetName(typeof(Statistic.Type), statistic.type));
                    return PlayVideo(sanityVideosHighSocialStatus[videoNumber]);
            }
        }

        throw new Exception("The video that should be shown could not be identified. Statistic: " + statistic + ". Parameter 'highSocialStatus' = " + highSocialStatus);
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
        {
            HideRawImage();
            GameManager.Instance.ForceGameLoop();
        }
    }
}
