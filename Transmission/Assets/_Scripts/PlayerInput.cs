using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Fungus;

[RequireComponent(typeof(CharacterController))]
public class PlayerInput : MonoBehaviour {

    protected CharacterController characterController;

    public string horizontalAxis;
    public string verticalAxis;
    public string testButton = "Test";

    public float horizontalSpeed = 3;
    public float verticalSpeed = 3;
    public MouseLook mouseLookRef;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        characterController.SimpleMove(
            transform.TransformDirection(
                new Vector3(Input.GetAxisRaw(horizontalAxis)* horizontalSpeed, 0, Input.GetAxisRaw(verticalAxis)* verticalSpeed)));

        mouseLookRef.Rotate(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        if (Input.GetButtonDown(testButton))
        {
            Test();
        }
    }


    void Test()
    {
        Flowchart.BroadcastFungusMessage("test");
    }
}
