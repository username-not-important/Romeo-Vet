using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using Assisticant.Fields;
using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.Wpf.SharpDX.Model;
using Camera = HelixToolkit.Wpf.SharpDX.Camera;
using Geometry3D = HelixToolkit.Wpf.SharpDX.Geometry3D;
using Material = HelixToolkit.Wpf.SharpDX.Material;
using PerspectiveCamera = HelixToolkit.Wpf.SharpDX.PerspectiveCamera;

namespace RomeoVet.Models
{
    public class AnatomyDisplayModel
    {
        private Observable<Geometry3D> _mesh = new Observable<Geometry3D>();
        private Observable<MaterialCore> _material = new Observable<MaterialCore>();

        public Geometry3D Mesh
        {
            get { return _mesh; }
            set { _mesh.Value = value; }
        }

        public MaterialCore Material
        {
            get { return _material; }
            set { _material.Value = value; }
        }
    }
}
