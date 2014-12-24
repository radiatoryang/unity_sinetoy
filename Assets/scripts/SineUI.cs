using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SineUI : MonoBehaviour {
	public static PreviewMove currentMover;
	public static bool usingModeX;
//	public Button modeButtonX, modeButtonY;

	SineWaver currentWaver { get { return usingModeX ? currentMover.moveX : currentMover.moveY; } }
	//SineWaver[] allWavers;
	public LineRenderer mainLine, mainToCircle, cosLine, ghostSine, ghostCos;
	public TrailRenderer parseRed, parseYellow;

	public Vector2 mainLineOffset;
	public float mainLineScaleX = 1f;
	public float mainLineScaleY = 1f;
	const float screenOffset = 10f;
	public int vertexCount = 100;

	public Slider sliderFreq, sliderPhase, sliderAmp;

	public float circleOffset;
	public float cosOffset;

	public Text text;

	public void SetMover ( PreviewMove mover ) {

		currentMover = mover;
		UpdateSliders ();
	}

	public void SetPhase ( float phas ) {
		currentWaver.phase = phas;
	}

	public void SetAmp ( float amp ) {
		currentWaver.amplitude = amp;
	}
	
	public void SetFreq ( float freq ) {
		currentWaver.frequency = freq;
	}

	public void SetMode ( bool setToX = true ) {
		usingModeX = setToX; //!usingModeX;
		parseRed.enabled = !usingModeX;
		parseYellow.enabled = usingModeX;
		mainLine.SetColors (new Color (1f, 0f, 0f, usingModeX ? 0.1f : 1f), usingModeX ? Color.clear : Color.black);
		cosLine.SetColors (new Color (1f, 1f, 0f, usingModeX ? 1f : 0.1f), usingModeX ? Color.black : Color.clear);
		UpdateSliders ();
	}

	void UpdateSliders () {
		sliderFreq.value = currentWaver.frequency;
		sliderAmp.value = currentWaver.amplitude;
		sliderPhase.value = currentWaver.phase;
	}

	// Use this for initialization
	void Start () {
	//	allWavers = GameObject.FindObjectsOfType<SineWaver>();
	//	currentWaver = allWavers[0];
		mainLine.SetVertexCount( vertexCount );
		cosLine.SetVertexCount (vertexCount);
		ghostSine.SetVertexCount (vertexCount);
		ghostCos.SetVertexCount (vertexCount);
		mainToCircle.SetVertexCount (3);
		SetMode ();
		currentWaver.amplitude = 2f;
		currentWaver.frequency = 2f;
		currentMover.moveY.amplitude = 2f;
		currentMover.moveY.frequency = 2f;
		UpdateSliders ();
	}

	public void Reset () {
		currentWaver.amplitude = 1f;
		currentWaver.frequency = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		for(int i=0; i<vertexCount; i++) {
			mainLine.SetPosition(i, new Vector3(i * mainLineScaleX - screenOffset, 
			                                    currentWaver.GetSineAt ( Time.time + i * mainLineScaleX ) * mainLineScaleY, 
			                                    -1f ) + new Vector3(mainLineOffset.x, mainLineOffset.y, 0f)
			                     );
			ghostSine.SetPosition(i, new Vector3(i * mainLineScaleX - screenOffset, 
			                                    currentWaver.GetSineAt ( Time.time + i * mainLineScaleX, true ) * mainLineScaleY, 
			                                    -1f ) + new Vector3(mainLineOffset.x, mainLineOffset.y, 0f)
			                     );
			cosLine.SetPosition (i, new Vector3( -circleOffset + currentWaver.GetCosAt ( Time.time + i * mainLineScaleX ) * mainLineScaleY,
			                                    i * -mainLineScaleX - cosOffset, 
			                                    -1f ) + new Vector3(mainLineOffset.x, mainLineOffset.y, 0f)
			                     );
			ghostCos.SetPosition (i, new Vector3( -circleOffset + currentWaver.GetCosAt ( Time.time + i * mainLineScaleX, true ) * mainLineScaleY,
			                                    i * -mainLineScaleX - cosOffset, 
			                                    -1f ) + new Vector3(mainLineOffset.x, mainLineOffset.y, 0f)
			                     );
		}

//		parseRed.enabled = !usingModeX;
//		parseYellow.enabled = usingModeX;
//		if (usingModeX) {
//
//		} else {
//
//		}

		Vector3 redStart = new Vector3 (-screenOffset, currentWaver.GetSineAt (Time.time) * mainLineScaleY, -1f) + new Vector3 (mainLineOffset.x, mainLineOffset.y, 0f);
		parseRed.transform.position = redStart + new Vector3 (-0.5f, 0f, 0f);
		mainToCircle.SetPosition (0, redStart );
		mainToCircle.transform.position = new Vector3 (-circleOffset + currentWaver.GetCosAt (Time.time) * mainLineScaleY, currentWaver.GetSineAt (Time.time) * mainLineScaleY, -1f) + new Vector3 (mainLineOffset.x, mainLineOffset.y, 0f);
		mainToCircle.SetPosition (1, mainToCircle.transform.position );
		Vector3 yellowStart = new Vector3 (mainToCircle.transform.position.x, 0f, -1f);
		parseYellow.transform.position = yellowStart + new Vector3 (0f, 0.5f, 0f);
		mainToCircle.SetPosition (2, yellowStart);

		int minutes = Mathf.FloorToInt(Time.time / 60F);
		int seconds = Mathf.FloorToInt(Time.time - minutes * 60);
		string niceTime = string.Format("{0:00}:{1:00}", minutes, seconds);
		text.text = "Mathf.Sin ( " + niceTime + " * " + currentWaver.frequency.ToString ("F2") + " + " + currentWaver.phase.ToString ("F2") + " ) * " + currentWaver.amplitude.ToString ("F2") + "   =   " + currentWaver.GetSineAt(Time.time).ToString("F2");
	}
}
