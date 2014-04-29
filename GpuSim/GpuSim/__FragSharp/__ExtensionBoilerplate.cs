// This file was auto-generated by FragSharp. It will be regenerated on the next compilation.
// Manual changes made will not persist and may cause incorrect behavior between compilations.

using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using FragSharpFramework;

namespace GpuSim
{
    [Hlsl("float4")]
    public partial struct unit
    {
        [Hlsl("float4")]
        public unit(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        [Hlsl("x")]
        public float x;

        [Hlsl("y")]
        public float y;

        [Hlsl("z")]
        public float z;

        [Hlsl("w")]
        public float w;

        [Hlsl("xy")]
        public vec2 xy { get { return new vec2(x, y); } set { x = value.x; y = value.y; } }

        [Hlsl("zw")]
        public vec2 zw { get { return new vec2(z, w); } set { z = value.x; w = value.y; } }

        [Hlsl("xyz")]
        public vec3 xyz { get { return new vec3(x, y, z); } set { x = value.x; y = value.y; z = value.z; } }

        [Hlsl("r")]
        public float r { get { return x; } set { x = value; } }

        [Hlsl("g")]
        public float g { get { return y; } set { y = value; } }

        [Hlsl("b")]
        public float b { get { return z; } set { z = value; } }

        [Hlsl("a")]
        public float a { get { return w; } set { w = value; } }

        [Hlsl("rgb")]
        public vec3 rgb { get { return xyz; } set { xyz = value; } }

        [Hlsl("rg")]
        public vec2 rg { get { return xy; } set { xy = value; } }

        [Hlsl("ba")]
        public vec2 ba { get { return zw; } set { zw = value; } }


        public static unit operator *(float a, unit v)
        {
            return new unit(a * v.x, a * v.y, a * v.z, a * v.w);
        }

        public static unit operator *(unit v, float a)
        {
            return new unit(a * v.x, a * v.y, a * v.z, a * v.w);
        }

        public static unit operator /(float a, unit v)
        {
            return new unit(a / v.x, a / v.y, a / v.z, a / v.w);
        }

        public static unit operator /(unit v, float a)
        {
            return new unit(v.x / a, v.y / a, v.z / a, v.w / a);
        }

        public static unit operator +(unit v, unit w)
        {
            return new unit(v.x + w.x, v.y + w.y, v.z + w.z, v.w + w.w);
        }

        public static unit operator -(unit v, unit w)
        {
            return new unit(v.x - w.x, v.y - w.y, v.z - w.z, v.w - w.w);
        }

        public static unit operator *(unit v, unit w)
        {
            return new unit(v.x * w.x, v.y * w.y, v.z * w.z, v.w * w.w);
        }

        public static unit operator /(unit v, unit w)
        {
            return new unit(v.x / w.x, v.y / w.y, v.z / w.z, v.w / w.w);
        }

        public static bool operator ==(unit v, unit w)
        {
            return
                v.x == w.x &&
                v.y == w.y &&
                v.z == w.z &&
                v.w == w.w;
        }

        public static bool operator !=(unit v, unit w)
        {
            return
                v.x != w.x ||
                v.y != w.y ||
                v.z != w.z ||
                v.w != w.w;
        }

        public static unit operator -(unit v)
        {
            return new unit(-v.x, -v.y, -v.z, -v.w);
        }

        public static implicit operator Vector4(unit v)
        {
            return new Vector4(v.x, v.y, v.z, v.w);
        }

        public static implicit operator unit(color v)
        {
            return new unit(v.x, v.y, v.z, v.w);
        }

        public static implicit operator color(unit v)
        {
            return new color(v.x, v.y, v.z, v.w);
        }

        public static explicit operator unit(Vector4 v)
        {
            return new unit(v.X, v.Y, v.Z, v.W);
        }

        public static readonly unit Zero = new unit(0, 0, 0, 0);
    }
}

namespace GpuSim
{
    [Hlsl("float4")]
    public partial struct data
    {
        [Hlsl("float4")]
        public data(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        [Hlsl("x")]
        public float x;

        [Hlsl("y")]
        public float y;

        [Hlsl("z")]
        public float z;

        [Hlsl("w")]
        public float w;

        [Hlsl("xy")]
        public vec2 xy { get { return new vec2(x, y); } set { x = value.x; y = value.y; } }

        [Hlsl("zw")]
        public vec2 zw { get { return new vec2(z, w); } set { z = value.x; w = value.y; } }

        [Hlsl("xyz")]
        public vec3 xyz { get { return new vec3(x, y, z); } set { x = value.x; y = value.y; z = value.z; } }

        [Hlsl("r")]
        public float r { get { return x; } set { x = value; } }

        [Hlsl("g")]
        public float g { get { return y; } set { y = value; } }

        [Hlsl("b")]
        public float b { get { return z; } set { z = value; } }

        [Hlsl("a")]
        public float a { get { return w; } set { w = value; } }

        [Hlsl("rgb")]
        public vec3 rgb { get { return xyz; } set { xyz = value; } }

        [Hlsl("rg")]
        public vec2 rg { get { return xy; } set { xy = value; } }

        [Hlsl("ba")]
        public vec2 ba { get { return zw; } set { zw = value; } }


        public static data operator *(float a, data v)
        {
            return new data(a * v.x, a * v.y, a * v.z, a * v.w);
        }

        public static data operator *(data v, float a)
        {
            return new data(a * v.x, a * v.y, a * v.z, a * v.w);
        }

        public static data operator /(float a, data v)
        {
            return new data(a / v.x, a / v.y, a / v.z, a / v.w);
        }

        public static data operator /(data v, float a)
        {
            return new data(v.x / a, v.y / a, v.z / a, v.w / a);
        }

        public static data operator +(data v, data w)
        {
            return new data(v.x + w.x, v.y + w.y, v.z + w.z, v.w + w.w);
        }

        public static data operator -(data v, data w)
        {
            return new data(v.x - w.x, v.y - w.y, v.z - w.z, v.w - w.w);
        }

        public static data operator *(data v, data w)
        {
            return new data(v.x * w.x, v.y * w.y, v.z * w.z, v.w * w.w);
        }

        public static data operator /(data v, data w)
        {
            return new data(v.x / w.x, v.y / w.y, v.z / w.z, v.w / w.w);
        }

        public static bool operator ==(data v, data w)
        {
            return
                v.x == w.x &&
                v.y == w.y &&
                v.z == w.z &&
                v.w == w.w;
        }

        public static bool operator !=(data v, data w)
        {
            return
                v.x != w.x ||
                v.y != w.y ||
                v.z != w.z ||
                v.w != w.w;
        }

        public static data operator -(data v)
        {
            return new data(-v.x, -v.y, -v.z, -v.w);
        }

        public static implicit operator Vector4(data v)
        {
            return new Vector4(v.x, v.y, v.z, v.w);
        }

        public static implicit operator data(color v)
        {
            return new data(v.x, v.y, v.z, v.w);
        }

        public static implicit operator color(data v)
        {
            return new color(v.x, v.y, v.z, v.w);
        }

        public static explicit operator data(Vector4 v)
        {
            return new data(v.X, v.Y, v.Z, v.W);
        }

        public static readonly data Zero = new data(0, 0, 0, 0);
    }
}

