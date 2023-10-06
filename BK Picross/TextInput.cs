using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace BK_Picross
{
    internal class TextInput
    {
        int xCord;
        int yCord;
        string text;
        int cursorPos;
        bool selected;
        SpriteFont font;
        Rectangle backRect;

        public TextInput(int x, int y, string startText, int cursorStart)
        {
            xCord = x;
            yCord = y;
            text = startText;
            cursorPos = cursorStart;
            backRect = new Rectangle();
        }

        public void loadFont(SpriteFont importFont)
        {
            font = importFont;
        }

        public string getText()
        {
            return text;
        }

        public void drawBox(SpriteBatch batch)
        {
            Vector2 pos;
            backRect.X = xCord;
            backRect.Y = yCord;
            backRect.Height = 32;
            backRect.Width = (int)font.MeasureString(text).X;
            pos = new Vector2(xCord, yCord);
            batch.DrawString(font, text, pos, new Microsoft.Xna.Framework.Color(0,0,0));
        }

        public void charEntered(char c)
        {
            string newText = text.Insert(cursorPos, c.ToString());
            text = newText;
            cursorPos++;
        }

        public void boxClicked(Vector2 touchPos)
        {
            if (touchPos.Y >= backRect.Top && touchPos.Y <= backRect.Bottom && touchPos.X >= backRect.Left && touchPos.X <= backRect.Right)
            {
                selected = true;
            } else
            {
                selected = false;
            }
        }

        public double boxType(GameTime gameTime, double lastClick)
        {
            if (selected)
            {
                KeyboardState state = Keyboard.GetState();
                if (state.IsKeyDown(Keys.A))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? 'A' : 'a'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.B))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? 'B' : 'b'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.C))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? 'C' : 'c'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.D))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? 'D' : 'd'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.E))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? 'E' : 'e'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.F))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? 'F' : 'f'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.G))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? 'G' : 'g'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.H))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? 'H' : 'h'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.I))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? 'I' : 'i'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.J))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? 'J' : 'j'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.K))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? 'K' : 'k'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.L))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? 'L' : 'l'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.M))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? 'M' : 'm'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.N))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? 'N' : 'n'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.O))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? 'O' : 'o'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.P))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? 'P' : 'p'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.Q))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? 'Q' : 'q'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.R))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? 'R' : 'r'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.S))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? 'S' : 's'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.T))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? 'T' : 't'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.U))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? 'U' : 'u'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.V))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? 'V' : 'v'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.W))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? 'W' : 'w'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.X))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? 'X' : 'x'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.Y))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? 'Y' : 'y'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.Z))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? 'Z' : 'z'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.NumPad0))
                {
                    charEntered('0');
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.NumPad1))
                {
                    charEntered('1');
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.NumPad2))
                {
                    charEntered('2');
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.NumPad3))
                {
                    charEntered('3');
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.NumPad4))
                {
                    charEntered('4');
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.NumPad5))
                {
                    charEntered('5');
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.NumPad6))
                {
                    charEntered('6');
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.NumPad7))
                {
                    charEntered('7');
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.NumPad8))
                {
                    charEntered('8');
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.NumPad9))
                {
                    charEntered('9');
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
            }
            return lastClick;
        }
    }
}
