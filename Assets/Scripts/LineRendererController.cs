using UnityEngine;
using System.Collections;

public class LineRendererController : MonoBehaviour {

	public GameObject Liner;
	private LineRenderer lineRenderer;

	private void Start()
	{
//		Instantiate (Liner, Vector3.zero, Quaternion.identity);
//		lineRenderer = Liner.GetComponent<LineRenderer> ();
//		lineRenderer.enabled = true;
//		lineRenderer.SetVertexCount (2);
//		Vector3 position = this.gameObject.transform.localPosition;
//		Vector3 nextPosition = position;
//
//		nextPosition.x += 10;
//		lineRenderer.SetPosition (0, position);
//		lineRenderer.SetPosition (1, nextPosition);

	}

	private void Update()
	{
		if (Input.GetMouseButton (0)) 
		{
			Instantiate (Liner, this.gameObject.transform.position, Quaternion.identity);
		}
	}
}
