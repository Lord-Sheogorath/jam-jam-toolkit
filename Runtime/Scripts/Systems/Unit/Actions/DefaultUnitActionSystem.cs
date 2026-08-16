namespace LordSheo.JJTK
{
	public class DefaultUnitActionSystem : IUnitActionSystem,
		ITickable
	{
		private IUnitAction _currentAction;
		private readonly UnitController _unit;

		public DefaultUnitActionSystem(UnitController unit)
		{
			_unit = unit;
		}
		
		public IUnitAction GetActive()
		{
			return _currentAction;
		}

		public void Start(IUnitAction action)
		{
			Stop();

			if (action == null)
			{
				return;
			}
			
			if (action.IsValid(_unit) == false)
			{
				return;
			}
			
			_currentAction = action;
			
			if (_currentAction != null)
			{
				_currentAction.Start(_unit);
			}
		}
		
		public void Tick(float deltaTime)
		{
			if (_currentAction != null)
			{
				_currentAction.Tick(deltaTime);
			}
		}
		
		public void Stop()
		{
			if (_currentAction != null)
			{
				_currentAction.Stop();
			}
			
			_currentAction = null;
		}
	}
}