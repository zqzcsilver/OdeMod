using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OdeMod.CardMode.Scenes.ConfigScene.ConfigSystem
{
    internal class ConfigManager
    {
        public static readonly string SavePath = Path.Combine(CardSystem.SavePath, "Configs");
        private Dictionary<string, ConfigBase> configs;

        public ConfigManager()
        {
            configs = new Dictionary<string, ConfigBase>();
        }

        public List<ConfigBase> GetAllConfigs() => configs.Values.ToList();

        public void LoadConfigs()
        {
            var containers = from c in GetType().Assembly.GetTypes()
                             where !c.IsAbstract && typeof(ConfigBase).IsAssignableFrom(c)
                             select c;
            ConfigBase config;
            foreach (var c in containers)
            {
                if (!configs.ContainsKey(c.FullName))
                {
                    config = (ConfigBase)Activator.CreateInstance(c);
                    config.Init();
                    config.LoadConfig();
                    configs.Add(c.FullName, config);
                }
            }
        }

        public void SaveConfigs()
        {
            if (!Directory.Exists(SavePath))
                Directory.CreateDirectory(SavePath);

            foreach (var config in configs.Values)
                config.SaveConfig();
        }

        public T GetConfig<T>() where T : ConfigBase
        {
            foreach (var config in configs.Values)
            {
                if (config is T)
                    return (T)config;
            }

            throw new ArgumentException($"{typeof(T).FullName} not found!");
        }

        public ConfigBase GetConfig(string name)
        {
            if (configs.ContainsKey(name))
                return configs[name];
            throw new ArgumentException($"{name} not found!");
        }
    }
}