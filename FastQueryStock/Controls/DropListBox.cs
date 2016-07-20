using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FastQueryStock.Controls
{

    public class DropListBox<T> : ListBox where T : class
    {
        private Point _dragStartPoint;
        public event EventHandler<ItemMoveEventArgs<T>> ItemMove;

        private P FindVisualParent<P>(DependencyObject child)
            where P : DependencyObject
        {
            var parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null)
                return null;

            P parent = parentObject as P;
            if (parent != null)
                return parent;

            return FindVisualParent<P>(parentObject);
        }

        public DropListBox()
        {
            this.AllowDrop = true;
            this.PreviewMouseMove += ListBox_PreviewMouseMove;            
            this.PreviewMouseLeftButtonDown += ListBoxItem_PreviewMouseLeftButtonDown;
            this.Drop += ListBoxItem_Drop;          
           
        }

        private void ListBox_PreviewMouseMove(object sender, MouseEventArgs e)
        {          
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var lb = sender as ListBox;
                var lbi = FindVisualParent<ListBoxItem>(((DependencyObject)e.OriginalSource));
                if (lbi != null)
                {
                    DragDrop.DoDragDrop(lbi, lbi.DataContext, DragDropEffects.Move);
                }
            }
        }

        private void ListBoxItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _dragStartPoint = e.GetPosition(null);      
        }

        private void ListBoxItem_Drop(object sender, DragEventArgs e)
        {
            if (sender is ListBox)
            {
                ListBox parent = (ListBox)sender;
                var source = e.Data.GetData(typeof(T)) as T;
                var target = GetDataFromListBox(parent, e.GetPosition(parent)) as T;

                int sourceIndex = this.Items.IndexOf(source);
                int targetIndex = this.Items.IndexOf(target);

                var moveArgs = new ItemMoveEventArgs<T>(source, target, sourceIndex, targetIndex);
                if (ItemMove != null)
                    ItemMove(sender, moveArgs);
               
                // move the list box item
                if(!moveArgs.Cancel)
                    Move(source, sourceIndex, targetIndex);
            }
        }

        private static object GetDataFromListBox(ListBox source, Point point)
        {
            UIElement element = source.InputHitTest(point) as UIElement;
            if (element != null)
            {
                object data = DependencyProperty.UnsetValue;
                while (data == DependencyProperty.UnsetValue)
                {
                    data = source.ItemContainerGenerator.ItemFromContainer(element);
                    if (data == DependencyProperty.UnsetValue)
                    {
                        element = VisualTreeHelper.GetParent(element) as UIElement;
                    }
                    if (element == source)
                    {
                        return null;
                    }
                }
                if (data != DependencyProperty.UnsetValue)
                {
                    return data;
                }
            }
            return null;
        }

        private void Move(T source, int sourceIndex, int targetIndex)
        {
            if (sourceIndex < targetIndex)
            {
                var items = this.ItemsSource as IList<T>;
                if (items != null)
                {
                    items.Insert(targetIndex + 1, source);
                    items.RemoveAt(sourceIndex);
                }
            }
            else
            {
                var items = this.ItemsSource as IList<T>;
                if (items != null)
                {
                    int removeIndex = sourceIndex + 1;
                    if (items.Count + 1 > removeIndex)
                    {
                        items.Insert(targetIndex, source);
                        items.RemoveAt(removeIndex);
                    }
                }
            }
        }

    }
}
