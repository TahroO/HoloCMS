using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using static JsonURLReaderScript;

/*
 * This script instantiates and configures Points of Interest (POIs) based on JSON data,
 * dynamically selecting appropriate layouts and populating UI elements with text, images, or videos.
 */

public class InstantiatePointOfInterestsScript : MonoBehaviour
{
    public GameObject defaultLayout;
    public GameObject[] layouts = new GameObject[7];
    private Dictionary<string, GameObject> layoutDictionary;

    public TestQRCodeDetection qrCodeReader;
    public List<GameObject> pointsOfInterests = new List<GameObject>();

    void InitializeLayoutDictionary()
    {
        layoutDictionary = new Dictionary<string, GameObject>
        {
            { "1", layouts[0] },
            { "2", layouts[1] },
            { "3", layouts[2] },
            { "4", layouts[3] },
            { "5", layouts[4] },
            { "6", layouts[5] },
            { "7", layouts[6] }
        };
    }

    public IEnumerator InstantiatePrefabs(PointOfInterest[] pointOfInterestList)
    {
        InitializeLayoutDictionary();

        // Check if JSONReader has data
        if (pointOfInterestList == null || pointOfInterestList == null || pointOfInterestList.Length == 0)
        {
            Debug.LogError("No Points of Interest available for instantiation.");
            yield break;
        }

        else
        {
            int index = 0;
            foreach (var poi in pointOfInterestList)
            {
                GameObject poiLayout;

                //Assign Layout Type
                if (layoutDictionary.ContainsKey(poi.field_layout_version))
                {
                    poiLayout = layoutDictionary[poi.field_layout_version];
                }
                else
                {
                    poiLayout = defaultLayout;
                }

                GameObject gameObject = Instantiate(poiLayout);
                gameObject.name = "PointOfInterest" + index;

                // Assign in qrCodeReader field
                qrCodeReader.pointOfInterests[index] = gameObject;

                gameObject.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);

                // ACCESS LAYOUT ELEMENTS 
                // Get the 'LayoutScript'
                var layoutComponent = gameObject.GetComponent<LayoutScript>();

                // Get the 'mediaBox' and 'textBox' GameObject
                var mediaBoxObject = layoutComponent._mediaBox;
                var textBoxObject = layoutComponent._textBox;

                // Get the 'MediaBoxScript' and 'TextBoxScript'
                var mediaboxScript = mediaBoxObject.GetComponent<MediaBoxScript>();
                var textboxScript = textBoxObject.GetComponent<TextBoxScript>();

                // Get field objects 
                var galleryObject = mediaboxScript._Gallery;
                var singleImageObject = mediaboxScript._SingleImage;
                var videoObject = mediaboxScript._Video;
                var headingObject = textboxScript._Heading;
                var fluidObject = textboxScript._Fluid;

                // HEADING
                // Get 'HeadingScript'
                var headingScript = headingObject.GetComponent<HeadingScript>();

                // Get field objects + access text + fill
                var headingItem = headingScript._heading_Text;
                TMP_Text headingText = headingItem.GetComponent<TMP_Text>();
                headingText.text = poi.title;

                // FLUID TEXT
                if (poi.text.Length >= 1)
                {
                    // Get 'FluidScript'
                    var fluidScript = fluidObject.GetComponent<FluidScript>();

                    // Get field objects + access text + fill
                    var fluidItem = fluidScript._fluid_Text;
                    TMP_Text fluidText = fluidItem.GetComponent<TMP_Text>();
                    fluidText.text = poi.text;
                    fluidObject.SetActive(true);
                }

                if (poi.text.Length < 1)
                {
                    fluidObject.SetActive(false);
                }

                // MEDIA
                if (poi.type == "text")
                {
                    mediaBoxObject.SetActive(false);
                }

                if (poi.type == "video")
                {
                    // Get 'VideoScript'
                    var videoScript = videoObject.GetComponent<VideoScript>();

                    // Get field objects + access video player + fill
                    var videoItem = videoScript._video_Video;
                    VideoPlayer videoPlayer = videoItem.GetComponent<VideoPlayer>();
                    videoPlayer.url = poi.video.media_video_file.url;

                    galleryObject.SetActive(false);
                    singleImageObject.SetActive(false);
                }
                else
                {
                    videoObject.SetActive(false);
                }

                if (poi.type == "image")
                {
                    if (poi.images.Count == 1) //SingleImage
                    {
                        // Get 'SingleImageScript'
                        var singleImageScript = singleImageObject.GetComponent<SingleImageScript>();

                        // Get field objects + access image loader + fill
                        var singleImageItem = singleImageScript._singleImage_Image;
                        ImageLoaderURL singleImageLoader = singleImageItem.GetComponent<ImageLoaderURL>();
                        singleImageLoader.urlData = poi.images[0].media_image.url;

                        galleryObject.SetActive(false);
                    }

                    else if (poi.images[1] != null)
                    {
                        //Gallery
                        singleImageObject.SetActive(false);

                        // Get 'GalleryScript'
                        var galleryScript = galleryObject.GetComponent<GalleryScript>();

                        // Get field objects
                        var galleryImage0 = galleryScript._gallery_Image0;
                        var galleryImage1 = galleryScript._gallery_Image1;
                        var galleryImage2 = galleryScript._gallery_Image2;

                        // Access image loader + fill
                        ImageLoaderURL imageLoader0 = galleryImage0.GetComponent<ImageLoaderURL>();
                        imageLoader0.urlData = poi.images[0].media_image.url;

                        ImageLoaderURL imageLoader1 = galleryImage1.GetComponent<ImageLoaderURL>();
                        imageLoader1.urlData = poi.images[1].media_image.url;

                        ImageLoaderURL imageLoader2 = galleryImage2.GetComponent<ImageLoaderURL>();
                        imageLoader2.urlData = poi.images[2].media_image.url;
                    }
                }

                gameObject.SetActive(false);
                index++;
            }
        }

        yield return true;
    }
}