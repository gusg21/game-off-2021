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
        public static GraphicsDeviceManager Graphics;
        /// <summary>
        /// The Drawing subsystem for textures and primitives.
        /// </summary>
        public static Drawing Drawing;
        /// <summary>
        /// The Camera to render the game through.
        /// </summary>
        public static Camera Camera;

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
        /// How wide the internal "screen" should be when rendered.
        /// </summary>
        public static int ScreenWidth
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
        public static int ScreenHeight
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
        public static int WindowWidth
        {
            get
            {
                return Instance.GraphicsDevice.PresentationParameters.BackBufferWidth;
            }
        }
        /// <summary>
        /// Get the height of the window.
        /// </summary>
        public static int WindowHeight
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
        /// <summary>
        /// Total count of frames 
        /// </summary>
        public static int Frames { get; private set; }

        // == Content ==

        /// <summary>
        /// The directory that the executable is located in.
        /// </summary>
        private static string AssemblyDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        /// <summary>
        /// The directory that the Content for the game is located in.
        /// </summary>
        public static string ContentDirectory
        {
            get { return Path.Combine(AssemblyDirectory, Instance.Content.RootDirectory); }
        }

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

            // Camera
            Camera = new Camera();

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

            GraphicsDevice.SetRenderTarget(null); // render to window
            GraphicsDevice.Clear(ClearColor);

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

        #region Shortcuts

        /// <summary>
        /// A shortcut to Engine.Instance.Content.LoadContent<T>().
        /// </summary>
        /// <typeparam name="T">The type of asset to load.</typeparam>
        /// <param name="assetName">The name of the asset (no extension) to load.</param>
        /// <returns>The loaded asset.</returns>
        public static T Load<T>(string assetName)
        {
            return Instance.Content.Load<T>(assetName);
        }

        /// <summary>
        /// A shortcut for <see cref="Load{T}(string)"/> for specifically Texture2Ds.
        /// </summary>
        /// <param name="texName">The name of the Texture2D to load (no extension).</param>
        /// <returns>The Texture2D.</returns>
        public static Texture2D LoadTex(string texName)
        {
            return Load<Texture2D>(texName);
        }

        #endregion
    }
}
