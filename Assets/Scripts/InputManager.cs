using UnityEngine;
using System.Collections;

public class InputManager : Singleton<InputManager>
{
    private float _sidewaysMotion = 0.0f;

    public float sidewaysMotion
    {
        get { return _sidewaysMotion; }
    }

    private void Update()
    {
#if UNITY_ANDROID
		Vector3 accel = Input.acceleration;
		_sidewaysMotion = accel.x;
#else
        _sidewaysMotion = Input.GetAxis("Horizontal");
#endif
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 40, 1000, 100), "_sidewaysMotion " + _sidewaysMotion.ToString());
    }
}