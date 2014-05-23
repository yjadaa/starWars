using UnityEngine;
using System.Collections;

public class MyGUI : MonoBehaviour {

    private Rect laserDeflectedPos;
    private Rect hitsRemainingPos;

    private PlayerBehavior playerState;

	// Use this for initialization
	void Start () {
        int h = Screen.height;
        //int w = Screen.width;

        laserDeflectedPos = new Rect( 30, 180, 300, 30 );
        hitsRemainingPos = new Rect( 30, 210, 300, 30 );

        GameObject gamePlayer = GameObject.FindWithTag( "Player" );
        if ( gamePlayer != null )
            playerState = gamePlayer.GetComponent<PlayerBehavior>();
        else
            playerState = null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.Label( laserDeflectedPos, "Lasers Deflected: " +  playerState.GetLasersDeflected() );
        GUI.Label( hitsRemainingPos, "Hits Remaining: " + playerState.GetHitsLeft() );
    }
}
