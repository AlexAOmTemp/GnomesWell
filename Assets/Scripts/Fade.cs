using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Fade : MonoBehaviour
{
    private float fadeTimeRemaining = 0.0f;
    private float fadeSpeed = 0.0f;

    public void FadeIn()
    {
        SetAlpha(1.0f);
        FadeTo(0.0f, 0.5f);
    }

    public void FadeTo(float alpha, float time)
    {
        float currentAlpha = this.GetComponent<Image>().color.a;
        float deltaAlpha = alpha - currentAlpha;
        fadeSpeed = deltaAlpha / time;
        fadeTimeRemaining = time;
    }

    public void SetAlpha(float alpha)
    {
        Color color = this.GetComponent<Image>().color;
        color.a = alpha;
        this.GetComponent<Image>().color = color;
    }

    private void Update()
    {
        if (fadeTimeRemaining > 0)
        {
            fadeTimeRemaining -= Time.deltaTime;

            Color color = this.GetComponent<Image>().color;
            color.a += fadeSpeed * Time.deltaTime;
            this.GetComponent<Image>().color = color;
        }
    }
}