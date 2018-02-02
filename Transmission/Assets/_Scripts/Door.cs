using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Door : MonoBehaviour {

    public InteractableBase interactableRef;
    public float openAngle = 90;
    public float rotateSpeed = 90;

    protected bool isOpen = false;
    protected Quaternion desiredRot;


    public string toCloseText = "Click to Close";
    public string toOpenText = "Click to Open";

    protected float startAngle;
	// Use this for initialization
	void Start () {
        interactableRef.promptText = isOpen? toCloseText:toOpenText;
        startAngle = transform.localRotation.eulerAngles.y;
    }
	
	// Update is called once per frame
	void Update () {
        if(transform.rotation != desiredRot)
            Rotating();

    }

    public void Toggle()
    {
        
        isOpen = !isOpen;
        interactableRef.promptText = isOpen ? toCloseText : toOpenText;
        desiredRot = Quaternion.AngleAxis(isOpen?openAngle+ startAngle: startAngle, Vector3.up);
        

    }


    protected void Rotating()
    {
        var rot = transform.rotation;
        transform.rotation = Quaternion.RotateTowards(rot, desiredRot, rotateSpeed * Time.deltaTime);
    }

}
