using JetBrains.Annotations;
using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadCubeFaces : MonoBehaviour
{
    public Transform raysUp;
    public Transform raysDown;
    public Transform raysLeft;
    public Transform raysRight;
    public Transform raysFront;
    public Transform raysBack;

    int layermask = 1 << 8;
    CubeState cubeState;


    // Start is called before the first frame update
    void Start()
    {
        cubeState = GetComponent<CubeState>();


    }

    public void ReadFaces()
    {
        cubeState.front = ReadFace(raysFront);
        cubeState.back = ReadFace(raysBack);
        cubeState.right = ReadFace(raysRight);
        cubeState.left = ReadFace(raysLeft);
        cubeState.up = ReadFace(raysUp);
        cubeState.down = ReadFace(raysDown);
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
