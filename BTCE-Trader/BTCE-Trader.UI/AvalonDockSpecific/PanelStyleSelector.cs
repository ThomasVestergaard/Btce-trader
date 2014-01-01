using System.Windows;
using System.Windows.Controls;
using BTCE_Trader.UI.UI;

namespace BTCE_Trader.UI.AvalonDockSpecific
{
    public class PanesStyleSelector : StyleSelector
    {
        public Style ToolStyle
        {
            get;
            set;
        }

        public Style FileStyle
        {
            get;
            set;
        }

        public override Style SelectStyle(object item, System.Windows.DependencyObject container)
        {
            if (item is AvalonDockDepthViewModel)
                return FileStyle;
            
            return base.SelectStyle(item, container);
        }
    }
}
