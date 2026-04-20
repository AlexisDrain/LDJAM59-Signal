using UnityEngine;

public class RotateTransformInPlace : MonoBehaviour
{
    /* Alexis Clay Drain */

    public float x_speed = 1.0f;
    public float y_speed = 1.0f;
    float x = 0f;
    float y = 0f;
    void LateUpdate()
    {
        x += Time.deltaTime * x_speed;
        y += Time.deltaTime * y_speed;
        x = Mathf.Clamp(x, 0f, 360f);
        y = Mathf.Clamp(y, 0f, 360f);
        transform.rotation = Quaternion.Euler(x, y, 0f);
    }

}
