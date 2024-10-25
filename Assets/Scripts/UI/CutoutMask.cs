using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CutoutMask : Image
{
    public override Material materialForRendering { get 
        { 
            Material mat = new(base.materialForRendering);
            mat.SetFloat("_StencilComp", (float)CompareFunction.NotEqual);
            return mat;
        } }
}
