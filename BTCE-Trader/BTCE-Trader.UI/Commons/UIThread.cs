using System.Windows.Threading;

namespace BTCE_Trader.UI.Commons
{
    public static class UiThread
    {
        public static Dispatcher UiDispatcher { get; private set; }
        public static void Init(Dispatcher uiDispatcher)
        {
            UiDispatcher = uiDispatcher;
        }

        public static bool IsUiDispatcher(Dispatcher dispatcher)
        {
            return UiDispatcher == dispatcher;
        }

        public static void Reset()
        {
            UiDispatcher = null;
        }
    }
}
