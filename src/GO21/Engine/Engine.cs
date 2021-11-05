using System;
using System.IO;
using System.Reflection;
using System.Runtime;
using GO21Engine.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GO21Engine
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
        public GraphicsDeviceManager Graphics;
        /// <summary>
        /// The Drawing subsystem for textures and primitives.
        /// </summary>
        public Drawing Drawing;
        /// <summary>
        /// The Camera to render the game through.
        /// </summary>
        public Camera Camera;

        // == Window ==

        /// <summary>
        /// The internal width of the game.
        /// </summary>
        public int Width;
        /// <summary>
        /// The internal height of the game.
        /// </summary>
        public int Height;
        /// <summary>
        /// The window title of the game.
        /// </summary>
        public string Title;
        /// <summary>
        /// The amount to scale the internal window by.
        /// </summary>
        public int Scale;
        /// <summary>
        /// How wide the internal "screen" should be when rendered.
        /// </summary>
        public int ScreenWidth
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
        /// How tall the internal "screen" should be when rendered.
        /// </summary>
        public int ScreenHeight
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
        /// <summary>
        /// Get the actual width of the window.
        /// </summary>
        public int WindowWidth
        {
            get
            {
                return Instance.GraphicsDevice.PresentationParameters.BackBufferWidth;
            }
        }
        /// <summary>
        /// Get the height of the window.
        /// </summary>
        public int WindowHeight
        {
            get
            {
                return Instance.GraphicsDevice.PresentationParameters.BackBufferHeight;
            }
        }

        // == Time ==

        /// <summary>
        /// The time since the last frame, including a time scaling feature.
        /// </summary>
        public float DeltaTime { get; private set; }
        /// <summary>
        /// The time since the last frame WITHOUT the time scaling.
        /// </summary>
        public float RawDeltaTime { get; private set; }
        /// <summary>
        /// The amount to speed up/slow down time.
        /// </summary>
        public float TimeRate = 1f;
        /// <summary>
        /// If this value is greater than zero, the game will pause and count down this timer.
        /// </summary>
        public float FreezeTimer;
        /// <summary>
        /// Total count of frames 
        /// </summary>
        public int Frames { get; private set; }

        // == Content ==

        /// <summary>
        /// The directory that the executable is located in.
        /// </summary>
        private string AssemblyDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        /// <summary>
        /// The directory that the Content for the game is located in.
        /// </summary>
        public string ContentDirectory
        {
            get { return Path.Combine(AssemblyDirectory, Instance.Content.RootDirectory); }
        }

        // == Util ==

        /// <summary>
        /// The color to clear the screen with.
        /// </summary>
        public Color ClearColor;
        /// <summary>
        /// Kill the game on ESC?
        /// </summary>
        public static bool ExitOnEscape;

        // == Scene ==

        // The current scene.
        private Scene _scene;
        // The scene to change to at the end of the frame.
        private Scene _nextScene;
        public Scene Scene
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

            // Camera
            Camera = new Camera(Width, Height);
            Camera.CenterOrigin();

            // Content
            Content.RootDirectory = "Content";

            // Graphics
            Graphics = new GraphicsDeviceManager(this);
            {
                Graphics.SynchronizeWithVerticalRetrace = true;
                Graphics.PreferMultiSampling = false;
                Graphics.GraphicsProfile = GraphicsProfile.HiDef;
                Graphics.PreferredBackBufferFormat = SurfaceFormat.Color;
                Graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;
            }
            Graphics.ApplyChanges();

            Console.WriteLine("w " + ScreenWidth + " h " + ScreenHeight);
            Graphics.IsFullScreen = false;
            Graphics.PreferredBackBufferWidth = ScreenWidth;
            Graphics.PreferredBackBufferHeight = ScreenHeight;
            Graphics.ApplyChanges();

            // Window
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += OnClientSizeChanged;

            // Flags
            IsMouseVisible = false;
            IsFixedTimeStep = false;
            ExitOnEscape = true;

            // GC
            GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
        }

        #region Callbacks

        // Called when the window is resized.
        protected void OnClientSizeChanged(object sender, EventArgs e)
        {
            Graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            Graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
        }

        #endregion

        #region Game Loop

        protected override void Initialize()
        {
            // Drawing
            Drawing = new Drawing(GraphicsDevice, Width, Height);
            Drawing.Initialize();

            base.Initialize();
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
                if (_scene != null)
                    _scene.SceneEnd();
                _scene = _nextScene;

                GC.Collect();
                GC.WaitForPendingFinalizers();

                _scene.SceneBegin();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draw the engine. Called every frame.
        /// </summary>
        /// <param name="gameTime">The game's timing information.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (_scene != null)
                _scene.BeforeDraw();

            Drawing.Begin(Camera.Matrix);

            if (_scene != null)
            {
                _scene.Draw();
                _scene.AfterDraw();
            }

            Drawing.End();

#if DEBUG
            Window.Title = Title + " " + Math.Floor(1.0f / gameTime.ElapsedGameTime.TotalSeconds) + " avg fps - " + (GC.GetTotalMemory(false) / 1048576f).ToString("F") + " MB";
#endif

            Frames++;

            base.Draw(gameTime);
        }

        #endregion

        #region Content

        /// <summary>
        /// A shortcut to Engine.Instance.Content.LoadContent<T>().
        /// </summary>
        /// <typeparam name="T">The type of asset to load.</typeparam>
        /// <param name="assetName">The name of the asset (no extension) to load.</param>
        /// <returns>The loaded asset.</returns>
        public T Load<T>(string assetName)
        {
            return Instance.Content.Load<T>(assetName);
        }

        /// <summary>
        /// A shortcut for <see cref="Load{T}(string)"/> for specifically Texture2Ds.
        /// </summary>
        /// <param name="texName">The name of the Texture2D to load (no extension).</param>
        /// <returns>The Texture2D.</returns>
        public Texture2D LoadTex(string texName)
        {
            return Load<Texture2D>(texName);
        }

        /// <summary>
        /// Load a lua acript from a given file.
        /// </summary>
        /// <param name="scriptName">The path to the lua script (no extension).</param>
        /// <returns>The LuaScript structure that contains the lua code.</returns>
        public LuaPipeline.LuaScript LoadLua(string scriptName)
        {
            return Instance.Content.Load<LuaPipeline.LuaScript>(scriptName);
        }

        #endregion
    }
}
