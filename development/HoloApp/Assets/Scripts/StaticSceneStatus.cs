using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticSceneStatus : MonoBehaviour
{
    // 1 = before JSONReader started
    // 2 = JSONReader ready
    // 3 = Instantiator ready, QR can be enabled
    // 4 = QR Code Reader Enabled
    
    public static int AppState;
}
