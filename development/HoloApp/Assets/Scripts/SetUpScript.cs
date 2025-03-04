using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using static JsonURLReaderScript;

/* 
 * This script initializes the application by sequentially loading JSON data, instantiating prefabs,  
 * and activating the QR code reader while providing status updates through a dialog message.  
 * It follows a coroutine-based setup process to ensure each step completes before proceeding to the next.  
 */

public class SetUpScript : MonoBehaviour
{
    [SerializeField]
    public GameObject jsonURLReader;
    [SerializeField]
    public GameObject instantiatePointOfInterests;
    [SerializeField]
    public GameObject qrCodeReader;
    [SerializeField]
    public GameObject dialog;
    [SerializeField]
    public GameObject dialogMessage;
    [SerializeField]
    private TMP_Text dialogMessageTMP;

    private int secondsBetweenSteps = 2;

    private void Start()
    {
            UpdateAppState(0);
            dialogMessageTMP = dialogMessage.GetComponent<TMP_Text>();
            UpdateDialogMessage("Starting Set Up...");

            StartCoroutine(SetUp());
    }

    private IEnumerator SetUp()
    {
            JsonURLReaderScript jsonURLReaderScript = jsonURLReader.GetComponent<JsonURLReaderScript>(); // Reference to  JSON reader script
            InstantiatePointOfInterestsScript instantiatePointOfInterestsScript = instantiatePointOfInterests.GetComponent<InstantiatePointOfInterestsScript>(); // Reference to prefab instantiator script

            // Step 1: Simulate initial loading
            UpdateAppState(1);

            // Step 2: Read JSON data
            yield return new WaitForSeconds(secondsBetweenSteps);
            UpdateDialogMessage("Loading data...");
            yield return jsonURLReaderScript.LoadJson();
            UpdateAppState(2);

            // Step 3: Instantiate Prefabs
            yield return new WaitForSeconds(secondsBetweenSteps);
            UpdateDialogMessage("Data Loaded, creating Points of Interest...");
            yield return instantiatePointOfInterestsScript.InstantiatePrefabs(jsonURLReaderScript.layoutList);
            UpdateAppState(3);

            // Step 4: Enable QR Code Reading
            yield return new WaitForSeconds(secondsBetweenSteps);
            UpdateDialogMessage("Everything is set up, activating scanning...");
            // activate QRReader
            qrCodeReader.SetActive(true);
            UpdateAppState(4);

            // Step 5: Turn off dialog
            yield return new WaitForSeconds(secondsBetweenSteps);
            dialog.SetActive(false);
    }

    private void UpdateAppState(int state)
    {
        StaticSceneStatus.AppState = state;
    }
    private void UpdateDialogMessage(string message)
    {
        if (dialogMessageTMP != null)
        {
            dialogMessageTMP.text = message;
        }
    }
}


