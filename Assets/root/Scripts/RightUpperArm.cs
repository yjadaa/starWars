using UnityEngine;
using System.Collections;

public class RightUpperArm : MonoBehaviour {

	KUInterface kin;
	bool isInitialized = false;
	Vector3 lastVec = new Vector3(0, 0, 0);
	
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

        Vector3 pos1 = kin.GetJointPos( KinectWrapper.Joints.SHOULDER_RIGHT );
		Vector3 pos2 = kin.GetJointPos(KinectWrapper.Joints.ELBOW_RIGHT);
        Vector3 vec = pos2 - pos1;
		vec.z = -1 * vec.z;
		vec = vec.normalized;
		
		Vector3 diffHandsPos = vec - lastVec;	
		
		transform.LookAt((transform.position + lastVec + 0.3f * diffHandsPos), new Vector3(0, 1, 0));
		lastVec = vec;
					
	}
}
