using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartTriggerCircle : TriggerCircleBase
{
    protected override void OnTrue()
    {
        TitlePhaseManager.Instance.IncrementReadyForPlayerCount();
    }

    protected override void OnFalse()
    {
        TitlePhaseManager.Instance.DecrementReadyForPlayerCount();
    }
}
