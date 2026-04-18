using UnityEngine;

public class GameManager : MonoBehaviour
{
    /* Alexis Clay Drain */

    public static PlayerInputAction playerInputAction;
    void Awake()
    {
        playerInputAction = new PlayerInputAction();
        playerInputAction.Enable();
    }

}
