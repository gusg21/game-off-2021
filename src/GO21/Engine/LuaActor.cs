using System;
using NLua;

namespace GO21Engine
{
    public class LuaActor : Actor
    {
        /// <summary>
        /// The Lua state for this scripted actor.
        /// </summary>
        public Lua State { get; private set; }

        public LuaActor(string script)
        {

            State = new Lua();
            State.LoadCLRPackage();
            //State["E"] = Engine.Instance;
            State.DoString(script);
        }

        public LuaActor(LuaPipeline.LuaScript script) : this(script.script) { }

        public object CallFunction(string name, params object[] parameters)
        {
            LuaFunction function = State[name] as LuaFunction;
            return function.Call(parameters)[0];
        }

        public object TryCallFunction(string name, params object[] parameters)
        {
            try
            {
                return CallFunction(name, parameters);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override void BeforeUpdate()
        {
            TryCallFunction("BeforeUpdate");

            base.BeforeUpdate();
        }

        public override void Update()
        {
            TryCallFunction("Update");

            base.Update();
        }

        public override void AfterUpdate()
        {
            TryCallFunction("AfterUpdate");

            base.AfterUpdate();
        }

        public override void BeforeDraw()
        {
            TryCallFunction("BeforeDraw");

            base.BeforeDraw();
        }

        public override void Draw()
        {
            TryCallFunction("Draw");

            base.Draw();
        }

        public override void AfterDraw()
        {
            TryCallFunction("AfterDraw");

            base.AfterDraw();
        }

        public override void OnAdded(ActorList list)
        {
            TryCallFunction("OnAdded", list);

            base.OnAdded(list);
        }

        public override void SceneBegin()
        {
            TryCallFunction("SceneBegin");

            base.SceneBegin();
        }

        public override void SceneEnd()
        {
            TryCallFunction("SceneEnd");

            base.SceneEnd();
        }
    }
}
