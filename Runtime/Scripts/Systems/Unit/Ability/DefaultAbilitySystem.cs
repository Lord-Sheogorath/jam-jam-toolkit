using System.Collections.Generic;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class DefaultAbilitySystem : IAbilitySystem
	{
		private readonly UnitController _unit;
		private readonly List<IAbilityTemplate> _templates = new();

		private readonly List<AbilityContext> _active = new();
		
		public DefaultAbilitySystem(UnitController unit)
		{
			_unit = unit;
		}
		
		public AbilityContext Start(int id, int level)
		{
			if (TryGetTemplate(id, out var template) == false)
			{
				Debug.LogError($"Failed to find ability template for '{id}'");
				return null;
			}

			// TO-DO: Check cooldowns
			
			var context = new AbilityContext()
			{
				instanceId = System.Guid.NewGuid().GetHashCode(),
				startTime = Time.time,
				
				ability = template.Create(),
			};

			context.ability.Setup(_unit, level);
			context.ability.Start();
			
			_active.Add(context);
			
			return context;
		}

		public void Tick(float deltaTime)
		{
			for (var i = _active.Count - 1; i >= 0; i--)
			{
				var context = _active[i];
				context.ability.Tick(deltaTime);
				
				if (context.ability.IsActive == false)
				{
					context.ability.Stop();
					_active.RemoveAt(i);
				}
			}
		}
		
		public void Stop(int instanceId)
		{
			var index = _active.FindIndex(c => c.instanceId == instanceId);

			if (index == -1)
			{
				return;
			}

			var context = _active[index];
			context.ability.Stop();
			_active.RemoveAt(index);
		}

		public void Add(IAbilityTemplate template)
		{
			_templates.Add(template);
		}

		public void Remove(IAbilityTemplate template)
		{
			_templates.Remove(template);
		}

		public bool TryGetTemplate(int id, out IAbilityTemplate template)
		{
			var index = _templates.FindIndex(t => t.Id == id);

			if (index == -1)
			{
				template = null;
				return false;
			}

			template = _templates[index];
			return true;
		}
	}
}