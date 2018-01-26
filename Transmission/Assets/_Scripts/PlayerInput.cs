using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerInput : MonoBehaviour {

    protected CharacterController characterController;

    public string horizontalAxis;
    public string verticalAxis;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        characterController.SimpleMove(new Vector3(Input.GetAxisRaw(horizontalAxis), 0, Input.GetAxisRaw(verticalAxis)));

    }
}
