using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoColorGame
{
    class colorObject
    {
        Texture2D _tx;
        Color _myColor;
        bool _visible = true;
        int _id;

        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        public Color MyColor
        {
            get { return _myColor; }
            set { _myColor = value; }
        }
        Vector2 _position;
        bool _chosen = false;

        public bool Chosen
        {
            get { return _chosen; }
            set { _chosen = value; }
        }
        public Rectangle Target
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, _tx.Width, _tx.Height); }
       }
        public Vector2 CenterPos {
            get { return Position + new Vector2(_tx.Width, _tx.Width) / 2; }
            }
        public int Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return _position;
            }

            set
            {
                _position = value;
            }
        }

        SpriteFont _font;

        public colorObject(SpriteFont Font, Vector2 pos, Texture2D tx, Color c, int cid)
        {
            _id = cid;
            _font = Font;
            Position = pos;
            _tx = tx;
            _myColor = c;
        }
        public void update(GameTime t) 
        {
            MouseState m = Mouse.GetState();
            if (m.LeftButton == ButtonState.Pressed)
                if (Target.Contains(new Point(m.X, m.Y)))
                    _chosen = true;
        }

        public void Draw(SpriteBatch sp)
        {
            sp.Begin();
            sp.Draw(_tx, Target, _myColor);
            if (_id != 0)
            {
                Vector2 IdSize = _font.MeasureString(_id.ToString());
                sp.DrawString(_font, _id.ToString(), Position + new Vector2(_tx.Width, _tx.Width) / 2 - IdSize / 2, Color.White);
            }
            sp.End();
        }
    }
}
