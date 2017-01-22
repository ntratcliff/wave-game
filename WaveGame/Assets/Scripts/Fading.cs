using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fading : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void FadeIn(GameObject text, float fadeTime)
    {
        StartCoroutine(FadeInTimed(text, fadeTime));
        Debug.Log("This object is " + text.name);
    }

    IEnumerator FadeInTimed(GameObject text, float time)
    {
        Text[] texts = text.GetComponentsInChildren<Text>();
        float alphaLoss = 1.0f / time;
        float timeSinceStart = 0;

        while (time-timeSinceStart > 0)
        {
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].color = new Color(texts[i].color.r, texts[i].color.g, texts[i].color.b, Mathf.Lerp(texts[i].color.a, 1, timeSinceStart / time));
            }
            timeSinceStart += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].color = new Color(texts[i].color.r, texts[i].color.g, texts[i].color.b, 1);
        }
    }

    public void FadeOut(GameObject text, float fadeTime)
    {
        StartCoroutine(FadeOutTimed(text, fadeTime));
    }

    IEnumerator FadeOutTimed(GameObject text, float time)
    {
        Text[] texts = text.GetComponentsInChildren<Text>();
        float alphaLoss = 1.0f / time;
        float timeSinceStart = 0;
        while (time - timeSinceStart > 0)
        {
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].color = new Color(texts[i].color.r, texts[i].color.g, texts[i].color.b, Mathf.Lerp(texts[i].color.a, 0, timeSinceStart / time));
            }
            timeSinceStart += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].color = new Color(texts[i].color.r, texts[i].color.g, texts[i].color.b, 0);
        }
    }
}
