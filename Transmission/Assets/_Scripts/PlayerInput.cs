using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Fungus;

[RequireComponent(typeof(CharacterController))]
public class PlayerInput : MonoBehaviour {

    protected CharacterController characterController;

    public Texture2D cursorTexture;

    public string horizontalAxis;
    public string verticalAxis;
    public string testButton = "Test";

    public float horizontalSpeed = 3;
    public float verticalSpeed = 3;
    public MouseLook mouseLookRef;

    private bool mouseMoveEnabled = true;
    private bool movementEnabled = true;
    private bool mouseClickEnabled = true;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    // Use this for initialization
    void Start () {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        DisableMouseCursor();

    }
	
	// Update is called once per frame
	void Update () {
        if (movementEnabled)
        {
            characterController.SimpleMove(
                transform.TransformDirection(
                    new Vector3(Input.GetAxisRaw(horizontalAxis) * horizontalSpeed, 0, Input.GetAxisRaw(verticalAxis) * verticalSpeed)));
        }
        if (mouseMoveEnabled)
        {
            mouseLookRef.Rotate(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        }
        if (mouseClickEnabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlayerController.Instance.OnInteract();
            }
        }
        if (Input.GetButtonDown(testButton))
        {
            Test();
        }

    }

    public void EnableMouseCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void DisableMouseCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void SetMouseMoveEnable(bool enable)
    {
        mouseMoveEnabled = enable;
    }
    public void SetMovementEnable(bool enable)
    {
        movementEnabled = enable;
        GamePlayUI.Instance.SetMiddleCursorEnable(enable);
        mouseClickEnabled = enable;
    }
    void Test()
    {
        Flowchart.BroadcastFungusMessage("test");
    }
}
