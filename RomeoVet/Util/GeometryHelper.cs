using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace RomeoVet.Util
{
    public static class GeometryHelper
    {
        public static Point3D Center(this Rect3D r)
        {
            return new Point3D((r.X + r.SizeX)/2.0, (r.Y + r.SizeY)/2.0, (r.Z + r.SizeZ)/2.0);
        }
    }
}
