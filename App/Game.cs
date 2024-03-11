using App.Engine.Template;

namespace App;

using OpenTK.Graphics.OpenGL;
using System;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Net.Mail;
using OpenTK.Mathematics;
using Engine;




    //ErrorChecker.InitializeGLDebugCallback(); ?
public class Game : GameWindow
{
      
        View Main = new View();
        private int Width;
        private int Height;
    
        private const int TargetFPS = 60; // Set your target FPS here
        private DateTime _lastFrameTime;

   

        public Game(int width, int height, string title) : base(GameWindowSettings.Default,
            new NativeWindowSettings() { Size = (1920/2, 1080/2), Title = "hi", Profile = ContextProfile.Compatability })
        {
           
            Width = width;
            Height = height;
            
        
            
            Main.addObject(new ColoredRectangle(Vector2.Zero , new Vector2(200,200),  Color4.Firebrick));
           
       

            
           // Main.addObject(new TexturedRectangle(new Vector2( 0,  0), new Vector2(1000, 1000), new Texture("resources/radarcrossection.png")));

            this.Resize += e => Main.Resize(e.Width, e.Height);
            this.Resize += e => this.resize(e.Width, e.Height);
            this.KeyDown += e => Update(e);
        }

        void resize(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }


        //void update();

        void Update(KeyboardKeyEventArgs e)
        {
           
        }
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            var elapsed = DateTime.Now - _lastFrameTime;
            var millisecondsPerFrame = 1000 / TargetFPS;
            if (elapsed.TotalMilliseconds < millisecondsPerFrame)
            {
                var delay = (int)(millisecondsPerFrame - elapsed.TotalMilliseconds);
                System.Threading.Thread.Sleep(delay);
            }
            
            _lastFrameTime = DateTime.Now;
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit );
            Main.draw();
            this.SwapBuffers();

        }
    }
