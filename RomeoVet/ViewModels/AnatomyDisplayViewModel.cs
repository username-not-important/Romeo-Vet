using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Resources;
using Assisticant;
using Assisticant.Fields;
using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.Wpf.SharpDX.Model;
using PropertyTools.Wpf;
using RomeoVet.Mesh;
using RomeoVet.Models;
using RomeoVet.Models.Anatomy;
using RomeoVet.ViewModels.Common;
using Camera = HelixToolkit.Wpf.SharpDX.Camera;
using Geometry3D = HelixToolkit.Wpf.SharpDX.Geometry3D;
using Material = HelixToolkit.Wpf.SharpDX.Material;

namespace RomeoVet.ViewModels
{
    public class AnatomyDisplayViewModel : AsynchronusViewModel
    {
        private AnatomyDisplayModel _display;

        public AnatomyDisplayViewModel(AnatomyDisplayModel display)
        {
            _display = display;
        }

        #region Skeleton

        public IList<BatchedMeshGeometryConfig> SkeletonBatch => _display.SkeletonBatch;
        public IList<Material> MaterialBatch => _display.MaterialBatch;

        public Geometry3D SelectedMesh
        {
            get { return _display.SelectedMesh; }
            set
            {
                _display.SelectedMesh = value;
                OnSelectionChanged(value);
            }
        }
        public Material MainSkeletonMaterial => _display.MainSkeletonMaterial;
        public Material SelectedMaterial => _display.SelectedMaterial;

        public Dictionary<string, Guid> BoneNameDictionary => _display.BoneNameDictionary;

        public Transform3D BatchedTransform => _display.BatchedTransform;

        #endregion

        #region Skin

        public Geometry3D SkinMesh
        {
            get { return _display.SkinMesh; }
            set { _display.SkinMesh = value; }
        }
        public Material SkinMaterial => _display.SkinMaterial;
        public Transform3D SkinTransform => _display.SkinTransform;

        #endregion

        public Stream ViewBoxTexture => Application.GetResourceStream(new Uri("Assets/ViewCube.jpg", UriKind.Relative)).Stream;

        public bool ShowSkin
        {
            get { return _display.ShowSkin; } set { _display.ShowSkin = value; }
        }
        public bool ShowSkeleton
        {
            get { return _display.ShowSkeleton; } set { _display.ShowSkeleton = value; }
        }

        public int LightIntensity
        {
            get
            {
                return _display.LightColor.ColorToHsvBytes()[2];
            }
            set
            {
                _display.LightColor = Color.FromRgb((byte)value, (byte)value, (byte)value);
            }
        }

        public Color LightColor => _display.LightColor;

        public event EventHandler ModelChanged;
        public event EventHandler<Geometry3D> SelectionChanged;

        public void LoadModels(IBatchProvider skeletonProvider,
                               IModelProvider skinProvider)
        {
            Perform(async delegate
            {
                await Task.Run(() =>
                {
                    var skeletonBatch = skeletonProvider.BuildModel();
                    var skinModel = skinProvider.BuildModel();

                    _display.ImportSkeleton(skeletonBatch);
                    _display.ImportSkin(skinModel);

                    _display.BoneNameDictionary = skeletonBatch.Names;

                    OnModelChanged();
                });

            });
        }

        protected virtual void OnModelChanged()
        {
            ModelChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnSelectionChanged(Geometry3D e)
        {
            SelectionChanged?.Invoke(this, e);
        }

        public string GetBoneName(Guid geometryGuid)
        {
            if (BoneNameDictionary.ContainsValue(geometryGuid))
                return BoneNameDictionary.First(p => p.Value == geometryGuid).Key;

            return "";
        }

        public Guid GetBoneGuid(string name)
        {
            if (BoneNameDictionary.ContainsKey(name))
                return BoneNameDictionary[name];

            return Guid.Empty;
        }

        public void SelectItem(string name, BodyPartType type)
        {
            Guid guid;

            switch (type)
            {
                case BodyPartType.Limb:
                    break;
                case BodyPartType.Category:
                    break;
                case BodyPartType.Bone:
                    guid = GetBoneGuid(name);
                    SelectedMesh = SkeletonBatch.First(c => c.Geometry.GUID == guid).Geometry;
                    break;
                case BodyPartType.Organ:
                    break;
                case BodyPartType.Muscle:
                    break;
                case BodyPartType.Vein:
                    break;
                case BodyPartType.Nerve:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            

        }

    }
}
