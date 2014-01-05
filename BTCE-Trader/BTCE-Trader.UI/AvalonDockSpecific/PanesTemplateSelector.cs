using System.Windows;
using System.Windows.Controls;

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

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
           return FileViewTemplate;
        }
    }
}
