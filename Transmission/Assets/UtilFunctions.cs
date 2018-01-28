using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilFunctions : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void TransitScene(string sceneName)
    {
        TCUtils.TCSceneTransitionHelper.Instance.StartLoadingScene(sceneName);
    }
}
