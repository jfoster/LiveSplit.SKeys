using System;
using System.Windows.Forms;
using LiveSplit.UI;
using System.Xml;

namespace LiveSplit.LiveKeys
{
    public partial class LiveKeysSettings : UserControl
    {
        public event EventHandler OnSettingsChanged;

        public LayoutMode Mode { get; set; }
        public Version Version { get; set; }

        public float ComponentHeight { get; set; }
        public float ComponentWidth { get; set; }

        public LiveKeysSettings()
        {
            InitializeComponent();
        }

        private void OnSettingChanged(object sender, BindingCompleteEventArgs e)
        {
            OnSettingsChanged?.Invoke(this, e);
        }

        public void SetSettings(XmlNode settings)
        {
            Version = SettingsHelper.ParseVersion(settings[nameof(Version)]);
            ComponentWidth = SettingsHelper.ParseFloat(settings[nameof(ComponentWidth)]);
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            var parent = document.CreateElement("Settings");
            SettingsHelper.CreateSetting(document, parent, nameof(Version), Version);
            SettingsHelper.CreateSetting(document, parent, nameof(ComponentWidth), ComponentWidth);
            return parent;
        }

        private void LiveKeysSettings_Load(object sender, EventArgs e)
        {
            if (Mode == LayoutMode.Vertical)
            {
                sizeUpDown.DataBindings.Clear();
                sizeUpDown.DataBindings.Add(nameof(sizeUpDown.Value), this, nameof(ComponentHeight), true, DataSourceUpdateMode.OnPropertyChanged).BindingComplete += OnSettingChanged;
                sizeLabel.Text = "Height";
            }
            else
            {
                sizeUpDown.DataBindings.Clear();
                sizeUpDown.DataBindings.Add(nameof(sizeUpDown.Value), this, nameof(ComponentWidth), true, DataSourceUpdateMode.OnPropertyChanged).BindingComplete += OnSettingChanged;
                sizeLabel.Text = "Width";
            }
        }
    }
}
