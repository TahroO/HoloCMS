using UnityEngine;
using UnityEngine.Serialization;

/*
 * This class manages switching between gallery images in a loop.
 * It updates the active image based on user navigation inputs.
 */

public class GallerySwitcher : MonoBehaviour
{
    [FormerlySerializedAs("background")] public GameObject[] galleryImages;
    private int index;

    void Start()
    {
        index = 0;
        UpdateGalleryImage();
    }

    void UpdateGalleryImage()
    {
        // Activate the current image
        for (int i = 0; i < galleryImages.Length; i++)
        {
            galleryImages[i].SetActive(i == index);
        }
    }

    public void Next()
    {
        index += 1;

        if (index >= galleryImages.Length)
        {
            index = 0; // Loop back to the first image
        }
        UpdateGalleryImage();
    }

    public void Previous()
    {
        index -= 1;

        if (index < 0)
        {
            index = galleryImages.Length - 1; // Loop back to the last image
        }
        UpdateGalleryImage();
    }
}