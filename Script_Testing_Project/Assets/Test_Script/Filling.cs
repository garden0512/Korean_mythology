using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filling : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Color targetColor = Color.red;
    public float duration = 2.0f;

    private float elapsedTime = 0f;
    private Texture2D texture;
    private Color originalColor;

    void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // 원래 스프라이트의 텍스처를 가져옵니다.
        texture = spriteRenderer.sprite.texture;
        originalColor = texture.GetPixel(0, 0);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        float fillAmount = Mathf.Clamp01(elapsedTime / duration);
        UpdateSpriteColor(fillAmount);
    }

    void UpdateSpriteColor(float fillAmount)
    {
        Texture2D newTexture = new Texture2D(texture.width, texture.height);

        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                Color pixelColor = texture.GetPixel(x, y);

                if (y < fillAmount * texture.height)
                {
                    newTexture.SetPixel(x, y, targetColor);
                }
                else
                {
                    newTexture.SetPixel(x, y, pixelColor);
                }
            }
        }

        newTexture.Apply();

        spriteRenderer.sprite = Sprite.Create(newTexture, spriteRenderer.sprite.rect, new Vector2(0.5f, 0.5f));
    }
}
