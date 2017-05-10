﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDashObject : CTrackItem
{


    public CDashObject(CPlayer tPlayer, float tDuration) : base(tPlayer,tDuration)
    {

    }
    public override void Activate()
    {
        mPlayer.SetMagnet(true);
    }

    public override void Deactivate()
    {
        mPlayer.SetMagnet(false);
    }
}