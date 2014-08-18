using UnityEngine;
using System.Collections;

public class Sponar : MonoBehaviour {

	public GameController gameController;
	public GameObject SponarObj;
	private const string SponarName = "Chara";
	private const float nextSponeTime = 0.1f;
	private float deltaTime	= 0.0f;
	private bool isSponeable	= true;

	private bool isReverse = false;
	private const float MaxXPosition = 2.0f;
	private const float MinXPosition = -2.0f;
	private const float SponarMoveSpeed = 3.0f;

	private void Start()
	{
		gameController.ObjectDestroyTrigger = Spone;
	}

	private void Update()
	{
//		if (Input.GetMouseButton (0) && isSponeable) 
		if( isSponeable )
		{
			Spone ();
			isSponeable = false;
		}
		deltaTime += Time.deltaTime;
		if (deltaTime > nextSponeTime)
		{
			deltaTime = 0.0f;
			isSponeable = true;
		}

		if (isReverse) {
			this.gameObject.transform.localPosition -= new Vector3 (Time.deltaTime * SponarMoveSpeed, 0, 0);
			if (this.gameObject.transform.localPosition.x < MinXPosition)
				isReverse = false;
		} 
		else
		{
			this.gameObject.transform.localPosition += new Vector3 (Time.deltaTime * SponarMoveSpeed, 0, 0);
			if (this.gameObject.transform.localPosition.x > MaxXPosition)
				isReverse = true;
		}
	}

	private bool Spone()
	{
		GameObject[] StageBlocks = GameObject.FindGameObjectsWithTag ("StageBlock");
		if (StageBlocks.Length < 20) 
		{
			int blockNum = Random.Range (Config.MinBlockNum, Config.MaxBlockNum);
			GameObject sponarClone = Instantiate (Resources.Load( string.Format( "Block{0:D3}", blockNum )), this.gameObject.transform.localPosition, Quaternion.identity) as GameObject;
			sponarClone.name = SponarName;
			sponarClone.transform.parent	= Camera.main.transform;
			return true;
		} 
		else
		{
			if (Config.UGameStatas	== GameStatus.Before)
				Config.UGameStatas	= GameStatus.Start;
		}
		return false;
	}
}
