using UnityEngine;
using System.Collections;

// core component for each element controlled by a sine wave
public class SineWaver : MonoBehaviour {

	float current;

	public float xOffset, yOffset;
	public float amplitude = 1f;
	public float frequency = 1f;

	// Use this for initialization
	void Start () {
	
	}

	public void SetAmp ( float amp ) {
		amplitude = amp;
	}

	public void SetFreq ( float freq ) {
		frequency = freq;
	}

	public float GetSineAt ( float t ) {
		return Mathf.Sin (t * frequency + xOffset) * amplitude + yOffset;
	}

	public float GetCosAt ( float t ) {
		return Mathf.Cos (t * frequency + xOffset) * amplitude + yOffset;
	}
	
	// Update is called once per frame
	void Update () {
		current = GetSineAt (Time.time);
	}
}
