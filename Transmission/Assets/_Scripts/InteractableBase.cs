using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Fungus;

public class InteractableBase : MonoBehaviour {


    public string promptText = "Click to interact";
    [SerializeField]
    private UnityEvent onClicked;

    public bool enableInteraction = true;
    public bool disableAfterClicked = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public virtual void OnClicked()
    {
        if(enableInteraction)
            onClicked.Invoke();
        if (disableAfterClicked)
            enableInteraction = false;
    }

    public void SendFungusMessage(string message)
    {
        print("Broad cast fungus: " + message);
        Flowchart.BroadcastFungusMessage(message);
    }
}
