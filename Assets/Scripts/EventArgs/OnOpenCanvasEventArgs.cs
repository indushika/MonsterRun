using System.Collections;
using System.Collections.Generic;
using MonsterRun.Canvas;
using UnityEngine;

public class OnOpenCanvasEventArgs 
{
    public CanvasConfigBase CanvasConfig
    {
        get;
        set;
    }

    public bool ClosePreviousCanvas
    {
        get;
        set; 
    }
}
