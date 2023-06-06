using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class CubeState : MonoBehaviour
{
    public List<GameObject> FrontFaces { get; private set; } = new List<GameObject>();
    public List<GameObject> BackFaces { get; private set; } = new List<GameObject>();
    public List<GameObject> LeftFaces { get; private set; } = new List<GameObject>();
    public List<GameObject> RightFaces { get; private set; } = new List<GameObject>();
    public List<GameObject> UpFaces { get; private set; } = new List<GameObject>();
    public List<GameObject> DownFaces { get; private set; } = new List<GameObject>();

    public List<GameObject> FrontPieces { get; private set; } = new List<GameObject>();
    public List<GameObject> BackPieces { get; private set; } = new List<GameObject>();
    public List<GameObject> LeftPieces { get; private set; } = new List<GameObject>();
    public List<GameObject> RightPieces { get; private set; } = new List<GameObject>();
    public List<GameObject> UpPieces { get; private set; } = new List<GameObject>();
    public List<GameObject> DownPieces { get; private set; } = new List<GameObject>();

    public Transform raysUp;
    public Transform raysDown;
    public Transform raysLeft;
    public Transform raysRight;
    public Transform raysFront;
    public Transform raysBack;

    int layermask = 1 << 8;

    public void UpdateState()
    {
        FrontFaces = ReadFace(raysFront);
        BackFaces = ReadFace(raysBack);
        RightFaces = ReadFace(raysRight);
        LeftFaces = ReadFace(raysLeft);
        UpFaces = ReadFace(raysUp);
        DownFaces = ReadFace(raysDown);

        FrontPieces = FrontFaces.Select(x => x.transform.parent.gameObject).ToList();
        BackPieces = BackFaces.Select(x => x.transform.parent.gameObject).ToList();
        LeftPieces = LeftFaces.Select(x => x.transform.parent.gameObject).ToList();
        RightPieces = RightFaces.Select(x => x.transform.parent.gameObject).ToList();
        UpPieces = UpFaces.Select(x => x.transform.parent.gameObject).ToList();
        DownPieces = DownFaces.Select(x => x.transform.parent.gameObject).ToList();
    }

    //public List<GameObject> GetSidePiecesFromFace(GameObject face)
    //{
    //    GameObject piece = face.transform.parent.gameObject;
        
    //}

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
