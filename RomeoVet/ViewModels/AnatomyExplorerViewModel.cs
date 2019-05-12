using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assisticant.Collections;
using RomeoVet.Models;
using RomeoVet.Models.Anatomy;
using RomeoVet.Util;

namespace RomeoVet.ViewModels
{
    public class AnatomyExplorerViewModel
    {
        public ObservableList<Animal> _anatomy = new ObservableList<Animal>();

        public IEnumerable<Animal> Anatomy => _anatomy;

        public void ImportAnatomy(string catalog)
        {
            _anatomy.Clear();
            _anatomy.Add(Animal.Create(catalog));
        }
        

    }
}
