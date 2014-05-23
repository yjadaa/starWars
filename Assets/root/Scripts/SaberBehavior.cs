using UnityEngine;
using System.Collections;

public class SaberBehavior : MonoBehaviour {

    public AudioClip SaberMove;

	private KUInterface kin;
	private bool isInitialized = false;
    private Vector3 lastHandPos = new Vector3( 0, 0, 0 );
	
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
		
		//Vector3 handVector = new Vector3(10, -150, 0);
		Vector3 leftHandPos = kin.GetJointPos(KinectWrapper.Joints.HAND_LEFT);
		Vector3 rightHandPos = kin.GetJointPos(KinectWrapper.Joints.HAND_RIGHT);
		float distance = Vector3.Distance(leftHandPos,rightHandPos) / 1000;
		//Vector3 shoulderCenterPos = kin.GetJointPos(KinectWrapper.Joints.SHOULDER_CENTER);
		//float handstoShoulderCenterDistance = Vector3.Distance(rightHandPos,shoulderCenterPos) / 1000;
		//Debug.Log(distance);
		if(distance < 5f) {
            Vector3 difference = rightHandPos - leftHandPos;
            Vector3 negZDifference = new Vector3( difference.x , difference.y, difference.z );

            negZDifference.z = -1 * negZDifference.z;
            Vector3 bothHandsPos = transform.position + negZDifference.normalized;
            Vector3 diffHandsPos = bothHandsPos - lastHandPos;
            transform.LookAt( lastHandPos + 0.3f * diffHandsPos, new Vector3( 0, 1, 0 ) );
            lastHandPos = lastHandPos + 0.3f * diffHandsPos;

            Debug.Log( Vector3.Magnitude( diffHandsPos ) );

            if ( Vector3.Magnitude( diffHandsPos ) > 0.45 )
            {
                audio.PlayOneShot( SaberMove );
            }



            Vector3 centerHands = rightHandPos - 0.5f * difference;
            Vector3 shoulderCenterPos = kin.GetJointPos( KinectWrapper.Joints.SHOULDER_CENTER );
            centerHands.y = 0;
            shoulderCenterPos.y = 0;
            Vector3 shoulderToHands = centerHands - shoulderCenterPos;
            shoulderToHands.z = -shoulderToHands.z;

            transform.localPosition = 0.75f * shoulderToHands.normalized;
            //Debug.Log( transform.localPosition + " " + transform.position + " " + shoulderToHands.normalized );

		}			
	}
}
