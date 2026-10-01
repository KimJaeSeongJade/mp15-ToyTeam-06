using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : SingletonBehaviour<GameManager>
{
    public enum GameState
    {
	    Title, // 타이틀
	    Main, // 인게임
	    Paused, // 상태
	    GameOver // 게임 오버
	}
    public GameState CurrentState { get; private set; }

    public int CurrentScore { get; private set; }
    public float PlayTime { get; private set; }

    private void Awake() => SetSingleton();

    private void Start()
    {

    }

    public void ChangeState(GameState state)
    {
	    CurrentState = state; // 게임 상태 변경
    }

    public void AddScore(int score)
    {
	    CurrentScore += score; // 코인 획득시 점수 추가
    }
}
