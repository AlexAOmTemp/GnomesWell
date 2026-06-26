using UnityEngine;
using System.Collections;

public class SpriteSwapper : MonoBehaviour 
{
	public Sprite spriteToUse;
	public SpriteRenderer spriteRenderer;
	
	private Sprite originalSprite;
	
    public void SwapSprite()
    {
	    if (spriteToUse == spriteRenderer.sprite) 
		    return;
	    
	    Debug.Log($"sprite swapped by {gameObject.name}");
	    originalSprite = spriteRenderer.sprite;
	    spriteRenderer.sprite = spriteToUse;
    }
    
	public void ResetSprite()
	{
		if (originalSprite == null) 
			return;
		
		Debug.Log("sprite resetted");
		spriteRenderer.sprite = originalSprite;
	}
}
