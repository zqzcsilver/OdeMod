using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace OdeMod.CardMode.Scenes
{
    internal class SceneManager
    {
        /// <summary>
        /// 当前场景
        /// </summary>
        public SceneBase Scene { get; private set; }

        /// <summary>
        /// 上一个场景
        /// </summary>
        public SceneBase LastScene { get; private set; }

        /// <summary>
        /// 所有场景
        /// </summary>
        private Dictionary<string, SceneBase> _scenes = new Dictionary<string, SceneBase>();

        public ChangeSceneStyleBase ChangeSceneStyle { get; private set; }
        public SceneBase this[string name] { get => _scenes[name]; }

        public void Load()
        {
            ClearAllScene();
            SceneBase scene;
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes().OrderBy((Type t) => t.FullName, StringComparer.InvariantCulture))
            {
                if (!type.IsAbstract && type.IsSubclassOf(typeof(SceneBase)))
                {
                    scene = (SceneBase)Activator.CreateInstance(type);
                    _scenes.Add(type.FullName, scene);
                }
            }
        }

        public void Init()
        {
            Load();
            foreach (var scene in _scenes.Values)
                scene.OnInit();
        }

        public void ChangeScene(SceneBase scene, ChangeSceneStyleBase changeSceneStyle)
        {
            scene?.BeSelected();
            Scene?.ExitSelected();

            LastScene = Scene;
            Scene = scene;
            ChangeSceneStyle = changeSceneStyle;
            if (ChangeSceneStyle == null)
                return;
            ChangeSceneStyle.SetScene(LastScene, Scene);
            ChangeSceneStyle.OnBegin();
        }

        public void ChangeScene(string sceneName, ChangeSceneStyleBase changeSceneStyle)
        {
            ChangeScene(this[sceneName], changeSceneStyle);
        }

        public void ChangeScene(SceneBase scene)
        {
            ChangeScene(scene, null);
        }

        public void ChangeScene(string sceneName)
        {
            ChangeScene(this[sceneName], null);
        }

        public void Draw(SpriteBatch sb)
        {
            if (Scene == null)
                return;
            if (ChangeSceneStyle == null)
            {
                Scene.Draw(sb);
            }
            else
            {
                if (ChangeSceneStyle.Draw(sb))
                    Scene.Draw(sb);
            }
        }

        public void Update(GameTime gt)
        {
            if (Scene == null)
                return;
            if (ChangeSceneStyle == null)
            {
                Scene.Update(gt);
            }
            else
            {
                if (!ChangeSceneStyle.Finish)
                {
                    ChangeSceneStyle.Update(gt);
                }
                else
                    ChangeSceneStyle = null;
            }
        }

        public void ClearAllScene()
        {
            Scene = null;
            foreach (var s in _scenes)
            {
                s.Value.UnLoad();
            }
            _scenes.Clear();
        }
    }
}