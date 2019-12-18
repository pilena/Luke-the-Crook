using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	public GameObject deathVFXPrefab;	     

	bool isAlive = true;				    
	int trapsLayer;						     


	void Start()
	{
		trapsLayer = LayerMask.NameToLayer("Traps");
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer != trapsLayer || !isAlive)
			return;

		isAlive = false;

		Instantiate(deathVFXPrefab, transform.position, transform.rotation);

		gameObject.SetActive(false);

		GameManager.PlayerDied();
		AudioManager.PlayDeathAudio();
	}
}
