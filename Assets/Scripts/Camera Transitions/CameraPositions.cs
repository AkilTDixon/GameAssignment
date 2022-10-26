using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositions : MonoBehaviour
{
    /*
 Camera Positions

Level 1 Stage 1: -1.47, 3.9, -53

Phase 1: -67.32, -0.02, -39.5

Phase 2: -67.32, 3.18, -48.15


Level 2:

Stage 1: 0, 1, -10

Boss: -181.1, 7.77, -23.12
 
 */
    public Vector3 Stage1Pos;
    public Vector3 Stage2Pos;
    public Vector3 Boss1Phase1Pos;
    public Vector3 Boss1Phase2Pos;

    void Start()
    {
        Stage1Pos = new Vector3(-1.47f, 3.9f, -53f);
        Stage2Pos = new Vector3(-32.78f, 4.531f, -45.92f);
        Boss1Phase1Pos = new Vector3(-67.32f, -0.02f, -39.5f);
        Boss1Phase2Pos = new Vector3(-68.269f, 2.431f, -44.088f);
    }
}


