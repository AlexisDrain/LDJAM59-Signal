using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /* Alexis Clay Drain */
    [Header("Normal values")]
    public float jumpImpulse = 11f;
    public float moveHorizontalVelocity = 3f;
    public float maxHorizontalVelocity = 3f;
    public float horizontalDrag = 0.98f;
    public float gravity = 10f;
    [Header("Energy values")]
    public float jumpImpulseEnergy = 11f;
    public float moveHorizontalVelocityEnergy = 3f;
    public float maxHorizontalVelocityEnergy = 3f;
    public float horizontalDragEnergy = 0.98f;
    public float gravityEnergy = 10f;

    [Header("Trampoline values")]
    private bool canDoubleJump;
    public float trampolineImpulse = 20f;

    [Header("Footsteps")]
    public float footstepDistanceToSFX = 1f;
    public AudioClip clipJump;
    public AudioClip clipJumpTrampoline;
    public AudioClip clipFootstep;
    private bool onGround = false;

    private Rigidbody myRigidbody;
    private Vector3 previousPositionOnGround;

    public TrailRenderer playerTrail;
    void Start()
    {
        previousPositionOnGround = transform.position;
        myRigidbody = GetComponent<Rigidbody>();
    }
    public void SetTrailColor(Color myColor) {
        playerTrail.endColor = myColor;
        playerTrail.startColor = myColor;
    }
    float jumpImpulseCurrent;
    private void Update() {
        if (GameManager.energyPowerUp) {
            jumpImpulseCurrent = jumpImpulseEnergy;
        } else {
            jumpImpulseCurrent = jumpImpulse;
        }
        if(onGround && GameManager.playerInputAction.Player.Jump.WasPressedThisFrame() && Time.timeScale >= 0.1f) {
            canDoubleJump = true;
            myRigidbody.AddForce(Vector3.up * jumpImpulseCurrent, ForceMode.Impulse);
            GameManager.SpawnLoudAudio(clipJump);
        }
        if(myRigidbody.linearVelocity.y > 0.1f && GameManager.playerInputAction.Player.Jump.WasReleasedThisFrame()) {
            myRigidbody.linearVelocity = new Vector3(myRigidbody.linearVelocity.x, myRigidbody.linearVelocity.y * 0.5f, myRigidbody.linearVelocity.z);
        }

        // trampoline jump
        if(GameManager.tallLevel) {
            if (onGround == false && canDoubleJump == true && GameManager.playerInputAction.Player.Jump.WasPressedThisFrame() && Time.timeScale >= 0.1f) {
                canDoubleJump = false;
                myRigidbody.linearVelocity = new Vector3(myRigidbody.linearVelocity.x, 0f, myRigidbody.linearVelocity.z);
                myRigidbody.AddForce(Vector3.up * trampolineImpulse, ForceMode.Impulse);
                GameManager.SpawnLoudAudio(clipJumpTrampoline);
            }

        }
    }
    public void TrampolineJump() {
        myRigidbody.AddForce(Vector3.up * trampolineImpulse, ForceMode.Impulse);
        GameManager.SpawnLoudAudio(clipJumpTrampoline);
    }
    float moveHorizontalVelocityCurrent;
    float maxHorizontalVelocityCurrent;
    float gravityCurrent;
    float horizontalDragCurrent;
    private void FixedUpdate() {

        if (GameManager.energyPowerUp) {
            moveHorizontalVelocityCurrent = moveHorizontalVelocityEnergy;
            maxHorizontalVelocityCurrent = maxHorizontalVelocityEnergy;
            gravityCurrent = gravityEnergy;
            horizontalDragCurrent = horizontalDragEnergy;
        } else {
            moveHorizontalVelocityCurrent = moveHorizontalVelocity;
            maxHorizontalVelocityCurrent = maxHorizontalVelocity;
            gravityCurrent = gravity;
            horizontalDragCurrent = horizontalDrag;
        }

        if (myRigidbody.position.x < -9f) {
            myRigidbody.position = new Vector3(-9f, myRigidbody.position.y, myRigidbody.position.z);
        } else if (myRigidbody.position.x > 9f) {
            myRigidbody.position = new Vector3(9f, myRigidbody.position.y, myRigidbody.position.z);
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
            myRigidbody.AddForce(Vector3.down * gravityCurrent, ForceMode.Force);
        }

        if(onGround == true && Vector3.Distance(previousPositionOnGround, transform.position) > footstepDistanceToSFX) {
            previousPositionOnGround = transform.position;
            // GameManager.SpawnLoudAudio(clipFootstep, new Vector2(0.9f, 1.2f));
        }

        Vector2 moveAxis = GameManager.playerInputAction.Player.Move.ReadValue<Vector2>();
        if (moveAxis.x > 0.1f) {
            myRigidbody.AddForce(Vector3.right * moveHorizontalVelocityCurrent * Mathf.Abs(moveAxis.x), ForceMode.Force);
        } else if (moveAxis.x < -0.1f) {
            myRigidbody.AddForce(Vector3.left * moveHorizontalVelocityCurrent * Mathf.Abs(moveAxis.x), ForceMode.Force);
        }
        float horizontalVelocity = Mathf.Clamp(myRigidbody.linearVelocity.x, -maxHorizontalVelocityCurrent, maxHorizontalVelocityCurrent);
        horizontalVelocity *= horizontalDragCurrent;
        myRigidbody.linearVelocity = new Vector3(horizontalVelocity, myRigidbody.linearVelocity.y, myRigidbody.linearVelocity.z);
    }
}
