using System.Windows;
using System.Windows.Controls;
using BTCE_Trader.UI.UI;
using Xceed.Wpf.AvalonDock.Layout;

namespace BTCE_Trader.UI.AvalonDockSpecific
{
    class PanesTemplateSelector : DataTemplateSelector
    {
        public PanesTemplateSelector()
        {

        }


        public DataTemplate FileViewTemplate
        {
            get;
            set;
        }

        public DataTemplate FileStatsViewTemplate
        {
            get;
            set;
        }

        public override DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            var itemAsLayoutContent = item as LayoutContent;

            if (item is AvalonDockDepthViewModel)
                return FileViewTemplate;
            
            return base.SelectTemplate(item, container);
        }
    }
}
