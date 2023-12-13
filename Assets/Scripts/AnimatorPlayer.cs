public static class AnimatorPlayer
{
    public static class Params
    {
        public const string IsRun = nameof(IsRun);
        public const string Jumped = nameof(Jumped);
        public const string IsGrounded = nameof(IsGrounded);
    }

    public static class States
    {
        public const string Idle = nameof(Idle);
        public const string Run = nameof(Run);
        public const string Jump = nameof(Jump);
    }
}