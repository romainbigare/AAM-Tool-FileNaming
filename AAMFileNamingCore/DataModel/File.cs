using AAMFileNamingCore.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AAMFileNamingCore.DataModel
{
    public class File : INotifyPropertyChanged
    {
        // project originator volume level doctype role typecode drawingserial
        // proj number _ file code _  document type _  date of issue _ rev _ descriptiion
        // volume / building	-	level	-	type	-	role	-	type code	drawing serial no.	_	short description (optional)

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public string Name => System.IO.Path.GetFileNameWithoutExtension(Path);
        public string Path { get; set; }
        public string Extension => System.IO.Path.GetExtension(Path);
        public string NewNameIso => $"{ProjectNumber}-{Originator}-{Volume}-{LevelString}-{DocumentTypeString}-{RoleString}-{TypeCodeString}{DrawingSerialNo}{CreateSeparator(Description, "_")}{Description}";

        private object CreateSeparator(string description, string sep)
        {
            if (string.IsNullOrEmpty(description))
                return "";

            return sep;
        }

        public string NewNameNonIso => $"{ProjectNumber}_{FileCodeString}{CreateSeparator(DocumentTypeString, "_")}{DocumentTypeString}_{DateOfIssue}{CreateSeparator(Revision, "_")}{Revision}{CreateSeparator(Description, "_")}{Description}";
        public string NewNameRootIso => $"{ProjectNumber}-{Originator}-{Volume}-{LevelString}-{DocumentTypeString}-{RoleString}-{TypeCodeString}-{Description}";
        public string NewNameRootNonIso => $"{ProjectNumber}_{FileCodeString}_{DocumentTypeString}_{DateOfIssue}_{Revision}_{Description}";
        #region Properties

        private string _projectNumber = "";
        public string ProjectNumber { get => _projectNumber; set => _projectNumber = value; }

        public string _volume = "XX";
        public string Volume { get => _volume; set => _volume = value; }


        private string _originator = "";
        public string Originator { get => _originator; set => _originator = value; }


        private string _description = "";
        public string Description { get => _description; set => _description = value; }
        // description naked is the description without any ( digit ) at the end
        public string DescriptionNaked => Regex.Replace(Description, @"\(\d+\)$", "");

        // description number is the number at the end of the description , in parenthesis
        public string DescriptionNumber
        {
            get
            {
                var match = Regex.Match(Description, @"\((\d+)\)$");
                return match.Success ? match.Groups[1].Value : "";
            }
        }

        private string _revision = "";
        public string Revision { get => _revision; set => _revision = value; }


        private Level _level = Level.NoLevelApplicable;
        public Level Level { get => _level; set => _level = value; }
        public string LevelString => NamingLists.GetAbbreviation(Level) ?? string.Empty;


        private DocumentType _documentType = DocumentType.None;
        public DocumentType DocumentType { get => _documentType; set => _documentType = value; }
        public string DocumentTypeString => NamingLists.GetAbbreviation(DocumentType) ?? string.Empty;


        private Role _role = Role.Architecture;
        public Role Role { get => _role; set => _role = value; }
        public string RoleString => NamingLists.GetAbbreviation(Role) ?? string.Empty;


        private TypeCode _typeCode = TypeCode.None;
        public TypeCode TypeCode { get => _typeCode; set => _typeCode = value; }
        public string TypeCodeString => NamingLists.GetAbbreviation(TypeCode) ?? string.Empty;


        private string _drawingSerialNo = "";
        public string DrawingSerialNo
        {
            get => _drawingSerialNo;
            set
            {
                _drawingSerialNo = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DrawingSerialNo"));
            }
        }
        public int DrawingSerialNoInt => int.TryParse(DrawingSerialNo, out int result) ? result : 0;


        private string _dateOfIssue = "";
        public string DateOfIssue { get => _dateOfIssue; set => _dateOfIssue = value; }

        private FileCode _fileCode = FileCode.None;
        public FileCode FileCode { get => _fileCode; set => _fileCode = value; }

        public string FileCodeString => NamingLists.GetAbbreviation(FileCode) ?? string.Empty;


        private FileType _fileType = FileType.None;
        public FileType FileType { get => _fileType; set => _fileType = value; }

        public string FileTypeString => NamingLists.GetAbbreviation(FileType) ?? string.Empty;
        public Brush IndicatorColor => (Brush)new BrushConverter().ConvertFromString(ColorUtil.ExtensionToHex(Extension.ToLower()));

        #endregion
        public File(string path)
        {
            Path = path;
            Task.Run(() => GuessProperties());
        }

        private async void GuessProperties()
        {
            bool IsoFormatApplied = IsISOFormatApplied();
            bool nonIsoFormatApplied = NonIsoFormatApplied();

            if (IsoFormatApplied)
                try
                {
                    Guesser.GuessFromExistingIsoName(this);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            else if (nonIsoFormatApplied)
                try
                {
                    Guesser.GuessFromExistingNonIsoName(this);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            else
            {
                Guesser.GuessFromScratch(this);
            }
        }
        public bool NonIsoFormatApplied()
        {
            // check file name for the following format :
            // something _ 2 to 6 char _ optional 1 to 2 char _ 6 char _ revision 2 to 6 char (optional) _ something else (description)
            // 1103_SK_A_240516_Wst06Credit
            string pattern = @".*_[a-zA-Z0-9]{2,6}_[a-zA-Z0-9]{0,2}_\d{6}_.*";
            var regex = new Regex(pattern);

            return regex.IsMatch(Name);
        }

        public bool IsISOFormatApplied()
        {
            // check file name for the following format : 
            string pattern = @"\*_AAM_[a-zA-Z0-9]{2}_[a-zA-Z0-9]{2}_[a-zA-Z0-9]{2}_[a-zA-Z0-9]{2}_\d{5}_.*";
            var regex = new Regex(pattern);
            return regex.IsMatch(Name);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Rename(string newName)
        {
        }

        public void Reset()
        {
        }

        public void Save(bool iso)
        {
            string nameToSave = iso ? NewNameIso : NewNameNonIso;
            Logger.Info($"Saving file name, old name {Name}, new name : {nameToSave}, path : {Path}");
        }

        public void UpdateValue(Type type, object value)
        {
            var prop = GetPropertyFromType(type);
            UpdateValue(prop, value);
        }

        public void UpdateValue(string propName, object value)
        {
            var prop = GetPropertyFromName(propName);
            UpdateValue(prop, value);
        }

        public void UpdateValue(PropertyInfo prop, object value)
        {
            if (prop == null)
                return;
            // convert the value to the correct type
            var convertedValue = Convert.ChangeType(value, prop.PropertyType);
            prop.SetValue(this, convertedValue);
            OnPropertyChanged(prop.Name);
            if (prop.PropertyType != typeof(string))
            {
                try
                {
                    OnPropertyChanged(prop.Name + "String");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            OnPropertyChanged(nameof(NewNameIso));
            OnPropertyChanged(nameof(NewNameNonIso));
        }

        private PropertyInfo GetPropertyFromType(Type type)
        {
            return GetType().GetProperties().FirstOrDefault(p => p.PropertyType == type);
        }

        private PropertyInfo GetPropertyFromName(string name)
        {
            return GetType().GetProperty(name);
        }

        internal void EnsureUniqueness(ICollection<File> selectedFiles, bool iso)
        {
            if (iso)
            {
                EnsureIsoUniqueness(selectedFiles);
            }
            else
            {
                EnsureNonIsoUniqueness(selectedFiles);
            }
        }

        private void EnsureNonIsoUniqueness(ICollection<File> selectedFiles)
        {
            var sameRoot = selectedFiles.Where(f => f.NewNameNonIso == NewNameNonIso);
            if (sameRoot == null || sameRoot.Count() < 2)
                return;

            sameRoot = sameRoot.Where(f => f != this);
            Description = FindNextAvailableDescription(sameRoot);
            OnPropertyChanged(nameof(Description));
        }

        private string FindNextAvailableDescription(IEnumerable<File> sameRoot)
        {
            var descriptions = sameRoot.Select(f => f.DescriptionNaked).ToList();
            var max = sameRoot.Max(f => f.DescriptionNumber);
            return $"{DescriptionNaked} ({max + 1})";
        }

        private void EnsureIsoUniqueness(ICollection<File> selectedFiles)
        {
            var sameRoot = selectedFiles.Where(f => f.NewNameIso == NewNameIso);
            if (sameRoot == null || sameRoot.Count() < 2)
                return;
            sameRoot = sameRoot.Where(f => f != this);
            DrawingSerialNo = FindNextAvailableSerial(sameRoot);
            OnPropertyChanged(nameof(DrawingSerialNo));
        }

        private string FindNextAvailableSerial(IEnumerable<File> sameRoot)
        {
            var serials = sameRoot.Select(f => f.DrawingSerialNoInt).ToList();
            var max = serials.Max() + 1;
            var zeros = 4 - max.ToString().Length;
            if (zeros > 0)
                return max.ToString().PadLeft(zeros, '0');

            return max.ToString();
        }
    }
}
