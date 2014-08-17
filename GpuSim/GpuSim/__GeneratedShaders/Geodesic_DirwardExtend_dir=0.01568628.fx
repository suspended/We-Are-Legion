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

// Texture Sampler for fs_param_Dirward, using register location 3
float2 fs_param_Dirward_size;
float2 fs_param_Dirward_dxdy;

Texture fs_param_Dirward_Texture;
sampler fs_param_Dirward : register(s3) = sampler_state
{
    texture   = <fs_param_Dirward_Texture>;
    MipFilter = Point;
    MagFilter = Point;
    MinFilter = Point;
    AddressU  = Clamp;
    AddressV  = Clamp;
};


// The following methods are included because they are referenced by the fragment shader.
bool GpuSim__SimShader__IsBlockingTile(float4 t)
{
    return t.r >= 0.01176471 - .001 || abs(t.r - 0.003921569) < .001 && abs(t.b - 0.1215686) > .001;
}

bool GpuSim__SimShader__IsValid(float direction)
{
    return direction > 0 + .001;
}

float2 GpuSim__SimShader__dir_to_vec(float direction)
{
    float angle = (float)((direction * 255 - 1) * (3.1415926 / 2.0));
    return GpuSim__SimShader__IsValid(direction) ? float2(cos(angle), sin(angle)) : float2(0, 0);
}

float2 GpuSim__SimShader__pack_val_2byte(float x)
{
    float2 packed = float2(0, 0);
    packed.x = floor(x / 256.0);
    packed.y = x - packed.x * 256.0;
    return packed / 255.0;
}

void GpuSim__SimShader__set_wall_pos(inout float4 d, float pos)
{
    d.ba = GpuSim__SimShader__pack_val_2byte(pos);
}

bool GpuSim__SimShader__ValidDirward(float4 d)
{
    return any(abs(d - float4(0, 0, 0, 0)) > .001);
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
    float4 here = tex2D(fs_param_Tiles, psin.TexCoords + (float2(0, 0)) * fs_param_Tiles_dxdy);
    float4 geo_here = tex2D(fs_param_Geo, psin.TexCoords + (float2(0, 0)) * fs_param_Geo_dxdy), geo_right = tex2D(fs_param_Geo, psin.TexCoords + (float2(1, 0)) * fs_param_Geo_dxdy), geo_up = tex2D(fs_param_Geo, psin.TexCoords + (float2(0, 1)) * fs_param_Geo_dxdy), geo_left = tex2D(fs_param_Geo, psin.TexCoords + (float2(-(1), 0)) * fs_param_Geo_dxdy), geo_down = tex2D(fs_param_Geo, psin.TexCoords + (float2(0, -(1))) * fs_param_Geo_dxdy), geo_up_right = tex2D(fs_param_Geo, psin.TexCoords + (float2(1, 1)) * fs_param_Geo_dxdy), geo_up_left = tex2D(fs_param_Geo, psin.TexCoords + (float2(-(1), 1)) * fs_param_Geo_dxdy), geo_down_right = tex2D(fs_param_Geo, psin.TexCoords + (float2(1, -(1))) * fs_param_Geo_dxdy), geo_down_left = tex2D(fs_param_Geo, psin.TexCoords + (float2(-(1), -(1))) * fs_param_Geo_dxdy);
    float4 dirward_here = tex2D(fs_param_Dirward, psin.TexCoords + (float2(0, 0)) * fs_param_Dirward_dxdy), dirward_right = tex2D(fs_param_Dirward, psin.TexCoords + (float2(1, 0)) * fs_param_Dirward_dxdy), dirward_up = tex2D(fs_param_Dirward, psin.TexCoords + (float2(0, 1)) * fs_param_Dirward_dxdy), dirward_left = tex2D(fs_param_Dirward, psin.TexCoords + (float2(-(1), 0)) * fs_param_Dirward_dxdy), dirward_down = tex2D(fs_param_Dirward, psin.TexCoords + (float2(0, -(1))) * fs_param_Dirward_dxdy), dirward_up_right = tex2D(fs_param_Dirward, psin.TexCoords + (float2(1, 1)) * fs_param_Dirward_dxdy), dirward_up_left = tex2D(fs_param_Dirward, psin.TexCoords + (float2(-(1), 1)) * fs_param_Dirward_dxdy), dirward_down_right = tex2D(fs_param_Dirward, psin.TexCoords + (float2(1, -(1))) * fs_param_Dirward_dxdy), dirward_down_left = tex2D(fs_param_Dirward, psin.TexCoords + (float2(-(1), -(1))) * fs_param_Dirward_dxdy);
    if (GpuSim__SimShader__IsBlockingTile(here))
    {
        __FinalOutput.Color = float4(0, 0, 0, 0);
        return __FinalOutput;
    }
    float4 output = float4(0, 0, 0, 0);
    float4 forward = float4(0, 0, 0, 0), forward_right = float4(0, 0, 0, 0), forward_left = float4(0, 0, 0, 0), rightward = float4(0, 0, 0, 0), leftward = float4(0, 0, 0, 0);
    float4 geo_forward = float4(0, 0, 0, 0), geo_forward_right = float4(0, 0, 0, 0), geo_forward_left = float4(0, 0, 0, 0), geo_rightward = float4(0, 0, 0, 0), geo_leftward = float4(0, 0, 0, 0);
    if (abs(0.01568628 - 0.007843138) < .001)
    {
        forward = dirward_up;
        forward_right = dirward_up_right;
        forward_left = dirward_up_left;
        rightward = dirward_right;
        leftward = dirward_left;
        geo_forward = geo_up;
        geo_forward_right = geo_up_right;
        geo_forward_left = geo_up_left;
        geo_rightward = geo_right;
        geo_leftward = geo_left;
    }
    else
    {
        if (abs(0.01568628 - 0.003921569) < .001)
        {
            forward = dirward_right;
            forward_right = dirward_down_right;
            forward_left = dirward_up_right;
            rightward = dirward_down;
            leftward = dirward_up;
            geo_forward = geo_right;
            geo_forward_right = geo_down_right;
            geo_forward_left = geo_up_right;
            geo_rightward = geo_down;
            geo_leftward = geo_up;
        }
        else
        {
            if (abs(0.01568628 - 0.01568628) < .001)
            {
                forward = dirward_down;
                forward_right = dirward_down_left;
                forward_left = dirward_down_right;
                rightward = dirward_left;
                leftward = dirward_right;
                geo_forward = geo_down;
                geo_forward_right = geo_down_left;
                geo_forward_left = geo_down_right;
                geo_rightward = geo_left;
                geo_leftward = geo_right;
            }
            else
            {
                if (abs(0.01568628 - 0.01176471) < .001)
                {
                    forward = dirward_left;
                    forward_right = dirward_up_left;
                    forward_left = dirward_down_left;
                    rightward = dirward_up;
                    leftward = dirward_down;
                    geo_forward = geo_left;
                    geo_forward_right = geo_up_left;
                    geo_forward_left = geo_down_left;
                    geo_rightward = geo_up;
                    geo_leftward = geo_down;
                }
            }
        }
    }
    if (geo_here.r > 0 + .001 && GpuSim__SimShader__IsBlockingTile(tex2D(fs_param_Tiles, psin.TexCoords + (GpuSim__SimShader__dir_to_vec(0.01568628)) * fs_param_Tiles_dxdy)))
    {
        output.rg = geo_here.ba;
        float2 pos_here = psin.TexCoords * fs_param_Tiles_size;
        if (abs(0.01568628 - 0.003921569) < .001 || abs(0.01568628 - 0.01176471) < .001)
        {
            GpuSim__SimShader__set_wall_pos(output, pos_here.x);
        }
        if (abs(0.01568628 - 0.007843138) < .001 || abs(0.01568628 - 0.01568628) < .001)
        {
            GpuSim__SimShader__set_wall_pos(output, pos_here.y);
        }
    }
    else
    {
        if (GpuSim__SimShader__ValidDirward(forward) && all(abs(forward.rg - geo_forward.ba) < .001))
        {
            output = forward;
        }
        else
        {
            if (GpuSim__SimShader__ValidDirward(forward_right) && all(abs(forward_right.rg - geo_forward_right.ba) < .001))
            {
                output = forward_right;
            }
            else
            {
                if (GpuSim__SimShader__ValidDirward(forward_left) && all(abs(forward_left.rg - geo_forward_left.ba) < .001))
                {
                    output = forward_left;
                }
                else
                {
                    if (GpuSim__SimShader__ValidDirward(rightward) && all(abs(rightward.rg - geo_rightward.ba) < .001))
                    {
                        output = rightward;
                    }
                    else
                    {
                        if (GpuSim__SimShader__ValidDirward(leftward) && all(abs(leftward.rg - geo_leftward.ba) < .001))
                        {
                            output = leftward;
                        }
                    }
                }
            }
        }
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