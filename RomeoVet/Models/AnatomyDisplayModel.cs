using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using Assisticant.Fields;

namespace RomeoVet.Models
{
    public class AnatomyDisplayModel
    {
        private Observable<Model3D> _model = new Observable<Model3D>();

        public Model3D Model
        {
            get { return _model; }
            set { _model.Value = value; }
        }
    }
}
