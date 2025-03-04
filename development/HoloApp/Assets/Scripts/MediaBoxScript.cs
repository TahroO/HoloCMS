using UnityEngine;

/*
 * This class manages UI elements by providing access to specific GameObjects.
 */

public class MediaBoxScript : MonoBehaviour
{
    public GameObject _gallery;
    public GameObject _singleImage;
    public GameObject _video;

    public GameObject _Gallery { get => _gallery; set => _gallery = value; }
    public GameObject _SingleImage { get => _singleImage; set => _singleImage = value; }
    public GameObject _Video { get => _video; set => _video = value; }
}
