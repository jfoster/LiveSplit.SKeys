using System;
using GitInfo;
using LiveSplit.SKeys;
using LiveSplit.Model;
using LiveSplit.UI.Components;

[assembly: ComponentFactory(typeof(SKeysFactory))]

namespace LiveSplit.SKeys
{
    public class SKeysFactory : IComponentFactory
    {
        public string ComponentName => "SKeys";

        public string Description => "SKeys component for LiveSplit";

        public ComponentCategory Category => ComponentCategory.Information;

        public string UpdateName => "";

        public string XMLURL => "";

        public string UpdateURL => "";

        public Version Version => GitVersion.Short.ToVersion();

        public IComponent Create(LiveSplitState state) => new SKeysComponent(state, this);
    }
}
