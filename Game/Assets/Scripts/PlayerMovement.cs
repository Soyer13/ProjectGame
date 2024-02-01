using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
   
    public float jumpPower = 7f;
    public float gravity = 10f;

    [SerializeField] KeyCode ShootButton;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] GameObject Bullet;
    private bool isShoot = true;

    [SerializeField] KeyCode DashKey;
    [SerializeField] float DashSpeed;
    private bool isDashReady = true;
    


    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    Vector3 forward;
    Vector3 right;
    float curSpeedX;
    float curSpeedY;
    CharacterController characterController;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (GameManager.instance.isPlayerDead == false && GameManager.instance.isGameStoped == false)
        { 
            #region Handles Movment
            forward = transform.TransformDirection(Vector3.forward);
            right = transform.TransformDirection(Vector3.right);



            curSpeedX = canMove ? walkSpeed * Input.GetAxis("Vertical") : 0;
            curSpeedY = canMove ? walkSpeed * Input.GetAxis("Horizontal") : 0;
            float movementDirectionY = moveDirection.y;
            if (Input.GetKey(DashKey) && isDashReady)
            {
                StartCoroutine(Dash());
            }
            else
            {
                moveDirection = (forward * curSpeedX) + (right * curSpeedY);
            }

            #endregion

            #region Handles Jumping
            if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
            {
                moveDirection.y = jumpPower;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            #endregion

            #region Handles Rotation
            characterController.Move(moveDirection * Time.deltaTime);

            if (canMove)
            {
                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }

            #endregion

            #region Shooting
            if(GameManager.instance.isGameStoped != true)
            {
                if(Input.GetKey(ShootButton) && isShoot)
                {
                    StartCoroutine(shoot());
                }
            }
            

            #endregion
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        

    }
    IEnumerator shoot()
    {
        Debug.Log("Shoot");
        isShoot = false;       
        GameObject bulletInst = Instantiate(Bullet, bulletSpawn.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(0.5f);
        isShoot = true;

    }
    IEnumerator Dash()
    {
        Debug.Log("Dash");
        isDashReady = false;
        moveDirection = (forward * curSpeedX * DashSpeed) + (right * curSpeedY);
        yield return new WaitForSeconds(3);
        isDashReady = true;
    }
}