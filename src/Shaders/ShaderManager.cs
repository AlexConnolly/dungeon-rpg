using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDG.Shaders
{
    public static class ShaderManager
    {
        private static Dictionary<string, Effect> _shaders;

        // Load all shaders based on the ShaderConfiguration array
        public static void Load(ShaderConfiguration[] shaders, GraphicsDevice graphicsDevice)
        {
            // Initialise the dictionary
            _shaders = new Dictionary<string, Effect>();

            // Load each shader
            foreach (var shaderConfig in shaders)
            {
                try
                {
                    // Read the shader from the specified resource location
                    string shaderCode = File.ReadAllText(shaderConfig.Resource);

                    // TODO: Load
                }
                catch (Exception ex)
                {
                    // Handle any errors (e.g., file not found, compilation errors)
                    Console.WriteLine($"Error loading shader {shaderConfig.Key}: {ex.Message}");
                }
            }
        }

        // Get the shader by its key
        public static Effect GetShader(string key)
        {
            if (_shaders.ContainsKey(key))
            {
                return _shaders[key];
            }
            else
            {
                throw new KeyNotFoundException($"Shader with key '{key}' not found.");
            }
        }
    }

}
