using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	
	static AudioManager current;

    public AudioClip musicClip;			

	public AudioClip levelStingClip;	
	public AudioClip deathStingClip;	
	public AudioClip winStingClip;		
	public AudioClip orbStingClip;      
	public AudioClip doorOpenStingClip; 

    public AudioClip[] walkStepClips;	
	public AudioClip deathClip;			
    public AudioClip jumpClip;			
	
	public AudioClip jumpVoiceClip;     
    public AudioClip deathVoiceClip;
	

	public AudioMixerGroup ambientGroup;
	public AudioMixerGroup musicGroup; 
	public AudioMixerGroup stingGroup;  
	public AudioMixerGroup playerGroup; 
	public AudioMixerGroup voiceGroup;  

	AudioSource ambientSource;			
    AudioSource musicSource;            
	AudioSource stingSource;            
	AudioSource playerSource;          
	AudioSource voiceSource;            


	void Awake()
	{
		
		if (current != null && current != this)
		{
			
			Destroy(gameObject);
			return;
		}

		
		current = this;
		DontDestroyOnLoad(gameObject);

		
		ambientSource	= gameObject.AddComponent<AudioSource>() as AudioSource;
        musicSource		= gameObject.AddComponent<AudioSource>() as AudioSource;
        stingSource		= gameObject.AddComponent<AudioSource>() as AudioSource;
        playerSource	= gameObject.AddComponent<AudioSource>() as AudioSource;
        voiceSource		= gameObject.AddComponent<AudioSource>() as AudioSource;

		
		ambientSource.outputAudioMixerGroup = ambientGroup;
		musicSource.outputAudioMixerGroup	= musicGroup;
		stingSource.outputAudioMixerGroup	= stingGroup;
		playerSource.outputAudioMixerGroup	= playerGroup;
		voiceSource.outputAudioMixerGroup	= voiceGroup;

		
        StartLevelAudio();
	}

    void StartLevelAudio()
    {
		
       
        current.ambientSource.loop = true;
        current.ambientSource.Play();

		
        current.musicSource.clip = current.musicClip;
        current.musicSource.loop = true;
        current.musicSource.Play();

		
		PlaySceneRestartAudio();
    }

	public static void PlayFootstepAudio()
	{

		if (current == null || current.playerSource.isPlaying)
			return;

		
		int index = Random.Range(0, current.walkStepClips.Length);
		
		
		current.playerSource.clip = current.walkStepClips[index];
		current.playerSource.Play();
	}

    public static void PlayCrouchFootstepAudio()
    {
		
		if (current == null || current.playerSource.isPlaying)
            return;

	}

    public static void PlayJumpAudio()
    {
		
		if (current == null)
            return;

		
        current.playerSource.clip = current.jumpClip;
        current.playerSource.Play();

		
		current.voiceSource.clip = current.jumpVoiceClip;
        current.voiceSource.Play();
    }

	public static void PlayDeathAudio()
	{
		
		if (current == null)
			return;

		
		current.playerSource.clip = current.deathClip;
        current.playerSource.Play();
		
		
		current.voiceSource.clip = current.deathVoiceClip;
        current.voiceSource.Play();
		
		
		current.stingSource.clip = current.deathStingClip;
        current.stingSource.Play();
	}


	public static void PlayOrbCollectionAudio()
	{

		if (current == null)
			return;

		
		current.stingSource.clip = current.orbStingClip;
        current.stingSource.Play();

	}

    public static void PlaySceneRestartAudio()
    {
		
		if (current == null)
            return;

	
		current.stingSource.clip = current.levelStingClip;
        current.stingSource.Play();
    }

	public static void PlayDoorOpenAudio()
	{
		
		if (current == null)
			return;

		
		current.stingSource.clip = current.doorOpenStingClip;
		current.stingSource.PlayDelayed(1f);
	}

	public static void PlayWonAudio()
    {
		
		if (current == null)
            return;

		
        current.ambientSource.Stop();


		current.stingSource.clip = current.winStingClip;
        current.stingSource.Play();
    }
}
