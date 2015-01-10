using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace GBT.UI.Behaviors
{
	public class ScrollToBehavior : Behavior<ItemsControl>
	{
		private string _SelectedPath;
		public string SelectedPath
		{
			get { return _SelectedPath; }
			set { _SelectedPath = value; }
		}

		protected override void OnAttached()
		{
			var dc = this.AssociatedObject.DataContext as INotifyPropertyChanged;
			if (dc != null)
				dc.PropertyChanged += ScrollToBehavior_PropertyChanged;
			this.AssociatedObject.DataContextChanged += AssociatedObject_DataContextChanged;
		}

		protected override void OnDetaching()
		{
			this.AssociatedObject.DataContextChanged -= AssociatedObject_DataContextChanged;
		}

		void AssociatedObject_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
		{
			if ((e.OldValue as INotifyPropertyChanged) != null)
				(e.OldValue as INotifyPropertyChanged).PropertyChanged -= ScrollToBehavior_PropertyChanged;
			if ((e.NewValue as INotifyPropertyChanged) != null)
				(e.NewValue as INotifyPropertyChanged).PropertyChanged += ScrollToBehavior_PropertyChanged;
		}


		void ScrollToBehavior_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName != this.SelectedPath)
				return;
			var value = sender.GetType().GetProperty(this.SelectedPath).GetValue(sender);
			if (value != null)
				VirtualizedScrollIntoView(this.AssociatedObject, value);
		}

		public void VirtualizedScrollIntoView(ItemsControl control, object item)
		{
			try
			{
				// this is basically getting a reference to the ScrollViewer defined in the ItemsControl's style (identified above).
				// you *could* enumerate over the ItemsControl's children until you hit a scroll viewer, but this is quick and
				// dirty!
				// First 0 in the GetChild returns the Border from the ControlTemplate, and the second 0 gets the ScrollViewer from
				// the Border.
				ScrollViewer sv = VisualTreeHelper.GetChild(VisualTreeHelper.GetChild((DependencyObject)control, 0), 0) as ScrollViewer;
				// now get the index of the item your passing in

				int index = control.Items.IndexOf(item);
				if (index != -1)
				{
					// since the scroll viewer is using content scrolling not pixel based scrolling we just tell it to scroll to the index of the item
					// and viola!  we scroll there!
					//sv.ScrollToVerticalOffset(index);
				}
			}
			catch (Exception ex)
			{
				Debug.Assert(false);
			}
		}

		
	}
}
