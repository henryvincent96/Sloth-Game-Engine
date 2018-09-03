using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;
using System.Drawing;
using System.Drawing.Imaging;

namespace Sloth_Engine.Managers
{
    static class ResourceManager
    {
        static Dictionary<String, int> Textures = new Dictionary<string, int>();
        static Dictionary<String, int> Audios = new Dictionary<string, int>();

        public static int LoadTexture(String filename)
        {
            int texture;
            Textures.TryGetValue(filename, out texture);

            if (texture == 0)
            {
                texture = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, texture);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

                Bitmap bitmap;
                try
                {
                    bitmap = new Bitmap("Textures/" + filename);
                }
                catch (ArgumentException)
                {
                    bitmap = new Bitmap("Textures/missingTexture.bmp");
                }

                BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bitmapData.Width, bitmapData.Height,
                    0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bitmapData.Scan0);

                bitmap.UnlockBits(bitmapData);

                Textures.Add(filename, texture);
            }

            return texture;
        }

        public static int LoadAudio(String filename)
        {
            int audioBuffer;
            Audios.TryGetValue(filename, out audioBuffer);

            if (audioBuffer == 0)
            {
                audioBuffer = AL.GenBuffer();

                Stream stream = (File.Open("Audio/" + filename, FileMode.Open));

                int channels, bitDepth, sampleRate;
                byte[] audioData;

                using (BinaryReader binaryReader = new BinaryReader(stream))
                {
                    string chunkID = new string((binaryReader.ReadChars(4)));
                    int riffChunk = binaryReader.ReadInt32();
                    string format = new string(binaryReader.ReadChars(4));
                    string subChunk1ID = new string(binaryReader.ReadChars(4));
                    int subChunk1Size = binaryReader.ReadInt32();
                    int audioFormat = binaryReader.ReadInt16();
                    channels = binaryReader.ReadInt16();
                    sampleRate = binaryReader.ReadInt32();
                    int byteRate = binaryReader.ReadInt32();
                    int blockAlign = binaryReader.ReadInt16();
                    bitDepth = binaryReader.ReadInt16();
                    string subChunk2ID = new string(binaryReader.ReadChars(4));
                    int subChunksSize = binaryReader.ReadInt32();

                    audioData = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);
                }

                ALFormat alFormat = 0;

                if (channels == 1)
                {
                    if (bitDepth == 8) { alFormat = ALFormat.Mono8; }
                    if (bitDepth == 16) { alFormat = ALFormat.Mono16; }
                }
                if(channels == 2)
                {
                    if (bitDepth == 8) { alFormat = ALFormat.Stereo8; }
                    if (bitDepth == 16) { alFormat = ALFormat.Stereo16; }
                }

                AL.BufferData(audioBuffer, alFormat, audioData, audioData.Length, sampleRate);
                if(AL.GetError() != ALError.NoError)
                {
                    throw new Exception("Error loading audio");
                }

                Audios.Add(filename, audioBuffer);
            }

            return audioBuffer;
        }
    }
}
