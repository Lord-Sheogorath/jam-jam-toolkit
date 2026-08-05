using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LordSheo.JJTK
{
	public class DefaultHealthDisplayUI : MonoBehaviour, 
		IHealthDisplayUI
	{
		public TextMeshProUGUI amountText;
		public Image fillImage;
		
		public IHealthSystem Current { get; protected set; }
        
		public virtual void Show(IHealthSystem health)
		{
			if (Current != null)
			{
				Current.OnChangedEvent -= Refresh;
			}

			Current = health;
			Current.OnChangedEvent += Refresh;

			Refresh();
		}

		public virtual void Refresh()
		{
			amountText.text = $"{Current.Current} / {Current.Max}";
			fillImage.fillAmount = (float)Current.Current / (float)Current.Max;
		}
	}
}