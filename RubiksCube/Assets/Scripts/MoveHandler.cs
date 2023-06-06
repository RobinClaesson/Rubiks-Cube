using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Newtonsoft.Json.Bson;
using Unity.VisualScripting;

public class MoveHandler : MonoBehaviour
{
    RotateCube rotateCube;
    TurnSides turnSides;
    CubeMapHandler cubeMapHandler;

    public Queue<Move> movesToDo = new Queue<Move>();

    bool IsMoving => rotateCube.IsRotating || turnSides.IsTurning;
    bool doRayCast = true;

    private void Start()
    {
        rotateCube = GetComponent<RotateCube>();
        turnSides = GetComponent<TurnSides>();
        cubeMapHandler = FindObjectOfType<CubeMapHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckKeys();
        CheckDebugKeys();

        //Make next move
        if (!IsMoving)
        {
            if (doRayCast)
            {
                cubeMapHandler.UpdateCubeMap();
                doRayCast = false;
                //print("Ray Cast");
            }

            if (movesToDo.Count > 0)
                MakeMove(movesToDo.Dequeue());
        }

    }

    public void DoRayCast()
    {
        doRayCast = true;
    }

    private void CheckKeys()
    {
        //There is probably a better way to do this than manually checking each key

        //Rotations
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (Input.GetKey(KeyCode.P))
            {
                AddMove(Move.Xp);
            }
            else
            {
                AddMove(Move.X);
            }
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (Input.GetKey(KeyCode.P))
            {
                AddMove(Move.Yp);
            }
            else
            {
                AddMove(Move.Y);
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (Input.GetKey(KeyCode.P))
            {
                AddMove(Move.Zp);
            }
            else
            {
                AddMove(Move.Z);
            }
        }

        //Sides
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (Input.GetKey(KeyCode.P))
            {
                AddMove(Move.Up);
            }
            else
            {
                AddMove(Move.U);
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.P))
            {
                AddMove(Move.Dp);
            }
            else
            {
                AddMove(Move.D);
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (Input.GetKey(KeyCode.P))
            {
                AddMove(Move.Lp);
            }
            else
            {
                AddMove(Move.L);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Input.GetKey(KeyCode.P))
            {
                AddMove(Move.Rp);
            }
            else
            {
                AddMove(Move.R);
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Input.GetKey(KeyCode.P))
            {
                AddMove(Move.Fp);
            }
            else
            {
                AddMove(Move.F);
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (Input.GetKey(KeyCode.P))
            {
                AddMove(Move.Bp);
            }
            else
            {
                AddMove(Move.B);
            }
        }
    }

    private void MakeMove(Move move)
    {
        int translatedMove = (int)move;
        switch (translatedMove)
        {
            case <= 5:
                rotateCube.MakeWholeRotationMove(move);
                break;

            case > 5 and <= 17:
                turnSides.MakeSideTurnMove(move);
                break;
        }

        doRayCast = true;
    }

    public void AddMove(Move move)
    {
        movesToDo.Enqueue(move);
    }

    public void AddMoves(List<Move> moves)
    {
        foreach (Move move in moves)
            movesToDo.Enqueue(move);
    }

    public void AddMoves(string moveString)
    {
        AddMoves(ConvertStringToMoves(moveString));
    }

    public List<Move> ConvertStringToMoves(string moveString)
    {
        return moveString.Replace('\'', 'p').Split(' ').Where(s => s != string.Empty).Select(x => (Move)Enum.Parse(typeof(Move), x)).ToList();
    }

    private void CheckDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            AddMoves(new List<Move>() { Move.X, Move.Xp, Move.Y, Move.Yp, Move.Z, Move.Zp });

        if (Input.GetKeyDown(KeyCode.Alpha2))
            AddMoves("X X X Y Y Y Z Z Z Xp X");

        if (Input.GetKeyDown(KeyCode.Alpha3))
            AddMoves("X X X' X' Y Y' Y Y' Z Z'       Xp X");

        if (Input.GetKeyDown(KeyCode.Alpha4))
            AddMoves("R U Rp Up");

        if (Input.GetKeyDown(KeyCode.Alpha5))
            for (int i = 0; i < 7; i++)
                AddMoves("R U Rp Up Y");
    }

}
