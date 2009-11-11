using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FieryMonkey {
    abstract class Ifs {
        public int Iterations;
        public List<Func<Vector2, Vector2>> Functions;
        public List<float> Weights;
        
        public float MinX = -1f;
        public float MaxX = 1f;
        public float MinY = -1f;
        public float MaxY = 1f;

        public Func<Vector2, Vector2> FinalTransformation;

        public Ifs() {
            Iterations = 20;
            Functions = new List<Func<Vector2, Vector2>>();
            Weights = new List<float>();
            FinalTransformation = (v) => v;
        }
    }
}
