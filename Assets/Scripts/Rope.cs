using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Rigidbody2D hook;
    public GameObject linkPrefab;
	public LineRenderer lineRenderer;					
	public LineRenderer lineRenderer2;				// backup linerender when the rope got cut
	public Candy candy;

    public int links = 7;

	private List<Transform> segments;
	private List<Transform> cuttedSegments;
	private bool gotCutted = false;

	private void Start()
	{
		lineRenderer.gameObject.SetActive(true);

		segments = new List<Transform>();
		cuttedSegments = new List<Transform>();

		GenerateRope();
	}

	private void Update()
	{
		DrawRope();
	}

	private void GenerateRope()
	{
		Rigidbody2D previousRb = hook;
		segments.Add(hook.GetComponent<Transform>());

		for (int i = 0; i < links; i++)
		{
			GameObject link = Instantiate(linkPrefab, hook.transform);					
			HingeJoint2D joint = link.GetComponent<HingeJoint2D>();					
			segments.Add(link.transform);										// save all the link
			joint.connectedBody = previousRb;									// connect link to previous link (hook will be the first connection)
			
			if(i < links - 1)													// if not last link
			{
				previousRb = link.GetComponent<Rigidbody2D>();					// save this link as previous link
			}
			else
			{
				candy.ConnectRopEnd(link.GetComponent<Rigidbody2D>());			// pass the link to the candy and to connect (if it the last link)
			}
		}

		cuttedSegments.AddRange(segments);										// keep another list of all the links
	}

	private void DrawRope()
	{
		if(segments != null)
		{
			Vector3[] segmentPositions = new Vector3[segments.Count];
			for (int i = 0; i < segments.Count; i++)
			{
				segmentPositions[i] = segments[i].position;
			}

			lineRenderer.positionCount = segments.Count;
			lineRenderer.SetPositions(segmentPositions);						// get all the link pos and draw lines between them

		}

		if(cuttedSegments != null && gotCutted)									// only draw this 2nd line when the rope got cut
		{
			
			Vector3[] segmentPositions = new Vector3[cuttedSegments.Count];
			for (int i = 0; i < cuttedSegments.Count; i++)
			{
				segmentPositions[i] = cuttedSegments[i].position;
			}

			lineRenderer2.positionCount = cuttedSegments.Count;
			lineRenderer2.SetPositions(segmentPositions);
		}
	}

	public void RopeCut(Transform cuttedSegment)
	{
		if (!gotCutted)
		{
			gotCutted = true;
			lineRenderer2.gameObject.SetActive(true);

			int index = segments.IndexOf(cuttedSegment);					// get the index of the link that got cut
				
			cuttedSegments.RemoveRange(0, index + 1);						// in the 2nd list remove links until the link that got cut (inlcuded) - use this to draw 2nd line
			segments.RemoveRange(index, segments.Count - index);			// in the 1st list remove link got cut and the rest after - use this to draw 1st line

			Destroy(cuttedSegment.gameObject);								// destroy the link that got cut

		}

	
	}
}
