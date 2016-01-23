using UnityEngine;
using System.Collections;
using System;

public class LvlFigure : MonoBehaviour {

	private LvlControler Controler;

	[SerializeField]
	private Texture FigurePicture;
	[SerializeField]
	private Vector3[] FigurePositionAngles;
	[SerializeField]
	private Vector3[] FigureAngles;


	[SerializeField]
	private Vector3[] PlayerFigurePositionAngles;
	[SerializeField]
	private Vector3[] PlayerFigureAngles;

	private Vector3 TestPointOfAngle;

	private int Points;

	[SerializeField]
	private float TestDistans;

	[SerializeField]
	private Vector3 PlayerAngle;
	[SerializeField]
	private bool PlayerAngleBool = false;

	[SerializeField]
	private float AngleCoof;

	// Use this for initialization
	void Start () {
		Controler = gameObject.GetComponent<LvlControler> ();
		CalculationOfAngles ();
		ResetPoin ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			ResetPoin();
		}
		if (Input.GetMouseButton (0)) {
			if (Vector3.Magnitude (TestPointOfAngle - Input.mousePosition) >= TestDistans) {
				if (PlayerAngleBool) {
					if (TestAngle ()) {
						if (Points < PlayerFigurePositionAngles.Length - 1) {
							CalculationOfPlayerAngles();
							Points++;
							PlayerAngle = Vector3.Normalize (TestPointOfAngle - Input.mousePosition);
						} else {
							ResetPoin ();
						}
					} 
				} else {
					PlayerAngle = Vector3.Normalize (TestPointOfAngle - Input.mousePosition);
					PlayerAngleBool = true;
				}
				PlayerFigurePositionAngles [Points] = TestPointOfAngle = Input.mousePosition;
			}

			if(Points == PlayerFigurePositionAngles.Length - 1 && Vector3.Magnitude(PlayerFigurePositionAngles[0] - Input.mousePosition) <= 50)
			{
				CalculationOfPlayerAngles();
				Test();
			}
		}
	}

	public void CalculationOfAngles()
	{
		FigureAngles = new Vector3[FigurePositionAngles.Length];
		for(int i = 0; i < FigureAngles.Length - 1; i++)
		{
			FigureAngles[i] = Vector3.Normalize(FigurePositionAngles[i+1] - FigurePositionAngles[i]);
		}
		FigureAngles[FigureAngles.Length - 1] = Vector3.Normalize(FigurePositionAngles[0] - FigurePositionAngles[FigureAngles.Length - 1]);
	}

	public void CalculationOfPlayerAngles(){
		PlayerFigureAngles [Points - 1] = Vector3.Normalize (PlayerFigurePositionAngles [Points] - PlayerFigurePositionAngles [Points - 1]);
	}

	public void ResetPoin()
	{
		PlayerFigurePositionAngles = new Vector3[FigurePositionAngles.Length + 1];
		PlayerFigureAngles = new Vector3[FigurePositionAngles.Length];
		TestPointOfAngle = Input.mousePosition;
		PlayerFigurePositionAngles[0] = Input.mousePosition;
		PlayerAngleBool = false;
		Points = 1;
	}

	public bool TestAngle()
	{
		if (Vector3.Magnitude (Vector3.Normalize (TestPointOfAngle - Input.mousePosition) - PlayerAngle) >= AngleCoof)
			return true;
		else 
			return false;
	}

	public void Test()
	{
		int offset = Array.FindIndex (FigureAngles, item => {
			return (Vector3.Magnitude (PlayerFigureAngles [0] - item) <= 0.25); });

		for(int i = 0; i < FigureAngles.Length; i++)
		{
			if(offset == FigureAngles.Length)
				offset = 0;
			if(Vector3.Magnitude(PlayerFigureAngles[i] - FigureAngles[offset]) >= 0.25)
			{
				Debug.Log("NO");
				ResetPoin();
				break;
			}
			offset++;
			if(i == FigureAngles.Length - 1)
			{
				Debug.Log("DA");
				Controler.WellDone();
				ResetPoin();
			}
		}
	}
	
}
