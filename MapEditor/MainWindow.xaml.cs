using MapEditor.Data;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MapEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();

            AuthorTools();
        }

        #region Public View Properties
        private string _ImagePath;
        public string ImagePath { get => _ImagePath; set => SetField(ref _ImagePath, value); }
        #endregion

        #region Data Binding
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        protected bool SetField<type>(ref type field, type value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<type>.Default.Equals(field, value)) return false;
            field = value;
            NotifyPropertyChanged(propertyName);
            return true;
        }
        #endregion

        #region Commands
        private void OpenCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
            => e.CanExecute = true;

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = Directory.Exists("Tools") ? System.IO.Path.GetFullPath("Tools") : Directory.GetCurrentDirectory();
                openFileDialog.Filter = "Map Editor tools (*.tool)|*.tool|All files (*.*)|*.*";
                if(openFileDialog.ShowDialog() == true)
                {
                    string yaml = File.ReadAllText(openFileDialog.FileName);
                    var options = (new YamlDotNet.Serialization.Deserializer()).Deserialize<Options>(yaml);
                    ApplyOptions(options);
                }
            }
            else
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
                openFileDialog.Filter = "Map files (*.map)|*.map|All files (*.*)|*.*";
                throw new NotImplementedException();
            }
        }

        private void ApplyOptions(Options options)
        {
            if(options.BackgroundColor != null)
            {
                Background = (Brush)new BrushConverter().ConvertFromString(options.BackgroundColor);
            }
        }
        private void AuthorTools()
        {
            if(!Directory.Exists("Tools"))
            {
                // Define internal tools
                Dictionary<string, Options> tools = new Dictionary<string, Options>()
                {
                    ["BlackBackground.Tool"] = new Options() { BackgroundColor = Colors.Black.ToString() }
                };

                // Save to files
                Directory.CreateDirectory("Tools");
                foreach (var tool in tools)
                {
                    WriteTool(System.IO.Path.Combine("Tools", tool.Key), tool.Value);
                }
            }
        }
        #endregion

        #region Helpers
        private void WriteTool(string filePath, Options options)
        {
            File.WriteAllText(filePath, new YamlDotNet.Serialization.Serializer().Serialize(options));
        }
        private Color ColorToMedia(System.Drawing.Color color)
        {
            return System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
        }
        private System.Drawing.Color MediaToColor(Color color)
        {
            return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        }
        #endregion
    }
}
