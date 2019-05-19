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
        private readonly AnatomyTreeHelper _helper = new AnatomyTreeHelper();

        public ObservableList<Animal> _anatomy = new ObservableList<Animal>();
        
        public IEnumerable<Animal> Anatomy => _anatomy;
        
        public void ImportAnatomy(string catalog)
        {
            _anatomy.Clear();
            _anatomy.Add(Animal.Create(catalog));
        }

        public void SelectBone(string bone)
        {
            var find = _helper.find(Anatomy.First().BodyParts, bone, BodyPartType.Bone);

            if (find != null)
                find.IsSelected = true;
        }

        public void DiscardSelection()
        {
            var selection = _helper.findSelection(Anatomy.First().BodyParts);

            if (selection != null)
                selection.IsSelected = false;
        }
    }
}
