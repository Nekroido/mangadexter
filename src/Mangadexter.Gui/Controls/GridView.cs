using Avalonia.Controls;
using Avalonia;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangadexter.Gui.Controls;

public class GridView : Grid
{
    public static readonly StyledProperty<IEnumerable> ItemsSourceProperty =
        AvaloniaProperty.Register<GridView, IEnumerable>(nameof(ItemsSource));

    public static readonly StyledProperty<int> ColumnsProperty =
        AvaloniaProperty.Register<GridView, int>(nameof(Columns), 1);

    public IEnumerable ItemsSource
    {
        get { return GetValue(ItemsSourceProperty); }
        set
        {
            SetValue(ItemsSourceProperty, value);
            UpdateItems(value);
        }
    }

    public int Columns
    {
        get { return GetValue(ColumnsProperty); }
        set
        {
            SetValue(ColumnsProperty, value);
            UpdateItems(ItemsSource);
        }
    }

    public GridView()
    {
    }

    private void UpdateItems(IEnumerable items)
    {
        this.Children.Clear();
        this.RowDefinitions.Clear();
        this.ColumnDefinitions.Clear();

        if (items == null)
        {
            return;
        }

        var list = items.Cast<object>().ToList();

        var rows = (int)Math.Ceiling((double)list.Count / Columns);

        for (var i = 0; i < rows; i++)
        {
            this.RowDefinitions.Add(new RowDefinition(1, GridUnitType.Star));
        }

        for (var i = 0; i < Columns; i++)
        {
            this.ColumnDefinitions.Add(new ColumnDefinition(1, GridUnitType.Star));
        }

        for (var i = 0; i < list.Count; i++)
        {
            var item = list[i];
            var control = new TextBlock { Text = item.ToString() }; // Replace with your control

            Grid.SetRow(control, i / Columns);
            Grid.SetColumn(control, i % Columns);

            this.Children.Add(control);
        }
    }
}

