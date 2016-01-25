using UnityEngine;
using System.Collections;

public class FigureController : MonoBehaviour {

	private Texture FigurePicture;
	private Vector3[] FigurePositionAngles;
	private float[] FigureAngles;
	private float[] FigureLengthParties;

	private Vector3[] PlayerFigurePositionAngles;
	private float[] PlayerFigureAngles;
	private float[] PlayerFigureLengthParties;

	private Vector3 TestPointOfAngle;

	private int Points;

	private float PlayerAngle;

	[SerializeField]
	private float IntermediateDistance;
	[SerializeField]
	private float AngleCoof;
	[SerializeField]
	private float ScaleCoof;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)&& gameObject.GetComponent<GameControler>().GameStatus) {
			ResetPoin();
		}
		if (Input.GetMouseButton (0) && gameObject.GetComponent<GameControler>().GameStatus) {
			if (Vector3.Magnitude (TestPointOfAngle - Input.mousePosition) >= IntermediateDistance) {
				if (Points > 0) {
					if (TestAngle ()) {
						if (Points < PlayerFigurePositionAngles.Length - 1) {
							Points++;
						} else {
							OffsetPointsPlayer();
						}
						PlayerAngle = Vector3.Angle (Vector3.up, TestPointOfAngle - Input.mousePosition);
					} 
				} else {
					PlayerAngle = Vector3.Angle (Vector3.up, TestPointOfAngle - Input.mousePosition);
					Points++;
				}
				PlayerFigurePositionAngles [Points] = TestPointOfAngle = Input.mousePosition;
			}

			if(Points == PlayerFigurePositionAngles.Length - 1 && Vector3.Magnitude(PlayerFigurePositionAngles[0] - Input.mousePosition) <= IntermediateDistance * 5)
			{
				PlayerFigurePositionAngles[Points] = PlayerFigurePositionAngles[0];
				AnalysisOfPlayerFigures();
				ComparisonFigures();
				OffsetPointsPlayer();
			}
		}
	}

	public void AnalysisOfFigures()
	{
		FigureAngles = new float[FigurePositionAngles.Length];
		FigureLengthParties = new float[FigurePositionAngles.Length];
		for(int i = 0; i < FigureAngles.Length - 1; i++)
		{
			FigureAngles[i] = Vector3.Angle(Vector3.up, FigurePositionAngles[i+1] - FigurePositionAngles[i]);
			FigureLengthParties[i] = Vector3.Magnitude(FigurePositionAngles[i+1] - FigurePositionAngles[i]);
		}
		FigureAngles[FigureAngles.Length - 1] = Vector3.Angle(Vector3.up, FigurePositionAngles[0] - FigurePositionAngles[FigureAngles.Length - 1]);
		FigureLengthParties[FigureLengthParties.Length - 1] = Vector3.Magnitude(FigurePositionAngles[0] - FigurePositionAngles[FigureAngles.Length - 1]);
	}

	public void AnalysisOfPlayerFigures()
	{
		PlayerFigureAngles = new float[FigurePositionAngles.Length];
		PlayerFigureLengthParties = new float[FigurePositionAngles.Length];
		for(int i = 0; i < FigurePositionAngles.Length; i++)
		{
			PlayerFigureAngles[i] = Vector3.Angle(Vector3.up, PlayerFigurePositionAngles[i+1] - PlayerFigurePositionAngles[i]);
			PlayerFigureLengthParties[i] = Vector3.Magnitude(PlayerFigurePositionAngles[i+1] - PlayerFigurePositionAngles[i]);
		}
	}

	public void ResetPoin()
	{
		PlayerFigurePositionAngles = new Vector3[FigurePositionAngles.Length + 1];
		PlayerFigureAngles = new float[FigurePositionAngles.Length];
		PlayerFigureLengthParties = new float[FigurePositionAngles.Length];
		TestPointOfAngle = Input.mousePosition;
		PlayerFigurePositionAngles[0] = Input.mousePosition;
		Points = 0;
	}

	public bool TestAngle()
	{
		if (Mathf.Abs(Vector3.Angle (Vector3.up, PlayerFigurePositionAngles[Points -1] - Input.mousePosition) - PlayerAngle) >= 10)
			return true;
		else 
			return false;
	}

	public void NewFigure(Vector3[] angleposition)
	{
		FigurePositionAngles = angleposition;
		AnalysisOfFigures ();
		ResetPoin ();
	}

	public void OffsetPointsPlayer()
	{
		for(int i = 0; i < FigurePositionAngles.Length; i++)
		{
			PlayerFigurePositionAngles[i] = PlayerFigurePositionAngles[i+1];
		}
	}

	public void ComparisonFigures()
	{
		for (int offset = 0; offset < FigurePositionAngles.Length; offset++) 
		{
			if (ComparisonAngle (offset) && ComparisonParties (offset)) {
				gameObject.SendMessage ("WellDone", SendMessageOptions.DontRequireReceiver);
				break;
			}
		}
	}

	public bool ComparisonAngle(int index)
	{
		for(int i = 0; i <= PlayerFigureAngles.Length; i++)
		{
			if(i == PlayerFigureAngles.Length)
			{
				return true;

			}
			if(index == FigureLengthParties.Length)
				index = 0;
			if(Mathf.Abs(PlayerFigureAngles[i] - FigureAngles[index]) >= AngleCoof)
			{
				break;
			}
			index++;
		}

		for(int i = 0; i <= PlayerFigureLengthParties.Length; i++)
		{
			if(i == PlayerFigureLengthParties.Length)
			{
				return true;

			}
			if(index < 0)
				index = FigureLengthParties.Length - 1;
			if(Mathf.Abs(PlayerFigureAngles[i]  - FigureAngles[index]) >= AngleCoof)
			{
				break;
			}
			index--;
		}
		
		return false;
	}

	public bool ComparisonParties(int index)
	{
		float scale_factor = PlayerFigureLengthParties [0] / FigureLengthParties[index];
		for(int i = 0; i < PlayerFigureLengthParties.Length +1; i++)
		{
			if(i == PlayerFigureLengthParties.Length)
			{
				return true;

			}
			if(index == FigureLengthParties.Length)
				index = 0;
			if((PlayerFigureLengthParties[i]) / (FigureLengthParties[index] * scale_factor) > 1f + ScaleCoof || (PlayerFigureLengthParties[i]) / (FigureLengthParties[index] * scale_factor) < 1f - ScaleCoof )
			{
				break;
			}
			index++;
		}
		for(int i = 0; i < PlayerFigureLengthParties.Length +1; i++)
		{
			if(i == PlayerFigureLengthParties.Length)
			{
				return true;

			}
			if(index < 0)
				index = FigureLengthParties.Length - 1;
			if((PlayerFigureLengthParties[i]) / (FigureLengthParties[index] * scale_factor) > 1f + ScaleCoof || (PlayerFigureLengthParties[i]) / (FigureLengthParties[index] * scale_factor) < 1f - ScaleCoof )
			{
				break;
			}
			index--;
		}

		return false;
	}
}
