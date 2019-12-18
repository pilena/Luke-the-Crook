using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public float amount;	    
    public float speed;		    
    
    Light localLight;		    
    float intensity;		      
	float offset;			      


	void Awake()
	{
		if(Application.isMobilePlatform)
			Destroy(this);
	}

	void Start()
    {
		localLight = GetComponentInChildren<Light>();

        intensity = localLight.intensity;
        offset = Random.Range(0, 10000);
    }

	void Update ()
	{
		float amt = Mathf.PerlinNoise(Time.time * speed + offset, Time.time * speed + offset) * amount;
		localLight.intensity = intensity + amt;
	}
}
