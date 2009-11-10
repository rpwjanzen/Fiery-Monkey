using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FieryMonkey {
    abstract class IFS {
        public List<Func<Vector2, Vector2>> Functions;
        public List<float> Weights;
        public IFS() {
            Functions = new List<Func<Vector2, Vector2>>();
            Weights = new List<float>();
        }
    }
}
