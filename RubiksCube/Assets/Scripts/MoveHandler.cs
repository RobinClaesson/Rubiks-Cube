using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Newtonsoft.Json.Bson;
using Unity.VisualScripting;

public class MoveHandler : MonoBehaviour
{
    RotateCube rotateCubeScript;
    public Queue<Move> movesToDo = new Queue<Move>();

    public bool IsMoving => rotateCubeScript.isRotating;

    private void Start()
    {
        rotateCubeScript = GetComponent<RotateCube>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckKeys();
        CheckDebugKeys();

        //Make next move
        if (movesToDo.Count > 0 && !IsMoving)
            MakeMove(movesToDo.Dequeue());
    }

    private void MakeMove(Move move)
    {
        switch (move)
        {
            case Move.X:
            case Move.Xp:
            case Move.Y:
            case Move.Yp:
            case Move.Z:
            case Move.Zp:
                rotateCubeScript.MakeWholeRotationMove(move);
                break;
        }
    }

    private void CheckKeys()
    {
        //There is probably a better way to do this than manually checking each key

        //Rotations
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (Input.GetKey(KeyCode.P))
            {
                movesToDo.Enqueue(Move.Xp);
            }
            else
            {
                movesToDo.Enqueue(Move.X);
            }
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (Input.GetKey(KeyCode.P))
            {
                movesToDo.Enqueue(Move.Yp);
            }
            else
            {
                movesToDo.Enqueue(Move.Y);
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (Input.GetKey(KeyCode.P))
            {
                movesToDo.Enqueue(Move.Zp);
            }
            else
            {
                movesToDo.Enqueue(Move.Z);
            }
        }
    }

    public void MakeMoves(List<Move> moves)
    {
        foreach (Move move in moves)
            movesToDo.Enqueue(move);
    }

    public void MakeMoves(string moveString)
    {
        MakeMoves(ConvertStringToMoves(moveString));
    }

    public List<Move> ConvertStringToMoves(string moveString)
    {
        return moveString.Replace('\'', 'p').Split(' ').Where(s => s != string.Empty).Select(x => (Move)Enum.Parse(typeof(Move), x)).ToList();
    }

    private void CheckDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            MakeMoves(new List<Move>() { Move.X, Move.Xp, Move.Y, Move.Yp, Move.Z, Move.Zp });

        if (Input.GetKeyDown(KeyCode.Alpha2))
            MakeMoves("X X X Y Y Y Z Z Z Xp X");

        if (Input.GetKeyDown(KeyCode.Alpha3))
            MakeMoves("X X X' X' Y Y' Y Y' Z Z'       Xp X");
    }

}
