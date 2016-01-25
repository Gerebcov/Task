using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;


public class GameControler : MonoBehaviour {

	[SerializeField]
	private Text Score;
	[SerializeField]
	private GameObject StartButton;
	private int Point;
	private int FigureIndex = -1;
	[SerializeField]
	private float TimeFoAngle;
	private float TimeLvl;
	[SerializeField]
	private float TimeFactor;

	private FiguresData Data;
	[SerializeField]
	private GameObject RestartButton;

	public bool GameStatus = false;


	// Use this for initialization
	void Start () {
		Data = GameObject.Find("FigureDataBase").GetComponent<FiguresData> ();
		Score.text = Point.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameStatus) {
			if (TimeLvl <= 0)
			{
				TimeLvl = 0;
				Fail ();
			}
			else
			{
				TimeLvl -= Time.deltaTime;
			}
			if(TimeLvl >= 10)
				GameObject.Find("Time").GetComponent<Text>().text = Math.Floor(TimeLvl).ToString();
			else
				GameObject.Find("Time").GetComponent<Text>().text = Math.Round(TimeLvl, 1).ToString();
		}
	}

	public void Fail(){
		GameStatus = false;
		RestartButton.SetActive (true);
	}

	public void NextLvl()
	{
		TimeFoAngle -= TimeFoAngle * TimeFactor;
		FigureIndex++;
		if (FigureIndex >= Data.Figures.Length) {
			FigureIndex = 0;
			Data.RandomFigures ();
		}
		Data.NextFigures (FigureIndex);
		TimeLvl = (float)Data.Figures[FigureIndex].AnglePositions.Length * TimeFoAngle;
	}

	public void ResetGame()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	public void StartGame()
	{
		StartButton.SetActive(false);
		NextLvl ();
		GameStatus = true;
	}

	public void  WellDone()
	{
		Point++;
		Score.text = Point.ToString ();
		NextLvl ();
	}
}
