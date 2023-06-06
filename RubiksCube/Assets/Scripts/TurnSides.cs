using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnSides : MonoBehaviour
{
    int layermask = 1 << 8;
    CubeState cubeState;
    Transform cubeTransform;

    public float turnSpeed = 200f;

    public Transform turnTarget;
    Transform middlePieceTransform;
    List<GameObject> otherTurningPieces = new List<GameObject>();
    public bool IsTurning => otherTurningPieces.Count > 0;

    // Start is called before the first frame update
    void Start()
    {
        cubeState = FindObjectOfType<CubeState>();
        cubeTransform = GameObject.FindGameObjectWithTag("Cube").transform;
        middlePieceTransform = turnTarget;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTurning();


        //if (Input.GetMouseButtonDown(0))
        //{
        //    RaycastHit raycastHit;
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    if(Physics.Raycast(ray, out raycastHit, 100f, layermask))
        //    {
        //        //List<GameObject> side = cubeState.GetSidePiecesFromPiece(raycastHit.transform.gameObject);
        //        //SetRotation(side, 90);
        //    }
        //}

    }

    private void UpdateTurning()
    {
        if (IsTurning)
        {
            if (middlePieceTransform.rotation != turnTarget.rotation)
            {
                var step = turnSpeed * Time.deltaTime;
                middlePieceTransform.rotation = Quaternion.RotateTowards(middlePieceTransform.rotation, turnTarget.rotation, step);
            }
            else
            {
                foreach (GameObject piece in otherTurningPieces)
                {
                    piece.transform.parent = cubeTransform;
                }
                otherTurningPieces.Clear();
            }

        }
    }

    public void MakeSideTurnMove(Move move)
    {

        switch (move)
        {
            case Move.U:
                SetRotation(cubeState.UpPieces, 90);
                break;
            case Move.Up:
                SetRotation(cubeState.UpPieces, -90);
                break;
            case Move.D:
                SetRotation(cubeState.DownPieces, 90);
                break;
            case Move.Dp:
                SetRotation(cubeState.DownPieces, -90);
                break;
            case Move.L:
                SetRotation(cubeState.LeftPieces, 90);
                break;
            case Move.Lp:
                SetRotation(cubeState.LeftPieces, -90);
                break;
            case Move.R:
                SetRotation(cubeState.RightPieces, 90);
                break;
            case Move.Rp:
                SetRotation(cubeState.RightPieces, -90);
                break;
            case Move.F:
                SetRotation(cubeState.FrontPieces, 90);
                break;
            case Move.Fp:
                SetRotation(cubeState.FrontPieces, -90);
                break;
            case Move.B:
                SetRotation(cubeState.BackPieces, 90);
                break;
            case Move.Bp:
                SetRotation(cubeState.BackPieces, -90);
                break;
        }
    }

    private void SetRotation(List<GameObject> side, float angle)
    {
        GameObject middlePiece = side.First(x => x.tag == "MiddlePiece");
        middlePieceTransform = middlePiece.transform;
        otherTurningPieces = side.Where(x => x != middlePiece).ToList();

        foreach (GameObject piece in otherTurningPieces)
        {
            piece.transform.parent = middlePiece.transform;
        }

        turnTarget.position = middlePieceTransform.position;
        turnTarget.rotation = middlePieceTransform.rotation;

        Vector3 axis = middlePiece.transform.localPosition.normalized;
        turnTarget.transform.Rotate(axis, angle);
    }


}
