using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class Bezier : MonoBehaviour, IOrdered {

    public Transform startTransform;
    public Transform endTransform;

    public int vertCount = 20;
    public int endCapCount = 5;
    public float radius = 0.5f;
    public float direction = 1.0f;
    
    private float length;
    private LineRenderer lineRenderer;

    public AnimationCurve falloff;

    void Start() {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.SetVertexCount(vertCount + endCapCount);
    }

    Vector3 point;
    Vector3 tangent;
    Vector3 normalVector;

    public int GetOrder() {return 3;}

    public void OrderedUpdate() {
        
        length = Vector3.Distance(startTransform.position, endTransform.position) / 2.0f;

        Vector3 c0 = startTransform.position;
        Vector3 c1 = startTransform.localToWorldMatrix.MultiplyPoint(new Vector3(0.0f, -length, 0.0f));
        Vector3 c2 = endTransform.localToWorldMatrix.MultiplyPoint(new Vector3(0.0f, length, 0.0f));
        Vector3 c3 = endTransform.position;
        
        for (int i = 0; i < vertCount; i++) {
            float t = (float)i / (float)(vertCount - 1);
            point = BezierPoint(c0, c1, c2, c3, t);
            tangent = BezierTangent(c0, c1, c2, c3, t);
            Vector3 rightVector = Vector3.Lerp(
                startTransform.right * direction,
                endTransform.right * direction,
                t
            );
            normalVector = Vector3.Cross(Vector3.Cross(tangent, rightVector), tangent).normalized;

            normalVector *= falloff.Evaluate(t) * radius;

            lineRenderer.SetPosition(i, point + normalVector);
        }

        tangent = tangent.normalized;
        normalVector = normalVector.normalized;

        for (int i = 0; i < endCapCount; i++) {
            float x = (float)i / (float)(endCapCount - 1);
            float y = Mathf.Sqrt(1.0f - x * x);
            Vector3 newPoint = point + tangent * x * radius + normalVector * y * radius;
            lineRenderer.SetPosition(i + vertCount, newPoint);
        }

    }

    Vector3 BezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
		{	
			float tt = t * t;
			float ttt = t * tt;
			float u = 1.0f - t;
			float uu = u * u;
			float uuu = u * uu;
			
			Vector3 B = new Vector3();
			B = uuu * p0;
			B += 3.0f * uu * t * p1;
			B += 3.0f * u * tt * p2;
			B += ttt * p3;
			
			return B;
		}

    Vector3 BezierTangent(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {	

		Vector3 q0 = p0 + ((p1 - p0) * t);
		Vector3 q1 = p1 + ((p2 - p1) * t);
		Vector3 q2 = p2 + ((p3 - p2) * t);

		Vector3 r0 = q0 + ((q1 - q0) * t);
		Vector3 r1 = q1 + ((q2 - q1) * t);
		Vector3 tangent = r1 - r0;
		return tangent;
    }
	
}