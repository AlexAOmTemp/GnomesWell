using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[RequireComponent (typeof(Collider2D))]
public class SignalOnTouch : MonoBehaviour 
{
	public UnityEvent onTouch;
	public bool playAudioOnTouch = true;

	private void OnTriggerEnter2D(Collider2D collider) 
	{
		Debug.Log("Trigger");
		SendSignal (collider.gameObject);
	}

	private void OnCollisionEnter2D(Collision2D collision) 
	{
        SendSignal (collision.gameObject);
	}

	private void SendSignal(GameObject objectThatHit)
	{
		if (!objectThatHit.CompareTag("Player")) 
			return;
		
		if (playAudioOnTouch) 
		{
			var audio = GetComponent<AudioSource>();
				
			if (audio && audio.gameObject.activeInHierarchy)
				audio.Play();
		}

		Debug.Log($"The collision between {gameObject.name} and {objectThatHit}");
		onTouch.Invoke();
	}
}
