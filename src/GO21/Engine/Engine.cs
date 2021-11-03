using System;
using System.Runtime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
    /// <summary>
    /// The Engine class represents the core of the game. You load custom scenes
    /// into it to create your game.
    /// </summary>
    public class Engine : Game
    {
        // == References ==

        /// <summary>
        /// Singleton object.
        /// </summary>
        public static Engine Instance;
        /// <summary>
        /// The GraphicsDeviceManager for the game.
        /// </summary>
        public static GraphicsDeviceManager Graphics;

        // == Window ==

        /// <summary>
        /// The internal width of the game.
        /// </summary>
        public static int Width;
        /// <summary>
        /// The internal height of the game.
        /// </summary>
        public static int Height;
        /// <summary>
        /// The window title of the game.
        /// </summary>
        public static string Title;
        /// <summary>
        /// The amount to scale the internal window by.
        /// </summary>
        public static int Scale;
        /// <summary>
        /// How wide the window should be. Dynamically calculated based on <see cref="Width"/> and <see cref="Scale"/>.
        /// </summary>
        public static int WindowWidth
        {
            get
            {
                if (Graphics.IsFullScreen)
                {
                    return GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                }
                else
                {
                    return Width * Scale;
                }
            }
        }
        /// <summary>
        /// How tall the window should be. Dynamically calculated based on <see cref="Height"/> and <see cref="Scale"/>.
        /// </summary>
        public static int WindowHeight
        {
            get
            {
                if (Graphics.IsFullScreen)
                {
                    return GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                }
                else
                {
                    return Height * Scale;
                }
            }
        }

        // == Time ==

        /// <summary>
        /// The time since the last frame, including a time scaling feature.
        /// </summary>
        public static float DeltaTime { get; private set; }
        /// <summary>
        /// The time since the last frame WITHOUT the time scaling.
        /// </summary>
        public static float RawDeltaTime { get; private set; }
        /// <summary>
        /// The amount to speed up/slow down time.
        /// </summary>
        public static float TimeRate = 1f;
        /// <summary>
        /// If this value is greater than zero, the game will pause and count down this timer.
        /// </summary>
        public static float FreezeTimer;

        // == Util ==

        /// <summary>
        /// The color to clear the screen with.
        /// </summary>
        public static Color ClearColor;
        /// <summary>
        /// Kill the game on ESC?
        /// </summary>
        public static bool ExitOnEscape;

        // == Scene ==

        // The current scene.
        private static Scene _scene;
        // The scene to change to at the end of the frame.
        private static Scene _nextScene;
        public static Scene Scene
        {
            get
            {
                return _scene;
            }
            set
            {
                _nextScene = value;
            }
        }

        /// <summary>
        /// Create a new Engine.
        /// NOTE: You can create only one Engine without undefined
        /// behavior.
        /// </summary>
        /// <param name="width">The internal width of the game. <see cref="Width"/></param>
        /// <param name="height">The internal height of the game. <see cref="Height"/></param>
        /// <param name="title">The window title of the game. <see cref="Title"/></param>
        public Engine(int width, int height, int scale, string title)
        {
            // Set up singleton
            Instance = this;

            // Apply parameters
            Width = width;
            Height = height;
            Scale = scale;
            Title = Window.Title = title;

            // Graphics device setup
            Graphics = new GraphicsDeviceManager(this);
            {
                Graphics.PreferredBackBufferWidth = WindowWidth;
                Graphics.PreferredBackBufferHeight = WindowHeight;

                Graphics.SynchronizeWithVerticalRetrace = true;
                Graphics.PreferMultiSampling = false;
                Graphics.GraphicsProfile = GraphicsProfile.HiDef;
                Graphics.PreferredBackBufferFormat = SurfaceFormat.Color;
                Graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;
            }
            Graphics.ApplyChanges();

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnClientSizeChanged;

            // Flags
            IsMouseVisible = false;
            IsFixedTimeStep = false;
            ExitOnEscape = true;

            // GC
            GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
        }

        // Called when the window is resized.
        protected void OnClientSizeChanged(object sender, EventArgs e)
        {
            Graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            Graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
        }

        /// <summary>
        /// Called every frame.
        /// </summary>
        /// <param name="gameTime">Information about the game's timing.</param>
        protected override void Update(GameTime gameTime)
        {
            // Update DeltaTimes
            RawDeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            DeltaTime = RawDeltaTime * TimeRate;

            // Freezing
            if (FreezeTimer > 0)
            {
                FreezeTimer = Math.Max(FreezeTimer - RawDeltaTime, 0);
            }
            else if (_scene != null)
            {
                // Update scene
                _scene.BeforeUpdate();
                _scene.Update();
                _scene.AfterUpdate();
            }

            // Switch scenes out if needed
            if (_scene != _nextScene)
            {
                _scene.SceneEnd();
                _scene = _nextScene;

                GC.Collect();
                GC.WaitForPendingFinalizers();

                _scene.SceneBegin();
            }

            base.Update(gameTime);
        }
    }
}
