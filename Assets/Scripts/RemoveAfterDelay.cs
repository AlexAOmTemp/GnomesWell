using UnityEngine;
using System.Collections;

public class RemoveAfterDelay : MonoBehaviour 
{
	public float delay = 1.0f;

	private void Start () 
	{
		StartCoroutine(nameof(Remove));
	}

	private IEnumerator Remove() 
	{
		yield return new WaitForSeconds(delay);
		Destroy (gameObject);
	}
}
