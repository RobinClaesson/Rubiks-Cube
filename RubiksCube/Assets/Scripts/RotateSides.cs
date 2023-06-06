using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RotateSides : MonoBehaviour
{
    int layermask = 1 << 7;
    CubeState cubeState;
    // Start is called before the first frame update
    void Start()
    {
        cubeState = FindObjectOfType<CubeState>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out raycastHit, 100f, layermask))
            {
                print(raycastHit.collider.gameObject.name);
            }
        }

        if(Input.GetKeyDown(KeyCode.U))
        {
            //read
            //SetRotation()
        }

    }

    private void SetRotation(List<GameObject> side, float angle)
    {
        GameObject middlePiece = side.First(x => x.tag == "MiddlePiece");
    }


}
