using UnityEngine;
using System.Collections;

public class GestureRecognizer : MonoBehaviour {

    KUInterface kin;
    bool isInitialized = false;

    public enum ARM_STATE
    {
        AS_POS_Z,
        AS_NEG_Z,
        AS_POS_X,
        AS_NEG_X,
        AS_POS_Y,
        AS_NEG_Y,
        AS_NONE
    };

    public ARM_STATE rightArm;
    public ARM_STATE leftArm;
    public float STATE_THRESHOLD = 0.95f;

    // Use this for initialization
    void Start()
    {
        GameObject kinectContainer = GameObject.FindWithTag("Kinect");
        if (kinectContainer != null)
        {
            kin = kinectContainer.GetComponent<KUInterface>();
            isInitialized = true;
        }
	}

    // Update is called once per frame
    void Update()
    {
        if (!isInitialized) return;

        // Check for lightning
        Vector3 rightHand = kin.GetJointPos(KinectWrapper.Joints.HAND_RIGHT);
        Vector3 rightShoulder = kin.GetJointPos(KinectWrapper.Joints.SHOULDER_RIGHT);
        Vector3 rightVec = rightHand - rightShoulder;
        rightVec.z = -1 * rightVec.z;
        if (rightVec.z / rightVec.magnitude > STATE_THRESHOLD)
        {
            rightArm = ARM_STATE.AS_POS_Z;
        }
        else if (rightVec.z / rightVec.magnitude < -STATE_THRESHOLD)
        {
            rightArm = ARM_STATE.AS_NEG_Z;
        }
        else if (rightVec.x / rightVec.magnitude < -STATE_THRESHOLD)
        {
            rightArm = ARM_STATE.AS_NEG_X;
        }
        else if (rightVec.x / rightVec.magnitude > STATE_THRESHOLD)
        {
            rightArm = ARM_STATE.AS_POS_X;
        }
        else if (rightVec.y / rightVec.magnitude < -STATE_THRESHOLD)
        {
            rightArm = ARM_STATE.AS_NEG_Y;
        }
        else if (rightVec.y / rightVec.magnitude > STATE_THRESHOLD)
        {
            rightArm = ARM_STATE.AS_POS_Y;
        }
        else
        {
            rightArm = ARM_STATE.AS_NONE;
        }



        Vector3 leftHand = kin.GetJointPos(KinectWrapper.Joints.HAND_LEFT);
        Vector3 leftShoulder = kin.GetJointPos(KinectWrapper.Joints.SHOULDER_LEFT);
        Vector3 leftVec = leftHand - leftShoulder;
        leftVec.z = -1 * leftVec.z;
        if (leftVec.z / leftVec.magnitude > STATE_THRESHOLD)
        {
            leftArm = ARM_STATE.AS_POS_Z;
        }
        else if (leftVec.z / leftVec.magnitude < -STATE_THRESHOLD)
        {
            leftArm = ARM_STATE.AS_NEG_Z;
        }
        else if (leftVec.x / leftVec.magnitude < -STATE_THRESHOLD)
        {
            leftArm = ARM_STATE.AS_NEG_X;
        }
        else if (leftVec.x / leftVec.magnitude > STATE_THRESHOLD)
        {
            leftArm = ARM_STATE.AS_POS_X;
        }
        else if (leftVec.y / leftVec.magnitude < -STATE_THRESHOLD)
        {
            leftArm = ARM_STATE.AS_NEG_Y;
        }
        else if (leftVec.y / leftVec.magnitude > STATE_THRESHOLD)
        {
            leftArm = ARM_STATE.AS_POS_Y;
        }
        else
        {
            leftArm = ARM_STATE.AS_NONE;
        }

        //Debug.Log(leftVec + " " + leftArm + " " + rightArm + " " + rightVec);
    }
}
