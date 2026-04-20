using UnityEngine;

public class TrampolineController : MonoBehaviour
{
    /* Alexis Clay Drain */
    public void TrampolineInvoke()
    {
        GameManager.playerTrans.GetComponent<PlayerController>().TrampolineJump();
    }

}
