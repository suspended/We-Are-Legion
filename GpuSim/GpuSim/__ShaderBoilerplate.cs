// This file was auto-generated by FragSharp. It will be regenerated on the next compilation.
// Manual changes made will not persist and may cause incorrect behavior between compilations.

using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using FragSharpFramework;

namespace FragSharpFramework.Boilerplate
{
    public class _
    {
        public static void Initialize(ContentManager Content)
        {
            GpuSim.DrawGrass.CompiledEffect = Content.Load<Effect>("FragSharpShaders/DrawGrass");
            GpuSim.DrawUnit.CompiledEffect = Content.Load<Effect>("FragSharpShaders/DrawUnit");
            GpuSim.Movement_Phase1.CompiledEffect = Content.Load<Effect>("FragSharpShaders/Movement_Phase1");
            GpuSim.Movement_Phase2.CompiledEffect = Content.Load<Effect>("FragSharpShaders/Movement_Phase2");
        }
    }
}

namespace GpuSim
{
    public partial class DrawGrass
    {
        public static Effect CompiledEffect;

        public static void Use(vec4 cameraPos, float cameraAspect, Texture2D Texture, RenderTarget2D Output)
        {
            GridHelper.GraphicsDevice.SetRenderTarget(Output);
            GridHelper.GraphicsDevice.Clear(Color.Transparent);
            Use(cameraPos, cameraAspect, Texture);
            GridHelper.DrawGrid();
        }
        public static void Use(vec4 cameraPos, float cameraAspect, Texture2D Texture)
        {
            CompiledEffect.Parameters["vs_param_cameraPos"].SetValue(FragSharp.Marshal(cameraPos));
            CompiledEffect.Parameters["vs_param_cameraAspect"].SetValue(FragSharp.Marshal(cameraAspect));
            CompiledEffect.Parameters["fs_param_Texture_Texture"].SetValue(FragSharp.Marshal(Texture));
            CompiledEffect.Parameters["fs_param_Texture_size"]   .SetValue(FragSharp.Marshal(vec(Texture.Width, Texture.Height)));
            CompiledEffect.Parameters["fs_param_Texture_d"]      .SetValue(FragSharp.Marshal(1.0f / vec(Texture.Width, Texture.Height)));
            CompiledEffect.CurrentTechnique.Passes[0].Apply();
        }
    }
}


namespace GpuSim
{
    public partial class DrawUnit
    {
        public static Effect CompiledEffect;

        public static void Use(vec4 cameraPos, float cameraAspect, Texture2D Current, Texture2D Previous, Texture2D Texture, float PercentSimStepComplete, RenderTarget2D Output)
        {
            GridHelper.GraphicsDevice.SetRenderTarget(Output);
            GridHelper.GraphicsDevice.Clear(Color.Transparent);
            Use(cameraPos, cameraAspect, Current, Previous, Texture, PercentSimStepComplete);
            GridHelper.DrawGrid();
        }
        public static void Use(vec4 cameraPos, float cameraAspect, Texture2D Current, Texture2D Previous, Texture2D Texture, float PercentSimStepComplete)
        {
            CompiledEffect.Parameters["vs_param_cameraPos"].SetValue(FragSharp.Marshal(cameraPos));
            CompiledEffect.Parameters["vs_param_cameraAspect"].SetValue(FragSharp.Marshal(cameraAspect));
            CompiledEffect.Parameters["fs_param_Current_Texture"].SetValue(FragSharp.Marshal(Current));
            CompiledEffect.Parameters["fs_param_Current_size"]   .SetValue(FragSharp.Marshal(vec(Current.Width, Current.Height)));
            CompiledEffect.Parameters["fs_param_Current_d"]      .SetValue(FragSharp.Marshal(1.0f / vec(Current.Width, Current.Height)));
            CompiledEffect.Parameters["fs_param_Previous_Texture"].SetValue(FragSharp.Marshal(Previous));
            CompiledEffect.Parameters["fs_param_Previous_size"]   .SetValue(FragSharp.Marshal(vec(Previous.Width, Previous.Height)));
            CompiledEffect.Parameters["fs_param_Previous_d"]      .SetValue(FragSharp.Marshal(1.0f / vec(Previous.Width, Previous.Height)));
            CompiledEffect.Parameters["fs_param_Texture_Texture"].SetValue(FragSharp.Marshal(Texture));
            CompiledEffect.Parameters["fs_param_Texture_size"]   .SetValue(FragSharp.Marshal(vec(Texture.Width, Texture.Height)));
            CompiledEffect.Parameters["fs_param_Texture_d"]      .SetValue(FragSharp.Marshal(1.0f / vec(Texture.Width, Texture.Height)));
            CompiledEffect.Parameters["fs_param_PercentSimStepComplete"].SetValue(FragSharp.Marshal(PercentSimStepComplete));
            CompiledEffect.CurrentTechnique.Passes[0].Apply();
        }
    }
}


namespace GpuSim
{
    public partial class Movement_Phase1
    {
        public static Effect CompiledEffect;

        public static void Use(Texture2D Current, RenderTarget2D Output)
        {
            GridHelper.GraphicsDevice.SetRenderTarget(Output);
            GridHelper.GraphicsDevice.Clear(Color.Transparent);
            Use(Current);
            GridHelper.DrawGrid();
        }
        public static void Use(Texture2D Current)
        {
            CompiledEffect.Parameters["fs_param_Current_Texture"].SetValue(FragSharp.Marshal(Current));
            CompiledEffect.Parameters["fs_param_Current_size"]   .SetValue(FragSharp.Marshal(vec(Current.Width, Current.Height)));
            CompiledEffect.Parameters["fs_param_Current_d"]      .SetValue(FragSharp.Marshal(1.0f / vec(Current.Width, Current.Height)));
            CompiledEffect.CurrentTechnique.Passes[0].Apply();
        }
    }
}


namespace GpuSim
{
    public partial class Movement_Phase2
    {
        public static Effect CompiledEffect;

        public static void Use(Texture2D Current, Texture2D Previous, RenderTarget2D Output)
        {
            GridHelper.GraphicsDevice.SetRenderTarget(Output);
            GridHelper.GraphicsDevice.Clear(Color.Transparent);
            Use(Current, Previous);
            GridHelper.DrawGrid();
        }
        public static void Use(Texture2D Current, Texture2D Previous)
        {
            CompiledEffect.Parameters["fs_param_Current_Texture"].SetValue(FragSharp.Marshal(Current));
            CompiledEffect.Parameters["fs_param_Current_size"]   .SetValue(FragSharp.Marshal(vec(Current.Width, Current.Height)));
            CompiledEffect.Parameters["fs_param_Current_d"]      .SetValue(FragSharp.Marshal(1.0f / vec(Current.Width, Current.Height)));
            CompiledEffect.Parameters["fs_param_Previous_Texture"].SetValue(FragSharp.Marshal(Previous));
            CompiledEffect.Parameters["fs_param_Previous_size"]   .SetValue(FragSharp.Marshal(vec(Previous.Width, Previous.Height)));
            CompiledEffect.Parameters["fs_param_Previous_d"]      .SetValue(FragSharp.Marshal(1.0f / vec(Previous.Width, Previous.Height)));
            CompiledEffect.CurrentTechnique.Passes[0].Apply();
        }
    }
}


