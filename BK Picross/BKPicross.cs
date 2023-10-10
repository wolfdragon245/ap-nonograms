using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
        Texture2D connectButton;
        Texture2D cursor;
        Vector2 touchPos;
        int clickY;
        int clickX;
        int row;
        int puzzleSize;
        double lastClick;
        Color color;
        ArchipelagoSession session;
        LoginResult result;
        bool hintSent;
        TextInput address = new TextInput(0, 64, "archipelago.gg:", 15);
        TextInput slotName = new TextInput(64, 0, "", 0);
        TextInput password = new TextInput(64, 32, "", 0);

        public BKPicross()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 496;
            _graphics.PreferredBackBufferHeight = 544;
            //_graphics.PreferredBackBufferWidth = 736;
            //_graphics.PreferredBackBufferHeight = 784;
            _graphics.ApplyChanges();
            Board localboard = new Board();
            touchPos = new Vector2();
            hintSent = false;
            puzzleSize = 1;

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
            connectButton = Content.Load<Texture2D>("connect");
            cursor = Content.Load<Texture2D>("cursor");
            address.loadFont(Content.Load<SpriteFont>("File"));
            address.loadTexture(Content.Load<Texture2D>("port"));
            slotName.loadFont(Content.Load<SpriteFont>("File"));
            slotName.loadTexture(Content.Load<Texture2D>("slot"));
            password.loadFont(Content.Load<SpriteFont>("File"));
            password.loadTexture(Content.Load<Texture2D>("password")); 
        }

        protected override void Update(GameTime gameTime)
        {
            color = Color.White;
            if (result != null)
            {
                if (!result.Successful)
                {
                    color = Color.Red;
                }
            }
            inputHandle(gameTime);
            if (Board.checkBoard())
            {
                color = Color.Lime;
                Board.clearFlag();
                if (!hintSent && result != null)
                {
                    sendHint();
                    hintSent = true;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(color);

            _spriteBatch.Begin();
            for (int k = 0; k < Board.board.GetLength(0); k++)
            {
                for ( int i = 0; i < Board.board.GetLength(1); i++)
                {
                    _spriteBatch.Draw((Board.board[k,i]) ? fill : empty, new Vector2(160+(32*i), 160+(32*k)), Color.White);
                }
            }
            for (int k = 0; k < Board.flags.GetLength(0); k++)
            {
                for (int i = 0; i < Board.flags.GetLength(1); i++)
                {
                    if (Board.flags[k, i])
                    {
                        _spriteBatch.Draw(mark, new Vector2(160 + (32 * i), 160 + (32 * k)), Color.White);
                    }
                }
            }
            verticalNums();
            horizontalNums();
            _spriteBatch.Draw(newGameButton, new Vector2(0,0), Color.White);
            if (result == null || !result.Successful)
            {
                    _spriteBatch.Draw(connectButton, new Vector2(0, 32), Color.White);
            }
            address.drawText(_spriteBatch, cursor);
            slotName.drawText(_spriteBatch, cursor);
            password.drawText(_spriteBatch, cursor);
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

        public void inputHandle(GameTime gameTime)
        {
            touchPos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            clickX = (int)(touchPos.X - 160) / 32;
            clickY = (int)(touchPos.Y - 160) / 32;

            if (Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString()) - lastClick > 125)
            {
                if (Mouse.GetState().LeftButton.ToString().Equals("Pressed"))
                {
                    if (clickY >= 0 && clickY <= Board.board.GetLength(0)-1 && clickX >= 0 && clickX <= Board.board.GetLength(1) - 1 && !Board.flags[clickY, clickX])
                    {
                        Board.board[clickY, clickX] = !Board.board[clickY, clickX];
                        lastClick = Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                    }
                    if (touchPos.Y >= 0 && touchPos.Y <= 32 && touchPos.X >= 0 && touchPos.X <= 64)
                    {
                        Board.clearBoard();
                        Board.randomizeSolution();
                        hintSent = false;

                        lastClick = Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                    }
                    if (result == null || !result.Successful)
                    {
                        if (touchPos.Y >= 32 && touchPos.Y <= 64 && touchPos.X >= 0 && touchPos.X <= 64)
                        {
                            session = ArchipelagoSessionFactory.CreateSession(address.getText());
                            result = session.TryConnectAndLogin("", slotName.getText(), ItemsHandlingFlags.NoItems, new Version(0, 4, 3),
                                    tags: new[] { "BK_Picross", "TextOnly" }, requestSlotData: false, password: password.getText());

                            lastClick = Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                        }
                    }
                    address.boxClicked(touchPos);
                    slotName.boxClicked(touchPos);
                    password.boxClicked(touchPos);
                    
                }
                if (Mouse.GetState().RightButton.ToString().Equals("Pressed"))
                {
                    if (clickY >= 0 && clickY <= Board.flags.GetLength(0) - 1 && clickX >= 0 && clickX <= Board.flags.GetLength(0) - 1 && !Board.board[clickY, clickX])
                    {
                        Board.flags[clickY, clickX] = !Board.flags[clickY, clickX];
                        lastClick = Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                    }
                }
                lastClick = address.boxType(gameTime, lastClick);
                lastClick = slotName.boxType(gameTime, lastClick);
                lastClick = password.boxType(gameTime, lastClick);
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
                    writeNums(hints[i], new Vector2(128-(32*i),160+(32*k)));
                }
                hints.Clear();
            }
        }

        public void horizontalNums()
        {
            row = 0;
            List<int> hints = new List<int>();
            for (int k = Board.boardSolution.GetLength(1)-1; k >= 0; k--)
            {
                for (int i = Board.boardSolution.GetLength(0)-1; i >= 0; i--)
                {
                    if (Board.boardSolution[i, k])
                    {
                        row++;
                    } else if (row>0)
                    {
                        hints.Add(row);
                        row = 0;
                    }
                    if (i == 0 && Board.boardSolution[i, k])
                    {
                        hints.Add(row);
                        row = 0;
                    }
                    if (i == 0 && hints.Count <= 0)
                    {
                        hints.Add(0);
                    }
                }
                for (int i = 0;i < hints.Count;i++)
                {
                    writeNums(hints[i], new Vector2(160 + (32 * k), 128-(32 * i)));
                }
                hints.Clear();
            }
        }

        public void sendHint()
        {
            var missing = session.Locations.AllMissingLocations;
            var alreadyHinted = session.DataStorage.GetHints()
                .Where(h => h.FindingPlayer == session.ConnectionInfo.Slot)
                .Select(h => h.LocationId);

            var availableForHinting = missing.Except(alreadyHinted).ToArray();

            if (availableForHinting.Any())
            {
                var locationId = availableForHinting[Board.random.Next(0, availableForHinting.Length)];

                session.Locations.ScoutLocationsAsync(true, locationId);
            }
        }
    }
}