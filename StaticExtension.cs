//using Box2DX.Common;
using Microsoft.Xna.Framework;

namespace Box2DX
{
    static class StaticExtension
    {
        public static float Cross(this Vector2 v, Vector2 a)
        {
            return 0;
        }

        public static void SetZero(this Vector2 v)
        {
            v.X = 0;
            v.Y = 0;
        }

        public static void Set(this Vector2 v, float x, float y)
        {
            v.X = x;
            v.Y = y;
        }
    }
}
