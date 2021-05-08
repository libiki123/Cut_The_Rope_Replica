using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeCutter : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetMouseButton(0))
		{
			Vector3 mouse = Input.mousePosition;
			mouse.z = 10;
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouse);
			RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
			
			if (hit)
			{
				if(hit.collider.tag == "Link")
				{
					//Destroy(hit.collider.gameObject);

					Transform link = hit.collider.gameObject.transform;
					link.root.GetComponent<Rope>().RopeCut(link);

				}
			}
		}
	}
}
