// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("a9laeWtWXVJx3RPdrFZaWlpeW1gSZtf5bD2i7CI7+ccCnDX/XE3XW935ebAoYypRR/4DC4SSj29NvCZQnuvURfRlTJ6j002d6WMmdKQhITydZIJpLfEzleV9cOTkSL4ZOb/oSXdrWqbMr2UZcLgAAhq/vP8yzcEC2VpUW2vZWlFZ2VpaW5FZY7dlCasIyb3hudEfv2q112bB4IKIdW6Yu2Vimb6wBfn9ufvmopIoGiEemvUwf/8MKLzECm0mvzB1wuZV4dTsW2zNlnfCgZRiPTUD7r0YMsgW/Exm4MJXprK84SfAAlWd4y07HBMOWgZBAbTuytqgNaLrYRypiJhu4dekzPLYJqU+oS5BLr0SneBnpm5W3MQlVH0+jwaEvQOvmllYWlta");
        private static int[] order = new int[] { 0,12,4,9,4,7,12,7,12,10,11,13,12,13,14 };
        private static int key = 91;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
