using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Light))]
public class LightIntensityChangeRepeat : MonoBehaviour {


    public AnimationCurve changeCurve;
    public float cyclingTime;
    protected Light light;
    protected float currentTime = 0;
    private void Awake()
    {
        light = GetComponent<Light>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        currentTime += Time.deltaTime;
        if(currentTime >= cyclingTime)
        {
            currentTime = currentTime - cyclingTime;
        }
        light.intensity = changeCurve.Evaluate(Mathf.Clamp01(currentTime / cyclingTime));
    }



}
