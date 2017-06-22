using System.Windows;

namespace rMedic.Helpers
{
    //from https://stackoverflow.com/questions/30925145/wpf-localization-dynamicresource-with-stringformat
    public class Localization
    {
        public static object GetResource(DependencyObject obj)
        {
            return obj.GetValue(ResourceProperty);
        }

        public static void SetResource(DependencyObject obj, object value)
        {
            obj.SetValue(ResourceProperty, value);
        }

        public static readonly DependencyProperty ResourceProperty =
            DependencyProperty.RegisterAttached("Resource", typeof(object), typeof(Localization), new PropertyMetadata(null, OnResourceChanged));

        private static void OnResourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d.ReadLocalValue(ResourceProperty).GetType().Name == "ResourceReferenceExpression")
                return;

            var fe = d as FrameworkElement;
            if (fe == null)
                return;

            fe.SetResourceReference(ResourceProperty, e.NewValue);
        }
    }
}
