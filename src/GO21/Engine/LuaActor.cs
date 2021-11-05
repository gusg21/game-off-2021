using System;
using System.Linq;
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
            State["EI"] = Engine.Instance;
            State.DoString(script);
        }

        public LuaActor(LuaPipeline.LuaScript script) : this(script.script) { }

        public object CallFunction(string name, params object[] parameters)
        {
            LuaFunction function = State[name] as LuaFunction;

            if (function == null)
            {
                Console.WriteLine($"Failed to call function {name}.");
                return null;
            }

            object[] results = function.Call(parameters);

            if (results.Count() > 0)
                return results[0];
            else
                return null;
        }

        public override void BeforeUpdate()
        {
            CallFunction("BeforeUpdate");

            base.BeforeUpdate();
        }

        public override void Update()
        {
            CallFunction("Update");

            base.Update();
        }

        public override void AfterUpdate()
        {
            CallFunction("AfterUpdate");

            base.AfterUpdate();
        }

        public override void BeforeDraw()
        {
            CallFunction("BeforeDraw");

            base.BeforeDraw();
        }

        public override void Draw()
        {
            CallFunction("Draw");

            base.Draw();
        }

        public override void AfterDraw()
        {
            CallFunction("AfterDraw");

            base.AfterDraw();
        }

        public override void OnAdded(ActorList list)
        {
            CallFunction("OnAdded", list);

            base.OnAdded(list);
        }

        public override void SceneBegin()
        {
            CallFunction("SceneBegin");

            base.SceneBegin();
        }

        public override void SceneEnd()
        {
            CallFunction("SceneEnd");

            base.SceneEnd();
        }
    }
}
