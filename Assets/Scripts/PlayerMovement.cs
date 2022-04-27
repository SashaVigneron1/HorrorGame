using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("General")]
    [SerializeField] bool isDebugging;
    bool canBeSeenByEnemies = false;

    bool isAlive = true;

    [Header("Respawning")]
    [SerializeField] float invincibleTime;
    float accInvincibleTime;
    bool justRespawned = false;

    [Header("Basic Movement")]
    [SerializeField] bool useJump;
    [SerializeField] float sprintSpeed;
    [SerializeField] float movementSpeed;
    [SerializeField] Rigidbody rb;

    [Header("Stamina")]
    [Tooltip("Minimum Stamina Required To Initiate Sprinting.")]
    [SerializeField] float minStaminaToSprint;
    [Tooltip("Sprint Time In Seconds.")]
    [SerializeField] float maxStamina;
    [Tooltip("Total Time In Seconds To Regenerate All Stamina")]
    [SerializeField] float staminaRegenTime;
    float stamina;
    bool isSprinting;

    [Header("Jumping")]
    [SerializeField] float maxJumpHeight;
    [SerializeField] float jumpSpeed;
    [SerializeField] float fallSpeed;
    float currMaxJumpHeight;
    bool inputJump = false;
    bool isOnGround = true;

    [Header("Rotation")]
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    [SerializeField] private Camera camera;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    [SerializeField] float rotateSpeed;
    [SerializeField] float minimumY = -60F;
    [SerializeField] float maximumY = 60F;
    float rotationX = 0F;
    float rotationY = 0F;
    Quaternion originalRotation;

    private void Start()
    {
        originalRotation = this.transform.rotation;

        stamina = maxStamina;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        HandleStamina();
        HandleMovement();
        if (useJump) HandleJump();
        HandleRotation();

        if (justRespawned)
        {
            canBeSeenByEnemies = false;

            accInvincibleTime += Time.deltaTime;
            if (accInvincibleTime >= invincibleTime)
            {
                justRespawned = false;
                isAlive = true;
                accInvincibleTime = 0.0f; 
            }
        }
    }

    #region MovementFunctions
    private void HandleStamina()
    {
        // Handle Stamina

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (stamina > minStaminaToSprint) isSprinting = true;
            else Debug.LogError("Not enough stamina!");
        }

        if (Input.GetKeyUp(KeyCode.LeftShift)) isSprinting = false;

        if (isSprinting)
        {
            stamina -= Time.deltaTime;
            if (stamina <= 0) isSprinting = false;
        }
        else
        {
            stamina += Time.deltaTime / staminaRegenTime;
        }

        stamina = Mathf.Clamp(stamina, 0.0f, 1.0f);
    }
    private void HandleMovement()
    {
        // Handle Movement
        float horizontalMovementSpeed = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        float verticalMovementSpeed = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;

        if (Input.GetAxis("Vertical") != 0 && Input.GetAxis("Horizontal") != 0)
        {
            // Change Movement Speed
            horizontalMovementSpeed *= Mathf.Sin(Mathf.Deg2Rad * 45.0f);
            verticalMovementSpeed *= Mathf.Sin(Mathf.Deg2Rad * 45.0f);
        }

        if (isSprinting)
        {
            horizontalMovementSpeed = horizontalMovementSpeed / movementSpeed * sprintSpeed;
            verticalMovementSpeed = verticalMovementSpeed / movementSpeed * sprintSpeed;
        }

        this.transform.Translate(horizontalMovementSpeed, 0.0f, verticalMovementSpeed, Space.Self);
    }
    private void HandleJump()
    {
        // Handle Jump

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isOnGround)
            {
                inputJump = true;
                currMaxJumpHeight = this.transform.position.y + maxJumpHeight;
            }
        }

        float verticalVelocity = 0.0f;

        if (inputJump && rb.velocity.y >= 0)
        {
            float distanceToMax = currMaxJumpHeight - transform.position.y;
            // Normalize
            distanceToMax = distanceToMax / maxJumpHeight;
            verticalVelocity += jumpSpeed * distanceToMax * Time.deltaTime;
            if (distanceToMax <= 0.1f) inputJump = false;
            if (isDebugging) Debug.Log("[Jumping] Distance To Max Jump Height: " + distanceToMax);
        }

        // Gravity
        if (!isOnGround && !inputJump) verticalVelocity -= fallSpeed * Time.deltaTime;

        transform.position += new Vector3(0, verticalVelocity, 0);

        isOnGround = Physics.Raycast(transform.position, Vector3.down, 1.01f);
    }
    private void HandleRotation()
    {
        //Handle Mouse Movement
        rotationX += Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
        rotationY += Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
        Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
        this.transform.localRotation = originalRotation * xQuaternion;
        camera.transform.localRotation = originalRotation * yQuaternion;
    }
    #endregion MovementFunctions

    #region Mutators
    public void SetCanBeSeenByEnemies(bool value)
    {
        canBeSeenByEnemies = value;
    }
    public bool CanBeSeenByEnemies()
    {
        return canBeSeenByEnemies;
    }
    public float GetStamina()
    {
        return stamina;
    }
    public float GetMaxStamina()
    {
        return maxStamina;
    }
    public float GetMinStaminaToSprint()
    {
        return minStaminaToSprint;
    }
    #endregion Mutators

    public void Respawn()
    {
        justRespawned = true;
    }
    public bool HasJustRespawned()
    {
        return justRespawned;
    }

    public void SetAlive(bool value)
    {
        isAlive = value;
    }

    public bool IsAlive()
    {
        return isAlive;
    }

}
