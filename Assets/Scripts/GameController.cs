using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	public delegate bool UserTrigger ();
	public UserTrigger ObjectDestroyTrigger;

	private List<GameObject> selectedObjs;

	private const float selectedAlpha = 0.5f;
	private const float normalAlpha   = 1.0f;
	private const float chainableDist = 1.5f;

	private void Start()
	{
		selectedObjs = new List<GameObject> ();
	}

	// Update is called once per frame
	private void Update () {
		if (Input.GetMouseButton (0) && Config.UGameStatas != GameStatus.Before) 
		{
			Ray ray	= Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit raycastHit	= new RaycastHit();
			if (Physics.Raycast(ray, out raycastHit, 100 )) 
			{
				if (raycastHit.collider.gameObject.tag == Config.StageBlockTagName) 
				{
					GameObject selectedObj	= raycastHit.collider.gameObject;
					if (selectedObjs.Count > 0) 
					{
						int prevIndex	= selectedObjs.Count - 1;
						GameObject prevSelectedObj	= selectedObjs [prevIndex];
						int prevBlockNum = prevSelectedObj.GetComponent<Block> ().id;
						int selectedBlockNum = selectedObj.GetComponent<Block> ().id;
						// if ids are same. chaining judge.
						if (isObjectPrevSame (selectedObj, selectedObjs)) 
						{
							int removeObjIndex   = selectedObjs.Count - 1;
							GameObject removeObj = selectedObjs [removeObjIndex];
							changeSpriteAlpha (removeObj, normalAlpha);
							selectedObjs.Remove (removeObj);
						}
						else if (isObjcetsDifference (selectedObj, prevSelectedObj) && calcObjsDistance (prevSelectedObj, selectedObj)) 
						{
							changeSpriteAlpha (selectedObj, selectedAlpha);
							selectedObjs.Add (selectedObj);
						} 
						else 
						{
							return;
						}
					} 
					else
					{
						changeSpriteAlpha (selectedObj, selectedAlpha);
						selectedObjs.Add (selectedObj);
					}
				}
			}
		}

		if (Input.GetMouseButtonUp (0)) 
		{
			Debug.Log (selectedObjs.Count);
			if (selectedObjs.Count >= Config.MinBlockChainedNum) 
			{
				for (int i = 0; i < selectedObjs.Count; i++) 
				{
					Destroy (selectedObjs [i]);
				}
				if (ObjectDestroyTrigger != null) 
				{
					while (ObjectDestroyTrigger ()) 
					{
					}
				}
			}
			else if (selectedObjs.Count > 0 && selectedObjs.Count < Config.MinBlockChainedNum ) 
			{
				for (int i = 0; i < selectedObjs.Count; i++) 
				{
					GameObject presentObj = selectedObjs [i];
					changeSpriteAlpha (presentObj, normalAlpha);
				}
			}
			selectedObjs.Clear ();
		}
	}

	private bool changeSpriteAlpha( GameObject obj, float alpha )
	{
		SpriteRenderer renderer	= obj.GetComponent<SpriteRenderer> ();
		if (renderer == null)
			return false;
			
		Color color	   = renderer.color;
		color.a        = alpha;
		renderer.color = color;
		return true;
	}

	private bool calcObjsDistance( GameObject startObject, GameObject endObject )
	{
		float dist = Vector3.Distance (startObject.transform.localPosition, endObject.transform.localPosition);
		if (dist <= chainableDist)
			return true;
		else
			return false;
	}

	private bool isObjcetsDifference( GameObject targetObj, GameObject prevObj )
	{
		if (targetObj == prevObj)
			return false;

		int prevBlockNum = prevObj.GetComponent<Block> ().id;
		int selectedBlockNum = targetObj.GetComponent<Block> ().id;
		if (selectedBlockNum != prevBlockNum)
			return false;

		return true;
	}

	private bool isObjectPrevSame( GameObject targetObj, List<GameObject> selectedObjs )
	{
		if (selectedObjs.Count == 1)
			return false;

		int PrevObjectIndex	= selectedObjs.Count - 2;
		Debug.Log (targetObj == selectedObjs [PrevObjectIndex]);
		if (selectedObjs.Contains (targetObj) && targetObj == selectedObjs[PrevObjectIndex] )
			return true;

		return false;
	}
}
