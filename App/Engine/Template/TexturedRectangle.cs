using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace App.Engine.Template;

public class TexturedRectangle : DrawObject
{
public DrawInfo DrawInfo { get; }

    public TexturedRectangle(Vector2 positon, Vector2 size, Texture texture)
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
        this.DrawInfo.mesh.Texture = texture;
        this.DrawInfo.mesh.AddAtribute(bufferlayout, new float[] { 0.0f, 0.0f, 1.0f, 0.0f, 1.0f, 1.0f, 0.0f, 1.0f });
        this.DrawInfo.mesh.AddAtribute(bufferlayout, new float[]  { 0.0f, 0.0f, 1.0f, 0.0f, 1.0f, 1.0f, 0.0f, 1.0f });
        this.DrawInfo.mesh.AddIndecies(new uint[] { 0, 1, 2, 2, 3, 0 });
        this.DrawInfo.mesh.Shader = new Shader("resources/Template/simple_texture.vert",
            "resources/Template/simple_texture.frag");
    }

    public TexturedRectangle(Texture texture) : this(new Vector2(0, 0), new Vector2(texture.Width, texture.Height), texture)
    {
        
    }
    
    public TexturedRectangle(Vector2 positon, Vector2 size, Texture texture, Shader shader) : this(positon, size, texture)
    {
        this.DrawInfo.mesh.Shader = shader;
    }

}