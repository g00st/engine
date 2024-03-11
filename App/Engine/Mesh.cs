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
    private int _verteciesLenght;
    private Shader _shader;
    private VAO _vao;
    private  List<Texture> _texture;
    private Matrix4 _MVP;
    private List<float[]> _Vertecies;
    private uint[] _Indecies;
    
    
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


    public void Draw(Matrix4 mvp)
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
        GL.DrawElements(PrimitiveType.Triangles, _Indecies.Length, DrawElementsType.UnsignedInt, 0);

    }




}