using Unity.Mathematics;

namespace RTS.Runtime.Helper
{
    public static class MathematicsHelper
    {
        public static float3 x0y(this float2 xy)
        {
            return new float3(xy.x,0,xy.y);
        }
    }
}