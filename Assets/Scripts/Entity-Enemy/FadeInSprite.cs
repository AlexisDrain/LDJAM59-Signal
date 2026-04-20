using UnityEngine;

public class FadeInSprite : MonoBehaviour
{
    /* Alexis Clay Drain */
    private SpriteRenderer mySpriteRenderer;
    private Color myColor;
    void OnEnable()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myColor = mySpriteRenderer.color;
        mySpriteRenderer.color = new Color(myColor.r, myColor.g, myColor.b, 0f);
    }
    private float alpha = 0f;
    private void LateUpdate() {
        alpha = Mathf.Lerp(alpha, 1f, 5f * Time.deltaTime);
        mySpriteRenderer.color = new Color(myColor.r, myColor.g, myColor.b, alpha);
    }
}
