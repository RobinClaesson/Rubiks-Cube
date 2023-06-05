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

    // Start is called before the first frame update
    void Start()
    {

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
            if (transform.rotation != rotationTarget.transform.rotation)
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
            if (swipe.y > 0 && swipe.x > 0f)
                MakeWholeRotationMove(WholeCubeRotation.X);
            else if (swipe.y < 0 && swipe.x < 0f)
                MakeWholeRotationMove(WholeCubeRotation.Xp);

            else if (swipe.x < 0 && swipe.y > -0.5f && swipe.y < 0.5f)
                MakeWholeRotationMove(WholeCubeRotation.Y);
            else if (swipe.x > 0 && swipe.y > -0.5f && swipe.y < 0.5f)
                MakeWholeRotationMove(WholeCubeRotation.Yp);

            else if (swipe.y < 0 && swipe.x > 0f)
                MakeWholeRotationMove(WholeCubeRotation.Z);
            else if (swipe.y > 0 && swipe.x < 0f)
                MakeWholeRotationMove(WholeCubeRotation.Zp);
            
        }
    }

    public void MakeWholeRotationMove(WholeCubeRotation move)
    {
        switch (move)
        {
            case WholeCubeRotation.X:
                rotationTarget.transform.Rotate(0, 0, -90, Space.World);
                break;

            case WholeCubeRotation.Xp:
                rotationTarget.transform.Rotate(0, 0, 90, Space.World);
                break;

            case WholeCubeRotation.Y:
                rotationTarget.transform.Rotate(0, 90, 0, Space.World);
                break;

            case WholeCubeRotation.Yp:
                rotationTarget.transform.Rotate(0, -90, 0, Space.World);
                break;

            case WholeCubeRotation.Z:
                rotationTarget.transform.Rotate(-90, 0, 0, Space.World);
                break;

            case WholeCubeRotation.Zp:
                rotationTarget.transform.Rotate(90, 0, 0, Space.World);
                break;
        }

        print(move.ToString());
    }
}
