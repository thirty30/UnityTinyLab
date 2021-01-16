using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKController : MonoBehaviour
{
    public bool IKActive = false;
    public AvatarIKGoal IKGoal;
    public float PositionWeight = 1;
    public float RotationWeight = 1;
    public GameObject Target;

    private Animator mAnimator;
    // Start is called before the first frame update
    void Start()
    {
        this.mAnimator = this.GetComponentInChildren<Animator>();
    }

    private void OnAnimatorIK()
    {
        if (IKActive == false)
        {
            this.mAnimator.SetIKPositionWeight(this.IKGoal, 0);
            this.mAnimator.SetIKRotationWeight(this.IKGoal, 0);
            this.mAnimator.SetIKPosition(this.IKGoal, Vector3.zero);
            this.mAnimator.SetIKRotation(this.IKGoal, Quaternion.Euler(0, 0, 0));
            return;
        }

        this.mAnimator.SetIKPositionWeight(this.IKGoal, this.PositionWeight);
        this.mAnimator.SetIKRotationWeight(this.IKGoal, this.RotationWeight);
        this.mAnimator.SetIKPosition(this.IKGoal, this.Target.transform.position);
        this.mAnimator.SetIKRotation(this.IKGoal, this.Target.transform.rotation);
    }
}
