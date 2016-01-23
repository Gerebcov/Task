using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class LvlControler : MonoBehaviour {

	[SerializeField]
	private Text PonintCol;
	[SerializeField]
	private GameObject StartButton;

	private int Point;

	[SerializeField]
	private IFigure[] Figures;

	private int Lvl;

	[SerializeField]
	private float TimeFoAngle;

	private float TimeFoLvl;
	private float TimeLvl;

	private int FigureIndex;

	// Use this for initialization
	void Start () {
//		Figures = Resources.LoadAll("Figures", typeof(IFigure)).Cast<IFigure>().ToArray();
////		Figures = (IFigure[])();
		PonintCol.text = Point.ToString ();
//		RandomFigures ();
		Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (TimeLvl >= TimeFoLvl)
			Fail ();
		else
			TimeLvl += Time.deltaTime;
		
	}

	public void RandomFigures()
	{
		IFigure F;
		for(int i = 0; i < Figures.Length * 2; i++)
		{
			int R = Random.Range(0, Figures.Length - 1);
			F = Figures[i];
			Figures[i] = Figures[R];
			Figures[R] = F;
		}
	}

	public void Fail(){
		TimeLvl = 0;
		Time.timeScale = 0;
	}

	public void NextLvl()
	{

	}

	public void ResetGame()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	public void StartGame()
	{
		StartButton.SetActive(false);
		Time.timeScale = 1;

	}

	public void  WellDone()
	{
		Point++;
		PonintCol.text = Point.ToString ();
		NextLvl ();
	}
}
