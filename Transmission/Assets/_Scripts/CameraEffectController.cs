using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffectController : MonoBehaviour {

    public static CameraEffectController Instance { get; private set; }


    public UnityStandardAssets.ImageEffects.BlurOptimized blurScriptRef;

    private float blurSpeed;
    private float blurTarget;

    private void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Mathf.Abs(blurScriptRef.blurSize- blurTarget) > 0.001f)
        {
            blurScriptRef.blurSize += Mathf.Sign(blurTarget - blurScriptRef.blurSize) * Mathf.Clamp(blurSpeed, 0, Mathf.Abs(blurScriptRef.blurSize - blurTarget));
            if(blurTarget > 0)
            {
                blurScriptRef.enabled = true;
            }
        }
        else
        {
            blurScriptRef.blurSize = blurTarget;
            if(blurTarget == 0)
            {
                blurScriptRef.enabled = false;
            }
        }
	}


    public void SetBlurSmooth(float target)
    {
        blurTarget = target;
    }
}
