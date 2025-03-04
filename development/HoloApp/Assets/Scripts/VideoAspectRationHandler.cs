using UnityEngine;
using UnityEngine.Video;

/*
 * This class calculates the aspect ratio of a video to match the texture on a RawImage component.
 */

public class VideoAspectRationHandler : MonoBehaviour
{
    public RectTransform rawImageRectTransform;
    public VideoPlayer videoPlayer;
    public RenderTexture videoTexture;

    void Start()
    {
        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.Prepare();
    }

    private void OnVideoPrepared(VideoPlayer vp)
    {
        int videoWidth = (int)vp.width;
        int videoHeight = (int)vp.height;

        float videoAspectRatio = (float)videoWidth / videoHeight;

        float targetWidth = rawImageRectTransform.rect.width;

        float targetHeight = targetWidth / videoAspectRatio;

        rawImageRectTransform.sizeDelta = new Vector2(targetWidth, targetHeight);

        videoTexture.Release();
        videoTexture.width = videoWidth;
        videoTexture.height = videoHeight;
        videoTexture.Create();

        vp.targetTexture = videoTexture;
    }
}