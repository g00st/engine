using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace App.Engine.Template;

public class ColoredRectpParticles : DrawObject
{
    
    public DrawInfo drawInfo { get; }
    private Bufferlayout posslayout;
    private int count;
    public int  Count {get{return count;}}

    public void SetColor(Color4 color)
    {
        drawInfo.mesh.Shader.Bind();
        drawInfo.mesh.Shader.setUniform4v("u_Color", color.R, color.G, color.B, color.A) ;
    }

    public ColoredRectpParticles(Vector2 positon, Vector2 size, Color4 color,int count, string name = "ColoredRectPariclesystem" )
    {
        this.drawInfo = new DrawInfo(positon, size, 0, null, name);
        this.drawInfo.Position = positon;
        this.drawInfo.Size = size;
        this.drawInfo.Rotation = 0;

        Bufferlayout bufferlayout = new Bufferlayout();
        bufferlayout.count = 2;
        bufferlayout.normalized = false;
        bufferlayout.offset = 0;
        bufferlayout.type = VertexAttribType.Float;
        bufferlayout.typesize = sizeof(float);
        count = count;
        this.drawInfo.mesh = new InstancedMesh(count);
        this.drawInfo.mesh.AddAtribute(bufferlayout, new float[] { 0.0f, 0.0f, 1.0f, 0.0f, 1.0f, 1.0f, 0.0f, 1.0f });
        this.drawInfo.mesh.AddIndecies(new uint[] { 0, 1, 2, 2, 3, 0 });
        
        posslayout = new Bufferlayout();
        posslayout.count = 2;
        posslayout.normalized = false;
        posslayout.offset = 0;
        posslayout.type = VertexAttribType.Float;
        posslayout.typesize = sizeof(float);
        
        var f = new float[count*2];
        for (int i = 0; i < count; i++)
        {
            f[i*2] = i*100; 
            f[i*2+1] = 0.0f;
        }
        
        ((InstancedMesh) this.drawInfo.mesh).AddInstancedAtribute(posslayout,f,"poss");
        this.drawInfo.mesh.Shader = new Shader( "resources/Template/instanced_poss.vert","resources/Template/single_color.frag");
        SetColor(color);

    }
    public void setPositions(float[] positions)
    {
        ((InstancedMesh) this.drawInfo.mesh).ChangeInstancedAtributeData(posslayout,positions,"poss");
    }   
}