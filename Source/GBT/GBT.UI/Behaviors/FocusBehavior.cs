using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace GBT.UI.Behaviors
{
	public class FocusBehavior : Behavior<UserControl>
	{
		private string _FocusFlag;
		public string FocusFlag
		{
			get { return _FocusFlag; }
			set { _FocusFlag = value; }
		}
		
		
		protected override void OnAttached()
		{
			this.AssociatedObject.DataContextChanged += AssociatedObject_DataContextChanged;
		}

		protected override void OnDetaching()
		{
			this.AssociatedObject.DataContextChanged -= AssociatedObject_DataContextChanged;
		}

		void AssociatedObject_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
		{
			if ((e.OldValue as INotifyPropertyChanged) != null)
				(e.OldValue as INotifyPropertyChanged).PropertyChanged -= FocusBehavior_PropertyChanged;
			if ((e.NewValue as INotifyPropertyChanged) != null)
				(e.NewValue as INotifyPropertyChanged).PropertyChanged += FocusBehavior_PropertyChanged;
		}

		void FocusBehavior_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName != this.FocusFlag)
				return;
			var value = (bool)sender.GetType().GetProperty(this.FocusFlag).GetValue(sender);
			if (value)
				this.AssociatedObject.Focus();
		}
		
	}
}
