using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour {


    public static GamePlayUI Instance { get; private set; }

    public GameObject normalCursorRef;
    public GameObject interactableCursorRef;
    public Text cursorPromptTextRef;

    public float skyTextFadeTime = 2;
    public TCUtils.TCFadingUI fadeUIRef;
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
        //FadoutSkyText();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FadinSkyText(string text)
    {
        fadeUIRef.FadeIn(skyTextFadeTime,null);
        fadeUIRef.GetComponentInChildren<Text>().text = text;
    }
    public void FadoutSkyText()
    {
        fadeUIRef.FadeOut(skyTextFadeTime, null);
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
