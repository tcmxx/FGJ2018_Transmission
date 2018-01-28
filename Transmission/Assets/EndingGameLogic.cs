using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingGameLogic : MonoBehaviour {
    public MovePath path;
    public Transform finalLookUpTransform;
    public Transform playerModifyTransform;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartEnding()
    {

        


        StartCoroutine(EndingCoroutine());
    }


    IEnumerator EndingCoroutine()
    {
        
        Transform t = path.paths[0];
        t.position = PlayerController.Instance.transform.position;
        path.EvaluatePath();
        PlayerInput.Instance.enabled = false;

        /*//start the rot
        Quaternion startO = Curves.LerpOrientation(path.points, 0);
        t.rotation = startO;
        PlayerController.Instance.GetComponentInChildren<MouseLook>().SmoothMoveRotTo(t.forward, 3, 2);
        yield return new WaitForSeconds(5);
        //start the move
        path.autoMovingObject = PlayerController.Instance.gameObject;
        PlayerController.Instance.GetComponent<CharacterController>().enabled = false;
        PlayerInput.Instance.enabled = false;
        path.StartMove(PlayerController.Instance.gameObject, 10);

        yield return new WaitForSeconds(10);
        */
        //look up the sky
        PlayerController.Instance.GetComponentInChildren<MouseLook>().SmoothMoveRotTo(finalLookUpTransform.forward, 5, 2);
        yield return new WaitForSeconds(5);
        foreach (var u in GamePlayUI.Instance.creditUIs)
        {
            u.gameObject.SetActive(true);
            u.FadeInOut(2, 2, 2, null);
            yield return new WaitForSeconds(6);
            u.gameObject.SetActive(false);
        }
        GamePlayUI.Instance.StartMenu();
        yield return null;
    }
}
