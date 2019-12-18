using UnityEngine;

public class Orb : MonoBehaviour
{
	public GameObject explosionVFXPrefab;	     

	int playerLayer;						       


	void Start()
	{
		playerLayer = LayerMask.NameToLayer("Player");

		GameManager.RegisterOrb(this);
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer != playerLayer)
			return;

		Instantiate(explosionVFXPrefab, transform.position, transform.rotation);
		
		AudioManager.PlayOrbCollectionAudio();

		GameManager.PlayerGrabbedOrb(this);

		gameObject.SetActive(false);
	}
}
