using System;
using LiveSplit.Model;
using LiveSplit.LiveKeys;
using LiveSplit.UI.Components;

[assembly: ComponentFactory(typeof(LiveKeysFactory))]

namespace LiveSplit.LiveKeys
{
    public class LiveKeysFactory : IComponentFactory
    {
        public string ComponentName => "Live Keys";

        public string Description => "Input visualiser component for LiveSplit";

        public ComponentCategory Category => ComponentCategory.Information;

        public string UpdateName => "";

        public string XMLURL => "";

        public string UpdateURL => "";

        public Version Version => Version.Parse("0.0.1");

        public IComponent Create(LiveSplitState state) => new LiveKeysComponent(state, this);
    }
}
