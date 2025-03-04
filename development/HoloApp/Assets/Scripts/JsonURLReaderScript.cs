using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/* This script fetches JSON data from a specified URL and processes it into a structured format  
 * using UnityWebRequest. It deserializes the data into PointOfInterest objects, 
 * making it accessible for further application logic.  
 */

public class JsonURLReaderScript : MonoBehaviour
{
    public string jsonURL;
    public PointOfInterest[] layoutList;

    [Serializable]
    public class MediaImage
    {
        public string url;
        public string title;
    }

    [Serializable]
    public class MediaImageWrapper
    {
        public MediaImage media_image;
    }

    [Serializable]
    public class MediaVideo
    {
        public string url;
        public string description;
    }

    [Serializable]
    public class MediaVideoWrapper
    {
        public MediaVideo media_video_file;
    }

    [Serializable]
    public class PointOfInterest
    {
        public string title;
        public string field_layout_version;
        public string text;
        public int id;
        public string type; // Type indicates "image", "video", or "text"
        public List<MediaImageWrapper> images; // List of MediaImageWrapper for nested structure
        public MediaVideoWrapper video; // MediaVideoWrapper for video structure
    }

    [Serializable]
    public class LayoutListWrapper
    {
        public PointOfInterest[] layoutLists;
    }

    public IEnumerator LoadJson()
    {
        if (!string.IsNullOrEmpty(jsonURL))
        {
            yield return StartCoroutine(getData());
        }
        else
        {
            Debug.LogError("No JSON file provided!");
        }
    }

    IEnumerator getData()
    {
        Debug.Log("Processing Data from: " + jsonURL);
        using (UnityWebRequest www = UnityWebRequest.Get(jsonURL))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                processJsonData(www.downloadHandler.text);
            }
            else
            {
                Debug.LogError(www.error + " has an error");
            }
        }
    }

    private void processJsonData(string json)
    {
        layoutList = JsonUtility.FromJson<LayoutListWrapper>("{\"layoutLists\":" + json + "}").layoutLists;
    }
}
