using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerInput : MonoBehaviour {

    protected CharacterController characterController;

    public string horizontalAxis;
    public string verticalAxis;

    public float horizontalSpeed = 3;
    public float verticalSpeed = 3;

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

    }
}
