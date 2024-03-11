using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace App.Engine.Template;

public class ColoredRectangle : DrawObject
{

    public DrawInfo DrawInfo { get; }

    public void SetColor(Color4 color)
    {
        ((SimpleColorShader)DrawInfo.mesh.Shader).setColor(color);
    }

    public ColoredRectangle(Vector2 positon, Vector2 size, Color4 color)
    {
        this.DrawInfo = new DrawInfo();
        this.DrawInfo.Position = positon;
        this.DrawInfo.Size = size;
        this.DrawInfo.Rotation = 0;

        Bufferlayout bufferlayout = new Bufferlayout();
        bufferlayout.count = 2;
        bufferlayout.normalized = false;
        bufferlayout.offset = 0;
        bufferlayout.type = VertexAttribType.Float;
        bufferlayout.typesize = sizeof(float);

        this.DrawInfo.mesh = new Mesh();
        this.DrawInfo.mesh.AddAtribute(bufferlayout, new float[] { 0.0f, 0.0f, 1.0f, 0.0f, 1.0f, 1.0f, 0.0f, 1.0f });
        this.DrawInfo.mesh.AddIndecies(new uint[] { 0, 1, 2, 2, 3, 0 });
        this.DrawInfo.mesh.Shader = new SimpleColorShader(color);

    }
}
    
   