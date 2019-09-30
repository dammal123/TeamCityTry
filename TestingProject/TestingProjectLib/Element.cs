using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;

namespace ProfilDrogi
{
    public class Element
    {
        public string Name { get; set; }
        public string Surface { get; set; }
        public float Width { get; set; }
        public int Hachure { get; set; }

        public Element(string name, string surface, float width, int hachure)
        {
            this.Name = name;
            this.Surface = surface;
            this.Width = width;
            this.Hachure = hachure;
        }
    }
}
