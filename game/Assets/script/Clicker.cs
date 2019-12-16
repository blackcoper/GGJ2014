using UnityEngine;
using System.Collections;


public class Clicker : MonoBehaviour 
{
	// Event Handler
	public delegate void OnClickEvent(GameObject g);
	public event OnClickEvent OnClick;
	
	// Handle our Ray and Hit
	void Update () 
	{
		// Ray
		Ray ray = Camera.mainCamera.ScreenPointToRay(Input.mousePosition);
		
		// Raycast Hit
		RaycastHit hit;
		
		if (Physics.Raycast(ray, out hit, 100))
		{
			// If we click it
			if (Input.GetMouseButtonUp(0))
			{
				// Notify of the event!
				OnClick(hit.transform.gameObject);
			}
		}
	}
}