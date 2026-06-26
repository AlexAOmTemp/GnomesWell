// BEGIN 2d_rope

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rope : MonoBehaviour
{
    public GameObject ropeSegmentPrefab;
    List<GameObject> ropeSegments = new List<GameObject>();

    public bool isIncreasing { get; set; }
    public bool isDecreasing { get; set; }

    public Rigidbody2D connectedObject;
    public float maxRopeSegmentLength = 1.0f;
    public float ropeSpeed = 4.0f;


    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        ResetLength();
    }

    public void ResetLength()
    {
        foreach (GameObject segment in ropeSegments)
        {
            Destroy(segment);
        }

        ropeSegments = new List<GameObject>();
        isDecreasing = false;
        isIncreasing = false;

        CreateRopeSegment();
    }

    private void CreateRopeSegment()
    {
        GameObject segment = Instantiate(ropeSegmentPrefab, transform.position, Quaternion.identity);
        segment.transform.SetParent(this.transform, true);

        var segmentBody = segment.GetComponent<Rigidbody2D>();
        var segmentJoint = segment.GetComponent<SpringJoint2D>();

        if (segmentBody == null || segmentJoint == null)
        {
            Debug.LogError("Rope segment body prefab has no " +
                           "Rigidbody2D and/or SpringJoint2D!");
            return;
        }

        ropeSegments.Insert(0, segment);
        if (ropeSegments.Count == 1)
        {
            var connectedObjectJoint = connectedObject.GetComponent<SpringJoint2D>();
            connectedObjectJoint.connectedBody = segmentBody;
            connectedObjectJoint.distance = 0.1f;
            segmentJoint.distance = maxRopeSegmentLength;
        }
        else
        {
            GameObject nextSegment = ropeSegments[1];

            var nextSegmentJoint = nextSegment.GetComponent<SpringJoint2D>();
            nextSegmentJoint.connectedBody = segmentBody;
            segmentJoint.distance = 0.0f;
        }

        segmentJoint.connectedBody = this.GetComponent<Rigidbody2D>();
    }

    private void RemoveRopeSegment()
    {
        if (ropeSegments.Count < 2)
            return;

        GameObject topSegment = ropeSegments[0];
        GameObject nextSegment = ropeSegments[1];

        var nextSegmentJoint = nextSegment.GetComponent<SpringJoint2D>();
        nextSegmentJoint.connectedBody = this.GetComponent<Rigidbody2D>();

        ropeSegments.RemoveAt(0);
        Destroy(topSegment);
    }

    private void Update()
    {
        GameObject topSegment = ropeSegments[0];
        var topSegmentJoint = topSegment.GetComponent<SpringJoint2D>();

        if (isIncreasing)
        {
            if (topSegmentJoint.distance >= maxRopeSegmentLength)
                CreateRopeSegment();
            else
                topSegmentJoint.distance += ropeSpeed * Time.deltaTime;
        }

        if (isDecreasing)
        {
            if (topSegmentJoint.distance <= 0.005f)
                RemoveRopeSegment();
            else
                topSegmentJoint.distance -= ropeSpeed * Time.deltaTime;
        }

        if (lineRenderer != null)
        {
            lineRenderer.positionCount = ropeSegments.Count + 2;
            lineRenderer.SetPosition(0, this.transform.position);

            for (int i = 0; i < ropeSegments.Count; i++)
            {
                lineRenderer.SetPosition(i + 1,
                    ropeSegments[i].transform.position);
            }

            var connectedObjectJoint = connectedObject.GetComponent<SpringJoint2D>();
            lineRenderer.SetPosition(
                ropeSegments.Count + 1,
                connectedObject.transform.TransformPoint(connectedObjectJoint.anchor)
            );
        }
    }
}