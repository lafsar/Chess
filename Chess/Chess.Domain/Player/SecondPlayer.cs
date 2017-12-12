using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	public class SecondPlayer : Player
	{
		public SecondPlayer(IPlayerMediator mediator)
			: base(mediator)
		{}

		protected override void AfterMove()
		{
			_mediator.GiveNextTurn(this);
		}
	}
}
