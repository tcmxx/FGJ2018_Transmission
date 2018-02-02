using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ConstrainToCylinder : MonoBehaviour
{

	public enum ConstrainRadius
	{
		UseGlobalRadiusWithOffset,
		UseCustomizedRadiu
	}

	public ConstrainRadius constrainRadius = ConstrainRadius.UseGlobalRadiusWithOffset;
	/// <summary>
	/// The radiu parameter. When the constrainRadius is UseGlobalRadius, this value does not do anything
	/// When the constrainRadius is UseGlobalRadiusWithOffset, this value is the offset from static radius define in Cylinder
	///  When the constrainRadius is UseCustomizedRadiu, this value is the actual radius to use
	/// </summary>
	public float radiuParameter;
	public bool constrainOnlyInEditMode = false;
	public bool autoRotate = false;
    public bool softConstraint = true;
    public readonly float softConstraintT = 4;
	private bool prevAutoRot;
	private Quaternion intialRotate;


	void Start(){
		UpdateInitialRotation();
	}

	private void LateUpdate()
	{
		if (!(Application.isPlaying && constrainOnlyInEditMode)) {
            Vector3 targetPos;
            if (constrainRadius == ConstrainRadius.UseGlobalRadiusWithOffset) {
                targetPos = Cylinder.ProjectPosition (transform.position, Cylinder.Radius + radiuParameter);
			} else {
                targetPos = Cylinder.ProjectPosition (transform.position, radiuParameter);
			}

            if (softConstraint)
            {
                targetPos = Vector3.Lerp(transform.position, targetPos, softConstraintT * Time.deltaTime);
            }

            transform.position = targetPos;

            //update rotation
            if (autoRotate && !prevAutoRot) {
				UpdateInitialRotation ();
			}
			if (autoRotate) {
				Vector3 center = Vector3.zero;
				center.y = transform.position.y;

				Quaternion rotLook = Quaternion.LookRotation ((center - transform.position).normalized);
				transform.rotation = rotLook * intialRotate;
			}

			prevAutoRot = autoRotate;
		}

	}



	private void UpdateInitialRotation(){
		Quaternion rot = transform.rotation;
		Vector3 center = Vector3.zero;
		center.y = transform.position.y;

		Quaternion rotLook = Quaternion.LookRotation ((center - transform.position).normalized);
		intialRotate = Quaternion.Inverse (rotLook)*rot;
	}
}
