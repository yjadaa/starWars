using UnityEngine;
using System.Collections;

public class LeftForearm : MonoBehaviour {

	KUInterface kin;
	bool isInitialized = false;
	
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
	
        Vector3 elbowPos = kin.GetJointPos( KinectWrapper.Joints.ELBOW_LEFT );
		Vector3 handPos = kin.GetJointPos(KinectWrapper.Joints.HAND_LEFT);
        Vector3 handVector = handPos - elbowPos;
		handVector.z = -1 * handVector.z;
		transform.LookAt((transform.position + handVector.normalized), new Vector3(0, 1, 0));
					
	}
}
