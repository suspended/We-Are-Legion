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
float4 vs_param_cameraPos;
float vs_param_cameraAspect;

// The following are variables used by the fragment shader (fragment parameters).
// Texture Sampler for fs_param_BuildingDistances, using register location 1
float2 fs_param_BuildingDistances_size;
float2 fs_param_BuildingDistances_dxdy;

Texture fs_param_BuildingDistances_Texture;
sampler fs_param_BuildingDistances : register(s1) = sampler_state
{
    texture   = <fs_param_BuildingDistances_Texture>;
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

// Texture Sampler for fs_param_Unit, using register location 3
float2 fs_param_Unit_size;
float2 fs_param_Unit_dxdy;

Texture fs_param_Unit_Texture;
sampler fs_param_Unit : register(s3) = sampler_state
{
    texture   = <fs_param_Unit_Texture>;
    MipFilter = Point;
    MagFilter = Point;
    MinFilter = Point;
    AddressU  = Clamp;
    AddressV  = Clamp;
};

float fs_param_blend;

float fs_param_radius;


// The following variables are included because they are referenced but are not function parameters. Their values will be set at call time.
// Texture Sampler for fs_param_FarColor, using register location 4
float2 fs_param_FarColor_size;
float2 fs_param_FarColor_dxdy;

Texture fs_param_FarColor_Texture;
sampler fs_param_FarColor : register(s4) = sampler_state
{
    texture   = <fs_param_FarColor_Texture>;
    MipFilter = Point;
    MagFilter = Point;
    MinFilter = Point;
    AddressU  = Clamp;
    AddressV  = Clamp;
};

// The following methods are included because they are referenced by the fragment shader.
float2 Game__SimShader__get_subcell_pos__VertexOut__vec2(VertexToPixel vertex, float2 grid_size)
{
    float2 coords = vertex.TexCoords * grid_size + float2(-(0.25), -(0.25));
    float i = floor(coords.x);
    float j = floor(coords.y);
    return coords - float2(i, j);
}

float2 FragSharpFramework__FragSharpStd__Float__vec2(float2 v)
{
    return floor(255 * v + float2(0.5, 0.5));
}

bool Game__SimShader__fake_selected__data(float4 u)
{
    float val = u.b;
    return 0.1254902 <= val + .0019 && val < 0.5019608 - .0019;
}

bool Game__SimShader__fake_selected__building(float4 u)
{
    return Game__SimShader__fake_selected__data(u);
}

float4 Game__SelectedUnitColor__Get__Single(VertexToPixel psin, float player)
{
    if (abs(player - 0.003921569) < .0019)
    {
        return tex2D(fs_param_FarColor, float2(1-0.25,-0.25+ 1 + (int)player) * fs_param_FarColor_dxdy);
    }
    if (abs(player - 0.007843138) < .0019)
    {
        return tex2D(fs_param_FarColor, float2(1-0.25,-0.25+ 2 + (int)player) * fs_param_FarColor_dxdy);
    }
    if (abs(player - 0.01176471) < .0019)
    {
        return tex2D(fs_param_FarColor, float2(1-0.25,-0.25+ 3 + (int)player) * fs_param_FarColor_dxdy);
    }
    if (abs(player - 0.01568628) < .0019)
    {
        return tex2D(fs_param_FarColor, float2(1-0.25,-0.25+ 4 + (int)player) * fs_param_FarColor_dxdy);
    }
    return float4(0.0, 0.0, 0.0, 0.0);
}

float FragSharpFramework__FragSharpStd__fint_round__Single(float v)
{
    return floor(255 * v + 0.5) * 0.003921569;
}

float Game__SimShader__get_type__BuildingDist(float4 u)
{
    return FragSharpFramework__FragSharpStd__fint_round__Single(u.b / 16.0);
}

float Game__SimShader__get_player__BuildingDist(float4 u)
{
    return u.b - Game__SimShader__get_type__BuildingDist(u) * 16.0;
}

int FragSharpFramework__FragSharpStd__Int__Single(float v)
{
    return (int)floor(255 * v + 0.5);
}

float Game__UnitType__BuildingIndex__Single(float type)
{
    return type - 0.02352941;
}

float4 Game__BuildingMarkerColors__Get__Single__Single(VertexToPixel psin, float player, float type)
{
    return tex2D(fs_param_FarColor, float2(3 + FragSharpFramework__FragSharpStd__Int__Single(Game__UnitType__BuildingIndex__Single(type))-0.25,-0.25+ FragSharpFramework__FragSharpStd__Int__Single(player)) * fs_param_FarColor_dxdy);
}

// Compiled vertex shader
VertexToPixel StandardVertexShader(float2 inPos : POSITION0, float2 inTexCoords : TEXCOORD0, float4 inColor : COLOR0)
{
    VertexToPixel Output = (VertexToPixel)0;
    Output.Position.w = 1;
    Output.Position.x = (inPos.x - vs_param_cameraPos.x) / vs_param_cameraAspect * vs_param_cameraPos.z;
    Output.Position.y = (inPos.y - vs_param_cameraPos.y) * vs_param_cameraPos.w;
    Output.TexCoords = inTexCoords;
    Output.Color = inColor;
    return Output;
}

// Compiled fragment shader
PixelToFrame FragmentShader(VertexToPixel psin)
{
    PixelToFrame __FinalOutput = (PixelToFrame)0;
    float4 info = tex2D(fs_param_BuildingDistances, psin.TexCoords + (-float2(0.25,0.25) + float2(0, 0)) * fs_param_BuildingDistances_dxdy);
    if (info.a > 0.05882353 + .0019)
    {
        __FinalOutput.Color = float4(0.0, 0.0, 0.0, 0.0);
        return __FinalOutput;
    }
    float2 subcell_pos = Game__SimShader__get_subcell_pos__VertexOut__vec2(psin, fs_param_BuildingDistances_size);
    float2 offset = FragSharpFramework__FragSharpStd__Float__vec2(info.rg - float2(0.1568628, 0.1568628));
    float2 index = float2(offset.x, offset.y);
    float4 b = tex2D(fs_param_Data, psin.TexCoords + (-float2(0.25,0.25) + index) * fs_param_Data_dxdy);
    float4 u = tex2D(fs_param_Unit, psin.TexCoords + (-float2(0.25,0.25) + index) * fs_param_Unit_dxdy);
    float l = length(255 * (info.rg - float2(0.1568628, 0.1568628)) - (subcell_pos - float2(0.5, 0.5)));
    if (Game__SimShader__fake_selected__building(b) && abs(u.g - 0.01568628) < .0019)
    {
        if (l > 0.8 * fs_param_radius + .0019 && l < fs_param_radius * 1.15 - .0019)
        {
            float4 clr = Game__SelectedUnitColor__Get__Single(psin, Game__SimShader__get_player__BuildingDist(info)) * 0.75;
            clr.a = 1;
            __FinalOutput.Color = clr * fs_param_blend;
            return __FinalOutput;
        }
        if (l < fs_param_radius - .0019)
        {
            float4 clr = Game__BuildingMarkerColors__Get__Single__Single(psin, Game__SimShader__get_player__BuildingDist(info), Game__SimShader__get_type__BuildingDist(info)) * 1.0;
            clr.a = 1;
            __FinalOutput.Color = clr * fs_param_blend;
            return __FinalOutput;
        }
    }
    else
    {
        if (l < fs_param_radius - .0019)
        {
            float4 clr = Game__BuildingMarkerColors__Get__Single__Single(psin, Game__SimShader__get_player__BuildingDist(info), Game__SimShader__get_type__BuildingDist(info));
            __FinalOutput.Color = clr * fs_param_blend;
            return __FinalOutput;
        }
    }
    __FinalOutput.Color = float4(0.0, 0.0, 0.0, 0.0);
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