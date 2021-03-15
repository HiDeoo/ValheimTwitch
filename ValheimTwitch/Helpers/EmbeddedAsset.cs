﻿using System.IO;
using System.Reflection;
using UnityEngine;

namespace ValheimTwitch.Helpers
{
    // https://github.com/valheimPlus/ValheimPlus/blob/development/ValheimPlus/Utility/EmbeddedAsset.cs
    public static class EmbeddedAsset
    {
        public static Stream LoadEmbeddedAsset(string assetPath)
        {
            Assembly objAsm = Assembly.GetExecutingAssembly();

            if (objAsm.GetManifestResourceInfo(objAsm.GetName().Name + "." + assetPath) != null)
            {
                return objAsm.GetManifestResourceStream(objAsm.GetName().Name + "." + assetPath);
            }

            return null;
        }

        public static bool LoadAssembly(string assetPath)
        {
            Stream fileStream = LoadEmbeddedAsset(assetPath);

            if (fileStream != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    fileStream.CopyTo(memoryStream);
                    Assembly.Load(memoryStream.ToArray());
                    fileStream.Dispose();

                    return true;
                }
            }

            return false;
        }

        public static Texture2D LoadTexture2D(string assetPath)
        {
            Texture2D texture = null;
            Stream fileStream = LoadEmbeddedAsset(assetPath);

            if (fileStream != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    texture = new Texture2D(2, 2);
                    
                    fileStream.CopyTo(memoryStream);
                    texture.LoadImage(memoryStream.ToArray());
                    fileStream.Dispose();
                }
            }

            return texture;
        }
    }

}
