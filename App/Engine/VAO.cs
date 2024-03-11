using OpenTK.Graphics.OpenGL4;
using GL = OpenTK.Graphics.OpenGL4.GL;
using VertexAttribType = OpenTK.Graphics.OpenGL4.VertexAttribType;

namespace App.Engine;
//vertex array object = VAO
public class VAO
{
    private uint _handle;
    private uint counter = 0;
    public VAO()
    {
        GL.CreateVertexArrays(1, out _handle);
        //handle= GL.GenVertexArray();
        ErrorChecker.CheckForGLErrors("A");
    }

    public void   LinkAtribute(float[] bufferData,Bufferlayout layout )
    {
        uint vbo  = 0;
        float[] a = bufferData.ToArray();
        GL.CreateBuffers(1, out  vbo);
        ErrorChecker.CheckForGLErrors("b1");
        GL.NamedBufferData(vbo, bufferData.Length*layout.typesize,bufferData ,BufferUsageHint.StaticDraw );
        ErrorChecker.CheckForGLErrors("b2");
        GL.EnableVertexArrayAttrib(_handle, counter);
        ErrorChecker.CheckForGLErrors("b3");
        GL.VertexArrayAttribFormat(_handle, counter, layout.count,layout.type , layout.normalized, 0);
        ErrorChecker.CheckForGLErrors("b4");
        GL.VertexArrayVertexBuffer(_handle, counter, vbo, (IntPtr) layout.offset,layout.count*layout.typesize );
        ErrorChecker.CheckForGLErrors("b5");
        GL.VertexArrayAttribBinding(_handle,counter,counter);
        ErrorChecker.CheckForGLErrors("b6");
        counter++;

    }

    public void LinkElements(uint[]bufferData)
    {
        uint vbe  = 0;
        ErrorChecker.CheckForGLErrors("chh");
        GL.CreateBuffers(1, out  vbe);
        Console.WriteLine(bufferData.Length);
        ErrorChecker.CheckForGLErrors("c0");
        GL.NamedBufferData(vbe, bufferData.Length*sizeof(uint),bufferData ,BufferUsageHint.StaticDraw );
        ErrorChecker.CheckForGLErrors("c1");
        GL.VertexArrayElementBuffer(_handle, vbe);
        ErrorChecker.CheckForGLErrors("c");
    }

    public void Bind()
    {
        GL.BindVertexArray(_handle);
        ErrorChecker.CheckForGLErrors("Bind VAO");
    }
    
}


public struct Bufferlayout
{
    public int count;
    public VertexAttribType type;
    public int offset;
    public bool normalized;
    public int typesize;
}