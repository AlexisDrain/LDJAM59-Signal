using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /* Alexis Clay Drain */
    private Rigidbody myRigidbody;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }
    private void Update() {
        if(GameManager.playerInputAction.Player.Jump.WasPressedThisFrame()) {
            myRigidbody.AddForce(Vector3.up * 9f, ForceMode.Force);
        }
    }
    private void FixedUpdate() {
        Vector2 moveAxis = GameManager.playerInputAction.Player.Move.ReadValue<Vector2>();
        if (moveAxis.x > 0.1f) {
            myRigidbody.AddForce(Vector3.up, ForceMode.Force);
        }
    }
}
