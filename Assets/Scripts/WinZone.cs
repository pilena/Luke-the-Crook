using UnityEngine;

public class WinZone : MonoBehaviour
{
	int playerLayer;           


	void Start()
	{
		playerLayer = LayerMask.NameToLayer("Player");
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer != playerLayer)
			return;

		Debug.Log("Player Won!");
		GameManager.PlayerWon();
	}
}
