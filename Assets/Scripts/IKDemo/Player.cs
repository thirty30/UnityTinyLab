using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Ladder LadderObj;
    public IKController mIKCtrRightHand;
    public IKController mIKCtrLeftHand;
    public IKController mIKCtrRightFoot;
    public IKController mIKCtrLeftFoot;

    private bool LeftOrRight = true;
    private int LeftHandStairIDX = 5;
    private int LeftFootStairIDX = 0;
    private int RightHandStairIDX = 6;
    private int RightFootStairIDX = 1;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            this.StartClimb();
        }

        if (Input.GetKeyDown(KeyCode.W) == true)
        {
            StartCoroutine(this.ClimbUpBody());
            StartCoroutine(this.ClimbUpHand());
            StartCoroutine(this.ClimbUpFoot());
        }

        if (Input.GetKeyDown(KeyCode.S) == true)
        {
            StartCoroutine(this.ClimbDownHand());
            StartCoroutine(this.ClimbDownBody());
            StartCoroutine(this.ClimbDownFoot());
        }
    }

    private void StartClimb()
    {
        if (this.LadderObj == null)
        {
            return;
        }

        this.transform.forward = this.LadderObj.transform.forward;
        this.transform.position = this.LadderObj.transform.position - this.transform.forward * 1.6f + new Vector3(0, 0.3f, 0);

        this.mIKCtrLeftFoot.IKActive = true;
        this.mIKCtrLeftFoot.PositionWeight = 1;
        this.mIKCtrLeftFoot.RotationWeight = 1;
        this.mIKCtrLeftFoot.Target = this.LadderObj.Stairs[this.LeftFootStairIDX].LeftPosition;

        this.mIKCtrRightFoot.IKActive = true;
        this.mIKCtrRightFoot.PositionWeight = 1;
        this.mIKCtrRightFoot.RotationWeight = 1;
        this.mIKCtrRightFoot.Target = this.LadderObj.Stairs[this.RightFootStairIDX].RightPosition;

        this.mIKCtrLeftHand.IKActive = true;
        this.mIKCtrLeftHand.PositionWeight = 1;
        this.mIKCtrLeftHand.RotationWeight = 1;
        this.mIKCtrLeftHand.Target = this.LadderObj.Stairs[this.LeftHandStairIDX].LeftPosition;

        this.mIKCtrRightHand.IKActive = true;
        this.mIKCtrRightHand.PositionWeight = 1;
        this.mIKCtrRightHand.RotationWeight = 1;
        this.mIKCtrRightHand.Target = this.LadderObj.Stairs[this.RightHandStairIDX].RightPosition;
    }

    IEnumerator ClimbUpBody()
    {
        Vector3 dir = this.LadderObj.Stairs[1].transform.position - this.LadderObj.Stairs[0].transform.position;
        dir.Normalize();
        Vector3 oriPosition = this.transform.position;
        float velocity = 2.0f;

        while (true)
        {
            if (Vector3.Distance(this.transform.position, oriPosition + dir * 1.0f) < 0.05f)
            {
                this.transform.position = oriPosition + dir * 1.0f;
                break;
            }
            this.transform.forward = this.LadderObj.transform.forward;
            this.transform.position += dir * Time.deltaTime * velocity;
            yield return 0;
        }
    }

    IEnumerator ClimbUpHand()
    {
        yield return new WaitForSeconds(0.25f);
        float velocity = 4.0f;

        if (this.LeftOrRight == true)
        {
            GameObject obj = new GameObject();
            obj.transform.position = this.mIKCtrLeftHand.Target.transform.position;
            obj.transform.rotation = this.mIKCtrLeftHand.Target.transform.rotation;
            this.mIKCtrLeftHand.Target = obj;
            this.LeftHandStairIDX += 2;

            while (true)
            {
                Vector3 dir = this.LadderObj.Stairs[this.LeftHandStairIDX].LeftPosition.transform.position - obj.transform.position;
                if (dir.magnitude <= 0.05f)
                {
                    this.mIKCtrLeftHand.Target = this.LadderObj.Stairs[this.LeftHandStairIDX].LeftPosition;
                    break;
                }
                obj.transform.position += dir.normalized * Time.deltaTime * velocity;
                yield return 0;
            }
            this.LeftOrRight = false;
        }
        else
        {
            GameObject obj = new GameObject();
            obj.transform.position = this.mIKCtrRightHand.Target.transform.position;
            obj.transform.rotation = this.mIKCtrRightHand.Target.transform.rotation;
            this.mIKCtrRightHand.Target = obj;
            this.RightHandStairIDX += 2;

            while (true)
            {
                Vector3 dir = this.LadderObj.Stairs[this.RightHandStairIDX].RightPosition.transform.position - obj.transform.position;
                if (dir.magnitude <= 0.05f)
                {
                    this.mIKCtrRightHand.Target = this.LadderObj.Stairs[this.RightHandStairIDX].RightPosition;
                    break;
                }
                obj.transform.position += dir.normalized * Time.deltaTime * velocity;
                yield return 0;
            }
            this.LeftOrRight = true;
        }
    }

    IEnumerator ClimbUpFoot()
    {
        yield return new WaitForSeconds(0.35f);
        float velocity = 3.0f;

        if (this.LeftOrRight == true)
        {
            GameObject obj = new GameObject();
            obj.transform.position = this.mIKCtrLeftFoot.Target.transform.position;
            obj.transform.rotation = this.mIKCtrLeftFoot.Target.transform.rotation;
            this.mIKCtrLeftFoot.Target = obj;
            this.LeftFootStairIDX += 2;
            while (true)
            {
                Vector3 dir = this.LadderObj.Stairs[this.LeftFootStairIDX].LeftPosition.transform.position - obj.transform.position;
                if (dir.magnitude <= 0.05f)
                {
                    this.mIKCtrLeftFoot.Target = this.LadderObj.Stairs[this.LeftFootStairIDX].LeftPosition;
                    break;
                }
                obj.transform.position += dir.normalized * Time.deltaTime * velocity;
                yield return 0;
            }
            this.LeftOrRight = false;
        }
        else
        {
            GameObject obj = new GameObject();
            obj.transform.position = this.mIKCtrRightFoot.Target.transform.position;
            obj.transform.rotation = this.mIKCtrRightFoot.Target.transform.rotation;
            this.mIKCtrRightFoot.Target = obj;
            this.RightFootStairIDX += 2;
            while (true)
            {
                Vector3 dir = this.LadderObj.Stairs[this.RightFootStairIDX].RightPosition.transform.position - obj.transform.position;
                if (dir.magnitude <= 0.05f)
                {
                    this.mIKCtrRightFoot.Target = this.LadderObj.Stairs[this.RightFootStairIDX].RightPosition;
                    break;
                }
                obj.transform.position += dir.normalized * Time.deltaTime * velocity;
                yield return 0;
            }
            this.LeftOrRight = true;
        }
    }

    IEnumerator ClimbDownBody()
    {
        yield return new WaitForSeconds(0.4f);
        float velocity = 2.0f;

        Vector3 dir = this.LadderObj.Stairs[0].transform.position - this.LadderObj.Stairs[1].transform.position;
        dir.Normalize();
        Vector3 oriPosition = this.transform.position;

        while (true)
        {
            if (Vector3.Distance(this.transform.position, oriPosition + dir * 1.0f) < 0.05f)
            {
                this.transform.position = oriPosition + dir * 1.0f;
                break;
            }
            this.transform.forward = this.LadderObj.transform.forward;
            this.transform.position += dir * Time.deltaTime * velocity;
            yield return 0;
        }
    }

    IEnumerator ClimbDownHand()
    {
        float velocity = 4.0f;

        if (this.LeftOrRight == true)
        {
            GameObject obj = new GameObject();
            obj.transform.position = this.mIKCtrRightHand.Target.transform.position;
            obj.transform.rotation = this.mIKCtrRightHand.Target.transform.rotation;
            this.mIKCtrRightHand.Target = obj;
            this.RightHandStairIDX -= 2;

            while (true)
            {
                Vector3 dir = this.LadderObj.Stairs[this.RightHandStairIDX].RightPosition.transform.position - obj.transform.position;
                if (dir.magnitude <= 0.05f)
                {
                    this.mIKCtrRightHand.Target = this.LadderObj.Stairs[this.RightHandStairIDX].RightPosition;
                    break;
                }
                obj.transform.position += dir.normalized * Time.deltaTime * velocity;
                yield return 0;
            }
            this.LeftOrRight = false;
        }
        else
        {
            GameObject obj = new GameObject();
            obj.transform.position = this.mIKCtrLeftHand.Target.transform.position;
            obj.transform.rotation = this.mIKCtrLeftHand.Target.transform.rotation;
            this.mIKCtrLeftHand.Target = obj;
            this.LeftHandStairIDX -= 2;

            while (true)
            {
                Vector3 dir = this.LadderObj.Stairs[this.LeftHandStairIDX].LeftPosition.transform.position - obj.transform.position;
                if (dir.magnitude <= 0.05f)
                {
                    this.mIKCtrLeftHand.Target = this.LadderObj.Stairs[this.LeftHandStairIDX].LeftPosition;
                    break;
                }
                obj.transform.position += dir.normalized * Time.deltaTime * velocity;
                yield return 0;
            }
            this.LeftOrRight = true;
        }
    }

    IEnumerator ClimbDownFoot()
    {
        yield return new WaitForSeconds(0.4f);
        float velocity = 3.0f;

        if (this.LeftOrRight == true)
        {
            GameObject obj = new GameObject();
            obj.transform.position = this.mIKCtrRightFoot.Target.transform.position;
            obj.transform.rotation = this.mIKCtrRightFoot.Target.transform.rotation;
            this.mIKCtrRightFoot.Target = obj;
            this.RightFootStairIDX -= 2;
            while (true)
            {
                Vector3 dir = this.LadderObj.Stairs[this.RightFootStairIDX].RightPosition.transform.position - obj.transform.position;
                if (dir.magnitude <= 0.05f)
                {
                    this.mIKCtrRightFoot.Target = this.LadderObj.Stairs[this.RightFootStairIDX].RightPosition;
                    break;
                }
                obj.transform.position += dir.normalized * Time.deltaTime * velocity;
                yield return 0;
            }
            this.LeftOrRight = false;
        }
        else
        {
            GameObject obj = new GameObject();
            obj.transform.position = this.mIKCtrLeftFoot.Target.transform.position;
            obj.transform.rotation = this.mIKCtrLeftFoot.Target.transform.rotation;
            this.mIKCtrLeftFoot.Target = obj;
            this.LeftFootStairIDX -= 2;
            while (true)
            {
                Vector3 dir = this.LadderObj.Stairs[this.LeftFootStairIDX].LeftPosition.transform.position - obj.transform.position;
                if (dir.magnitude <= 0.05f)
                {
                    this.mIKCtrLeftFoot.Target = this.LadderObj.Stairs[this.LeftFootStairIDX].LeftPosition;
                    break;
                }
                obj.transform.position += dir.normalized * Time.deltaTime * velocity;
                yield return 0;
            }
            this.LeftOrRight = true;
        }
    }
}
