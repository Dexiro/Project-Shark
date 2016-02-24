using UnityEngine;
using System.Collections;

public class FPSController : MonoBehaviour {

    //Public
    public float moveSpeed = 5.0f;
    public float mouseSpeed = 2.5f;
    public float lookRange = 75.0f;
    public float jumpSpeed = 0.1f;

    public Camera playerCamera;

    //Private
    CharacterController charControl;
    float verticalRotation = 0.0f;
    float verticalVelocity = 0.0f;


    //Initialization
    void Start ()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.lockState = CursorLockMode.Locked;

        charControl = GetComponent<CharacterController>();
    }
	
	//Called once per frame
	void Update ()
    {
        ////Rotation
        //Mouse Horizontal
        float rotLeftRight = Input.GetAxis("Mouse X") * mouseSpeed;
        transform.Rotate(0.0f, rotLeftRight, 0.0f);

        //Mouse Vertical
        float rotUpDown = Input.GetAxis("Mouse Y") * mouseSpeed;
        verticalRotation -= rotUpDown * mouseSpeed;
        verticalRotation = Mathf.Clamp(verticalRotation, -lookRange, lookRange);
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0.0f, 0.0f);

        ////Movement
        float forwardSpeed = Input.GetAxis("Vertical") * moveSpeed;
        float sideSpeed = Input.GetAxis("Horizontal") * moveSpeed;

        //Jump + Gravity
        if(charControl.isGrounded)
        {
            verticalVelocity = 0.0f;

            if(Input.GetKey(KeyCode.Space))
            {
                verticalVelocity = jumpSpeed;
            }
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        Debug.Log(verticalVelocity);

        //Character Controller
        Vector3 finalSpeed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed);
        finalSpeed = transform.rotation * finalSpeed;
        charControl.Move(finalSpeed * Time.deltaTime);
    }
}
