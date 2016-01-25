using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour, IFigure {

	public Texture FigyreImage { get{ return Image; }}
	public Vector3[] AnglePositions{ get{ return Position; }}
	
	public Texture Image;
	public Vector3[] Position;
}
