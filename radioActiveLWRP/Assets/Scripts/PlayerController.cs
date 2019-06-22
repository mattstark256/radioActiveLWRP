using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerData playerData = null;

    float lookY;
    float smoothLookX;
    float smoothLookY;
    Camera cameraComponent;
    CharacterController characterController;
    Vector3 moveDirection;


    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        cameraComponent = GetComponentInChildren<Camera>();
    }


    void Update()
    {
        if (playerData.inputEnabled)
        {
            Look();
        }
    }


    private void FixedUpdate()
    {
        if (playerData.inputEnabled)
        {
            Move();
        }
    }


    void Look()
    {
        // Mouse player turning
        float lookX = Input.GetAxisRaw("Mouse X") * playerData.lookSpeed;
        smoothLookX = Mathf.Lerp(smoothLookX, lookX, playerData.lookSmoothing);
        transform.Rotate(0, smoothLookX, 0);

        // Mouse camera pitch
        lookY -= Input.GetAxisRaw("Mouse Y") * playerData.lookSpeed;
        smoothLookY = Mathf.Lerp(smoothLookY, lookY, playerData.lookSmoothing);
        smoothLookY = Mathf.Clamp(smoothLookY, -90, 90);
        cameraComponent.transform.localRotation = Quaternion.Euler(smoothLookY, 0, 0);
    }


    void Move()
    {

        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= playerData.moveSpeed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = playerData.jumpSpeed;
            }
        }

        // Apply gravity
        moveDirection.y = moveDirection.y - (playerData.gravity * Time.deltaTime);

        characterController.Move(moveDirection * Time.deltaTime);
    }


    public void SetInputEnabled(bool enabled)
    {
        playerData.inputEnabled = enabled;
    }
}
