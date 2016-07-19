using FastQueryStock.Controls;
using FastQueryStock.ViewModels;
using FastQueryStock.ViewModels.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FastQueryStock.UserControls
{
    /// <summary>
    /// StockListUserControl.xaml 的互動邏輯
    /// </summary>
    public partial class StockListUserControl : UserControl
    {
        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object),
                typeof(StockListUserControl),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty ItemSourcesProperty =
          DependencyProperty.Register("ItemSources", typeof(IEnumerable),
              typeof(StockListUserControl),
              new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty ItemDoubleClickCommandProperty =
            DependencyProperty.Register("ItemDoubleClickCommand", typeof(ICommand),
                typeof(StockListUserControl));

        public static readonly DependencyProperty ItemMovedCommandProperty =
           DependencyProperty.Register("ItemMovedCommand", typeof(ICommand),
               typeof(StockListUserControl));

        public static readonly DependencyProperty MenuItemsProperty =
         DependencyProperty.Register("MenuItems", typeof(IEnumerable),
             typeof(StockListUserControl),
             new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }
        public IEnumerable ItemSources
        {
            get { return (IEnumerable)GetValue(ItemSourcesProperty); }
            set { SetValue(ItemSourcesProperty, value); }
        }
        public ICommand ItemDoubleClickCommand
        {
            get { return (ICommand)GetValue(ItemDoubleClickCommandProperty); }
            set { SetValue(ItemDoubleClickCommandProperty, value); }
        }

        public ICommand ItemMovedCommand
        {
            get { return (ICommand)GetValue(ItemMovedCommandProperty); }
            set { SetValue(ItemMovedCommandProperty, value); }
        }

        public IEnumerable MenuItems
        {
            get { return (IEnumerable)GetValue(MenuItemsProperty); }
            set { SetValue(MenuItemsProperty, value); }
        }


        public StockListUserControl()
        {
            InitializeComponent();
            
        }

        #region Event Handler
        private void StockListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            if (listBox == null)
                return;

            if(listBox.SelectedItem != null && ItemDoubleClickCommand != null)
            {
                ItemDoubleClickCommand.Execute(listBox.SelectedItem);
            }
        }
        #endregion

        private void ListBox_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            var s = listBox;
            var sss = stockListControl.Resources;
        }

        private void StockListBox_ItemMoved(object sender, ItemMoveEventArgs<RealTimeStockItem> e)
        {
            if (ItemMovedCommand != null)
            {
                ItemMovedCommand.Execute(e);
            }
        }
    }
}
