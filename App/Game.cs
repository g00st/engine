using App.Engine.ImGuisStuff;
using App.Engine.Template;
using ImGuiNET;

namespace App;

using OpenTK.Graphics.OpenGL4;
using System;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

using System.Drawing;
using System.Net.Mail;
using OpenTK.Mathematics;
using Engine;




   
public class Game : GameWindow
{
        ImGuiController _controller;
        View Main = new View();
        private int Width;
        private int Height;
    
        private const int TargetFPS = 60; // Set your target FPS here
        private DateTime _lastFrameTime;

   

        public Game(int width, int height, string title) : base(GameWindowSettings.Default,
            new NativeWindowSettings() { Size = (1920/2, 1080/2), Title = "hi", Profile = ContextProfile.Core })
        {
            ErrorChecker.InitializeGLDebugCallback(); 
            _controller = new ImGuiController(ClientSize.X, ClientSize.Y);
            Width = width;
            Height = height;
            
           var par = new ColoredRectpParticles(new Vector2(0,0), new Vector2(100,100), Color4.Aqua,10,"par");
       //     Main.addObject(par);
           Main.addObject(new ColoredRectangle(Vector2.Zero , new Vector2(200,200),  Color4.Firebrick));
            for( int i = 0, j = 0; i < 10; i++, j++)
            { 
                Main.addObject(new ColoredRectangle(new Vector2(i * 100, j * 100), new Vector2(100, 100), Color4.Firebrick, "rec" + i));
            }
           
       

            
           // Main.addObject(new TexturedRectangle(new Vector2( 0,  0), new Vector2(1000, 1000), new Texture("resources/radarcrossection.png")));

            this.Resize += e => Main.Resize(e.Width, e.Height);
            this.Resize += e => this.resize(e.Width, e.Height);
            this.KeyDown += e => Update(e);
        }

        void resize(int width, int height)
        {
            _controller.WindowResized(ClientSize.X, ClientSize.Y);
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
            _controller.Update(this, (float)args.Time);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);
            Main.draw();
            
              DrawInfo.darwImguiDebugWindow();
              ImGui.ShowIDStackToolWindow();
              ImGuiController.CheckGLError("End of frame");
              
              _controller.Render();
            this.SwapBuffers();

        }
        protected override void OnTextInput(TextInputEventArgs e)
        {
            base.OnTextInput(e);
            _controller.PressChar((char)e.Unicode);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            _controller.MouseScroll(e.Offset);
        }
    }
