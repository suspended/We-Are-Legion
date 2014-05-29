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
// Texture Sampler for fs_param_Current, using register location 1
float2 fs_param_Current_size;
float2 fs_param_Current_dxdy;

Texture fs_param_Current_Texture;
sampler fs_param_Current : register(s1) = sampler_state
{
    texture   = <fs_param_Current_Texture>;
    MipFilter = Point;
    MagFilter = Point;
    MinFilter = Point;
    AddressU  = Clamp;
    AddressV  = Clamp;
};

// Texture Sampler for fs_param_Next, using register location 2
float2 fs_param_Next_size;
float2 fs_param_Next_dxdy;

Texture fs_param_Next_Texture;
sampler fs_param_Next : register(s2) = sampler_state
{
    texture   = <fs_param_Next_Texture>;
    MipFilter = Point;
    MagFilter = Point;
    MinFilter = Point;
    AddressU  = Clamp;
    AddressV  = Clamp;
};

// The following methods are included because they are referenced by the fragment shader.
bool GpuSim__SimShader__IsValid(float direction)
{
    return direction > 0 + .001;
}

float2 GpuSim__SimShader__dir_to_vec(float direction)
{
    float angle = (float)((direction * 255 - 1) * (3.1415926 / 2.0));
    return GpuSim__SimShader__IsValid(direction) ? float2(cos(angle), sin(angle)) : float2(0, 0);
}

bool GpuSim__SimShader__selected(float4 u)
{
    float val = u.b;
    return val >= 0.02745098 - .001;
}

void GpuSim__SimShader__set_prior_direction(inout float4 u, float dir)
{
    u.b = dir + (GpuSim__SimShader__selected(u) ? 0.02745098 : 0.0);
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
    float4 next = tex2D(fs_param_Next, psin.TexCoords + (float2(0, 0)) * fs_param_Next_dxdy);
    float4 here = tex2D(fs_param_Current, psin.TexCoords + (float2(0, 0)) * fs_param_Current_dxdy);
    float4 ahead = tex2D(fs_param_Next, psin.TexCoords + (GpuSim__SimShader__dir_to_vec(here.r)) * fs_param_Next_dxdy);
    if (abs(ahead.g - 0.0) < .001 && abs(ahead.r - here.r) < .001)
    {
        next = float4(0, 0, 0, 0);
    }
    GpuSim__SimShader__set_prior_direction(next, next.r);
    __FinalOutput.Color = next;
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