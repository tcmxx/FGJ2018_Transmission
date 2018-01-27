using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour {


    public static GamePlayUI Instance { get; private set; }

    public GameObject normalCursorRef;
    public GameObject interactableCursorRef;
    public Text cursorPromptTextRef;


    public enum CursorType
    {
        Normal,
        Interactive
    }

    private void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start () {
        SetCursor(CursorType.Normal);

    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void SetCursor(CursorType type, string message = null) {
        if(type == CursorType.Interactive)
        {
            normalCursorRef.SetActive(false);
            interactableCursorRef.SetActive(true);
        }else if(type == CursorType.Normal)
        {
            normalCursorRef.SetActive(true);
            interactableCursorRef.SetActive(false);
        }

        if(message != null)
        {
            cursorPromptTextRef.text = message;
        }
    }


}
