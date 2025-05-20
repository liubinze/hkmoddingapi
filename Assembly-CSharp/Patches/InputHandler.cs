using MonoMod;
using UnityEngine;

#pragma warning disable 1591
#pragma warning disable CS0649

namespace Modding.Patches
{
    [MonoModPatch("global::InputHandler")]
    public class InputHandler : global::InputHandler
    {
        [MonoModIgnore]
        private bool isTitleScreenScene;

        [MonoModIgnore]
        private bool isMenuScene;

        [MonoModIgnore]
        private bool controllerPressed;

        [MonoModIgnore]
        private GameManager gm;

		[MonoModIgnore]
		private extern void SetCursorVisible(bool value);
        // Reverted cursor behavior
        [MonoModReplace]
	public void OnGUI()
	{
		Cursor.lockState = CursorLockMode.None;
		if (this.isTitleScreenScene)
		{
			this.SetCursorVisible(false);
			return;
		}
		if (!this.isMenuScene)
		{
			ModHooks.OnCursor(gm, this.SetCursorVisible, this.controllerPressed);
			return;
		}
		this.SetCursorVisible(!this.controllerPressed);
	}
    }
}
