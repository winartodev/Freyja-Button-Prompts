using Log = Freyja.Logger.Logger;

namespace Freyja.ButtonPrompts.DebugLog
{
    public static class DebugLog
    {
        private const string PrefixName = "Freyja Button Prompts";

        private static Log _show;

        public static Log Show
        {
            get
            {
                if (_show == null)
                {
                    _show = Log.AddLog(PrefixName);
                }

                return _show;
            }
        }
    }
}