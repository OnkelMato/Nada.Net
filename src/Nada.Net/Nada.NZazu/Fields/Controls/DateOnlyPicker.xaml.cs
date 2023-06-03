using Nada.NZazu.Contracts.Adapter;
using Nada.NZazu.Contracts.Suggest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserControl = System.Windows.Controls.UserControl;

namespace Nada.NZazu.Fields.Controls
{
    /// <summary>
    /// Interaction logic for DateOnlyPicker.xaml
    /// </summary>
    public partial class DateOnlyPicker : UserControl
    {
        public DateOnlyPicker()
        {
            InitializeComponent();

            DatePicker.SelectedDateFormat = DatePickerFormat.Short;
        }

        private static void UpdateControl(DateOnlyPicker control, DateOnly? val)
        {
            control.DatePicker.IsEnabled = !control.IsReadOnly;
            control.DatePicker.SelectedDate = val.HasValue ? new DateTime(val.Value.Year, val.Value.Month, val.Value.Day) : null;
        }

        #region dependency properties: Value

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value), typeof(DateOnly?), typeof(DateOnlyPicker), new PropertyMetadata(default(DateOnly?), ValueChangedCallback));

        private static void ValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is DateOnlyPicker control)) return;
            var val = e.NewValue as DateOnly?;

            UpdateControl(control, val);
        }

        public DateOnly? Value
        {
            get => (DateOnly?)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        #endregion

        #region dependency properties: IsReadOnly

        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register(
            "IsReadOnly", typeof(bool), typeof(GeoLocationBox), new PropertyMetadata(false, IsReadOnlyChangedCallback));

        private static void IsReadOnlyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is DateOnlyPicker box)) return;
            box.DatePicker.IsEnabled = !(bool)e.NewValue;
        }

        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        #endregion

        private void DateSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePicker.SelectedDate == null)
                Value = null;
            else
            {
                var val = DatePicker.SelectedDate.Value;
                Value = new DateOnly(val.Year, val.Month, val.Day);
            }
        }
    }
}
