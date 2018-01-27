using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Fungus;

public class InteractableBase : MonoBehaviour {


    public string promptText = "Click to interact";
    [SerializeField]
    private UnityEvent onClicked;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public virtual void OnClicked()
    {
        onClicked.Invoke();
    }

    public void SendFungusMessage(string message)
    {
        print("Broad cast fungus: " + message);
        Flowchart.BroadcastFungusMessage(message);
    }
}
