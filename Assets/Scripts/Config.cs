using System;
using System.Collections;

public static class Config {
	public const int MinBlockNum = 1;
	public const int MaxBlockNum = 5;
	public const string StageBlockTagName = "StageBlock";
	public static GameStatus UGameStatas  = GameStatus.Before;

	public const int MinBlockChainedNum = 3;
}

public enum GameStatus
{
	Before
	,Start
	,Play
	,Finish
}