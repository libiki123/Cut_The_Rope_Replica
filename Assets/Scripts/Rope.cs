using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Rigidbody2D hook;
    public GameObject linkPrefab;
	public LineRenderer lineRenderer;
	public LineRenderer lineRenderer2;
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
			segments.Add(link.transform);
			joint.connectedBody = previousRb;
			
			if(i < links - 1)
			{
				previousRb = link.GetComponent<Rigidbody2D>();
			}
			else
			{
				candy.ConnectRopEnd(link.GetComponent<Rigidbody2D>());
			}
		}

		cuttedSegments.AddRange(segments);
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
			lineRenderer.SetPositions(segmentPositions);

		}

		if(cuttedSegments != null && gotCutted)
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

			int index = segments.IndexOf(cuttedSegment);
			int remainingIndex = segments.Count - index;

			
			cuttedSegments.RemoveRange(0, index + 1);
			segments.RemoveRange(index, remainingIndex);

			Destroy(cuttedSegment.gameObject);

		}

	
	}
}
