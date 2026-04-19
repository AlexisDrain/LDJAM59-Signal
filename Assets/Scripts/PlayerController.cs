using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /* Alexis Clay Drain */
    public float jumpImpulse = 11f;
    public float moveHorizontalVelocity = 3f;
    public float maxHorizontalVelocity = 3f;
    public float horizontalDrag = 0.98f;
    public float gravity = 10f;
    public float footstepDistanceToSFX = 1f;
    public AudioClip clipJump;
    public AudioClip clipFootstep;
    private bool onGround = false;

    private Rigidbody myRigidbody;
    private Vector3 previousPositionOnGround;
    void Start()
    {
        previousPositionOnGround = transform.position;
        myRigidbody = GetComponent<Rigidbody>();
    }
    private void Update() {
        if(onGround && GameManager.playerInputAction.Player.Jump.WasPressedThisFrame() && Time.timeScale >= 0.1f) {
            myRigidbody.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
            GameManager.SpawnLoudAudio(clipJump);
        }
        if(myRigidbody.linearVelocity.y > 0.1f && GameManager.playerInputAction.Player.Jump.WasReleasedThisFrame()) {
            myRigidbody.linearVelocity = new Vector3(myRigidbody.linearVelocity.x, myRigidbody.linearVelocity.y * 0.5f, myRigidbody.linearVelocity.z);
        }
    }
    private void FixedUpdate() {

        if(myRigidbody.position.x <= -10f) {
            myRigidbody.position = new Vector3(-10f, myRigidbody.position.y, myRigidbody.position.z);
        } else if (myRigidbody.position.x > 10f) {
            myRigidbody.position = new Vector3(10f, myRigidbody.position.y, myRigidbody.position.z);
        }

        RaycastHit hit;
        Physics.Linecast(transform.position + new Vector3(0f, 0.1f, 0f), transform.position + new Vector3(0f, -0.2f, 0f), out hit);
        if (hit.collider) {
            if(hit.collider.CompareTag("Floor")) {
                onGround = true;
            }
        } else {
            // GameManager.SpawnLoudAudio(clipFootstep, new Vector2(0.9f, 1.2f)); // landSFX
            onGround = false;
            myRigidbody.AddForce(Vector3.down * gravity, ForceMode.Force);
        }

        if(onGround == true && Vector3.Distance(previousPositionOnGround, transform.position) > footstepDistanceToSFX) {
            previousPositionOnGround = transform.position;
            // GameManager.SpawnLoudAudio(clipFootstep, new Vector2(0.9f, 1.2f));
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
