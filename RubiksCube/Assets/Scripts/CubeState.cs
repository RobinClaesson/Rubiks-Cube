using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeState : MonoBehaviour
{
    public List<GameObject> front = new List<GameObject>();
    public List<GameObject> back = new List<GameObject>();
    public List<GameObject> left = new List<GameObject>();
    public List<GameObject> right = new List<GameObject>();
    public List<GameObject> up = new List<GameObject>();
    public List<GameObject> down = new List<GameObject>();

    public Transform raysUp;
    public Transform raysDown;
    public Transform raysLeft;
    public Transform raysRight;
    public Transform raysFront;
    public Transform raysBack;

    int layermask = 1 << 8;

    public void ReadFaces()
    {
        front = ReadFace(raysFront);
        back = ReadFace(raysBack);
        right = ReadFace(raysRight);
        left = ReadFace(raysLeft);
        up = ReadFace(raysUp);
        down = ReadFace(raysDown);
    }

    private List<GameObject> ReadFace(Transform rayTransform)
    {
        List<GameObject> faces = new List<GameObject>();

        foreach (Transform startTransform in rayTransform)
        {
            RaycastHit raycastHit;
            if (Physics.Raycast(startTransform.position, rayTransform.forward, out raycastHit, 2, layermask))
            {
                faces.Add(raycastHit.collider.gameObject);
                //Debug.DrawRay(startTransform.position, rayTransform.forward * 2, raycastHit.collider.gameObject.GetComponent<Renderer>().material.color);
            }

        }

        return faces;
    }
}
