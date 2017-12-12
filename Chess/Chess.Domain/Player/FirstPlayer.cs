using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Domain
{
	public class FirstPlayer : Player
	{
		public FirstPlayer(IPlayerMediator mediator)
			: base(mediator)
		{

		}

		protected override void AfterMove()
		{
			_mediator.GiveNextTurn(this);
		}
	}
}
