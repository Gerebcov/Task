using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class GameControler : MonoBehaviour {

	[SerializeField]
	private Text PonintCol;
	[SerializeField]
	private GameObject StartButton;

	private int Point;

	[SerializeField]
	private Square[] Figures;

	private int Lvl = 0;
	private int FigureIndex = -1;

	[SerializeField]
	private float TimeFoAngle;

	private float TimeLvl;

	[SerializeField]
	private float TimeFactor;




	// Use this for initialization
	void Start () {
//		Figures[0] = (Square)Resources.LoadAll("Figures/Square");
		PonintCol.text = Point.ToString ();
//		RandomFigures ();
		Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
//		if (TimeLvl <= 0)
//			Fail ();
//		else
//			TimeLvl -= Time.deltaTime;
		
	}

//	public void RandomFigures()
//	{
//		IFigure F;
//		for(int i = 0; i < Figures.Length * 2; i++)
//		{
//			int R = Random.Range(0, Figures.Length - 1);
//			F = Figures[i];
//			Figures[i] = Figures[R];
//			Figures[R] = F;
//		}
//	}

	public void Fail(){
		TimeLvl = 1;
		Time.timeScale = 0;
	}

	public void NextLvl()
	{
		Lvl++;
		TimeFoAngle -= TimeFoAngle * TimeFactor;
		if (FigureIndex < Figures.Length)
			FigureIndex++;
		else {
			FigureIndex = 0;
//			RandomFigures();
		}
		gameObject.SendMessage ("NewFigure", FigureIndex, SendMessageOptions.DontRequireReceiver);
		TimeLvl = Figures [FigureIndex].AnglePosition.Length * TimeFoAngle;
	}

	public void ResetGame()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	public void StartGame()
	{
		StartButton.SetActive(false);
		Time.timeScale = 1;
//		NextLvl ();
	}

	public void  WellDone()
	{
		Point++;
		PonintCol.text = Point.ToString ();
		NextLvl ();
	}
}
