using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RomeoVet.Models.Anatomy
{
    public enum BodyPartType
    {
        Limb, Category, Bone, Organ, Muscle, Vein, Nerve
    }

    public class BodyPart
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("Type")]
        public BodyPartType PartType { get; set; }

        [XmlElement("BodyPart", typeof(BodyPart))]
        public ObservableCollection<BodyPart> Children { get; set; }

        [XmlAttribute("IsExpanded")]
        public bool IsExpanded { get; set; }

        [XmlAttribute("IsSelected")]
        public bool IsSelected { get; set; }

        public BodyPart()
        {
            Children = new ObservableCollection<BodyPart>();
        }
    }
}
