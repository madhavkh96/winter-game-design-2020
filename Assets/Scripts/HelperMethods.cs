using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperMethods
{
    public static float CountDown(float time) {
        time -= Time.deltaTime;
        return time;
    }
}
