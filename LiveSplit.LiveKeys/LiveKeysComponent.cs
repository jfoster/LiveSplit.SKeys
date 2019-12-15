using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using SKeys9;

namespace LiveSplit.LiveKeys
{
    class LiveKeysComponent : IComponent
    {
        private LiveKeysFactory Factory;
        private LiveKeysSettings Settings;
        private InfoTextComponent TextComponent;

        private Hooks Hooks;
        private Input Input;

        public LiveKeysComponent(LiveSplitState state, LiveKeysFactory f)
        {
            Factory = f;
            Settings = new LiveKeysSettings(f);

            TextComponent = new InfoTextComponent("", "");
            TextComponent.NameLabel.HorizontalAlignment = StringAlignment.Near;
            TextComponent.NameLabel.ForeColor = state.LayoutSettings.TextColor;
            TextComponent.ValueLabel.HorizontalAlignment = StringAlignment.Near;
            TextComponent.ValueLabel.ForeColor = state.LayoutSettings.TextColor;

            AddHooks();
        }

        void AddHooks()
        {
            Hooks = new Hooks();
            Input = new Input();

            Input.OnKeysChanged += UpdateText;

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
            string buttontext = string.Empty;
            foreach (string s in e.ActiveButtons)
            {
                buttontext += (s + " ");
            }
            TextComponent.InformationName = buttontext;

            string scrolltext = string.Empty;
            foreach (string s in e.ScrollCount.Keys)
            {
                if (e.ScrollCount[s] != 0)
                {
                    scrolltext += (s + " " + e.ScrollCount[s] + " ");
                }
            }
            TextComponent.InformationValue = scrolltext;
        }

        public string ComponentName => Factory.ComponentName;

        public float PaddingTop => TextComponent.PaddingTop;

        public float PaddingBottom => TextComponent.PaddingBottom;

        public float PaddingLeft => TextComponent.PaddingLeft;

        public float PaddingRight => TextComponent.PaddingRight;

        public float HorizontalWidth => Settings.ComponentWidth != 0 ? Settings.ComponentWidth : 256f;

        public float MinimumHeight => 0f;

        public float VerticalHeight => TextComponent.VerticalHeight;

        public float MinimumWidth => 0f;

        public IDictionary<string, Action> ContextMenuControls => null;

        private void PrepareDraw(LiveSplitState state, LayoutMode mode)
        {
            TextComponent.PrepareDraw(state, mode);
        }

        public void DrawHorizontal(Graphics g, LiveSplitState state, float height, Region clipRegion)
        {
            PrepareDraw(state, LayoutMode.Horizontal);
            TextComponent.DrawHorizontal(g, state, height, clipRegion);
        }

        public void DrawVertical(Graphics g, LiveSplitState state, float width, Region clipRegion)
        {
            PrepareDraw(state, LayoutMode.Vertical);
            TextComponent.DrawVertical(g, state, width, clipRegion);
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
            TextComponent.LongestString = TextComponent.InformationName;

            TextComponent.Update(invalidator, state, width, height, mode);
        }

        public void Dispose()
        {
            TextComponent.Dispose();
        }
    }
}
