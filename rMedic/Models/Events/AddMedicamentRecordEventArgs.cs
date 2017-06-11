using System;

namespace rMedic.Models.Events
{
    public class AddMedicamentRecordEventArgs : EventArgs
    {
        public MedicamentRecord Record { get; set; }
    }
}
