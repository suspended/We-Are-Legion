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
// Texture Sampler for fs_param_Path, using register location 1
float2 fs_param_Path_size;
float2 fs_param_Path_dxdy;

Texture fs_param_Path_Texture;
sampler fs_param_Path : register(s1) = sampler_state
{
    texture   = <fs_param_Path_Texture>;
    MipFilter = Point;
    MagFilter = Point;
    MinFilter = Point;
    AddressU  = Clamp;
    AddressV  = Clamp;
};

// Texture Sampler for fs_param_Current, using register location 2
float2 fs_param_Current_size;
float2 fs_param_Current_dxdy;

Texture fs_param_Current_Texture;
sampler fs_param_Current : register(s2) = sampler_state
{
    texture   = <fs_param_Current_Texture>;
    MipFilter = Point;
    MagFilter = Point;
    MinFilter = Point;
    AddressU  = Clamp;
    AddressV  = Clamp;
};

// Texture Sampler for fs_param_CurData, using register location 3
float2 fs_param_CurData_size;
float2 fs_param_CurData_dxdy;

Texture fs_param_CurData_Texture;
sampler fs_param_CurData : register(s3) = sampler_state
{
    texture   = <fs_param_CurData_Texture>;
    MipFilter = Point;
    MagFilter = Point;
    MinFilter = Point;
    AddressU  = Clamp;
    AddressV  = Clamp;
};

// The following methods are included because they are referenced by the fragment shader.
bool GpuSim__SimShader__Something(float4 u)
{
    return u.r > 0 + .001;
}

bool GpuSim__SimShader__IsBuilding(float4 u)
{
    return u.r >= 0.007843138 - .001;
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
    float4 output = float4(0, 0, 0, 0);
    float4 data_here = tex2D(fs_param_Current, psin.TexCoords + (float2(0, 0)) * fs_param_Current_dxdy);
    float4 unit_here = tex2D(fs_param_CurData, psin.TexCoords + (float2(0, 0)) * fs_param_CurData_dxdy);
    if (GpuSim__SimShader__Something(data_here) && GpuSim__SimShader__IsBuilding(unit_here))
    {
        output.b = unit_here.g;
        output.rg = float2(0.1568628, 0.1568628);
        output.a = 0.0;
    }
    else
    {
        float4 right = tex2D(fs_param_Path, psin.TexCoords + (float2(1, 0)) * fs_param_Path_dxdy), up = tex2D(fs_param_Path, psin.TexCoords + (float2(0, 1)) * fs_param_Path_dxdy), left = tex2D(fs_param_Path, psin.TexCoords + (float2(-(1), 0)) * fs_param_Path_dxdy), down = tex2D(fs_param_Path, psin.TexCoords + (float2(0, -(1))) * fs_param_Path_dxdy);
        float min_dist = 1.0;
        if (left.a < min_dist - .001)
        {
            output.b = left.b;
            min_dist = left.a;
            output.rg = left.rg - float2(0.003921569, 0.0);
        }
        if (down.a < min_dist - .001)
        {
            output.b = down.b;
            min_dist = down.a;
            output.rg = down.rg - float2(0.0, 0.003921569);
        }
        if (right.a < min_dist - .001)
        {
            output.b = right.b;
            min_dist = right.a;
            output.rg = right.rg + float2(0.003921569, 0.0);
        }
        if (up.a < min_dist - .001)
        {
            output.b = up.b;
            min_dist = up.a;
            output.rg = up.rg + float2(0.0, 0.003921569);
        }
        output.a = min_dist + 0.003921569;
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