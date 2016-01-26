using UnityEngine;
using System.Collections;

public class FigureController : MonoBehaviour {

	private Texture FigurePicture;
	private Vector3[] FigurePositionAngles;
	private float[] FigureAnglesX;
	private float[] FigureAnglesY;
	private float[] FigureLengthParties;

	private Vector3[] PlayerFigurePositionAngles;
	private float[] PlayerFigureAnglesX;
	private float[] PlayerFigureAnglesY;
	private float[] PlayerFigureLengthParties;

	private Vector3 TestPointOfAngle;

	private float PlayerAngleX;
	private float PlayerAngleY;

	private bool StartedMoving = false;

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
			PlayerFigurePositionAngles [PlayerFigurePositionAngles.Length - 1] = Input.mousePosition;

			if (Vector3.Magnitude (TestPointOfAngle - Input.mousePosition) >= IntermediateDistance) {
				if (StartedMoving) {
					if (TestAngle ()) {
						OffsetPointsPlayer();
						StartedMoving = false;
					}
				} else {
					PlayerAngleX = Vector3.Angle (Vector3.up, TestPointOfAngle - Input.mousePosition);
					PlayerAngleY = Vector3.Angle (Vector3.right, TestPointOfAngle - Input.mousePosition);
					StartedMoving = true;
				}
				TestPointOfAngle = Input.mousePosition;
			}

			if(Vector3.Magnitude(PlayerFigurePositionAngles[0] - Input.mousePosition) <= IntermediateDistance * 5)
			{
				PlayerFigurePositionAngles[PlayerFigurePositionAngles.Length - 1] = PlayerFigurePositionAngles[0];
				AnalysisOfPlayerFigures();
				ComparisonFigures();
				OffsetPointsPlayer();
			}
		}
	}

	public void AnalysisOfFigures()
	{
		FigureAnglesX = new float[FigurePositionAngles.Length];
		FigureAnglesY = new float[FigurePositionAngles.Length];
		FigureLengthParties = new float[FigurePositionAngles.Length];
		for(int i = 0; i < FigureAnglesX.Length - 1; i++)
		{
			FigureAnglesX[i] = Vector3.Angle(Vector3.up, FigurePositionAngles[i+1] - FigurePositionAngles[i]);
			FigureAnglesY[i] = Vector3.Angle(Vector3.right, FigurePositionAngles[i+1] - FigurePositionAngles[i]);
			FigureLengthParties[i] = Vector3.Magnitude(FigurePositionAngles[i+1] - FigurePositionAngles[i]);
		}
		FigureAnglesX[FigureAnglesX.Length - 1] = Vector3.Angle(Vector3.up, FigurePositionAngles[0] - FigurePositionAngles[FigureAnglesX.Length - 1]);
		FigureAnglesY[FigureAnglesY.Length - 1] = Vector3.Angle(Vector3.right, FigurePositionAngles[0] - FigurePositionAngles[FigureAnglesY.Length - 1]);
		FigureLengthParties[FigureLengthParties.Length - 1] = Vector3.Magnitude(FigurePositionAngles[0] - FigurePositionAngles[FigurePositionAngles.Length - 1]);
	}

	public void AnalysisOfPlayerFigures()
	{
		PlayerFigureAnglesX = new float[FigurePositionAngles.Length];
		PlayerFigureAnglesY = new float[FigurePositionAngles.Length];
		PlayerFigureLengthParties = new float[FigurePositionAngles.Length];
		for(int i = 0; i < FigurePositionAngles.Length; i++)
		{
			PlayerFigureAnglesX[i] = Vector3.Angle(Vector3.up, PlayerFigurePositionAngles[i+1] - PlayerFigurePositionAngles[i]);
			PlayerFigureAnglesY[i] = Vector3.Angle(Vector3.right, PlayerFigurePositionAngles[i+1] - PlayerFigurePositionAngles[i]);
			PlayerFigureLengthParties[i] = Vector3.Magnitude(PlayerFigurePositionAngles[i+1] - PlayerFigurePositionAngles[i]);
		}
	}

	public void ResetPoin()
	{
		PlayerFigurePositionAngles = new Vector3[FigurePositionAngles.Length + 1];
		PlayerFigureAnglesX = new float[FigurePositionAngles.Length];
		PlayerFigureAnglesY = new float[FigurePositionAngles.Length];
		PlayerFigureLengthParties = new float[FigurePositionAngles.Length];
		TestPointOfAngle = Input.mousePosition;
		PlayerFigurePositionAngles[0] = Input.mousePosition;
		StartedMoving = false;
	}

	public bool TestAngle()
	{
		if (Mathf.Abs(Vector3.Angle (Vector3.up, TestPointOfAngle - Input.mousePosition) - PlayerAngleX) >= AngleCoof || Mathf.Abs(Vector3.Angle (Vector3.right, TestPointOfAngle - Input.mousePosition) - PlayerAngleY) >= AngleCoof)
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
			if (DirectComparison (offset) || ReverseComparison (offset)) {
				gameObject.SendMessage ("WellDone", SendMessageOptions.DontRequireReceiver);
				break;
			}
		}
	}

	public bool DirectComparison(int index)
	{
		float scale_factor = PlayerFigureLengthParties [0] / FigureLengthParties[index];
		for(int i = 0; i <= PlayerFigureAnglesX.Length + 1; i++)
		{
			if(i == PlayerFigureAnglesX.Length)
			{
				return true;
			}
			if(index == FigureLengthParties.Length)
				index = 0;
			if((PlayerFigureLengthParties[i]) / (FigureLengthParties[index] * scale_factor) > 1f + ScaleCoof || (PlayerFigureLengthParties[i]) / (FigureLengthParties[index] * scale_factor) < 1f - ScaleCoof || Mathf.Abs(PlayerFigureAnglesX[i] - FigureAnglesX[index]) >= AngleCoof || Mathf.Abs(PlayerFigureAnglesY[i] - FigureAnglesY[index]) >= AngleCoof)
			{
				break;
			}
			index++;
		}
		return false;
	}

	public bool ReverseComparison(int index)
	{

		float scale_factor = PlayerFigureLengthParties [0] / FigureLengthParties[index];

		for(int i = 0; i < PlayerFigureLengthParties.Length +1; i++)
		{
			if(i == PlayerFigureLengthParties.Length)
			{
				return true;
			}
			if(index < 0)
				index = FigureLengthParties.Length - 1;
			if((PlayerFigureLengthParties[i]) / (FigureLengthParties[index] * scale_factor) > 1f + ScaleCoof || (PlayerFigureLengthParties[i]) / (FigureLengthParties[index] * scale_factor) < 1f - ScaleCoof || Mathf.Abs(PlayerFigureAnglesX[i] - FigureAnglesX[index]) >= AngleCoof || Mathf.Abs(PlayerFigureAnglesY[i] - FigureAnglesY[index]) >= AngleCoof)
			{
				break;
			}
			index--;
		}
		return false;
	}
}
