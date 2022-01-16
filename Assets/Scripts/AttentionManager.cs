using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class AttentionManager : MonoBehaviour
{
    protected AttentionManager() { }
    public float playerAttention = 0f;
    public Slider sliderNoise;
    public static AttentionManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        StartCoroutine(SliderVariation());
    }
    public void IncreaseNoise(float noise)
    {
        StartCoroutine(IncreasingNoise(noise));
    }
    public void DecreaseNoise(float noise)
    {

        StartCoroutine(DecreasingNoise(noise));
    }
    IEnumerator IncreasingNoise(float noise)
    {
        for (float i = 0; i < noise; i++)
        {
            playerAttention += 1;
            sliderNoise.value = playerAttention / 100;
            yield return new WaitForSeconds(0.01f);
        }

    }
    IEnumerator DecreasingNoise(float noise)
    {
        for (float i = 0; i < noise; i++)
        {
            playerAttention -= 1;
            sliderNoise.value = playerAttention / 100;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator SliderVariation()
    {
        bool variationEffect = true;
        float temp = 0.07f;
        while(variationEffect)
        {
             sliderNoise.value += 0.01f;
            yield return new WaitForSeconds(temp);
            sliderNoise.value -= 0.005f;
            yield return new WaitForSeconds(temp);
            sliderNoise.value += 0.005f;
            yield return new WaitForSeconds(temp);
            sliderNoise.value -= 0.01f;
            yield return new WaitForSeconds(temp);
        }

    }
}

