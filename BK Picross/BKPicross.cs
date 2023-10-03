using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;

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
            _graphics.ApplyChanges();
            Board localboard = new Board();
            touchPos = new Vector2();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            fill = Content.Load<Texture2D>("filled");
            empty = Content.Load<Texture2D>("empty");
            mark = Content.Load<Texture2D>("mark");
            nine = Content.Load<Texture2D>("9");
            eight = Content.Load<Texture2D>("8");
            seven = Content.Load<Texture2D>("7");
            six = Content.Load<Texture2D>("6");
            five = Content.Load<Texture2D>("5");
            four = Content.Load<Texture2D>("4");
            three = Content.Load<Texture2D>("3");
            two = Content.Load<Texture2D>("2");
            one = Content.Load<Texture2D>("1");
            zero = Content.Load<Texture2D>("0");
            newGameButton = Content.Load<Texture2D>("new_game_button");

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
            for (int k = 0; k < Board.board.GetLength(0); k++)
            {
                for ( int i = 0; i < Board.board.GetLength(1); i++)
                {
                    _spriteBatch.Draw(empty, new Vector2(160+(32*i), 160+(32*k)), Color.White);
                }
            }
            verticalNums();
            _spriteBatch.Draw(newGameButton, new Vector2(500,220), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void writeNums(int num, Vector2 cords)
        {
            switch(num)
            {
                case 0:
                    _spriteBatch.Draw(zero, cords, Color.White);
                    break;
                case 1:
                    _spriteBatch.Draw(one, cords, Color.White);
                    break;
                case 2:
                    _spriteBatch.Draw(two, cords, Color.White);
                    break;
                case 3:
                    _spriteBatch.Draw(three, cords, Color.White);
                    break;
                case 4:
                    _spriteBatch.Draw(four, cords, Color.White);
                    break;
                case 5:
                    _spriteBatch.Draw(five, cords, Color.White);
                    break;
                case 6:
                    _spriteBatch.Draw(six, cords, Color.White);
                    break;
                case 7:
                    _spriteBatch.Draw(seven, cords, Color.White);
                    break;
                case 8:
                    _spriteBatch.Draw(eight, cords, Color.White);
                    break;
                case 9:
                    _spriteBatch.Draw(nine, cords, Color.White);
                    break;
                case 10:
                    _spriteBatch.Draw(zero, cords, Color.White);
                    _spriteBatch.Draw(one, new Vector2(cords.X-16,cords.Y), Color.White);
                    break;
            }
        }

        public void verticalNums()
        {
            row = 0;
            List<int> hints = new List<int>();
            for (int k = 0; k < Board.boardSolution.GetLength(0); k++)
            {
                for (int i = Board.boardSolution.GetLength(1)-1; i >= 0; i--)
                {
                    if (Board.boardSolution[k,i])
                    {
                        row++;
                    } else if (row > 0)
                    {
                        hints.Add(row);
                        row = 0;
                    }
                    if (i==0 && Board.boardSolution[k,i])
                    {
                        hints.Add(row);
                        row = 0;
                    }
                    if (i==0 && hints.Count <= 0)
                    {
                        hints.Add(0);
                    }
                }
                for (int i = 0; i < hints.Count; i++)
                {
                    writeNums(hints[i], new Vector2(128-(32*i),448-(32*k)));
                }
                hints.Clear();
            }
        }
    }
}