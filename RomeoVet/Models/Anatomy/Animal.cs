using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using RomeoVet.Util;

namespace RomeoVet.Models.Anatomy
{
    public class Animal
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlIgnore]
        public bool IsExpanded { get; set; } = true;

        [XmlIgnore]
        public bool IsSelected { get; set; } = false;

        public ObservableCollection<BodyPart> BodyParts { get; set; }

        public Animal()
        {
            BodyParts=new ObservableCollection<BodyPart>();
        }

        public static Animal Create(string catalog)
        {
               return TextSerializer.XmlDeserializeFromString<Animal>(catalog);
        }
    }
}
