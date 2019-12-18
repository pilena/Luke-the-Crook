using UnityEngine;

public class EmissionPulse : MonoBehaviour
{
	public float maxIntensity = 15f;	   
	public float damping = 2f;			      

	Material material;					   
	int emissionColorProperty;			     


	void Start ()
	{
		Renderer renderer = GetComponent<Renderer>();
		material = renderer.material;

		emissionColorProperty = Shader.PropertyToID("_EmissionColor");
	}

	void Update()
	{
		float emission = Mathf.PingPong(Time.time * damping, maxIntensity);

		Color finalColor = Color.white * emission;

		material.SetColor(emissionColorProperty, finalColor);
	}
}
