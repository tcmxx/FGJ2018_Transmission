using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static PlayerController Instance { get;private set; }

    protected Camera mainCamera;
    public float maxRaycastDistance = 50;
    public LayerMask raycastLayer;
    
    protected InteractableBase currentInteractable = null;
    private void Awake()
    {
        mainCamera = Camera.main;
        Instance = this;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RaycastCamera();
        if(currentInteractable != null)
        {
            GamePlayUI.Instance.SetCursor(GamePlayUI.CursorType.Interactive, currentInteractable.promptText);
        }
        else
        {
            GamePlayUI.Instance.SetCursor(GamePlayUI.CursorType.Normal,"");
        }


    }

    public void OnInteract()
    {
        if (currentInteractable != null)
        {
            currentInteractable.OnClicked();
            print("Interact with " + currentInteractable.name);
        }
    }
    
    private void RaycastCamera()
    {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo, maxRaycastDistance, raycastLayer, QueryTriggerInteraction.Collide)){
            currentInteractable = hitInfo.collider.gameObject.GetComponent<InteractableBase>();
            if(currentInteractable != null&& currentInteractable.enableInteraction == false)
            {
                currentInteractable = null;
            }
        }
        else
        {
            currentInteractable = null;
        }
        
    }
    



}
