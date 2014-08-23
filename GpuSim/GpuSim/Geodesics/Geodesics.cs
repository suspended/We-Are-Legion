using FragSharpFramework;

namespace GpuSim
{
    public partial class Geodesic_Outline : SimShader
    {
        [FragmentShader]
        geo FragmentShader(VertexOut vertex, Field<tile> Tiles, [Vals.Bool] bool Anti)
        {
            tile
                here       = Tiles[Here],
                right      = Tiles[RightOne],
                up         = Tiles[UpOne],
                left       = Tiles[LeftOne],
                down       = Tiles[DownOne],
                up_right   = Tiles[UpRight],
                up_left    = Tiles[UpLeft],
                down_right = Tiles[DownRight],
                down_left  = Tiles[DownLeft];

            if (IsBlockingTile(here)) return geo.Nothing;

            float dir = 0;

            if (IsBlockingTile(up_left))    dir = Anti ? Dir.Left  : Dir.Up;
            if (IsBlockingTile(up_right))   dir = Anti ? Dir.Up    : Dir.Right;
            if (IsBlockingTile(down_right)) dir = Anti ? Dir.Right : Dir.Down;
            if (IsBlockingTile(down_left))  dir = Anti ? Dir.Down  : Dir.Left;

            if (Anti)
            {
                if (IsBlockingTile(right))
                {
                    dir = Dir.Up;
                    if (IsBlockingTile(up))
                    {
                        dir = Dir.Left;
                        if (IsBlockingTile(left))
                            dir = Dir.Down;
                    }
                }

                if (IsBlockingTile(up))
                {
                    dir = Dir.Left;
                    if (IsBlockingTile(left))
                    {
                        dir = Dir.Down;
                        if (IsBlockingTile(down))
                            dir = Dir.Right;
                    }
                }

                if (IsBlockingTile(left))
                {
                    dir = Dir.Down;
                    if (IsBlockingTile(down))
                    {
                        dir = Dir.Right;
                        if (IsBlockingTile(right))
                            dir = Dir.Up;
                    }
                }

                if (IsBlockingTile(down))
                {
                    dir = Dir.Right;
                    if (IsBlockingTile(right))
                    {
                        dir = Dir.Up;
                        if (IsBlockingTile(up))
                            dir = Dir.Left;
                    }
                }
            }
            else
            {
                if (IsBlockingTile(right))
                {
                    dir = Dir.Down;
                    if (IsBlockingTile(down))
                    {
                        dir = Dir.Left;
                        if (IsBlockingTile(left))
                            dir = Dir.Up;
                    }
                }

                if (IsBlockingTile(up))
                {
                    dir = Dir.Right;
                    if (IsBlockingTile(right))
                    {
                        dir = Dir.Down;
                        if (IsBlockingTile(down))
                            dir = Dir.Left;
                    }
                }

                if (IsBlockingTile(left))
                {
                    dir = Dir.Up;
                    if (IsBlockingTile(up))
                    {
                        dir = Dir.Right;
                        if (IsBlockingTile(right))
                            dir = Dir.Down;
                    }
                }

                if (IsBlockingTile(down))
                {
                    dir = Dir.Left;
                    if (IsBlockingTile(left))
                    {
                        dir = Dir.Up;
                        if (IsBlockingTile(up))
                            dir = Dir.Right;
                    }
                }
            }

            geo output = geo.Nothing;
            output.dir = dir;

            int surround_count =
                (IsBlockingTile(up)    ? 1 : 0) +
                (IsBlockingTile(left)  ? 1 : 0) +
                (IsBlockingTile(down)  ? 1 : 0) +
                (IsBlockingTile(right) ? 1 : 0);

            if (output.dir > _0 && surround_count == 3)
            {
                output.bad = _true;
            }

            return output;
        }
    }

    public partial class Geodesic_OutlineCleanup : SimShader
    {
        [FragmentShader]
        geo FragmentShader(VertexOut vertex, Field<tile> Tiles, Field<geo> Geo, [Vals.Bool] bool Anti)
        {
            tile
                here       = Tiles[Here],
                right      = Tiles[RightOne],
                up         = Tiles[UpOne],
                left       = Tiles[LeftOne],
                down       = Tiles[DownOne],
                up_right   = Tiles[UpRight],
                up_left    = Tiles[UpLeft],
                down_right = Tiles[DownRight],
                down_left  = Tiles[DownLeft];

            geo
                geo_here       = Geo[Here],
                geo_right      = Geo[RightOne],
                geo_up         = Geo[UpOne],
                geo_left       = Geo[LeftOne],
                geo_down       = Geo[DownOne],
                geo_up_right   = Geo[UpRight],
                geo_up_left    = Geo[UpLeft],
                geo_down_right = Geo[DownRight],
                geo_down_left  = Geo[DownLeft];

            if (IsBlockingTile(here)) return geo.Nothing;

            geo output = geo_here;

            if (!(IsBlockingTile(right) && IsBlockingTile(left)) &&
                (geo_here.dir == Dir.Up && geo_up.dir == Dir.Down || geo_here.dir == Dir.Down && geo_down.dir == Dir.Up))
            {
                output.dir = IsBlockingTile(right) ? Dir.Left : Dir.Right;
            }

            if (!(IsBlockingTile(up) && IsBlockingTile(down)) &&
                (geo_here.dir == Dir.Right && geo_right.dir == Dir.Left || geo_here.dir == Dir.Left && geo_left.dir == Dir.Right))
            {
                output.dir = IsBlockingTile(up) ? Dir.Down : Dir.Up;
            }

            if (Geo[dir_to_vec(output.dir)].bad == _true && geo_here.bad == _false)
                output.dir = Reverse(output.dir);

            int surround_count =
                (IsBlockingTile(up)    ? 1 : 0) +
                (IsBlockingTile(left)  ? 1 : 0) +
                (IsBlockingTile(down)  ? 1 : 0) +
                (IsBlockingTile(right) ? 1 : 0);

            float bad_count = geo_up.bad + geo_left.bad + geo_down.bad + geo_right.bad;

            if (surround_count >= 2 && bad_count >= _1 ||
                geo_up.bad == _true && geo_down.bad == _true ||
                geo_right.bad == _true && geo_left.bad == _true)
                output.bad = _true;

            return output;
        }
    }

    public partial class Geodesic_StorePos : SimShader
    {
        [FragmentShader]
        geo FragmentShader(VertexOut vertex, Field<geo> Geo)
        {
            geo here = Geo[Here];

            if (here.dir == _0) return here;

            vec2 pos = vertex.TexCoords * Geo.Size;
            set_geo_pos_id(ref here, pos);

            return here;
        }
    }

    public partial class Geodesic_ExtremityPropagation : SimShader
    {
        float flatten(vec2 pos)
        {
            return pos.x + 4096 * pos.y;
        }

        [FragmentShader]
        geo FragmentShader(VertexOut vertex, Field<geo> Geo)
        {
            geo
                here       = Geo[Here],
                right      = Geo[RightOne],
                up         = Geo[UpOne],
                left       = Geo[LeftOne],
                down       = Geo[DownOne],
                up_right   = Geo[UpRight],
                up_left    = Geo[UpLeft],
                down_right = Geo[DownRight],
                down_left  = Geo[DownLeft];

            if (here.dir == _0) return here;

            vec2
                extr_here       = geo_pos_id(here),
                extr_right      = geo_pos_id(right),
                extr_up         = geo_pos_id(up),
                extr_left       = geo_pos_id(left),
                extr_down       = geo_pos_id(down),
                extr_up_right   = geo_pos_id(up_right),
                extr_up_left    = geo_pos_id(up_left),
                extr_down_right = geo_pos_id(down_right),
                extr_down_left  = geo_pos_id(down_left);

            float
                val_here       = flatten(extr_here),
                val_right      = flatten(extr_right),
                val_up         = flatten(extr_up),
                val_left       = flatten(extr_left),
                val_down       = flatten(extr_down),
                val_up_right   = flatten(extr_up_right),
                val_up_left    = flatten(extr_up_left),
                val_down_right = flatten(extr_down_right),
                val_down_left  = flatten(extr_down_left);

            if (val_here < val_right)      { here.pos_storage = right     .pos_storage; val_here = val_right; }
            if (val_here < val_up)         { here.pos_storage = up        .pos_storage; val_here = val_up; }
            if (val_here < val_left)       { here.pos_storage = left      .pos_storage; val_here = val_left; }
            if (val_here < val_down)       { here.pos_storage = down      .pos_storage; val_here = val_down; }
            if (val_here < val_up_right)   { here.pos_storage = up_right  .pos_storage; val_here = val_up_right; }
            if (val_here < val_up_left)    { here.pos_storage = up_left   .pos_storage; val_here = val_up_left; }
            if (val_here < val_down_right) { here.pos_storage = down_right.pos_storage; val_here = val_down_right; }
            if (val_here < val_down_left)  { here.pos_storage = down_left .pos_storage; val_here = val_down_left; }

            return here;
        }
    }

    public partial class Geodesic_SetGeoId : SimShader
    {
        [FragmentShader]
        geo FragmentShader(VertexOut vertex, Field<geo> Geo)
        {
            geo geo_here = Geo[Here];

            geo_here.geo_id = ReducedGeoId(geo_pos_id(geo_here));
            geo_here.dist = _0;

            return geo_here;
        }
    }

    public partial class Geodesic_PolarDistance : SimShader
    {
        [FragmentShader]
        vec4 FragmentShader(VertexOut vertex, Field<geo> Geo, Field<vec4> Distance)
        {
            geo
                here       = Geo[Here],
                right      = Geo[RightOne],
                up         = Geo[UpOne],
                left       = Geo[LeftOne],
                down       = Geo[DownOne];

            float
                dist_right      = unpack_val(Distance[RightOne].xy),
                dist_up         = unpack_val(Distance[UpOne].xy),
                dist_left       = unpack_val(Distance[LeftOne].xy),
                dist_down       = unpack_val(Distance[DownOne].xy);

            if (here.dir == _0) return vec4.Zero;

            float dist = 0;

            // Calculate the geo_id of this cell
            geo temp_geo = geo.Nothing;
            vec2 pos = vertex.TexCoords * Geo.Size;
            set_geo_pos_id(ref temp_geo, pos);

            // ... if that geo_id matches the id of the geo info here, then this is the "master" or "12 o' clock" cell of the geodesic line going through this cell.
            if (here.pos_storage == temp_geo.pos_storage)
            {
                // That means its polar distance is 0 by definition.
                dist = 0;
            }
            else
            {
                //geo into = Geo[dir_to_vec(here.dir)];
                //if (into.pos_storage == here.pos_storage) dist = unpack_val(Distance[dir_to_vec(here.dir)].xy) - 1;
                if (here.dir == Dir.Left)  dist = max(_0, dist_left  - 1);
                if (here.dir == Dir.Right) dist = max(_0, dist_right - 1);
                if (here.dir == Dir.Up)    dist = max(_0, dist_up    - 1);
                if (here.dir == Dir.Down)  dist = max(_0, dist_down  - 1);

                // Otherwise its polar distance is 1 plus the polar distance of whatever cell comes "before" it (by following the geo backwards "counterclockwise").
                if (right.dir == Dir.Left  && dist_right >= dist) dist = dist_right + 1;
                if (left.dir  == Dir.Right && dist_left  >= dist) dist = dist_left  + 1;
                if (up.dir    == Dir.Down  && dist_up    >= dist) dist = dist_up    + 1;
                if (down.dir  == Dir.Up    && dist_down  >= dist) dist = dist_down  + 1;

                //// If nothing points into this tile, then this is a "degenerate" part of the geodesic (flows out, but nothing flows into it) ...
                //if (right.dir != Dir.Left && left.dir != Dir.Right && up.dir != Dir.Down && down.dir != Dir.Up)
                //{ 
                //    // ... in which case the polar distance should be the max of surrounding polar distances
                //    //if (right.pos_storage == here.pos_storage) dist = max(dist, dist_right - 1);
                //    //if (up   .pos_storage == here.pos_storage) dist = max(dist, dist_up    - 1);
                //    //if (left .pos_storage == here.pos_storage) dist = max(dist, dist_left  - 1);
                //    //if (down .pos_storage == here.pos_storage) dist = max(dist, dist_down  - 1);
                //    dist = max(max(dist_right, dist_left, dist_up, dist_down) - 1, 0);
                //}
            }

            // Pack the polar distance into 2-bytes and return it in
            vec4 output = vec4.Zero;
            output.xy = pack_val_2byte(dist);
            
            return output;
        }
    }

    public partial class Geodesic_SetCircumference : SimShader
    {
        [FragmentShader]
        vec4 FragmentShader(VertexOut vertex, Field<geo> Geo, Field<vec4> Distance)
        {
            vec4 info_here = Distance[Here];
            geo here       = Geo[Here];

            if (here.dir == _0) return vec4.Zero;

            vec2 pos_here = vertex.TexCoords * Geo.Size;
            vec2 start_pos = geo_pos_id(here);
            RelativeIndex GeoStart = (RelativeIndex)(start_pos - pos_here);

            geo
                right      = Geo[GeoStart + RightOne],
                up         = Geo[GeoStart + UpOne],
                left       = Geo[GeoStart + LeftOne],
                down       = Geo[GeoStart + DownOne];

            float circum = 0;
            if (right.pos_storage == here.pos_storage) circum = max(circum, unpack_val(Distance[GeoStart + RightOne].xy));
            if (up   .pos_storage == here.pos_storage) circum = max(circum, unpack_val(Distance[GeoStart + UpOne].xy));
            if (left .pos_storage == here.pos_storage) circum = max(circum, unpack_val(Distance[GeoStart + LeftOne].xy));
            if (down .pos_storage == here.pos_storage) circum = max(circum, unpack_val(Distance[GeoStart + DownOne].xy));

            // Pack the polar circumference into 2-bytes
            info_here.zw = pack_val_2byte(circum);

            return info_here;
        }
    }

    public partial class Geodesic_Polarity : SimShader
    {
        [FragmentShader]
        dirward FragmentShader(VertexOut vertex, Field<dirward> Dirward, Field<geo> Geo, Field<geo> ShiftedGeo, Field<vec4> Info, Field<vec4> ShiftedInfo, [Dir.Vals] float dir)
        {
            geo
                geo_here  =        Geo[Here],
                geo_shift = ShiftedGeo[Here];

            if (geo_here.dir == _0) return dirward.Nothing;

            vec4
                info_here  =        Info[Here],
                info_shift = ShiftedInfo[Here];

            if (geo_here.pos_storage != geo_shift.pos_storage) return Dirward[Here];

            float
                dist_here  = unpack_val(info_here.xy),
                dist_shift = unpack_val(info_shift.xy),
                circum     = unpack_val(info_here.zw);

            float diff = dist_here - dist_shift;

            float clockwise = 0, counterclockwise = 0;
            if (diff > 0)
            {
                clockwise = diff;
                counterclockwise = circum - diff;
            }
            else
            {
                clockwise = circum + diff;
                counterclockwise = -diff;
            }            

            dirward output = dirward.Nothing;
            output.polarity = 1;
            output.polarity = clockwise > counterclockwise ? 0 : 1;
            output.geo_id = vec(1,1); // WARNING

            return output;
        }
    }

    public partial class Geodesic_DirwardExtend : SimShader
    {
        [FragmentShader]
        dirward FragmentShader(VertexOut vertex, Field<tile> Tiles, Field<geo> Geo, Field<dirward> Dirward, [Dir.Vals] float dir)
        {
            tile
                here = Tiles[Here];

            geo
                geo_here       = Geo[Here],
                geo_right      = Geo[RightOne],
                geo_up         = Geo[UpOne],
                geo_left       = Geo[LeftOne],
                geo_down       = Geo[DownOne],
                geo_up_right   = Geo[UpRight],
                geo_up_left    = Geo[UpLeft],
                geo_down_right = Geo[DownRight],
                geo_down_left  = Geo[DownLeft];

            dirward
                dirward_here       = Dirward[Here],
                dirward_right      = Dirward[RightOne],
                dirward_up         = Dirward[UpOne],
                dirward_left       = Dirward[LeftOne],
                dirward_down       = Dirward[DownOne],
                dirward_up_right   = Dirward[UpRight],
                dirward_up_left    = Dirward[UpLeft],
                dirward_down_right = Dirward[DownRight],
                dirward_down_left  = Dirward[DownLeft];

            if (IsBlockingTile(here)) return dirward.Nothing;

            dirward output = dirward.Nothing;

            dirward forward = dirward.Nothing, forward_right = dirward.Nothing, forward_left = dirward.Nothing, rightward = dirward.Nothing, leftward = dirward.Nothing;
            geo geo_forward = geo.Nothing, geo_forward_right = geo.Nothing, geo_forward_left = geo.Nothing, geo_rightward = geo.Nothing, geo_leftward = geo.Nothing;

            // Get the surrounding dirward info and store it relative to the direction we consider forward
            if (dir == Dir.Up)
            {
                forward       = dirward_up;
                forward_right = dirward_up_right;
                forward_left  = dirward_up_left;
                rightward     = dirward_right;
                leftward      = dirward_left;

                geo_forward       = geo_up;
                geo_forward_right = geo_up_right;
                geo_forward_left  = geo_up_left;
                geo_rightward     = geo_right;
                geo_leftward      = geo_left;
            }
            else if (dir == Dir.Right)
            {
                forward       = dirward_right;
                forward_right = dirward_down_right;
                forward_left  = dirward_up_right;
                rightward     = dirward_down;
                leftward      = dirward_up;

                geo_forward       = geo_right;
                geo_forward_right = geo_down_right;
                geo_forward_left  = geo_up_right;
                geo_rightward     = geo_down;
                geo_leftward      = geo_up;
            }
            else if (dir == Dir.Down)
            {
                forward       = dirward_down;
                forward_right = dirward_down_left;
                forward_left  = dirward_down_right;
                rightward     = dirward_left;
                leftward      = dirward_right;

                geo_forward       = geo_down;
                geo_forward_right = geo_down_left;
                geo_forward_left  = geo_down_right;
                geo_rightward     = geo_left;
                geo_leftward      = geo_right;
            }
            else if (dir == Dir.Left)
            {
                forward       = dirward_left;
                forward_right = dirward_up_left;
                forward_left  = dirward_down_left;
                rightward     = dirward_up;
                leftward      = dirward_down;

                geo_forward       = geo_left;
                geo_forward_right = geo_up_left;
                geo_forward_left  = geo_down_left;
                geo_rightward     = geo_up;
                geo_leftward      = geo_down;
            }

            if (geo_here.dir > 0 && IsBlockingTile(Tiles[dir_to_vec(dir)]))
            {
                output.geo_id = geo_here.geo_id;
                output.dist_to_wall = _0;
            }

            else if (ValidDirward(forward)       && forward.geo_id       == geo_forward.geo_id)       { output = forward;       output.dist_to_wall += _1; }
            else if (ValidDirward(forward_right) && forward_right.geo_id == geo_forward_right.geo_id) { output = forward_right; output.dist_to_wall += _1; }
            else if (ValidDirward(forward_left)  && forward_left.geo_id  == geo_forward_left.geo_id)  { output = forward_left;  output.dist_to_wall += _1; }
            else if (ValidDirward(rightward)     && rightward.geo_id     == geo_rightward.geo_id)     { output = rightward;     output.dist_to_wall += _0; }
            else if (ValidDirward(leftward)      && leftward.geo_id      == geo_leftward.geo_id)      { output = leftward;      output.dist_to_wall += _0; }

            return output;
        }
    }

    public partial class Geodesic_ConvertToBlocking : SimShader
    {
        [FragmentShader]
        tile FragmentShader(VertexOut vertex, Field<tile> Tiles, Field<geo> Geo)
        {
            tile tile_here = Tiles[Here];
            geo  geo_here  = Geo[Here];

            if (IsValid(geo_here.dir))
                tile_here.type = TileType.Trees;

            return tile_here;
        }
    }

    public partial class Geodesic_Flatten : SimShader
    {
        void InheritsFrom(ref geo outer_geo, geo inner_geo)
        {
            outer_geo.dist   = inner_geo.dist + _1;
            outer_geo.geo_id = inner_geo.geo_id;            
        }

        [FragmentShader]
        geo FragmentShader(VertexOut vertex, Field<geo> Geo, Field<geo> OuterGeo)
        {
            geo
                geo_here       = Geo[Here],
                geo_right      = Geo[RightOne],
                geo_up         = Geo[UpOne],
                geo_left       = Geo[LeftOne],
                geo_down       = Geo[DownOne],
                geo_up_right   = Geo[UpRight],
                geo_up_left    = Geo[UpLeft],
                geo_down_right = Geo[DownRight],
                geo_down_left  = Geo[DownLeft];

            geo
                outer_geo_here = OuterGeo[Here];

            if (IsValid(geo_here.dir)) return geo_here;

            outer_geo_here.dist = _255; // Start off as maximum possible 1-byte distance, since we will be taking the min of surrounding distances (and adding _1)
            if      (outer_geo_here.dist > geo_right.dist      && IsValid(geo_right.dir)     ) InheritsFrom(ref outer_geo_here, geo_right);
            else if (outer_geo_here.dist > geo_up.dist         && IsValid(geo_up.dir)        ) InheritsFrom(ref outer_geo_here, geo_up);
            else if (outer_geo_here.dist > geo_left.dist       && IsValid(geo_left.dir)      ) InheritsFrom(ref outer_geo_here, geo_left);
            else if (outer_geo_here.dist > geo_down.dist       && IsValid(geo_down.dir)      ) InheritsFrom(ref outer_geo_here, geo_down);
            else if (outer_geo_here.dist > geo_up_right.dist   && IsValid(geo_up_right.dir)  ) InheritsFrom(ref outer_geo_here, geo_up_right);
            else if (outer_geo_here.dist > geo_up_left.dist    && IsValid(geo_up_left.dir)   ) InheritsFrom(ref outer_geo_here, geo_up_left);
            else if (outer_geo_here.dist > geo_down_right.dist && IsValid(geo_down_right.dir)) InheritsFrom(ref outer_geo_here, geo_down_right);
            else if (outer_geo_here.dist > geo_down_left.dist  && IsValid(geo_down_left.dir) ) InheritsFrom(ref outer_geo_here, geo_down_left);

            return outer_geo_here;
        }
    }    
}
