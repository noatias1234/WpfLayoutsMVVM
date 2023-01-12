using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfLayoutsMvvm.Annotations;

namespace WpfLayoutsMvvm
{
    public class LayoutsViewModel : INotifyPropertyChanged
    {

        private List<Layout> _layouts;
        private string _layoutName;

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public LayoutsViewModel()
        {
            _layouts = new List<Layout>
            {
                new("Full", 2, 2, new Dictionary<string, LayoutChild>
                {
                    { "TestsRegion", new LayoutChild(0, 0, 1, 1) },
                    { "ReportsRegion", new LayoutChild(1, 0, 1, 2) },
                }),

                new("Partial", 2, 1, new Dictionary<string, LayoutChild>
                {
                    { "TestsRegion", new LayoutChild(0, 0, 1, 1) },
                    { "ReportsRegion", new LayoutChild(0, 1, 1, 1) },
                }),

            };

            _layoutName = "Full";
        }

        public List<Layout> Layouts
        {
            get => _layouts;
            set
            {
                if (Equals(value, _layouts)) return;
                _layouts = value;
                OnPropertyChanged();
            }
        }

        public string LayoutName
        {
            get => _layoutName;
            set
            {
                if (Equals(value, _layoutName)) return;
                _layoutName = value;
                OnPropertyChanged();
            }
        }
    }
}
