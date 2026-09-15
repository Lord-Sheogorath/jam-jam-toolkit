namespace LordSheo.JJTK
{
	public class UnitStatSystemConnector
	{
		private readonly UnitController _unit;
		
		public UnitStatSystemConnector(UnitController unit)
		{
			_unit = unit;
		}

		public void Initialise()
		{
			_unit.StatSystem.OnChangedEvent -= OnStatChangedCallback;
			_unit.StatSystem.OnChangedEvent += OnStatChangedCallback;
		}

		private void OnStatChangedCallback(StatType type, ChangedFloatValue change)
		{
			if (type == StatType.max_health)
			{
				_unit.HealthSystem.SetMax((int)change.current);
			}
		}
	}
}