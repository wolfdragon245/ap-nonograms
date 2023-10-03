using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BK_Picross
{
    public class BKPicross : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D fill;
        Texture2D empty;
        Texture2D mark;
        Texture2D nine;
        Texture2D eight;
        Texture2D seven;
        Texture2D six;
        Texture2D five;
        Texture2D four;
        Texture2D three;
        Texture2D two;
        Texture2D one;
        Texture2D zero;
        Texture2D newGameButton;
        Vector2 touchPos;
        int ClickY;
        int ClickX;
        int row;
        long lastClick;

        public BKPicross()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 640;
            _graphics.PreferredBackBufferHeight = 513;
            Board localboard = new Board();
            touchPos = new Vector2();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            empty = Content.Load<Texture2D>("empty");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(empty, new Vector2(0, 0), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}