using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsixprop
{
    struct TextViewSelection
    {
        public TextViewPosition StartPosition { get; set; }
        public TextViewPosition EndPosition { get; set; }
        public string Text { get; set; }
		//是否是选中文字
		public bool IsSelection { get; set; }

		public TextViewSelection(TextViewPosition a, TextViewPosition b, string text,bool isSelect=true)
        {
            StartPosition = TextViewPosition.Min(a, b);
            EndPosition = TextViewPosition.Max(a, b);
            Text = text;
			IsSelection = isSelect;
        }
    }


    public struct TextViewPosition
    {
        private readonly int _column;
        private readonly int _line;
		private readonly int _positon;

        public TextViewPosition(int line, int column,int position)
        {
            _line = line;
            _column = column;
			_positon = position;


		}

        public int Line { get { return _line; } }
        public int Column { get { return _column; } }
		public int Posion { get { return _positon; } }





		public static bool operator <(TextViewPosition a, TextViewPosition b)
        {
            if (a.Line < b.Line)
            {
                return true;
            }
            else if (a.Line == b.Line)
            {
                return a.Column < b.Column;
            }
            else
            {
                return false;
            }
        }

        public static bool operator >(TextViewPosition a, TextViewPosition b)
        {
            if (a.Line > b.Line)
            {
                return true;
            }
            else if (a.Line == b.Line)
            {
                return a.Column > b.Column;
            }
            else
            {
                return false;
            }
        }

        public static TextViewPosition Min(TextViewPosition a, TextViewPosition b)
        {
            return a > b ? b : a;
        }

        public static TextViewPosition Max(TextViewPosition a, TextViewPosition b)
        {
            return a > b ? a : b;
        }
    }
}
