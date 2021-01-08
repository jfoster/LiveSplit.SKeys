using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using GraphicsExtensions;
using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using SKeys.Core;

namespace LiveSplit.SKeys
{
    class SKeysComponent : IComponent
    {
        private readonly SKeysFactory Factory;
        private readonly SKeysSettings Settings;

        public Hooks Hooks { get; }
        private Input Input;
        private SimpleLabel Label;

        public SKeysComponent(LiveSplitState state, SKeysFactory f)
        {
            Factory = f;

            Settings = new SKeysSettings()
            {
                ComponentHeight = 50f,
                ComponentWidth = 100f,
                Version = f.Version,
            };

            Label = new SimpleLabel();

            Input = new Input();
            Input.OnKeysChanged += UpdateText;

            Hooks = new Hooks();
            Hooks.OnKeyDown += Input.OnButtonDown;
            Hooks.OnKeyUp += Input.OnButtonUp;
            Hooks.OnMouseDown += Input.OnMouseDown;
            Hooks.OnMouseUp += Input.OnMouseUp;
            Hooks.OnMouseScroll += Input.OnMouseScroll;
            Hooks.OnMouseMove += Input.OnMouseMove;

            Hooks.EnableHooks();
        }


        void UpdateText(object sender, ChangeEventArgs e)
        {
            Label.Text = string.Empty;
            foreach (string s in e.ActiveButtons)
            {
                Label.Text += (s + " ");
            }
            Label.Text += "\n";
            foreach (string s in e.ScrollCount.Keys)
            {
                if (e.ScrollCount[s] != 0)
                {
                    Label.Text += (s + " " + e.ScrollCount[s] + " ");
                }
            }
        }

        public string ComponentName => Factory.ComponentName;

        public float PaddingTop => 8f;

        public float PaddingBottom => 8f;

        public float PaddingLeft => 8f;

        public float PaddingRight => 8f;

        public float HorizontalWidth => Settings.ComponentWidth;

        public float VerticalHeight => Settings.ComponentHeight;

        public float MinimumHeight => 0f;

        public float MinimumWidth => 0f;

        public IDictionary<string, Action> ContextMenuControls => null;

        public void DrawHorizontal(Graphics g, LiveSplitState state, float height, Region clipRegion)
        {
            Draw(g, state, HorizontalWidth, height, clipRegion);
        }

        public void DrawVertical(Graphics g, LiveSplitState state, float width, Region clipRegion)
        {
            Draw(g, state, width, VerticalHeight, clipRegion);
        }

        private void Draw(Graphics g, LiveSplitState state, float width, float height, Region clipRegion)
        {
            Label.Width = width;
            Label.Height = height;

            Label.ForeColor = state.LayoutSettings.TextColor;
            Label.ShadowColor = state.LayoutSettings.ShadowsColor;
            Label.OutlineColor = state.LayoutSettings.TextOutlineColor;
            Label.HasShadow = state.LayoutSettings.DropShadows;

            var font = state.LayoutSettings.TextFont;
            Label.Font = g.AdjustFontSize(font, 5, font.SizeInPoints, Label.Text, width, height);

            Label.Draw(g);
        }

        public Control GetSettingsControl(LayoutMode mode)
        {
            Settings.Mode = mode;
            return Settings;
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            return Settings.GetSettings(document);
        }

        public void SetSettings(XmlNode settings)
        {
            Settings.SetSettings(settings);
        }

        public void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            if (invalidator != null)
            {
                invalidator.Invalidate(0, 0, width, height);
            }
        }

        public void Dispose()
        {
        }
    }
}
