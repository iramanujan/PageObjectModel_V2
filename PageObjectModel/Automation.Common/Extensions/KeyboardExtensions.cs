using TestStack.White.InputDevices;
using TestStack.White.WindowsAPI;

namespace Automation.Common.Extensions
{
    public static class KeyboardExtensions
    {
        public static void PressShortcutKeys(this IKeyboard keyboard, KeyboardInput.SpecialKeys[] specialKeys, params string[] keys)
        {
            foreach (var key in specialKeys)
            {
                keyboard.HoldKey(key);
            }
            foreach (var key in keys)
            {
                keyboard.Enter(key);
            }
            foreach (var key in specialKeys)
            {
                keyboard.LeaveKey(key);
            }
        }

        public static void PressShortcutKeys(this IKeyboard keyboard, KeyboardInput.SpecialKeys specialKey, params string[] keys)
        {
            keyboard.PressShortcutKeys(new[] { specialKey }, keys);
        }
    }
}
