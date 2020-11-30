﻿using System.Collections;
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

	public int gemsCount;
	[SerializeField] public PickableCounterGUI pickableCounterGUI;

	[Header("Game Events")]
	public OnPickableEvent onPickableEvent;
	public void IncrementPickableGUI(int score)
	{
		gemsCount += score;
		if (pickableCounterGUI != null) pickableCounterGUI.SetScore(gemsCount);
	}
}