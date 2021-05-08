using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
	public float distanceFromChainEnd = 0.5f;

    public void ConnectRopEnd(Rigidbody2D endRb)
	{
		HingeJoint2D joint = gameObject.AddComponent<HingeJoint2D>();
		joint.autoConfigureConnectedAnchor = false;
		joint.connectedBody = endRb;
		joint.anchor = Vector2.zero;
		joint.connectedAnchor = new Vector2(0f, -distanceFromChainEnd);
	}
}
