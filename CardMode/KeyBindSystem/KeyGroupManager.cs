using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace OdeMod.CardMode.KeyBindSystem
{
    internal class KeyGroupManager
    {
        private Dictionary<string, KeyGroup> _keyGroups;

        public KeyGroupManager()
        {
            _keyGroups = new Dictionary<string, KeyGroup>();
        }

        public bool RegisterKeyGroup(KeyGroup keyGroup)
        {
            if (keyGroup == null || _keyGroups.ContainsKey(keyGroup.Name))
                return false;
            _keyGroups.Add(keyGroup.Name, keyGroup);
            return true;
        }

        public KeyGroup GetKeyGroup(string name)
        {
            if (!_keyGroups.ContainsKey(name))
                return null;
            return _keyGroups[name];
        }

        public void Update(GameTime gt)
        {
            foreach (var kg in _keyGroups.Values)
            {
                kg.Update(gt);
            }
        }
    }
}