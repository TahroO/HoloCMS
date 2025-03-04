using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

/*
 *  This script loads an image from a URL and assigns it to a UI Image component in Unity.
*/

public class ImageLoaderURL : MonoBehaviour
{
    public Image mainImage;
    public string urlData;
    public int maxTry = 3;
    public int currTry = 0;
    public Texture2D backupImage;

    void Start()
    {
        LoadImage();
    }

    public void LoadImage()
    {
        backupImage = Resources.Load<Texture2D>("backup_texture");
       
        StartCoroutine(RequestUrlImage());
    }

    IEnumerator RequestUrlImage()
    {
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(urlData))
        {
            yield return webRequest.SendWebRequest();
            var texture = DownloadHandlerTexture.GetContent(webRequest);

            if (texture != null)
            {
                Sprite _temp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                mainImage.sprite = (_temp);
            }
            else
            {
                if (currTry <= maxTry)
                {
                    StartCoroutine(RequestUrlImage());
                    currTry++;
                }
                else
                {
                    Sprite _temp = Sprite.Create(backupImage, new Rect(0, 0, backupImage.width, backupImage.height), Vector2.zero);
                    mainImage.sprite = (_temp);
                }
            }
        }  
    }

    private void WaitForSecondsRealtime(int v)
    {
        throw new NotImplementedException();
    }
}