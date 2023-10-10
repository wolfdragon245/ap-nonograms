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
        Texture2D button;

        public TextInput(int x, int y, string startText, int cursorStart)
        {
            xCord = x;
            yCord = y;
            text = startText;
            cursorPos = cursorStart;
        }

        public void loadFont(SpriteFont importFont)
        {
            font = importFont;
        }

        public void loadTexture(Texture2D import)
        {
            button = import;
        }

        public string getText()
        {
            return text;
        }

        public void drawText(SpriteBatch batch, Texture2D cursorTex)
        {
            var pos = new Vector2(xCord, yCord);
            var pos2 = new Vector2(5,496);
            var po3 = new Vector2(5+font.MeasureString(text).X, 496);
            batch.Draw(button, pos, Color.White);
            if (selected)
            {
                batch.DrawString(font, text, pos2, new Microsoft.Xna.Framework.Color(0, 0, 0));
                batch.Draw(cursorTex, po3, Color.White);
            }
        }

        public void charEntered(char c)
        {
            string newText = text.Insert(cursorPos, c.ToString());
            text = newText;
            cursorPos++;
        }

        public void boxClicked(Vector2 touchPos)
        {
            if (touchPos.Y >= yCord && touchPos.Y <= yCord+32 && touchPos.X >= xCord && touchPos.X <= xCord+64)
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
                if (state.IsKeyDown(Keys.D0))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? ')' : '0'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.D1))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? '!' : '1'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.D2))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? '@' : '2'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.D3))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? '#' : '3'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.D4))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? '$' : '4'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.D5))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? '%' : '5'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.D6))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? '^' : '6'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.D7))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? '&' : '7'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.D8))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? '*' : '8'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.D9))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? '(' : '9'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.Space))
                {
                    charEntered(' ');
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.Back))
                {
                    if (cursorPos > 0)
                    {
                        text = text.Remove(text.Length - 1, 1);
                        cursorPos--;
                        return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                    }
                }
                if (state.IsKeyDown(Keys.Delete)){
                    text = "";
                    cursorPos = 0;
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.OemPeriod))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? '>' : '.'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.OemComma))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? '<' : ','));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.OemQuestion))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? '?' : '/'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.OemSemicolon))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? ':' : ';'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.OemQuotes))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? '"' : '\''));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.OemOpenBrackets))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? '{' : '['));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.OemCloseBrackets))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? '}' : ']'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.OemPipe))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? '|' : '\\'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.OemMinus))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? '_' : '-'));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.OemPlus))
                {
                    charEntered((state.IsKeyDown(Keys.LeftShift) ? '+' : '='));
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
                if (state.IsKeyDown(Keys.Enter))
                {
                    selected = false;
                    return Convert.ToDouble(gameTime.TotalGameTime.TotalMilliseconds.ToString());
                }
            }
            return lastClick;
        }
    }
}
