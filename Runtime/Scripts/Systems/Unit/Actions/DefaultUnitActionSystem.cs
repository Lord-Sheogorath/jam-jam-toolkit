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

		public void SetActive(IUnitAction action)
		{
			StopActive();

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
				_currentAction.OnEnter(_unit);
			}
		}

		public void StopActive()
		{
			if (_currentAction != null)
			{
				_currentAction.OnExit();
			}
			
			_currentAction = null;
		}

		public void Tick(float deltaTime)
		{
			if (_currentAction != null)
			{
				_currentAction.OnUpdate();
			}
		}
	}
}