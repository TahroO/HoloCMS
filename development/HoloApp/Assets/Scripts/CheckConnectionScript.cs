using UnityEngine;

/*
 * This class checks the device's internet connectivity at regular intervals.
 * It displays a popup if the device is offline and updates the connection status.
 */

public class CheckConnectionScript : MonoBehaviour
{
    public GameObject notConnectedPopUp;

    public GameObject closeApplicationButton;

    // in seconds
    private readonly float _waitTime = 5f;
    private float _timer;

    void Start()
    {
        CheckConnection();
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _waitTime)
        {
            CheckConnection();
            _timer = 0;
        }
    }

    private void CheckConnection()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            notConnectedPopUp.SetActive(true);
            closeApplicationButton.SetActive(true);
        }
    }
}