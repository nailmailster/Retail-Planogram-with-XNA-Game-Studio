using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Net;
//using Microsoft.Xna.Framework.Storage;

namespace Categories
{
    class good
    {
        public Texture2D texture;
        public string code;
        public string description;
        public int index;
        public Vector2 position, oldPosition;
        public Vector2 size;
        public Color colour;
        public bool active;
        public float sum;
        public float percent;
        public int qty;
        public float percent2;

        public bool dactive;
        public float dsum;
        public float dpercent;
        public int dqty;
        public float dpercent2;
    }
}
