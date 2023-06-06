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
        cubeState.UpdateState();

        UpdateCubeFace(cubeState.UpFaces, upMap);
        UpdateCubeFace(cubeState.DownFaces, downMap);
        UpdateCubeFace(cubeState.LeftFaces, leftMap);
        UpdateCubeFace(cubeState.RightFaces, rightMap);
        UpdateCubeFace(cubeState.FrontFaces, frontMap);
        UpdateCubeFace(cubeState.BackFaces, backMap);
    }

    private void UpdateCubeFace(List<GameObject> faces, Transform sideMap)
    {
        int i = 0;
        foreach(Transform map in sideMap)
        {
            map.GetComponent<UnityEngine.UI.Image>().color = faces[i++].GetComponent<Renderer>().material.color;
        }
    }


}
