using UnityEngine;
using System.Collections;

public class Square : MonoBehaviour, IFigure {

	public Texture Figure { get { return Img; } private set{Img = value ; }}
	public Vector3[] AnglePosition { get{return Pos ; } private set{Pos = value ; }}

	private Texture Img;
	private Vector3[] Pos = new Vector3[] {new Vector3(0,0,0),new Vector3(0,1,0),new Vector3(1,1,0),new Vector3(1,0,0)};
}
