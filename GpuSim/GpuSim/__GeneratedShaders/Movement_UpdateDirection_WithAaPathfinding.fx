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
// Texture Sampler for fs_param_TargetData, using register location 1
float2 fs_param_TargetData_size;
float2 fs_param_TargetData_dxdy;

Texture fs_param_TargetData_Texture;
sampler fs_param_TargetData : register(s1) = sampler_state
{
    texture   = <fs_param_TargetData_Texture>;
    MipFilter = Point;
    MagFilter = Point;
    MinFilter = Point;
    AddressU  = Clamp;
    AddressV  = Clamp;
};

// Texture Sampler for fs_param_Data, using register location 2
float2 fs_param_Data_size;
float2 fs_param_Data_dxdy;

Texture fs_param_Data_Texture;
sampler fs_param_Data : register(s2) = sampler_state
{
    texture   = <fs_param_Data_Texture>;
    MipFilter = Point;
    MagFilter = Point;
    MinFilter = Point;
    AddressU  = Clamp;
    AddressV  = Clamp;
};

// Texture Sampler for fs_param_Extra, using register location 3
float2 fs_param_Extra_size;
float2 fs_param_Extra_dxdy;

Texture fs_param_Extra_Texture;
sampler fs_param_Extra : register(s3) = sampler_state
{
    texture   = <fs_param_Extra_Texture>;
    MipFilter = Point;
    MagFilter = Point;
    MinFilter = Point;
    AddressU  = Clamp;
    AddressV  = Clamp;
};

// Texture Sampler for fs_param_Current, using register location 4
float2 fs_param_Current_size;
float2 fs_param_Current_dxdy;

Texture fs_param_Current_Texture;
sampler fs_param_Current : register(s4) = sampler_state
{
    texture   = <fs_param_Current_Texture>;
    MipFilter = Point;
    MagFilter = Point;
    MinFilter = Point;
    AddressU  = Clamp;
    AddressV  = Clamp;
};

// Texture Sampler for fs_param_Paths_Right, using register location 5
float2 fs_param_Paths_Right_size;
float2 fs_param_Paths_Right_dxdy;

Texture fs_param_Paths_Right_Texture;
sampler fs_param_Paths_Right : register(s5) = sampler_state
{
    texture   = <fs_param_Paths_Right_Texture>;
    MipFilter = Point;
    MagFilter = Point;
    MinFilter = Point;
    AddressU  = Clamp;
    AddressV  = Clamp;
};

// Texture Sampler for fs_param_Paths_Left, using register location 6
float2 fs_param_Paths_Left_size;
float2 fs_param_Paths_Left_dxdy;

Texture fs_param_Paths_Left_Texture;
sampler fs_param_Paths_Left : register(s6) = sampler_state
{
    texture   = <fs_param_Paths_Left_Texture>;
    MipFilter = Point;
    MagFilter = Point;
    MinFilter = Point;
    AddressU  = Clamp;
    AddressV  = Clamp;
};

// Texture Sampler for fs_param_Paths_Up, using register location 7
float2 fs_param_Paths_Up_size;
float2 fs_param_Paths_Up_dxdy;

Texture fs_param_Paths_Up_Texture;
sampler fs_param_Paths_Up : register(s7) = sampler_state
{
    texture   = <fs_param_Paths_Up_Texture>;
    MipFilter = Point;
    MagFilter = Point;
    MinFilter = Point;
    AddressU  = Clamp;
    AddressV  = Clamp;
};

// Texture Sampler for fs_param_Paths_Down, using register location 8
float2 fs_param_Paths_Down_size;
float2 fs_param_Paths_Down_dxdy;

Texture fs_param_Paths_Down_Texture;
sampler fs_param_Paths_Down : register(s8) = sampler_state
{
    texture   = <fs_param_Paths_Down_Texture>;
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

float GpuSim__SimShader__unpack_coord(float2 packed)
{
    float coord = 0;
    packed = floor(255.0 * packed + float2(0.5, 0.5));
    coord = 256 * packed.x + packed.y;
    return coord;
}

float2 GpuSim__SimShader__unpack_vec2(float4 packed)
{
    float2 v = float2(0, 0);
    v.x = GpuSim__SimShader__unpack_coord(packed.rg);
    v.y = GpuSim__SimShader__unpack_coord(packed.ba);
    return v;
}

bool GpuSim__SimShader__IsValid(float direction)
{
    return direction > 0 + .001;
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
    float4 here = tex2D(fs_param_Current, psin.TexCoords + (float2(0, 0)) * fs_param_Current_dxdy);
    float4 extra_here = tex2D(fs_param_Extra, psin.TexCoords + (float2(0, 0)) * fs_param_Extra_dxdy);
    if (GpuSim__SimShader__Something(here))
    {
        float4 path = float4(0, 0, 0, 0);
        float4 right = tex2D(fs_param_Current, psin.TexCoords + (float2(1, 0)) * fs_param_Current_dxdy), up = tex2D(fs_param_Current, psin.TexCoords + (float2(0, 1)) * fs_param_Current_dxdy), left = tex2D(fs_param_Current, psin.TexCoords + (float2(-(1), 0)) * fs_param_Current_dxdy), down = tex2D(fs_param_Current, psin.TexCoords + (float2(0, -(1))) * fs_param_Current_dxdy);
        float4 right_path = tex2D(fs_param_Paths_Right, psin.TexCoords + (float2(0, 0)) * fs_param_Paths_Right_dxdy), up_path = tex2D(fs_param_Paths_Up, psin.TexCoords + (float2(0, 0)) * fs_param_Paths_Up_dxdy), left_path = tex2D(fs_param_Paths_Left, psin.TexCoords + (float2(0, 0)) * fs_param_Paths_Left_dxdy), down_path = tex2D(fs_param_Paths_Down, psin.TexCoords + (float2(0, 0)) * fs_param_Paths_Down_dxdy);
        float4 target = tex2D(fs_param_TargetData, psin.TexCoords + (float2(0, 0)) * fs_param_TargetData_dxdy);
        float4 data_here = tex2D(fs_param_Data, psin.TexCoords + (float2(0, 0)) * fs_param_Data_dxdy);
        float2 Destination = GpuSim__SimShader__unpack_vec2(target);
        float cur_angle = atan2(psin.TexCoords.y - Destination.y * fs_param_TargetData_dxdy.y, psin.TexCoords.x - Destination.x * fs_param_TargetData_dxdy.x);
        cur_angle = (cur_angle + 3.14159) / (2 * 3.14159);
        float target_angle = extra_here.a;
        if (Destination.x > psin.TexCoords.x * fs_param_TargetData_size.x + .001)
        {
            path = right_path;
            if (Destination.y < psin.TexCoords.y * fs_param_TargetData_size.y - .001)
            {
                if (cur_angle < target_angle - .001 || abs(right_path.r - 0.003921569) < .001 && GpuSim__SimShader__Something(right))
                {
                    path = down_path;
                    if (GpuSim__SimShader__Something(down))
                    {
                        path = right_path;
                    }
                }
            }
            else
            {
                if (cur_angle > target_angle + .001 || abs(right_path.r - 0.003921569) < .001 && GpuSim__SimShader__Something(right))
                {
                    path = up_path;
                    if (GpuSim__SimShader__Something(up))
                    {
                        path = right_path;
                    }
                }
            }
        }
        else
        {
            path = left_path;
            if (Destination.y < psin.TexCoords.y * fs_param_TargetData_size.y - .001)
            {
                if (cur_angle > target_angle + .001 || abs(left_path.r - 0.01176471) < .001 && GpuSim__SimShader__Something(left))
                {
                    path = down_path;
                    if (GpuSim__SimShader__Something(down))
                    {
                        path = left_path;
                    }
                }
            }
            else
            {
                if (cur_angle < target_angle - .001 || abs(left_path.r - 0.01176471) < .001 && GpuSim__SimShader__Something(left))
                {
                    path = up_path;
                    if (GpuSim__SimShader__Something(up))
                    {
                        path = left_path;
                    }
                }
            }
        }
        if ((path.g > 1 + .001 || path.b > 1 + .001) && GpuSim__SimShader__IsValid(path.r))
        {
            here.r = path.r;
        }
    }
    __FinalOutput.Color = here;
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