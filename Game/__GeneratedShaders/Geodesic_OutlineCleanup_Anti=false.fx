// This file was auto-generated by FragSharp. It will be regenerated on the next compilation.
// Manual changes made will not persist and may cause incorrect behavior between compilations.

#define PIXEL_SHADER ps_3_0
#define VERTEX_SHADER vs_3_0

// Vertex shader data structure definition
struct VertexToPixel
{
    float4 Position   : POSITION0;
    float4 Color      : COLOR0;
    float2 TexCoords  : TEXCOORD0;
    float2 Position2D : TEXCOORD2;
};

// Fragment shader data structure definition
struct PixelToFrame
{
    float4 Color      : COLOR0;
};

// The following are variables used by the vertex shader (vertex parameters).

// The following are variables used by the fragment shader (fragment parameters).
// Texture Sampler for fs_param_Tiles, using register location 1
float2 fs_param_Tiles_size;
float2 fs_param_Tiles_dxdy;

Texture fs_param_Tiles_Texture;
sampler fs_param_Tiles : register(s1) = sampler_state
{
    texture   = <fs_param_Tiles_Texture>;
    MipFilter = Point;
    MagFilter = Point;
    MinFilter = Point;
    AddressU  = Clamp;
    AddressV  = Clamp;
};

// Texture Sampler for fs_param_Geo, using register location 2
float2 fs_param_Geo_size;
float2 fs_param_Geo_dxdy;

Texture fs_param_Geo_Texture;
sampler fs_param_Geo : register(s2) = sampler_state
{
    texture   = <fs_param_Geo_Texture>;
    MipFilter = Point;
    MagFilter = Point;
    MinFilter = Point;
    AddressU  = Clamp;
    AddressV  = Clamp;
};


// The following variables are included because they are referenced but are not function parameters. Their values will be set at call time.

// The following methods are included because they are referenced by the fragment shader.
bool Game__SimShader__IsBlockingTile__tile(float4 t)
{
    return t.r >= 0.01176471 - .0019 || abs(t.r - 0.003921569) < .0019 && abs(t.b - 0.1215686) > .0019;
}

bool Game__SimShader__IsValid__Single(float direction)
{
    return direction > 0 + .0019;
}

float2 Game__SimShader__dir_to_vec__Single(float direction)
{
    float angle = (float)((direction * 255 - 1) * (3.1415926 / 2.0));
    return Game__SimShader__IsValid__Single(direction) ? float2(cos(angle), sin(angle)) : float2(0, 0);
}

float Game__SimShader__Reverse__Single(float dir)
{
    dir += 2 * 0.003921569;
    if (dir > 0.01568628 + .0019)
    {
        dir -= 4 * 0.003921569;
    }
    return dir;
}

// Compiled vertex shader
VertexToPixel StandardVertexShader(float2 inPos : POSITION0, float2 inTexCoords : TEXCOORD0, float4 inColor : COLOR0)
{
    VertexToPixel Output = (VertexToPixel)0;
    Output.Position.w = 1;
    Output.Position.xy = inPos.xy;
    Output.TexCoords = inTexCoords;
    return Output;
}

// Compiled fragment shader
PixelToFrame FragmentShader(VertexToPixel psin)
{
    PixelToFrame __FinalOutput = (PixelToFrame)0;
    float4 here = tex2D(fs_param_Tiles, psin.TexCoords + (-float2(0.25,0.25) + float2(0, 0)) * fs_param_Tiles_dxdy), right = tex2D(fs_param_Tiles, psin.TexCoords + (-float2(0.25,0.25) + float2(1, 0)) * fs_param_Tiles_dxdy), up = tex2D(fs_param_Tiles, psin.TexCoords + (-float2(0.25,0.25) + float2(0, 1)) * fs_param_Tiles_dxdy), left = tex2D(fs_param_Tiles, psin.TexCoords + (-float2(0.25,0.25) + float2(-(1), 0)) * fs_param_Tiles_dxdy), down = tex2D(fs_param_Tiles, psin.TexCoords + (-float2(0.25,0.25) + float2(0, -(1))) * fs_param_Tiles_dxdy), up_right = tex2D(fs_param_Tiles, psin.TexCoords + (-float2(0.25,0.25) + float2(1, 1)) * fs_param_Tiles_dxdy), up_left = tex2D(fs_param_Tiles, psin.TexCoords + (-float2(0.25,0.25) + float2(-(1), 1)) * fs_param_Tiles_dxdy), down_right = tex2D(fs_param_Tiles, psin.TexCoords + (-float2(0.25,0.25) + float2(1, -(1))) * fs_param_Tiles_dxdy), down_left = tex2D(fs_param_Tiles, psin.TexCoords + (-float2(0.25,0.25) + float2(-(1), -(1))) * fs_param_Tiles_dxdy);
    float4 geo_here = tex2D(fs_param_Geo, psin.TexCoords + (-float2(0.25,0.25) + float2(0, 0)) * fs_param_Geo_dxdy), geo_right = tex2D(fs_param_Geo, psin.TexCoords + (-float2(0.25,0.25) + float2(1, 0)) * fs_param_Geo_dxdy), geo_up = tex2D(fs_param_Geo, psin.TexCoords + (-float2(0.25,0.25) + float2(0, 1)) * fs_param_Geo_dxdy), geo_left = tex2D(fs_param_Geo, psin.TexCoords + (-float2(0.25,0.25) + float2(-(1), 0)) * fs_param_Geo_dxdy), geo_down = tex2D(fs_param_Geo, psin.TexCoords + (-float2(0.25,0.25) + float2(0, -(1))) * fs_param_Geo_dxdy), geo_up_right = tex2D(fs_param_Geo, psin.TexCoords + (-float2(0.25,0.25) + float2(1, 1)) * fs_param_Geo_dxdy), geo_up_left = tex2D(fs_param_Geo, psin.TexCoords + (-float2(0.25,0.25) + float2(-(1), 1)) * fs_param_Geo_dxdy), geo_down_right = tex2D(fs_param_Geo, psin.TexCoords + (-float2(0.25,0.25) + float2(1, -(1))) * fs_param_Geo_dxdy), geo_down_left = tex2D(fs_param_Geo, psin.TexCoords + (-float2(0.25,0.25) + float2(-(1), -(1))) * fs_param_Geo_dxdy);
    if (Game__SimShader__IsBlockingTile__tile(here))
    {
        __FinalOutput.Color = float4(0, 0, 0, 0);
        return __FinalOutput;
    }
    float4 output = geo_here;
    if (!((Game__SimShader__IsBlockingTile__tile(right) && Game__SimShader__IsBlockingTile__tile(left))) && (abs(geo_here.r - 0.007843138) < .0019 && abs(geo_up.r - 0.01568628) < .0019 || abs(geo_here.r - 0.01568628) < .0019 && abs(geo_down.r - 0.007843138) < .0019))
    {
        output.r = Game__SimShader__IsBlockingTile__tile(right) ? 0.01176471 : 0.003921569;
    }
    if (!((Game__SimShader__IsBlockingTile__tile(up) && Game__SimShader__IsBlockingTile__tile(down))) && (abs(geo_here.r - 0.003921569) < .0019 && abs(geo_right.r - 0.01176471) < .0019 || abs(geo_here.r - 0.01176471) < .0019 && abs(geo_left.r - 0.003921569) < .0019))
    {
        output.r = Game__SimShader__IsBlockingTile__tile(up) ? 0.01568628 : 0.007843138;
    }
    if (abs(tex2D(fs_param_Geo, psin.TexCoords + (-float2(0.25,0.25) + Game__SimShader__dir_to_vec__Single(output.r)) * fs_param_Geo_dxdy).a - 0.003921569) < .0019 && abs(geo_here.a - 0.0) < .0019)
    {
        output.r = Game__SimShader__Reverse__Single(output.r);
    }
    int surround_count = (Game__SimShader__IsBlockingTile__tile(up) ? 1 : 0) + (Game__SimShader__IsBlockingTile__tile(left) ? 1 : 0) + (Game__SimShader__IsBlockingTile__tile(down) ? 1 : 0) + (Game__SimShader__IsBlockingTile__tile(right) ? 1 : 0);
    float bad_count = geo_up.a + geo_left.a + geo_down.a + geo_right.a;
    if (surround_count >= 2 - .0019 && bad_count >= 0.003921569 - .0019 || abs(geo_up.a - 0.003921569) < .0019 && abs(geo_down.a - 0.003921569) < .0019 || abs(geo_right.a - 0.003921569) < .0019 && abs(geo_left.a - 0.003921569) < .0019)
    {
        output.a = 0.003921569;
    }
    __FinalOutput.Color = output;
    return __FinalOutput;
}

// Shader compilation
technique Simplest
{
    pass Pass0
    {
        VertexShader = compile VERTEX_SHADER StandardVertexShader();
        PixelShader = compile PIXEL_SHADER FragmentShader();
    }
}