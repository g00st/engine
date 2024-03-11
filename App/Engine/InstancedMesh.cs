using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace App.Engine;

public class InstancedMesh : Mesh
{
    private Dictionary<string,uint> _instanceAtributes = new Dictionary<string, uint>();
     private int instanceCount;
        public InstancedMesh(int instanceCount) : base()
        {
            this.instanceCount = instanceCount;
        }

        public void AddInstancedAtribute(Bufferlayout bufferlayout, float[] data, string name)
        {
            if (data.Length != instanceCount* bufferlayout.count)
            {
                throw new ArgumentException("InstanceatributesAtributes must aling with instancecount: " + data.Length + "   " + instanceCount);
            }
            var s = _vao.LinkAtribute(data, bufferlayout,1);
            Console.WriteLine("poss "+s);
            _instanceAtributes.Add(name,s);
        }
       
        public void ChangeInstancedAtributeData(Bufferlayout bufferlayout, float[] data, string name)
        {
            if (data.Length != instanceCount * bufferlayout.count && !_instanceAtributes.ContainsKey(name))
            {
                throw new ArgumentException("InstanceatributesAtributes must aling with instancecount: " + data.Length + "   " + instanceCount);
            }
            _vao.UpdateAttributeData(data, bufferlayout, _instanceAtributes[name]);

        }
        
     
        public virtual void Draw(Matrix4 mvp , DrawInfo drawInfo, Vector2 cameraPosition, Matrix4 camera, float cameraRotation)
     {
         
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
         GL.DrawElementsInstanced(PrimitiveType.Triangles, _Indecies.Length, DrawElementsType.UnsignedInt,IntPtr.Zero, instanceCount);
     }
}