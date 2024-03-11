using App.Engine;
using OpenTK.Mathematics;
using DrawElementsType = OpenTK.Graphics.OpenGL4.DrawElementsType;
using GL = OpenTK.Graphics.OpenGL4.GL;
using PrimitiveType = OpenTK.Graphics.OpenGL4.PrimitiveType;


namespace App.Engine;

public class Mesh
{
    public Mesh()
    {
        _texture = new List<Texture>();
        _Vertecies = new List<float[]>();
        _vao = new VAO();
    }
    protected int _verteciesLenght;
    protected Shader _shader;
    protected VAO _vao;
    protected  List<Texture> _texture;
    protected Matrix4 _MVP;
    protected List<float[]> _Vertecies;
    protected uint[] _Indecies;
    
    
    public Shader Shader
    {
        get { return _shader; }
        set { _shader = value; }
    }

    public Texture Texture
    {
        get { return _texture[0]; }
        set { _texture.Add(value); }
    }


    public void AddAtribute(Bufferlayout bufferlayout, float[] data)
    {
        if (_verteciesLenght != 0 && _verteciesLenght != data.Length/bufferlayout.count)
        {
            throw new ArgumentException("Atributes must be same lenght " + data.Length/bufferlayout.count + "   " + _verteciesLenght );
        }

        _verteciesLenght = data.Length/bufferlayout.count;
        _vao.LinkAtribute(data,bufferlayout);
       _Vertecies.Add(data);
        
    }
    

    public void AddIndecies(uint[] ind)
    {
        _vao.LinkElements(ind);
        _Indecies = ind;
    }
    
    


    public virtual void Draw(Matrix4 mvp , DrawInfo drawInfo, Vector2 cameraPosition, Matrix4 camera, float cameraRotation)
    {
        //uniform callback haben
        //aus zb shader.frag alle uniforms holen
        _vao.Bind();
        uint count = 0;
        foreach (var VARIABLE in _texture)
        {
            
            VARIABLE.Bind(count);
            count++;
        }

        _shader.Bind();
        _shader.setUniformM4("u_MVP", mvp); 
        _shader.setUniformV2f ("u_Position",drawInfo.Position);
        _shader.setUniformV2f ("u_Size",drawInfo.Size);
        _shader.setUniform1v ("u_Rotation" ,drawInfo.Rotation); 
        _shader.setUniformV2f ("u_CameraPosition",cameraPosition);
        _shader.setUniformM4 ("u_Camera",camera);
        _shader.setUniform1v ("u_CameraRotation" ,cameraRotation);
        
        GL.DrawElements(PrimitiveType.Triangles, _Indecies.Length, DrawElementsType.UnsignedInt, 0);

    }




}