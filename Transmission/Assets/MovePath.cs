using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovePath : MonoBehaviour {


    public List<Transform> paths;

    public UnityEvent onPathFinished;
    public bool autoTurning = true;
    public float turningRadius = 0.2f;

    public List<Curves.CurvePoint> points;

    public enum PathMode
    {
        Direct,
        Bezier,
        Bspline,
        CRspline
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EvaluatePath(int steps)
    {
        //Curves.eve
    }


    public void StartMove(GameObject obj)
    {

    }

    public void StopMove(GameObject obj)
    {

    }

    public void ResetMove()
    {
        
    }
}
