using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace AsepritePipeline
{
    public class AsepriteJSON
    {
        public Dictionary<string, AsepriteFrame> frames;
        public AsepriteMeta meta;
    }

    public class AsepriteFrame
    {
        public AsepriteRectangle frame;
        public bool rotated;
        public bool trimmed;
        public AsepriteRectangle spriteSourceSize;
        public AsepriteDimensions sourceSize;
        public int duration;
    }

    public class AsepriteMeta
    {
        public string app;
        public string version;
        public string image;
        public string format;
        public AsepriteDimensions size;
        public string scale;
        public List<AsepriteTag> frameTags;
        public List<AsepriteLayer> layers;
        public List<AsepriteSlice> slices;
    }

    public class AsepriteTag
    {
        public string name;
        public int from;
        public int to;
        public string direction;
    }

    public class AsepriteLayer
    {
        public string name;
        public int opacity;
        public string blendMode;
    }

    public class AsepriteSlice
    {
        // TODO: Slice?
    }

    public class AsepriteRectangle
    {
        public int x, y, w, h;

        public Rectangle ToXnaRect() => new(x, y, w, h);
    }

    public class AsepriteDimensions
    {
        public int w;
        public int h;
    }
}
