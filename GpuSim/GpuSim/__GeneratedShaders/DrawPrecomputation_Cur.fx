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

// Texture Sampler for fs_param_Previous, using register location 2
float2 fs_param_Previous_size;
float2 fs_param_Previous_dxdy;

Texture fs_param_Previous_Texture;
sampler fs_param_Previous : register(s2) = sampler_state
{
    texture   = <fs_param_Previous_Texture>;
    MipFilter = Point;
    MagFilter = Point;
    MinFilter = Point;
    AddressU  = Clamp;
    AddressV  = Clamp;
};

// The following methods are included because they are referenced by the fragment shader.
bool GpuSim__SimShader__selected(float4 u)
{
    float val = u.b;
    return val >= 0.01960784 - .001;
}

bool GpuSim__SimShader__Something(float4 u)
{
    return u.r > 0 + .001;
}

bool GpuSim__SimShader__IsValid(float direction)
{
    return direction > 0 + .001;
}

float GpuSim__SimShader__prior_direction(float4 u)
{
    float val = u.b;
    return val % 0.01960784;
}

float2 GpuSim__SimShader__direction_to_vec(float direction)
{
    float angle = (direction * 255 - 1) * (3.141593 / 2.0);
    return GpuSim__SimShader__IsValid(direction) ? float2(cos(angle), sin(angle)) : float2(0, 0);
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
    float4 output = float4(0.0, 0.0, 0.0, 0.0);
    float4 cur = tex2D(fs_param_Current, psin.TexCoords + (float2(0, 0)) * fs_param_Current_dxdy);
    float4 pre = tex2D(fs_param_Previous, psin.TexCoords + (float2(0, 0)) * fs_param_Previous_dxdy);
    float selected_offset = GpuSim__SimShader__selected(cur) ? 0.01568628 : 0.0;
    float anim = 0;
    float2 vel = float2(0, 0);
    if (GpuSim__SimShader__Something(cur) && abs(cur.g - 0.003921569) < .001)
    {
        anim = cur.r;
    }
    else
    {
        if (GpuSim__SimShader__IsValid(cur.r))
        {
            anim = GpuSim__SimShader__prior_direction(cur);
            vel = GpuSim__SimShader__direction_to_vec(GpuSim__SimShader__prior_direction(cur));
        }
        else
        {
            __FinalOutput.Color = float4(0.0, 0.0, 0.0, 0.0);
            return __FinalOutput;
        }
    }
    float2 uv= (float2)0;
    uv.x = 0;
    uv.y = anim + selected_offset;
    output.xy = uv;
    output.zw = vel / 2 + float2(0.5, 0.5);
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