using UnityEngine;
using System.Collections;

public class Shoulders : MonoBehaviour {

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

        Vector3 pos1 = kin.GetJointPos( KinectWrapper.Joints.SHOULDER_LEFT );
        Vector3 pos2 = kin.GetJointPos( KinectWrapper.Joints.SHOULDER_RIGHT );
        Vector3 vec = pos2 - pos1;
        //vec.z = -1 * vec.z;
        vec.y = 0;
        vec = Vector3.Cross( Vector3.up, vec );
        vec.z = -1 * vec.z;
        //Debug.Log( vec.normalized + new Vector3(0, 1, 0));
		transform.LookAt((transform.position + vec.normalized), new Vector3(0, 1, 0));
					
	}
}
