using UnityEngine;

/*
 * This class manages UI elements by providing access to specific GameObjects.
 */

public class TextBoxScript : MonoBehaviour
{
    public GameObject _heading;
    public GameObject _fluid;

    public GameObject _Heading
    {
        get => _heading;
        set => _heading = value;
    }

    public GameObject _Fluid
    {
        get => _fluid;
        set => _fluid = value;
    }
}