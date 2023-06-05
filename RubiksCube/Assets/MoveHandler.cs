using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHandler : MonoBehaviour
{
    public RotateCube rotateCubeScript;

    // Update is called once per frame
    void Update()
    {

        //There is probably a better way to do this than manually checking each key

        //Rotations
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (Input.GetKey(KeyCode.P))
            {
                rotateCubeScript.MakeWholeRotationMove(WholeCubeRotation.Xp);
            }
            else
            {
                rotateCubeScript.MakeWholeRotationMove(WholeCubeRotation.X);
            }
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (Input.GetKey(KeyCode.P))
            {
                rotateCubeScript.MakeWholeRotationMove(WholeCubeRotation.Yp);
            }
            else
            {
                rotateCubeScript.MakeWholeRotationMove(WholeCubeRotation.Y);
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (Input.GetKey(KeyCode.P))
            {
                rotateCubeScript.MakeWholeRotationMove(WholeCubeRotation.Zp);
            }
            else
            {
                rotateCubeScript.MakeWholeRotationMove(WholeCubeRotation.Z);
            }
        }
    }

}
