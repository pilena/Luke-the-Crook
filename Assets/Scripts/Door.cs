using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
	Animator anim;			    
	int openParameterID;

    public int index;
    public string levelName;


	void Start()
	{
		anim = GetComponent<Animator>();

		openParameterID = Animator.StringToHash("Open");

		GameManager.RegisterDoor(this);
	}

	public void Open()
	{
		anim.SetTrigger(openParameterID);
		AudioManager.PlayDoorOpenAudio();
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //SceneManager.LoadScene(2);
            SceneManager.LoadScene(levelName);
        }
    }
}
