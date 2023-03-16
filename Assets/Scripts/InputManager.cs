// BEGIN 2d_inputmanager

using UnityEngine;
using System.Collections;

// Translates the accelerometer data into sideways motion info.
public class InputManager : Singleton<InputManager> {

	// How much we're moving. -1.0 = full left, +1.0 = full right
	private float _sidewaysMotion = 0.0f;

    // This property is declared as read-only, so that other classes can't 
    // change it
    public float sidewaysMotion {
		get {
			return _sidewaysMotion;
		}
	}
	
	// Every frame, store the tilt
	void Update () {
#if UNITY_ANDROID
		Vector3 accel = Input.acceleration;
		_sidewaysMotion = accel.x;
#else
        _sidewaysMotion = Input.GetAxis("Horizontal");
#endif
    }
    void OnGUI()
    {
		//GUI.Label(new Rect(0, 0, 1000, 100), "position: " + transform.position.ToString());
		GUI.Label(new Rect(0, 40, 1000, 100), "_sidewaysMotion " + _sidewaysMotion.ToString());
    }
}
// END 2d_inputmanager
