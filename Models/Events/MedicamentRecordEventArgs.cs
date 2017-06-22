using System;

namespace rMedic.Models.Events
{
    public class MedicamentRecordEventArgs : EventArgs
    {
        public MedicamentRecord Record { get; set; }
    }
}
