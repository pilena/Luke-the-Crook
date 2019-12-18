using UnityEngine;
using UnityEngine.EventSystems;

public class TouchButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	int pointerID;
	bool buttonHeld;
	bool buttonPressed;
	
	void Awake ()
	{
		pointerID = -999;
	}

	public void OnPointerDown(PointerEventData data)
	{
		if (pointerID != -999)
			return;
		
		pointerID = data.pointerId;
		
		buttonHeld = true;
		buttonPressed = true;
	}

	public void OnPointerUp (PointerEventData data)
	{
		if (data.pointerId != pointerID)
			return;
		
		pointerID = -999;
		buttonHeld = false;
		buttonPressed = false;
	}

	public bool GetButtonDown () 
	{
		if (buttonPressed)
		{
			buttonPressed = false;
			return true;
		}

		return false;
	}

	public bool GetButton()
	{
		return buttonHeld;
	}
}