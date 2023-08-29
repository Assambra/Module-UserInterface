using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helper
{
	public static class AnchorPresets
	{
		private static bool presetTopLeft = false;
		private static bool presetTopRight = false;
		private static bool presetBottomRight = false;
		private static bool presetBottomLeft = false;

		public static void SetPresetTopLeft(RectTransform window)
		{
			presetTopLeft = true;

			window.anchorMin = new Vector2(0, 1);
			window.anchorMax = new Vector2(0, 1);
			window.pivot = new Vector2(0, 1);

			window.anchoredPosition += new Vector2(-window.sizeDelta.x / 2, window.sizeDelta.y / 2);
		}

		public static void SetPresetTopRight(RectTransform window)
		{
			presetTopRight = true;

			window.anchorMin = new Vector2(1, 1);
			window.anchorMax = new Vector2(1, 1);
			window.pivot = new Vector2(1, 1); ;
			window.anchoredPosition += new Vector2(window.sizeDelta.x / 2, window.sizeDelta.y / 2);
		}

		public static void SetPresetBottomRight(RectTransform window)
		{
			presetBottomRight = true;

			window.anchorMin = new Vector2(1, 0);
			window.anchorMax = new Vector2(1, 0);
			window.pivot = new Vector2(1, 0);
			window.anchoredPosition += new Vector2(window.sizeDelta.x / 2, -window.sizeDelta.y / 2);
		}

		public static void SetPresetBottomLeft(RectTransform window)
		{
			presetBottomLeft = true;

			window.anchorMin = new Vector2(0, 0);
			window.anchorMax = new Vector2(0, 0);
			window.pivot = new Vector2(0, 0);
			window.anchoredPosition += new Vector2(-window.sizeDelta.x / 2, -window.sizeDelta.y / 2);
		}

		public static void SetPresetMiddleCenter(RectTransform window)
		{
			window.anchorMin = new Vector2(0.5f, 0.5f);
			window.anchorMax = new Vector2(0.5f, 0.5f);
			window.pivot = new Vector2(0.5f, 0.5f);

			if (presetTopLeft)
			{
				window.anchoredPosition -= new Vector2(-window.sizeDelta.x / 2, window.sizeDelta.y / 2);
				presetTopLeft = false;
			}
			else if (presetTopRight)
			{
				window.anchoredPosition -= new Vector2(window.sizeDelta.x / 2, window.sizeDelta.y / 2);
				presetTopRight = false;
			}
			else if (presetBottomRight)
			{
				window.anchoredPosition -= new Vector2(window.sizeDelta.x / 2, -window.sizeDelta.y / 2);
				presetBottomRight = false;
			}
			else if (presetBottomLeft)
			{
				window.anchoredPosition -= new Vector2(-window.sizeDelta.x / 2, -window.sizeDelta.y / 2);
				presetBottomLeft = false;
			}
		}
	}
}

