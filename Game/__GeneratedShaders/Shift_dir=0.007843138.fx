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
// Texture Sampler for fs_param_Random, using register location 1
float2 fs_param_Random_size;
float2 fs_param_Random_dxdy;

Texture fs_param_Random_Texture;
sampler fs_param_Random : register(s1) = sampler_state
{
    texture   = <fs_param_Random_Texture>;
    MipFilter = Point;
    MagFilter = Point;
    MinFilter = Point;
    AddressU  = Wrap;
    AddressV  = Wrap;
};


// The following variables are included because they are referenced but are not function parameters. Their values will be set at call time.

// The following methods are included because they are referenced by the fragment shader.
bool Game__SimShader__IsValid__Single(float direction)
{
    return direction > 0 + .0019;
}

float2 Game__SimShader__dir_to_vec__Single(float direction)
{
    float angle = (float)((direction * 255 - 1) * (3.1415926 / 2.0));
    return Game__SimShader__IsValid__Single(direction) ? float2(cos(angle), sin(angle)) : float2(0, 0);
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
    __FinalOutput.Color = tex2D(fs_param_Random, psin.TexCoords + (-float2(0.25,0.25) + Game__SimShader__dir_to_vec__Single(0.007843138)) * fs_param_Random_dxdy);
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