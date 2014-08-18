using UnityEngine;
using System.Collections;

public class LinerController : MonoBehaviour {
	public GameObject LineObject;
	private LineRenderer liner;
	private const float TouchInterval = 0.05f;
	private float deltaTime = 0.0f;
	private bool touchEnable = true;
	private bool controll	= true;
	private int lineIndex = 1;

	private void Start()
	{
		liner	= this.gameObject.GetComponent<LineRenderer> ();
		Debug.Log (liner);
		liner.enabled	= false;
	}

	private void Update()
	{
		if (Input.GetMouseButton (0) && touchEnable && controll) 
		{
			touchEnable	= false;
			touchDisplay ( Input.mousePosition );
		}
		if (Input.GetMouseButtonUp (0)) 
		{
			controll = true;
		}
		deltaTime += Time.deltaTime;
		if (deltaTime > TouchInterval) 
		{
			deltaTime = 0;
			touchEnable = true;
		}
	}

	private void touchDisplay( Vector3 position )
	{
		Vector3 screenPoint = position;
		screenPoint.z	= 10.0f;
		Vector3 convertWPoint	= Camera.main.ScreenToWorldPoint (screenPoint);
		Debug.Log ("world point : " + convertWPoint);

		liner.enabled = true;
		liner.SetVertexCount (lineIndex);
		liner.SetPosition (lineIndex - 1, convertWPoint);
		lineIndex++;
	}
}
