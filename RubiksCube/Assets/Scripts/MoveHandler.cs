using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Text;

public class MoveHandler : MonoBehaviour
{
    RotateCube rotateCube;
    TurnSides turnSides;
    CubeMapHandler cubeMapHandler;

    public List<Move> movesToDo = new List<Move>();

    bool IsMoving => rotateCube.IsRotating || turnSides.IsTurning;
    bool doRayCast = true;
    
    public bool IsScrambling { get; private set; } = false;

    private void Start()
    {
        rotateCube = GetComponent<RotateCube>();
        turnSides = GetComponent<TurnSides>();
        cubeMapHandler = FindObjectOfType<CubeMapHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsScrambling)
            CheckKeys();

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
            {
                MakeMove(movesToDo[0]);
                movesToDo.RemoveAt(0);
            }
            else
            {
                IsScrambling = false;
            }
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
            if (Input.GetKey(KeyCode.P) || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
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
            if (Input.GetKey(KeyCode.P) || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
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
            if (Input.GetKey(KeyCode.P) || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
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
            if (Input.GetKey(KeyCode.P) || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
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
            if (Input.GetKey(KeyCode.P) || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
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
            if (Input.GetKey(KeyCode.P) || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
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
            if (Input.GetKey(KeyCode.P) || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
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
            if (Input.GetKey(KeyCode.P) || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
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
            if (Input.GetKey(KeyCode.P) || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                AddMove(Move.Bp);
            }
            else
            {
                AddMove(Move.B);
            }
        }


        //Scramble
        if (Input.GetKeyDown(KeyCode.S))
        {
            ScrambleCube();
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
        movesToDo.Add(move);
    }

    public void AddMoves(List<Move> moves)
    {
        movesToDo.AddRange(moves);
    }

    public void AddMoves(string moveString)
    {
        movesToDo.AddRange(ConvertStringToMoves(moveString));
    }

    private string[] possibleRandomMoves = new string[] { "U", "L", "F", "R", "B", "D" };
    public void ScrambleCube()
    {
        if (!IsMoving)
        {
            IsScrambling = true;
            List<string> baseMoves = new List<string>();
            int last = -1, current = -1;
            for (int i = 0; i < 25; i++)
            {
                while (last == current)
                    current = UnityEngine.Random.Range(0, possibleRandomMoves.Length);

                baseMoves.Add(possibleRandomMoves[current]);
                last = current;
            }

            string moves = string.Empty;
            foreach (string move in baseMoves)
            {
                int varation = UnityEngine.Random.Range(0, 3);

                switch (varation)
                {
                    case 0:
                        moves += $"{move} ";
                        break;
                    case 1:
                        moves += $"{move}p ";
                        break;
                    case 2:
                        moves += $"{move}2 ";
                        break;
                }
            }

            AddMoves(moves);
        }
    }


    public List<Move> ConvertStringToMoves(string moveString)
    {
        var splitMoves = moveString.Replace('\'', 'p').Split(' ').Where(s => s != string.Empty).ToList();

        for (int i = 0; i < splitMoves.Count(); i++)
        {
            if (splitMoves[i].Contains('2'))
            {
                var move = splitMoves[i].Substring(0, 1);
                splitMoves.RemoveAt(i);
                splitMoves.Insert(i, move);
                splitMoves.Insert(i, move);
            }
        }

        return splitMoves.Select(x => (Move)Enum.Parse(typeof(Move), x)).ToList();
    }

}
