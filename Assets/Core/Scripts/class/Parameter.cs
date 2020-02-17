using UnityEngine;
public class Parameter
{
    public class Input
    {
        public static string Horizontal = "Horizontal";
        public static string Vertical = "Vertical";
        public static string MouseX = "Mouse X";
        public static string MouseY = "Mouse Y";
        public static KeyCode Fire = KeyCode.Mouse0;
    }

    public class Animator
    {
        public static string Horizontal = "VX";
        public static string Vertical = "VY";
        public static string Action = "Action";
        public static string Attack = "Attack";
        public static string Idle = "Idle";
        public static string Death = "Death";
        public static string Hit = "Hit";
    }

    public class Tag
    {
        public static string Monster = "Monster";
        public static string Player = "Player";
        public static string MonsterSpawn = "SpawnPos";
        public static string Block = "Block";
    }
}
