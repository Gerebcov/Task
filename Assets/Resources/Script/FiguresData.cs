using UnityEngine;
using System.Collections;
using System.IO;
using System;
using UnityEngine.UI;

public class FiguresData : MonoBehaviour {

	[SerializeField]
	public IFigure[] Figures;

	// Use this for initialization
	void Start () {
		Figures = gameObject.GetComponents<IFigure> ();
		RandomFigures ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RandomFigures()
	{
		IFigure F;
		for(int i = 0; i < Figures.Length; i++)
		{
			int R = UnityEngine.Random.Range(0, Figures.Length - 1);
			F = Figures[i];
			Figures[i] = Figures[R];
			Figures[R] = F;
		}
	}

	public void NextFigures(int index)
	{
		GameObject.Find ("Controller").GetComponent<FigureController> ().NewFigure (Figures [index].AnglePositions);
		GameObject.Find ("FigureImage").GetComponent<RawImage> ().texture = Figures [index].FigyreImage; 
	}
}
