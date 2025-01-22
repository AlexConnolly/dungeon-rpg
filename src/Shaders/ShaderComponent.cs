using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Shaders
{
    public class ShaderComponent : GameComponent
    {
        public Effect Effect { get; set; }
        string blueScaleShaderCode = @"
        sampler TextureSampler : register(s0);

        float4 MainPS(float4 colour : COLOR0, float2 texCoord : TEXCOORD0) : COLOR0
        {
            // Sample the texture
            float4 texColour = tex2D(TextureSampler, texCoord);

            // Convert to blue-scale by zeroing out red and green
            texColour.r = 0.0f;
            texColour.g = 0.0f;

            return texColour;
        }

        technique BlueScale
        {
            pass Pass1
            {
                PixelShader = compile ps_4_0 MainPS();
            }
        }
        ";

        public override void Initialize()
        {

        }
    }
}
