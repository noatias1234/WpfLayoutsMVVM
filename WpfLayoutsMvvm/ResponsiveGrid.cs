using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfLayoutsMvvm;

public class ResponsiveGrid : Grid
{
    public ResponsiveGrid()
    {
        Loaded += (s, e) => Refresh();
    }

    private void Refresh()
    {
        if (!IsLoaded) return;

        if (string.IsNullOrEmpty(LayoutName)) return;
        if (Layouts is null) return;

        var layout = Layouts.FirstOrDefault(x => x.Name.Equals(LayoutName));
        if(layout is null) return;

        ColumnDefinitions.Clear();
        RowDefinitions.Clear();

        for (int i = 0; i < layout.Columns; i++)
        {
            ColumnDefinitions.Add(new ColumnDefinition());
        }
        for(int i = 0; i < layout.Rows; i++)
        {
            RowDefinitions.Add(new RowDefinition());
        }

        foreach (FrameworkElement child in Children)
        {
            if(!layout.LayoutChildren.TryGetValue(child.Name, out var prop)) continue;
            SetRow(child,prop.Row);
            SetRowSpan(child,prop.RowSpan);
            SetColumn(child, prop.Column);
            SetColumnSpan(child, prop.ColumnSpan);
        }
    }

    public List<Layout> Layouts
    {
        get => (List<Layout>)GetValue(LayoutsProperty);
        set => SetValue(LayoutsProperty, value);
    }

    public static readonly DependencyProperty LayoutsProperty =
        DependencyProperty.Register("Layouts", typeof(List<Layout>), typeof(ResponsiveGrid),
            new FrameworkPropertyMetadata(OnLayoutChanged));

    public string LayoutName
    {
        get => (string)GetValue(LayoutNameProperty);
        set => SetValue(LayoutNameProperty, value);
    }

    public static readonly DependencyProperty LayoutNameProperty =
        DependencyProperty.Register("LayoutName", typeof(string), typeof(ResponsiveGrid),
            new FrameworkPropertyMetadata(OnLayoutChanged));

    private static void OnLayoutChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var responsiveGrid = (ResponsiveGrid)d;
        responsiveGrid.Refresh();
    }

}
public class Layout
{
    public string Name { get; set; }
    public int Rows { get; set; }
    public int Columns { get; set; }
    public Dictionary<string, LayoutChild> LayoutChildren { get; set; }

    public Layout(string name, int rows, int columns, Dictionary<string, LayoutChild> layoutChildren)
    {
        Name = name;
        Rows = rows;
        Columns = columns;
        LayoutChildren = layoutChildren;
    }
}


public class LayoutChild
{
    public int ColumnSpan { get; set; }
    public int RowSpan { get; set; }
    public int Column { get; set; }
    public int Row { get; set; }

    public LayoutChild(int column, int row, int columnSpan, int rowSpan)
    {
        Column=column;
        Row=row;
        ColumnSpan=columnSpan;
        RowSpan=rowSpan;
    }
}