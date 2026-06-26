using UnityEngine;
using System.Collections;


[RequireComponent(typeof(SpriteRenderer))]
public class BodyPart : MonoBehaviour
{
    public Sprite detachedSprite;
    public Sprite burnedSprite;
    public Transform bloodFountainOrigin;
    private bool detached = false;

    public void Detach()
    {
        detached = true;
        this.tag = "Untagged";
        transform.SetParent(null, true);
    }

    public void Update()
    {
        if (detached == false)
            return;
        
        var rigidbody = GetComponent<Rigidbody2D>();

        if (rigidbody.IsSleeping())
        {
            foreach (Joint2D joint in GetComponentsInChildren<Joint2D>())
                Destroy(joint);

            foreach (Rigidbody2D body in GetComponentsInChildren<Rigidbody2D>())
                Destroy(body);

            foreach (Collider2D collider in GetComponentsInChildren<Collider2D>())
                Destroy(collider);

            Destroy(this);
        }
    }

    public void ApplyDamageSprite(Gnome.DamageType damageType)
    {
        Sprite spriteToUse = damageType switch
        {
            Gnome.DamageType.Burning => burnedSprite,
            Gnome.DamageType.Slicing => detachedSprite,
            _ => null
        };

        if (spriteToUse != null)
        {
            GetComponent<SpriteRenderer>().sprite = spriteToUse;
        }
    }
}