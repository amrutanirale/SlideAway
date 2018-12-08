using UnityEngine;
using System.Collections;

public static class GameEventManager
{
	public static int playerDieCounter = 0;

	public delegate void GameEvent ();

	static E_STATES m_gameState = E_STATES.e_game;

	public static void SetState (E_STATES state)
	{
		m_gameState = state;
	}

	public static E_STATES GetState ()
	{
		return m_gameState;
	}

	public enum E_STATES
	{
		e_game,
		e_pause,
	}

	;

}


