using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Adventure
{
    class Input {
        static Dictionary<Keys, bool> keys = new Dictionary<Keys, bool>();
        public static bool IsKeyDown (Keys key) {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(key)) {
                if (!keys[key])
                    return true;
                keys[key] = true;
            }

            if(ks.IsKeyUp(key)) {
                keys[key] = false;
            }
            return false;
        }

        public static bool IsKeyPressed (Keys key)
        {
            KeyboardState ks = Keyboard.GetState();
            return ks.IsKeyDown(key);
        }
    }
}
