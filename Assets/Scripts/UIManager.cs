using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
	
	static UIManager current;

	public TextMeshProUGUI orbText;			
	public TextMeshProUGUI deathText;		
	public TextMeshProUGUI gameOverText;	      


	void Awake()
	{
		
		if (current != null && current != this)
		{
			
			Destroy(gameObject);
			return;
		}

		
		current = this;
		DontDestroyOnLoad(gameObject);
	}

	public static void UpdateOrbUI(int orbCount)
	{
		
		if (current == null)
			return;

		
		current.orbText.text = orbCount.ToString();
	}

	public static void UpdateDeathUI(int deathCount)
	{
		
		if (current == null)
			return;

		
		current.deathText.text = deathCount.ToString();
	}

	public static void DisplayGameOverText()
	{
		
		if (current == null)
			return;

		
		current.gameOverText.enabled = true;
	}
}
