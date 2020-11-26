using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerTool
{
	None,
	Hammer,
	Brush,
	Static
}

public class GameManager : Singleton<GameManager>
{
	public PlayerTool currentPlayerTool = PlayerTool.None;
}
