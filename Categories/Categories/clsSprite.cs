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
    class clsSprite
    {
        public Texture2D texture;
        public Vector2 position, position2;
        public Vector2 size, size2, infoSize;
        public bool pressed, released;
        public int parent;
        public string catName;
        public int firstLine;
        public float sum;
        public float percent, percent2;
        public bool active;
        public bool visible;
        public int level;
        public int qty;
        public float qtyPercent;

        public float dsum;
        public float dpercent, dpercent2;
        public bool dactive;
        public int dqty;
        public float dqtyPercent;

        public clsSprite(Texture2D newTexture, Vector2 newPosition, Vector2 newSize)
        {
            texture = newTexture;
            position = newPosition;
            size = newSize;
            visible = false;
            level = -1;
        }
    }
}
