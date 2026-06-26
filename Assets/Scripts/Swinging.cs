using UnityEngine;
using System.Collections;

public class Swinging : MonoBehaviour
{
    public float swingSensitivity = 0.5f;
    private Vector2 _force;

    private void FixedUpdate()
    {
        if (GetComponent<Rigidbody2D>() == null)
        {
            Destroy(this);
            return;
        }

        float swing = InputManager.instance.sidewaysMotion;
        _force = new Vector2(swing * swingSensitivity, 0);
        GetComponent<Rigidbody2D>().AddForce(_force);
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 30, 1000, 100), "force " + _force.ToString());
    }
}