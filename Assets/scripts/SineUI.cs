using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SineUI : MonoBehaviour {

	SineWaver currentWaver;
	SineWaver[] allWavers;
	public LineRenderer mainLine, mainToCircle, cosLine;

	public Vector2 mainLineOffset;
	public float mainLineScaleX = 1f;
	public float mainLineScaleY = 1f;
	const float screenOffset = 10f;
	public int vertexCount = 100;

	public float circleOffset;
	public float cosOffset;

	public Text text;

	// Use this for initialization
	void Start () {
		allWavers = GameObject.FindObjectsOfType<SineWaver>();
		currentWaver = allWavers[0];
		mainLine.SetVertexCount( vertexCount );
		cosLine.SetVertexCount (vertexCount);
		mainToCircle.SetVertexCount (3);
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
			cosLine.SetPosition (i, new Vector3( -circleOffset + currentWaver.GetCosAt ( Time.time + i * mainLineScaleX ) * mainLineScaleY,
			                                    i * -mainLineScaleX - cosOffset, 
			                                    -1f ) + new Vector3(mainLineOffset.x, mainLineOffset.y, 0f)
			                     );
		}

		mainToCircle.SetPosition (0, new Vector3( -screenOffset, currentWaver.GetSineAt (Time.time) * mainLineScaleY, -1f) + new Vector3(mainLineOffset.x, mainLineOffset.y, 0f) );
		mainToCircle.transform.position = new Vector3 (-circleOffset + currentWaver.GetCosAt (Time.time) * mainLineScaleY, currentWaver.GetSineAt (Time.time) * mainLineScaleY, -1f) + new Vector3 (mainLineOffset.x, mainLineOffset.y, 0f);
		mainToCircle.SetPosition (1, mainToCircle.transform.position );
		mainToCircle.SetPosition (2, new Vector3 (mainToCircle.transform.position.x, 0f, -1f));

		int minutes = Mathf.FloorToInt(Time.time / 60F);
		int seconds = Mathf.FloorToInt(Time.time - minutes * 60);
		string niceTime = string.Format("{0:00}:{1:00}", minutes, seconds);
		text.text = "Mathf.Sin ( " + niceTime + " * " + currentWaver.frequency.ToString ("F2") + " ) * " + currentWaver.amplitude.ToString ("F2") + "   = " + currentWaver.GetSineAt(Time.time).ToString("F2");
	}
}
