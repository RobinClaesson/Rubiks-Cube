using System.Collections.Generic;
using UnityEngine;

public class CubeMapHandler : MonoBehaviour
{
    public Transform frontMap;
    public Transform backMap;
    public Transform leftMap;
    public Transform rightMap;
    public Transform upMap;
    public Transform downMap;

    CubeState cubeState;
    // Start is called before the first frame update
    void Start()
    {
        cubeState = FindObjectOfType<CubeState>();
    }

    public void UpdateCubeMap()
    {
        cubeState.ReadFaces();

        UpdateCubeFace(cubeState.up, upMap);
        UpdateCubeFace(cubeState.down, downMap);
        UpdateCubeFace(cubeState.left, leftMap);
        UpdateCubeFace(cubeState.right, rightMap);
        UpdateCubeFace(cubeState.front, frontMap);
        UpdateCubeFace(cubeState.back, backMap);
    }

    private void UpdateCubeFace(List<GameObject> hitFaces, Transform sideMap)
    {
        int i = 0;
        foreach(Transform map in sideMap)
        {
            map.GetComponent<UnityEngine.UI.Image>().color = hitFaces[i++].GetComponent<Renderer>().material.color;
        }
    }


}
