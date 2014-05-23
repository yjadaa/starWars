using UnityEngine;
using System.Collections;

public class RightForearm : MonoBehaviour {

	KUInterface kin;
	bool isInitialized = false;
    public Vector3 handVector;
	Vector3 lastHandVec = new Vector3(0, 0, 0);
	
	// Use this for initialization
	void Start () {
		GameObject kinectContainer = GameObject.FindWithTag("Kinect");
		if(kinectContainer != null)
		{
			kin = kinectContainer.GetComponent<KUInterface>();
			isInitialized = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(!isInitialized) return;
	
        Vector3 elbowPos = kin.GetJointPos( KinectWrapper.Joints.ELBOW_RIGHT );
		Vector3 handPos = kin.GetJointPos(KinectWrapper.Joints.HAND_RIGHT);
        handVector = handPos - elbowPos;
		handVector.z = -1 * handVector.z;
		handVector = handVector.normalized;
		
		Vector3 diffHandsPos = handVector - lastHandVec;	
		
		transform.LookAt((transform.position + lastHandVec + 0.3f * diffHandsPos), new Vector3(0, 1, 0));
		lastHandVec = handVector;
	}
}
