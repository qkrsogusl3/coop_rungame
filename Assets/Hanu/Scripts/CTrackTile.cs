﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTrackTile : MonoBehaviour {

    public Vector3 Direction;
    public CTrackCreater.TRACKKIND Kind;

    public int Index;

    public void SetIndex(int tIndex)
    {
        Index = tIndex;
    }

}
