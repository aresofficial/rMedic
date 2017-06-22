using System.ComponentModel;
using System.Windows;

namespace rMedic.ViewModels.Attributes
{
    public class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        #region Fields

        private string resourceName;

        #endregion

        #region Constructors

        public LocalizedDescriptionAttribute(string resourceName)
        {
            this.resourceName = resourceName;
        }

        #endregion

        #region DescriptionAttribute Members

        public override string Description
        {
            get { return (string)Application.Current.FindResource($"r_unit_{resourceName}"); }
        }

        #endregion

        #region Properties

        public string ResourceName
        {
            get { return resourceName; }
        }

        #endregion
    }
}
