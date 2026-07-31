using UnityEngine;

namespace LordSheo.JJTK
{
	public class UnitControllerLogger : MonoBehaviour
	{
		private UnitController _unit;
		
		private void Awake()
		{
			_unit = GetComponent<UnitController>();
		}

		private void Start()
		{
			var statSystem = _unit.GetSystem<IStatSystem>();
			statSystem.OnAddedEvent += (type, amount) => Log($"[Unit/Stat] Added, {amount.ToDisplayString()}, {type}");
			statSystem.OnRemovedEvent += (type, amount) => Log($"[Unit/Stat] Removed, {amount.ToDisplayString()}, {type}");
			statSystem.OnChangedEvent += (type, amount) => Log($"[Unit/Stat] Changed, {amount.ToDisplayString()}, {type}");

			var inventorySystem = _unit.GetSystem<IInventorySystem>();
			inventorySystem.OnAddedEvent += (type, amount) => Log($"[Unit/Inventory] Added, {amount}, {type}");
			inventorySystem.OnRemovedEvent += (type, amount) => Log($"[Unit/Inventory] Removed, {amount}, {type}");
			inventorySystem.OnChangedEvent += (type, amount) => Log($"[Unit/Inventory] Changed, {amount}, {type}");

			var healthSystem = _unit.GetSystem<IHealthSystem>();
			healthSystem.OnHealedEvent += (amount) => Log($"[Unit/Health] Healed, {amount.ToDisplayString()}");
			healthSystem.OnDamagedEvent += (amount) => Log($"[Unit/Health] Damaged, {amount.ToDisplayString()}");
			healthSystem.OnChangedEvent += () => Log($"[Unit/Health] Changed");
			healthSystem.OnDeathEvent += () => Log($"[Unit/Health] Death");

			var combatSystem = _unit.GetSystem<ICombatSystem>();
			combatSystem.OnChangedEvent += (prev, current) => Log($"[Unit/Combat] Changed, {prev}, {current}");
		}

		private void Log(string message)
		{
			Debug.Log(message, this);
		}
	}
}