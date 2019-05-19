using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Assisticant.Fields;
using RomeoVet.Annotations;

namespace RomeoVet.Models.Anatomy
{
    public enum BodyPartType
    {
        Limb, Category, Bone, Organ, Muscle, Vein, Nerve
    }

    public class BodyPart : INotifyPropertyChanged
    {
        private bool _isExpanded;
        private bool _isSelected;
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("Type")]
        public BodyPartType PartType { get; set; }

        [XmlElement("BodyPart", typeof(BodyPart))]
        public ObservableCollection<BodyPart> Children { get; set; }

        [XmlAttribute("IsExpanded")]
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (value == _isExpanded) return;
                _isExpanded = value;
                OnPropertyChanged();
            }
        }

        [XmlAttribute("IsSelected")]
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value == _isSelected) return;
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public BodyPart()
        {
            Children = new ObservableCollection<BodyPart>();
        }

        #region INPC

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
