using UnityEngine;
using System.Collections;

public class ParticleControl : MonoBehaviour {
	[SerializeField]
	private ParticleSystem Emitter;
	private Vector3 MousePosition;

	// Use this for initialization
	void Start () {
		Emitter = gameObject.GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		MousePosition = new Vector3 ((Input.mousePosition.x / Screen.width) * 14, ((Input.mousePosition.y) / Screen.height) * 10, -9);
		gameObject.transform.position = MousePosition;

		if (Input.GetMouseButtonDown (0)) {
			Emitter.emissionRate = 50;
		}
		if (Input.GetMouseButtonUp (0)) {
			Emitter.emissionRate = 0;
		}
	
	}
}
