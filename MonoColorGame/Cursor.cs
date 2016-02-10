using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoColorGame
{
    public class Cursor
    {
        private Texture2D _tx;
        private Vector2 _position;
        private Vector2 _origin;

        public Texture2D Tx
        {
            get
            {
                return _tx;
            }

            set
            {
                _tx = value;
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

        public Vector2 Origin
        {
            get
            {
                return new Vector2(_tx.Width,_tx.Height)/2;
            }

            set
            {
                _origin = value;
            }
        }

        public Cursor(Texture2D tx, Vector2 pos)
        {
            _tx = tx;
            _position = pos;
            _origin = Position + new Vector2(_tx.Width, _tx.Height) / 2;
        }

        public void Draw(SpriteBatch sp)
        {
            sp.Begin();
            //sp.Draw(_tx, _position, Color.White);
            sp.Draw(_tx, _position,null, Color.White,0f,Origin,1f,SpriteEffects.None,0f);
            sp.End();
        }

    }
}
