using System.Collections.Generic;

using Terraria.Graphics.Effects;

namespace OdeMod.ShaderDatas.ScreenShaderDatas
{
    internal class OdeScreenShaderDataManager
    {
        private Dictionary<string, OdeScreenShaderData> _datas;

        public OdeScreenShaderData this[string name]
        {
            get
            {
                if (_datas.ContainsKey(name))
                    return _datas[name];
                else
                    return null;
            }
        }

        public OdeScreenShaderDataManager()
        {
            _datas = new Dictionary<string, OdeScreenShaderData>();
        }

        public void Update()
        {
            foreach (var data in _datas)
            {
                data.Value.OnUpdate();
                if (data.Value.Visible && !Filters.Scene[data.Key].IsActive())
                {
                    // 开启滤镜
                    Filters.Scene.Activate(data.Key);
                }
                else if (!data.Value.Visible && Filters.Scene[data.Key].IsActive())
                {
                    Filters.Scene.Deactivate(data.Key);
                }
            }
        }

        public void Register(string name, OdeScreenShaderData data, EffectPriority effectPriority = EffectPriority.VeryLow)
        {
            if (_datas.ContainsKey(name))
                return;
            Filters.Scene[name] = new Filter(data, effectPriority);
            Filters.Scene[name].Load();
            _datas.Add(name, data);
            data.OnRegister();
        }

        public void Remove(string name)
        {
            if (!_datas.ContainsKey(name))
                return;
            _datas[name].OnRemove();
            _datas.Remove(name);
            Filters.Scene[name] = null;
        }
    }
}