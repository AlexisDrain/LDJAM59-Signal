using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class FadeSignalText : MonoBehaviour
{
    /* Alexis Clay Drain */
    public Transform targetTrans;
    private TextMeshProUGUI myText;

    private float distance;
    void Start() {
        myText = GetComponent<TextMeshProUGUI>();
        GameManager.levelEndEvent.AddListener(() => Destroy(gameObject));
    }
    void LateUpdate()
    {

        distance = Vector3.Distance(transform.position, targetTrans.position);
        myText.alpha = Mathf.Clamp(distance - 6f, 0f, 1f);
    }

}
