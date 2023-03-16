// BEGIN 2d_swinging

using UnityEngine;
using System.Collections;

// Uses the input manager to apply sideways forces to an object.
// Used to make the gnome swing side-to-side.
public class Swinging : MonoBehaviour {

	// How much should we swing by? Bigger numbers = more swing
	public float swingSensitivity = 0.5f;

	// Use FixedUpdate instead of Update, in order to play better with 
	// the physics engine
	private Vector2 _force;

    void FixedUpdate() {

		// If we have no ridigbody (anymore), remove this component
		if (GetComponent<Rigidbody2D>() == null) {
			Destroy (this);
			return;
		}

		// Get the tilt amount from the InputManager
		float swing = InputManager.instance.sidewaysMotion;

		// Calculate a force to apply
		_force = new Vector2(swing * swingSensitivity, 0);
		
		// Apply the force
		GetComponent<Rigidbody2D>().AddForce(_force);
	}
    void OnGUI()
    {
        //GUI.Label(new Rect(0, 0, 1000, 100), "position: " + transform.position.ToString());
        GUI.Label(new Rect(0, 30, 1000, 100), "force " + _force.ToString());
    }
}
// END 2d_swinging