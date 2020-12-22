using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using SKeys.Core;

namespace LiveSplit.LiveKeys
{
    class LiveKeysComponent : IComponent
    {
        private readonly LiveKeysFactory Factory;
        private readonly LiveKeysSettings Settings;

        public Hooks Hooks { get; }
        private Input Input;

        private string text;

        public LiveKeysComponent(LiveSplitState state, LiveKeysFactory f)
        {
            Factory = f;

            Settings = new LiveKeysSettings()
            {
                ComponentHeight = 50f,
                ComponentWidth = 100f,
                Version = f.Version,
            };

            Input = new Input();
            Input.OnKeysChanged += UpdateText;

            Hooks = new Hooks();
            Hooks.OnKeyDown += Input.OnButtonDown;
            Hooks.OnKeyUp += Input.OnButtonUp;
            Hooks.OnMouseDown += Input.OnMouseDown;
            Hooks.OnMouseUp += Input.OnMouseUp;
            Hooks.OnMouseScroll += Input.OnMouseScroll;
            Hooks.OnMouseMove += Input.OnMouseMove;

            //Hooks.EnableHooks();
        }


        void UpdateText(object sender, ChangeEventArgs e)
        {
            text = string.Empty;
            foreach (string s in e.ActiveButtons)
            {
                text += (s + " ");
            }
            text += "\n";
            foreach (string s in e.ScrollCount.Keys)
            {
                if (e.ScrollCount[s] != 0)
                {
                    text += (s + " " + e.ScrollCount[s] + " ");
                }
            }
        }

        private Font AdjustedFont(Graphics g, string s, Font f, SizeF cs, float maxFontSize, float minFontSize)
        {
            for (float AdjustedSize = maxFontSize; AdjustedSize >= minFontSize; AdjustedSize--)
            {
                var newFont = new Font(f.Name, AdjustedSize, f.Style);
                var newSize = g.MeasureString(s, newFont);
                if (cs.Width > newSize.Width && cs.Height > newSize.Height)
                {
                    return newFont;
                }
            }
            return f;
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

        //private void Draw(Graphics g, LiveSplitState state, float width, float height, Region clipRegion)
        //{
        //    var size = new SizeF(width, height);
        //    var font = state.LayoutSettings.TextFont;
        //    var color = state.LayoutSettings.TextColor;

        //    font = AdjustedFont(g, text, font, size, font.SizeInPoints, 5);
        //    g.DrawString(text, font, new SolidBrush(color), new RectangleF(new PointF(0, 0), size), new StringFormat());
        //}

        private void Draw(Graphics g, LiveSplitState state, float width, float height, Region clipRegion)
        {
            var size = new SizeF(width, height);
            var font = state.LayoutSettings.TextFont;
            var color = state.LayoutSettings.TextColor;

            font = AdjustedFont(g, text, font, size, font.SizeInPoints, 5);

            SimpleLabel label = new SimpleLabel();

            g.DrawString(text, font, new SolidBrush(color), new RectangleF(new PointF(0, 0), size), new StringFormat());
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
