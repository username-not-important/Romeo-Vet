using System;
using System.Collections.ObjectModel;
using RomeoVet.Models.Anatomy;

namespace RomeoVet.ViewModels
{
    public class AnatomyTreeHelper
    {
        public BodyPart find(ObservableCollection<BodyPart> children, string name, BodyPartType type)
        {
            foreach (var bodyPart in children)
            {
                if (bodyPart.PartType == type && (bodyPart.Name.StartsWith(name) || name.StartsWith(bodyPart.Name)))
                    return bodyPart;
                else
                {
                    var f = find(bodyPart.Children, name, type);

                    if (f != null)
                    {
                        bodyPart.IsExpanded = true;
                        return f;
                    }
                    else
                        bodyPart.IsExpanded = false;
                }
            }

            return null;
        }

        public BodyPart findSelection(ObservableCollection<BodyPart> children)
        {
            foreach (var bodyPart in children)
            {
                if (bodyPart.IsSelected)
                    return bodyPart;
                else
                {
                    var f = findSelection(bodyPart.Children);

                    if (f != null)
                        return f;
                }
            }

            return null;

        }
    }
}