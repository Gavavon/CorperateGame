using UnityEngine;
using System.Globalization;
using TMPro;

namespace InfimaGames.LowPolyShooterPack.Interface
{
	/// <summary>
	/// Current Grenades Text.
	/// </summary>
	public class TextAmmoReserves : ElementText
	{
		#region FIELDS SERIALIZED

		[Title(label: "Colors")]

		[Tooltip("Determines if the color of the text should changes as grenades are thrown.")]
		[SerializeField]
		private bool updateColor = true;

		[Tooltip("Determines how fast the color changes as the grenade are thrown.")]
		[SerializeField]
		private float emptySpeed = 1.5f;

		[Tooltip("Color used on this text when the player character has no grendes.")]
		[SerializeField]
		private Color emptyColor = Color.red;

		[Title(label: "Reference")]

		[Tooltip("Infinity Image reference")]
		[SerializeField]
		private GameObject infinityImage;

		#endregion

		#region METHODS

		/// <summary>
		/// Tick.
		/// </summary>
		protected override void Tick()
		{
			//change to an infinte sign if the gun has infinite ammo
			if (equippedWeaponBehaviour.GetAmmoIsInfinite())
			{
				textMesh.enabled = false;
				infinityImage.SetActive(true);
				return;
			}
			else 
			{
				textMesh.enabled = true;
				infinityImage.SetActive(false);
			}

			//Current.
			float current = equippedWeaponBehaviour.GetAmmoReservesCurrent();
			//Total.
			float total = equippedWeaponBehaviour.GetAmmoReservesTotal();

			//Update Text.
			textMesh.text = current.ToString(CultureInfo.InvariantCulture);

			//Determine if we should update the text's color.
			if (updateColor)
			{
				//Calculate Color Alpha. Helpful to make the text color change based on grenade count.
				float colorAlpha = (current / total) * emptySpeed;
				//Lerp Color. This makes sure that the text color changes based on grenade count.
				textMesh.color = Color.Lerp(emptyColor, Color.white, colorAlpha);
			}
		}

		#endregion
	}
}
