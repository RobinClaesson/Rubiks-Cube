using Newtonsoft.Json.Bson;
using UnityEngine;

public class RotateCube : MonoBehaviour
{
    Vector2 pressPos;
    Vector2 swipe;

    Vector3 oldMousePos;

    public GameObject rotationTarget;
    public float rotationSpeed = 200f;
    public float dragSpeed = 0.1f;

    public bool isRotating => transform.rotation != rotationTarget.transform.rotation;

    MoveHandler moveHandler;

    private void Start()
    {
        moveHandler = FindObjectOfType<MoveHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckSwipe();
        UpdateRotation();

        oldMousePos = Input.mousePosition;
    }

    /// <summary>
    /// Updates the rotation of the cube based on the mouse position and target rotation
    /// </summary>
    private void UpdateRotation()
    {
        if (Input.GetMouseButton(1))
        {
            //Drag cube while right click is down
            Vector3 mouseDelta = (Input.mousePosition - oldMousePos) * dragSpeed;
            transform.rotation = Quaternion.Euler(mouseDelta.y, -mouseDelta.x, 0) * transform.rotation;
        }
        else
        {
            //Rotate cube towards the swiped position
            if (isRotating)
            {
                var step = rotationSpeed * Time.deltaTime;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationTarget.transform.rotation, step);
            }
        }
    }

    /// <summary>
    /// Checks if a swipe was made and rotates the cube accordingly
    /// </summary>
    private void CheckSwipe()
    {
        //Save position at initial click
        if (Input.GetMouseButtonDown(1))
        {
            pressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        //Calclulate Swipe on release
        if (Input.GetMouseButtonUp(1))
        {
            swipe = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - pressPos;
            swipe.Normalize();

            //Find what swipe was made
            if (swipe.x < 0 && swipe.y > -0.5f && swipe.y < 0.5f)
                MakeWholeRotationMove(Move.Y);
            else if (swipe.x > 0 && swipe.y > -0.5f && swipe.y < 0.5f)
                MakeWholeRotationMove(Move.Yp);

            else if (swipe.y < 0 && swipe.x > 0f)
                MakeWholeRotationMove(Move.Z);
            else if (swipe.y > 0 && swipe.x < 0f)
                MakeWholeRotationMove(Move.Zp);

            else if (swipe.y > 0 && swipe.x > 0f)
                MakeWholeRotationMove(Move.X);
            else if (swipe.y < 0 && swipe.x < 0f)
                MakeWholeRotationMove(Move.Xp);

            moveHandler.DoRayCast();
        }
    }

    public void MakeWholeRotationMove(Move move)
    {
        switch (move)
        {
            case Move.X:
                rotationTarget.transform.Rotate(0, 0, -90, Space.World);
                break;

            case Move.Xp:
                rotationTarget.transform.Rotate(0, 0, 90, Space.World);
                break;

            case Move.Y:
                rotationTarget.transform.Rotate(0, 90, 0, Space.World);
                break;

            case Move.Yp:
                rotationTarget.transform.Rotate(0, -90, 0, Space.World);
                break;

            case Move.Z:
                rotationTarget.transform.Rotate(-90, 0, 0, Space.World);
                break;

            case Move.Zp:
                rotationTarget.transform.Rotate(90, 0, 0, Space.World);
                break;
        }
    }
}
