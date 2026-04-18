using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /* Alexis Clay Drain */
    public float jumpImpulse = 11f;
    public float moveHorizontalVelocity = 3f;
    public float maxHorizontalVelocity = 3f;
    public float horizontalDrag = 0.98f;
    public float gravity = 10f;

    private bool onGround = false;

    private Rigidbody myRigidbody;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }
    private void Update() {
        if(onGround && GameManager.playerInputAction.Player.Jump.WasPressedThisFrame()) {
            myRigidbody.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
        }
        if(myRigidbody.linearVelocity.y > 0.1f && GameManager.playerInputAction.Player.Jump.WasReleasedThisFrame()) {
            myRigidbody.linearVelocity = new Vector3(myRigidbody.linearVelocity.x, myRigidbody.linearVelocity.y * 0.5f, myRigidbody.linearVelocity.z);
        }
    }
    private void FixedUpdate() {

        RaycastHit hit;
        Physics.Linecast(transform.position + new Vector3(0f, 0.1f, 0f), transform.position + new Vector3(0f, -0.2f, 0f), out hit);
        if (hit.collider) {
            if(hit.collider.CompareTag("Floor")) {
                onGround = true;
            }
        } else {
            onGround = false;
            myRigidbody.AddForce(Vector3.down * gravity, ForceMode.Force);
        }

        Vector2 moveAxis = GameManager.playerInputAction.Player.Move.ReadValue<Vector2>();
        if (moveAxis.x > 0.1f) {
            myRigidbody.AddForce(Vector3.right * moveHorizontalVelocity * Mathf.Abs(moveAxis.x), ForceMode.Force);
        } else if (moveAxis.x < -0.1f) {
            myRigidbody.AddForce(Vector3.left * moveHorizontalVelocity * Mathf.Abs(moveAxis.x), ForceMode.Force);
        }
        float horizontalVelocity = Mathf.Clamp(myRigidbody.linearVelocity.x, -maxHorizontalVelocity, maxHorizontalVelocity);
        horizontalVelocity *= horizontalDrag;
        myRigidbody.linearVelocity = new Vector3(horizontalVelocity, myRigidbody.linearVelocity.y, myRigidbody.linearVelocity.z);
    }
}
