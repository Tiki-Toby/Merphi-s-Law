using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HairyEngine.HairyCamera
{
    public static class Utils
    {
        public static float Remap(this float value, float from1, float to1, float from2, float to2)
        {
            return Mathf.Clamp((value - from1) / (to1 - from1) * (to2 - from2) + from2, from2, to2);
        }
        public static Vector3 NablaMultiply(this Vector3 v1, Vector3 v2)
        {
            v1.x *= v2.x;
            v1.y *= v2.y;
            v1.z *= v2.z;
            return v1;
        }
    }
}