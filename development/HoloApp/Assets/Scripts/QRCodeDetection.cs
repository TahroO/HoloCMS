using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.OpenXR;

/*
 * This class handles QR code detection and processes detected markers in AR.
 * It activates and positions corresponding points of interest based on markers.
 * The script listens for marker changes.
 */

public class TestQRCodeDetection : MonoBehaviour
{
    [SerializeField] private GameObject mainText;
    [SerializeField] private ARMarkerManager markerManager;

    public GameObject[] pointOfInterests = new GameObject[10];

    private Dictionary<GameObject, bool> _poiWithPosition = new Dictionary<GameObject, bool>();

    private GameObject _currentPointOfInterest;


    private void Start()
    {
        if (markerManager == null)
        {
            Debug.LogError("ARMarkerManager is not assigned.");
            return;
        }

        markerManager.markersChanged += OnMarkersChanged;
    }

    private void OnMarkersChanged(ARMarkersChangedEventArgs args)
    {
        foreach (var addedMarker in args.added)
        {
            HandleAddedMarker(addedMarker);
        }

        foreach (var updatedMarker in args.updated)
        {
            HandleUpdatedMarker(updatedMarker);
        }

        foreach (var removedMarkerId in args.removed)
        {
            HandleRemovedMarker(removedMarkerId);
        }
    }

    private void HandleAddedMarker(ARMarker addedMarker)

    {
        ProcessQrCode(addedMarker);
    }

    private void HandleUpdatedMarker(ARMarker updatedMarker)
    {
        ProcessQrCode(updatedMarker);
    }

    private void HandleRemovedMarker(ARMarker removedMarkerId)
    {
    }

    private void ActivatePointOfInterest(GameObject pointOfInterest)
    {
        if (_currentPointOfInterest == pointOfInterest)
        {
            return;
        }

        if (_currentPointOfInterest == null)
        {
            _currentPointOfInterest = pointOfInterest;
        }

        if (_currentPointOfInterest != null && _currentPointOfInterest != pointOfInterest)
        {
            _currentPointOfInterest.SetActive(false);
        }

        pointOfInterest.SetActive(true);
        _currentPointOfInterest = pointOfInterest;
    }

    private void SetPositionAndRotationToMarker(GameObject pointOfInterest, Transform markerTransform)
    {
        if (_poiWithPosition.ContainsKey(pointOfInterest))
        {
            return;
        }

        // defines local offset for point of interest to match the marker position
        Vector3 localOffset = new Vector3(0.1135f, 0.477f, 0.05f);

        // calculates local coordinates to world coordinates with offset values
        Vector3 updatedMarkerPosition = markerTransform.TransformPoint(localOffset);

        // defines rotation to flip 180 degrees in x direction (hololens2 specific)
        Quaternion rotationAdjustment = Quaternion.Euler(180, 0, 0);

        // calculates the new rotation with euler adjustment
        Quaternion updatedMarkerRotation = markerTransform.rotation * rotationAdjustment;

        // applies positioning and rotation to point of interest
        pointOfInterest.transform.position = updatedMarkerPosition;
        pointOfInterest.transform.rotation = updatedMarkerRotation;

        _poiWithPosition.Add(pointOfInterest, true);
    }


    private void ProcessQrCode(ARMarker marker)
    {
        Transform markerTransform = marker.transform;

        switch (marker.GetDecodedString())
        {
            case "0":

                ActivatePointOfInterest(pointOfInterests[0]);

                SetPositionAndRotationToMarker(pointOfInterests[0],
                    markerTransform);

                break;

            case "1":

                ActivatePointOfInterest(pointOfInterests[1]);

                SetPositionAndRotationToMarker(pointOfInterests[1],
                    markerTransform);

                break;

            case "2":

                ActivatePointOfInterest(pointOfInterests[2]);

                SetPositionAndRotationToMarker(pointOfInterests[2],
                    markerTransform);

                break;

            case "3":

                ActivatePointOfInterest(pointOfInterests[3]);

                SetPositionAndRotationToMarker(pointOfInterests[3],
                    markerTransform);

                break;

            case "4":

                ActivatePointOfInterest(pointOfInterests[4]);

                SetPositionAndRotationToMarker(pointOfInterests[4],
                    markerTransform);

                break;

            case "5":

                ActivatePointOfInterest(pointOfInterests[5]);

                SetPositionAndRotationToMarker(pointOfInterests[5], 
                    markerTransform);

                break;

            case "6":

                ActivatePointOfInterest(pointOfInterests[6]);

                SetPositionAndRotationToMarker(pointOfInterests[6],
                    markerTransform);

                break;

            case "7":

                ActivatePointOfInterest(pointOfInterests[7]);

                SetPositionAndRotationToMarker(pointOfInterests[7],
                    markerTransform);

                break;

            case "8":

                ActivatePointOfInterest(pointOfInterests[8]);

                SetPositionAndRotationToMarker(pointOfInterests[8],
                    markerTransform);

                break;

            case "9":

                ActivatePointOfInterest(pointOfInterests[9]);

                SetPositionAndRotationToMarker(pointOfInterests[9],
                    markerTransform);

                break;
        }
    }
}