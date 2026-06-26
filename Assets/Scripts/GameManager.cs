using UnityEngine;
using System.Collections;
using System;

public class GameManager : Singleton<GameManager>
{
    public GameObject startingPoint;
    public Rope rope;
    public CameraFollow cameraFollow;
    public GameObject gnomePrefab;
    public RectTransform mainMenu;
    public RectTransform gameplayMenu;
    public RectTransform gameOverMenu;
    public bool gnomeInvincible { get; set; }
    public float delayAfterDeath = 1.0f;
    public AudioClip gnomeDiedSound;
    public AudioClip gameOverSound;
    
    private Gnome currentGnome;
    
    private void Start()
    {
        Reset();
    }
    
    public void Reset()
    {
        if (gameOverMenu)
            gameOverMenu.gameObject.SetActive(false);

        if (mainMenu)
            mainMenu.gameObject.SetActive(false);

        if (gameplayMenu)
            gameplayMenu.gameObject.SetActive(true);
        
        var resetObjects = FindObjectsOfType<Resettable>();

        foreach (Resettable r in resetObjects)
        {
            r.Reset();
        }
        
        CreateNewGnome();
        Time.timeScale = 1.0f;
    }
 
    private void CreateNewGnome()
    {
        RemoveGnome();
        GameObject newGnome = (GameObject) Instantiate(gnomePrefab,
            startingPoint.transform.position,
            Quaternion.identity);
        currentGnome = newGnome.GetComponent<Gnome>();
        rope.gameObject.SetActive(true);
        rope.connectedObject = currentGnome.ropeBody;
        rope.ResetLength();
        cameraFollow.target = currentGnome.cameraFollowTarget;
    }

    private void RemoveGnome()
    {
        if (gnomeInvincible)
            return;
        
        rope.gameObject.SetActive(false);
        cameraFollow.target = null;
        
        if (currentGnome != null)
        {

            currentGnome.holdingTreasure = false;

            currentGnome.gameObject.tag = "Untagged";

            foreach (Transform child in currentGnome.transform)
            {
                child.gameObject.tag = "Untagged";
            }
            
            currentGnome = null;
        }
    }

    private void KillGnome(Gnome.DamageType damageType)
    {
        var audio = GetComponent<AudioSource>();
        if (audio)
        {
            audio.PlayOneShot(this.gnomeDiedSound);
        }
        
        currentGnome.ShowDamageEffect(damageType);
        
        if (gnomeInvincible == false)
        {
            currentGnome.DestroyGnome(damageType);
            RemoveGnome();
            StartCoroutine(ResetAfterDelay());
        }
    }

    private IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(delayAfterDeath);
        Reset();
    }

    public void TrapTouched()
    {
        Debug.Log("TrapTouched");
        KillGnome(Gnome.DamageType.Slicing);
    }
    
    public void FireTrapTouched()
    {
        KillGnome(Gnome.DamageType.Burning);
    }
    
    public void TreasureCollected()
    {
        Debug.Log("Treasure Collected");
        currentGnome.holdingTreasure = true;
    }

    public void ExitReached()
    {
        if (currentGnome != null && currentGnome.holdingTreasure == true)
        {
            var audio = GetComponent<AudioSource>();
            if (audio)
            {
                audio.PlayOneShot(this.gameOverSound);
            }
            
            Time.timeScale = 0.0f;
            
            if (gameOverMenu)
                gameOverMenu.gameObject.SetActive(true);

            if (gameplayMenu)
                gameplayMenu.gameObject.SetActive(false);
        }
    }

    public void SetPaused(bool paused)
    {
        if (paused)
        {
            Time.timeScale = 0.0f;
            mainMenu.gameObject.SetActive(true);
            gameplayMenu.gameObject.SetActive(false);
        }
        else
        {
            Time.timeScale = 1.0f;
            mainMenu.gameObject.SetActive(false);
            gameplayMenu.gameObject.SetActive(true);
        }
    }

    public void RestartGame()
    {
        Destroy(currentGnome.gameObject);
        currentGnome = null;
        Reset();
    }
}
