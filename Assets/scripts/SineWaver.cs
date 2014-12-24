using UnityEngine;
using System.Collections;

// core component for each element controlled by a sine wave
public class SineWaver {
//	public static SineWaver currentWaver;

	public float currentSin { get { return GetSineAt (Time.time); } }
	public float currentCos { get { return GetCosAt (Time.time); } }

//	public float xOffset, yOffset;
	public float amplitude = 1f;
	public float frequency = 0f;
	public float phase = 0f;

//	// Use this for initialization
//	void Start () {
//		currentWaver = this;
//	}

//	public void SetCurrentWaver () {
//		currentWaver = this;
//	}
//
//	public void SetAmp ( float amp ) {
//		amplitude = amp;
//	}
//
//	public void SetFreq ( float freq ) {
//		frequency = freq;
//	}

	public float GetSineAt ( float t, bool ignorePhase = false ) {
		return Mathf.Sin (t * frequency + ( ignorePhase ? 0f : phase) ) * amplitude;
	}

	public float GetCosAt ( float t, bool ignorePhase = false ) {
		return Mathf.Cos (t * frequency + ( ignorePhase? 0f: phase) ) * amplitude;
	}
	
	// Update is called once per frame
//	void Update () {
//		current = GetSineAt (Time.time);
//	}
}
