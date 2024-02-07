using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRaycastReciever
{
    public void RaycastAct(PlayerInputReciever.TTouchState ttouchState);
}
