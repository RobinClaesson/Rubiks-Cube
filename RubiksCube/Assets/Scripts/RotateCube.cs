using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
            if (LeftSwipe())
            {
                rotationTarget.transform.Rotate(0, 90, 0, Space.World);
            }
            else if (RightSwipe())
            {
                rotationTarget.transform.Rotate(0, -90, 0, Space.World);
            }
            else if (UpLeftSwipe())
            {
                rotationTarget.transform.Rotate(90, 0, 0, Space.World);
            }
            else if (DownLeftSwipe())
            {
                rotationTarget.transform.Rotate(0, 0, 90, Space.World);
            }
            else if (UpRightSwipe())
            {
                rotationTarget.transform.Rotate(0, 0, -90, Space.World);
            }
            else if (DownRightSwipe())
            {
                rotationTarget.transform.Rotate(-90, 0, 0, Space.World);
            }
        }
    }

    //functions to determine a swipe
    bool LeftSwipe()
    {
        return swipe.x < 0 && swipe.y > -0.5f && swipe.y < 0.5f;
    }

    bool RightSwipe()
    {
        return swipe.x > 0 && swipe.y > -0.5f && swipe.y < 0.5f;
    }

    bool UpLeftSwipe()
    {
        return swipe.y > 0 && swipe.x < 0f;
    }

    bool UpRightSwipe()
    {
        return swipe.y > 0 && swipe.x > 0f;
    }

    bool DownLeftSwipe()
    {
        return swipe.y < 0 && swipe.x < 0f;
    }

    bool DownRightSwipe()
    {
        return swipe.y < 0 && swipe.x > 0f;
    }


}
