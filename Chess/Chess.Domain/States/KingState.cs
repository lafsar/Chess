using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	public abstract class KingState
	{
		private King _kingPiece;
		public King KingPiece
		{
			get { return _kingPiece; }
			set { _kingPiece = value;  }
		}

	}
}
