using Unity.Mathematics;
using UnityEngine;

namespace RTS.Runtime.Helper
{
    public static class MathematicsHelper
    {
        public static float3 x0y(this float2 xy)
        {
            return new float3(xy.x,0,xy.y);
        }

        public static float4 color2float4(this Color color)
        {
            return new float4(color.r, color.g, color.b, color.a);
        }
    }
}