using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnFX : MonoBehaviour
{
    public AudioSource myFX;
    public AudioClip hover;
    public AudioClip enter;

    public void Hover()
    {
        myFX.PlayOneShot(hover);

    }

    public void Enter()
    {
        myFX.PlayOneShot(enter);

    }
}
