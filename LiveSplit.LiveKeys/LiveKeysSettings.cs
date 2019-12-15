using System;
using System.Windows.Forms;
using LiveSplit.UI;
using System.Xml;

namespace LiveSplit.LiveKeys
{
    public partial class LiveKeysSettings : UserControl
    {
        private LiveKeysFactory Factory;
        public event EventHandler OnSettingsChanged;

        public LayoutMode Mode { get; set; }

        public float ComponentWidth { get; set; }

        public LiveKeysSettings(LiveKeysFactory f)
        {
            Factory = f;

            InitializeComponent();

            ComponentWidth = 100f;

            widthUpDown.DataBindings.Add(nameof(widthUpDown.Value), this, nameof(ComponentWidth), true, DataSourceUpdateMode.OnPropertyChanged).BindingComplete += OnSettingChanged;
        }

        private void OnSettingChanged(object sender, BindingCompleteEventArgs e)
        {
            OnSettingsChanged?.Invoke(this, e);
        }

        public void SetSettings(XmlNode settings)
        {
            ComponentWidth = SettingsHelper.ParseFloat(settings[nameof(ComponentWidth)]);
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            var parent = document.CreateElement("Settings");
            SettingsHelper.CreateSetting(document, parent, nameof(Factory.Version), Factory.Version);
            SettingsHelper.CreateSetting(document, parent, nameof(ComponentWidth), ComponentWidth);
            return parent;
        }
    }
}
