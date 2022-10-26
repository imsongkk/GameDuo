using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchToStart : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI touchToStart;

    float endAlpha = 0.0f;
    float startAlpha = 1.0f;

    float animationTime = 1.0f;
    float deltaTime = 0.0f;

    public void OnClick()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("MainScene");
    }

    void Update()
    {
        if (animationTime < deltaTime)
        {
            deltaTime = 0.0f;
            float tempAlpha = startAlpha;
            startAlpha = endAlpha;
            endAlpha = tempAlpha;
        }

        deltaTime += Time.deltaTime;

        float alpha = Mathf.Lerp(startAlpha, endAlpha, deltaTime / animationTime);
        touchToStart.color = new Color(touchToStart.color.r, touchToStart.color.g, touchToStart.color.b, alpha);
    }
}
