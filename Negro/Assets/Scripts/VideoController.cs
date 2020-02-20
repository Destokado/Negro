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

    public void ShowVideoFor(Statistic statistic)
    {
        Debug.Log("Showing video for stat " + Enum.GetName(typeof(Statistic.Type), statistic.type));
        
        throw new NotImplementedException();
    }
}
