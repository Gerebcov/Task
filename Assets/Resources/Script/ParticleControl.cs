using UnityEngine;
using System.Collections;

public class ParticleControl : MonoBehaviour {
	[SerializeField]
	private ParticleSystem Emitter;
	private Vector3 MousePosition;

	private const int RECT_WIDTH = 14;
	private const int RECT_HEIGHT = 10;

	// Use this for initialization
	void Start () {
		Emitter = gameObject.GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		MousePosition = new Vector3 ((Input.mousePosition.x / Screen.width) * RECT_WIDTH, ((Input.mousePosition.y) / Screen.height) * RECT_HEIGHT, -9);
		gameObject.transform.position = MousePosition;

		if (Input.GetMouseButtonDown (0)) {
			Emitter.emissionRate = 10;
		}
		if (Input.GetMouseButtonUp (0)) {
			Emitter.emissionRate = 0;
		}
	
	}
}
