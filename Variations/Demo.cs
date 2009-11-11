using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FieryMonkey {
    class Demo : Ifs {
        public Demo() {
            Functions.Add((v) => {
                var x = 0.635724f * v.X + -0.389743f * v.Y - 0.082431f;
                var y = 0.320669f * v.X + 0.326878f * v.Y + 0.630195f;
                return new Vector2(x, y);
            });
            Weights.Add(0.33f);

            Functions.Add((v) => {
                var x = 0.589982f * v.X + -0.270471f * v.Y + 0.660299f;
                var y = 0.363869f * v.X + 0.570111f * v.Y + 0.062168f;
                return new Vector2(x, y);
            });
            Weights.Add(0.33f);

            Functions.Add((v) => {
                var x = 0.261893f * v.X + 0.229017f * v.Y + -0.135317f;
                var y = 0.250562f * v.X + 0.55856f * v.Y + 0.067391f;
                return new Vector2(x, y);
            });
            Weights.Add(0.33f);

            MinX = -1f;
            MaxX = 1.3f;
            MinY = -0.5f;
            MaxY = 1.5f;
        }
    }
}
