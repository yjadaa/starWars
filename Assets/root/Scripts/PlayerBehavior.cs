using UnityEngine;
using System.Collections.Generic;

public class PlayerBehavior : MonoBehaviour
{

    public int START_HITS_LEFT = 5;
    private int hitsLeft;
    private int lasersDeflected;
	public float healthRemaining;
	public float FULL_HEALTH = 100;
	public float HealhHealing =5f;
	public AudioClip healSound;
	public AudioClip music;

    private GestureRecognizer.ARM_STATE rightArm = GestureRecognizer.ARM_STATE.AS_NONE;
    private GestureRecognizer.ARM_STATE leftArm = GestureRecognizer.ARM_STATE.AS_NONE;
    public Gesture currentGesture = Gesture.NONE;
    GameObject selector;
	LeftArmForce laf;
	float lastHealNoise = 0f;

    public enum Gesture
    {
        LIGHTNING,
        HEAL,
        SELECT,
        HOLDING,
        THROW,
        PUSH,
        NONE
    };
	
	public string EnumToString(Gesture g) {
		switch(g) {
			case Gesture.LIGHTNING:
			return "Lightning";
			
		case Gesture.HEAL:
			return "Heal";
		case Gesture.SELECT:
			return "Select";
		case Gesture.THROW:
			return "Throw";
		case Gesture.PUSH:
			return "Push";
		case Gesture.HOLDING:
			return "Holding an object";
		default:
			return "";
		}
		return "";
	}

    // Use this for initialization
    void Start()
    {
        hitsLeft = START_HITS_LEFT;
        lasersDeflected = 0;
        selector = GameObject.FindWithTag("Selector");
		laf = GameObject.FindGameObjectWithTag("LAF").GetComponent<LeftArmForce>();
		healthRemaining = FULL_HEALTH;
		lastHealNoise = Time.time + 3;
		
		//audio.PlayOneShot(music);
		audio.volume = 1f;
    }

    // Update is called once per frame
    void Update()
    {
		
		
        if (hitsLeft <= 0)
        {
            hitsLeft = 0;
            Debug.Log("Player Died");
        }

        //GestureRecognizer.ARM_STATE rightArm =

        GameObject gestureObject = GameObject.FindWithTag( "Gesture" );
        if (gestureObject != null)
        {
            bool rChange = false;
            bool lChange = false;

            GestureRecognizer gRec = gestureObject.GetComponent<GestureRecognizer>();
            if (rightArm != gRec.rightArm)
            {
                rightArm = gRec.rightArm;
                //Debug.Log(Time.deltaTime + "right " + rightArm);
                rChange = true;
            }
            if (leftArm != gRec.leftArm)
            {
                leftArm = gRec.leftArm;
                //Debug.Log(Time.deltaTime + "left " + leftArm);
                lChange = true;
            }

            if (leftArm == GestureRecognizer.ARM_STATE.AS_NEG_X &&
                rightArm == GestureRecognizer.ARM_STATE.AS_POS_X &&
                (currentGesture == Gesture.NONE || currentGesture == Gesture.HEAL || currentGesture == Gesture.SELECT))
            {
                currentGesture = Gesture.HEAL;
                this.Heal();
            }

            else if (leftArm == GestureRecognizer.ARM_STATE.AS_POS_Z &&
                rightArm == GestureRecognizer.ARM_STATE.AS_POS_Z &&
                (currentGesture == Gesture.NONE || currentGesture == Gesture.LIGHTNING || currentGesture == Gesture.SELECT))
            {
                currentGesture = Gesture.LIGHTNING;
                this.Lightning();
            }

            else if (leftArm == GestureRecognizer.ARM_STATE.AS_NEG_X &&
                rightArm == GestureRecognizer.ARM_STATE.AS_POS_Z &&
                (currentGesture == Gesture.NONE || currentGesture == Gesture.SELECT))
            {
                currentGesture = Gesture.SELECT;
                this.Select();
            }

            else if (leftArm == GestureRecognizer.ARM_STATE.AS_NEG_Y &&
                rightArm == GestureRecognizer.ARM_STATE.AS_POS_Z &&
                (currentGesture == Gesture.SELECT))
            {
                currentGesture = Gesture.HOLDING;
                this.Grab();
            }

            else if (leftArm == GestureRecognizer.ARM_STATE.AS_NEG_X &&
                rightArm == GestureRecognizer.ARM_STATE.AS_POS_Z &&
                (currentGesture == Gesture.HOLDING))
            {
                currentGesture = Gesture.HEAL;
                this.Throw();
            }

            else if (leftArm == GestureRecognizer.ARM_STATE.AS_POS_Z &&
                (currentGesture == Gesture.NONE || currentGesture == Gesture.PUSH || currentGesture == Gesture.SELECT))
            {
                currentGesture = Gesture.PUSH;
                this.Push();
            }
            else if (currentGesture != Gesture.SELECT && currentGesture != Gesture.HOLDING)
            {
                //Debug.Log("None" + Time.deltaTime);
                currentGesture = Gesture.NONE;
				
            }
			else {
				Selector s = selector.GetComponent<Selector>();
		        s.Disable();
				s.Hide();
				laf.DisablePush();
				laf.DisableLightning();
			}
			
			Debug.Log(this.currentGesture + " " +  Time.deltaTime);
        }
    }

    public void Select()
    {
        //Debug.Log("Select" + Time.deltaTime);
		Selector s = selector.GetComponent<Selector>();
        s.Disable();
		s.Show();
    }

    public void Grab()
    {
        //Debug.Log("Grab" + Time.deltaTime);
		Selector s = selector.GetComponent<Selector>();
        s.Enable();
    }

    public void Throw()
    {
        Debug.Log("Throw" + Time.deltaTime);
        Selector s = selector.GetComponent<Selector>();
        s.Throw();
    }

    public void Lightning()
    {
        //Debug.Log("Shoot lightning" + Time.deltaTime);
		laf.EnableLightning();
    }

    public void Heal()
    {
        if(healthRemaining < 100)
		healthRemaining = Mathf.Clamp(healthRemaining + HealhHealing*Time.deltaTime, 0.0f, 100.0f);
		if(Time.deltaTime - lastHealNoise > 3) {
			audio.PlayOneShot(healSound);
			lastHealNoise = Time.time;
		}
    }

    public void Push()
    {
        Debug.Log("Push" + Time.deltaTime);
		laf.EnablePush();
    }

    public void TakeDamage(float damage)
    {
        hitsLeft--;
    }

    public void LaserDeflected()
    {
        lasersDeflected++;
    }

    public int GetHitsLeft() { return hitsLeft; }
    public int GetLasersDeflected() { return lasersDeflected; }
}
