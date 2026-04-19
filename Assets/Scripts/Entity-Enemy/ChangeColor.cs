using TMPro;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    /* Alexis Clay Drain */
    // public Color newColor;
    public void UpdateColor(string newColor) {
        Color myColor;
        ColorUtility.TryParseHtmlString(newColor, out myColor);
        if (GetComponent<TextMeshProUGUI>()) {
            GetComponent<TextMeshProUGUI>().color = myColor;
        }
    }

}
