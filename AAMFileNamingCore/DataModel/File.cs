using AAMFileNamingCore.UI;
using AAMFileNamingCore.Util;
using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using MessageBox = System.Windows.MessageBox;

namespace AAMFileNamingCore.DataModel
{
    public class File : INotifyPropertyChanged
    {
        // project originator volume level doctype role typecode drawingserial
        // proj number _ file code _  document type _  date of issue _ rev _ descriptiion
        // volume / building	-	level	-	type	-	role	-	type code	drawing serial no.	_	short description (optional)

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private object CreateSeparator(string description, string sep)
        {
            if (string.IsNullOrEmpty(description))
                return "";

            return sep;
        }

        #region Properties

        public string Name => System.IO.Path.GetFileNameWithoutExtension(Path);
        public string Path { get; set; }
        public string User => Environment.UserName.Substring(0, 2).ToUpper();
        public string Extension => System.IO.Path.GetExtension(Path);
        public string NewNameIso => $"{ProjectNumber}-{Originator}-{Volume}-{LevelString}-{DocumentTypeIsoString}-{RoleString}-{TypeCodeString}{DrawingSerialNo}{CreateSeparator(Description, "_")}{Description}";
        public string NewNameNonIso => $"{ProjectNumber}_{FileCodeString}{CreateSeparator(DocumentTypeString, "_")}{DocumentTypeString}_{DateOfIssue}{CreateSeparator(Revision, "_")}{Revision}{CreateSeparator(Description, "_")}{Description}";
        public string NewNameForComments => Name.ToLower().Contains("comment") ? Name : $"{Name}_{User}ForComments_{DateOfIssue}";
        public string NewNameRootIso => $"{ProjectNumber}-{Originator}-{Volume}-{LevelString}-{DocumentTypeIsoString}-{RoleString}-{TypeCodeString}-{Description}";
        public string NewNameRootNonIso => $"{ProjectNumber}_{FileCodeString}_{DocumentTypeIsoString}_{DateOfIssue}_{Revision}_{Description}";
        public string NewNameRootForComments => $"{Name}_{User}ForComments_{DateOfIssue}";
        public string NewPathIso => System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Path), NewNameIso + Extension);
        public string NewPathNonIso => System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Path), NewNameNonIso + Extension);
        public string NewPathForComments => System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Path), NewNameForComments + Extension);

        //NewNameForComments

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
        public string LevelString => GetAbbreviation(typeof(Level)) ?? string.Empty;


        private DocumentTypeIso _documentTypeIso = DocumentTypeIso.None;
        public DocumentTypeIso DocumentTypeIso { get => _documentTypeIso; set => _documentTypeIso = value; }
        public string DocumentTypeIsoString => GetAbbreviation(typeof(DocumentTypeIso)) ?? string.Empty;
        

        private DocumentType _documentType = DocumentType.None;
        public DocumentType DocumentType { get => _documentType; set => _documentType = value; }
        public string DocumentTypeString => GetAbbreviation(typeof(DocumentType)) ?? string.Empty;


        private Role _role = Role.Architecture;
        public Role Role { get => _role; set => _role = value; }
        public string RoleString => GetAbbreviation(typeof(Role)) ?? string.Empty;


        private TypeCode _typeCode = TypeCode.None;
        public TypeCode TypeCode { get => _typeCode; set => _typeCode = value; }
        public string TypeCodeString => GetAbbreviation(typeof(TypeCode)) ?? string.Empty;

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
        public string FileCodeString => GetAbbreviation(typeof(FileCode)) ?? string.Empty;


        private FileType _fileType = FileType.None;
        public FileType FileType { get => _fileType; set => _fileType = value; }
        public string FileTypeString => GetAbbreviation(typeof(FileType)) ?? string.Empty;


        public Dictionary<Type, string> CustomProperties { get; set; } = new Dictionary<Type, string>();

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

        public bool Rename(NamingProtocol protocol)
        {
            string nameToSave = "";
            string pathToSave = ""; 

            if(protocol == NamingProtocol.Iso19650)
            {
                nameToSave = NewNameIso;
                pathToSave = NewPathIso;
            }
            else if(protocol == NamingProtocol.NonIso)
            {
                nameToSave = NewNameNonIso;
                pathToSave = NewPathNonIso;
            }
            else if (protocol == NamingProtocol.ForComments)
            {
                nameToSave = NewNameForComments;
                pathToSave = NewPathForComments;
            }

            // try to rename the file
            try
            {
                System.IO.File.Move(Path, pathToSave);
                Logger.Info($"File renamed from {Name} to {nameToSave} using protocol : {protocol.ToString()}");
                this.Path = pathToSave;
                OnPropertyChanged(nameof(Path));
                OnPropertyChanged(nameof(Name));
                return true;
            }

            catch (IOException ex) when (ex.HResult == -2146232800) // File already exists
            {
                // ask to rename to (1) or (2) or (3) etc
                Logger.Warn($"File already exists: {ex.Message}");
                var res = MessageBox.Show("The file exists. Do you want to add a timestamp to avoid overwriting the file?", nameToSave, MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (res == MessageBoxResult.OK)
                {
                    // add a timestamp
                    string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                    
                    Description = $"{Description}{timestamp}";
                    OnPropertyChanged(nameof(Description));

                    try
                    {
                        System.IO.File.Move(Path, pathToSave);
                        Logger.Info($"File renamed from {Name} to {nameToSave} using protocol :  {protocol.ToString()}");

                        OnPropertyChanged(nameof(Name));
                        OnPropertyChanged(nameof(Path));

                        return true;
                    }
                    catch (Exception e)
                    {
                        Logger.Error($"Error renaming file: {e.Message}");
                        MessageBox.Show($"Error renaming file: {e.Message}");
                        return false;
                    }
                }

            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show($"Unauthorized access: {ex.Message}");
                Logger.Error($"Unauthorized access: {ex.Message}");
                return false;
            }
            catch (PathTooLongException ex)
            {
                MessageBox.Show($"Path too long: {ex.Message}");
                Logger.Error($"Path too long: {ex.Message}");
                return false;
            }
            catch (DirectoryNotFoundException ex)
            {
                MessageBox.Show($"Directory not found: {ex.Message}");
                Logger.Error($"Directory not found: {ex.Message}");
                return false;
            }
            catch (NotSupportedException ex)
            {
                MessageBox.Show($"Not supported: {ex.Message}");
                Logger.Error($"Not supported: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                Logger.Error($"Error: {ex.Message}");
                return false;
            }

            return true;
        }

        public void UpdateValue(DataInput dataInput)
        {
            // if input is manual override, then 
            if (dataInput.manualOverride)
            {
                string s = dataInput?.SelectedItem as string;
                s = s?.Trim().ToUpper();
                SetCustomProperty(dataInput.inputType, s);

            }
            else
            {
                RemoveCustomProperty(dataInput.inputType);
                UpdateValue(dataInput.inputType, dataInput.SelectedItem);
            }
        }

        private void SetCustomProperty(Type inputType, string selectedItem)
        {
            if (CustomProperties.ContainsKey(inputType))
            {
                CustomProperties[inputType] = selectedItem;
            }
            else
            {
                CustomProperties.Add(inputType, selectedItem);
            }

            var prop = GetPropertyFromType(inputType);

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

        private void RemoveCustomProperty(Type type)
        {
            if (CustomProperties.ContainsKey(type))
            {
                CustomProperties.Remove(type);
            }
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

        private string? GetAbbreviation(Type type)
        {
            if(CustomProperties.ContainsKey(type))
            {
                return CustomProperties[type];
            }

            var prop = GetPropertyFromType(type);
            return NamingLists.GetAbbreviation(prop.GetValue(this)) ?? string.Empty;
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
