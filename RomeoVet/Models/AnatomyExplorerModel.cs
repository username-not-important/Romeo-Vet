using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assisticant.Fields;
using RomeoVet.Models.Anatomy;

namespace RomeoVet.Models
{
    public class AnatomyExplorerModel
    {
        private Animal _animal;

        public Animal Anatomy => _animal;

        public void ImportAnatomy(string catalog)
        {
            _animal = Animal.Create(catalog);
        }
    }
}
