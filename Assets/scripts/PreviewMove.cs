using UnityEngine;
using System.Collections;

public class PreviewMove : MonoBehaviour {

	public SineWaver moveX, moveY; //, rot;
	Vector3 startPos;
	LineRenderer axisPreview;
//	Quaternion startRot;

	public void SetScale (float scale ) {
		transform.localScale = Vector3.one * scale;
	}

	// Use this for initialization
	void Awake () {
		axisPreview = GetComponent<LineRenderer>();
		moveX = new SineWaver ();
		moveY = new SineWaver ();
//		rot = new SineWaver ();

		startPos = transform.position;
//		startRot = transform.rotation;
	//	SineUI.currentWaver = moveY;
		SineUI.currentMover = this;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = startPos + Vector3.right * moveX.currentCos * 0.45f + Vector3.up * moveY.currentSin * 0.4f;
//		transform.rotation = startRot * Quaternion.Euler (0f, 0f, rot.current);

		if (SineUI.currentMover == this) {
			axisPreview.enabled = true;
			if ( SineUI.usingModeX ) {
				axisPreview.SetColors (Color.yellow, Color.yellow);
				axisPreview.SetPosition (0, startPos + Vector3.right * moveX.amplitude * 0.5f - Vector3.forward);
				axisPreview.SetPosition (1, startPos + Vector3.right * -moveX.amplitude * 0.5f - Vector3.forward);
			} else {
				axisPreview.SetColors (Color.red, Color.red);
				axisPreview.SetPosition (0, startPos + Vector3.up * moveY.amplitude * 0.5f - Vector3.forward);
				axisPreview.SetPosition (1, startPos + Vector3.up * -moveY.amplitude * 0.5f - Vector3.forward);
			}
		} else {
			axisPreview.enabled = false;
		}
	}
}
