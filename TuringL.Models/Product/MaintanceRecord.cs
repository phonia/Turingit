using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuringL.Models
{
    public partial class MaintanceRecord:EntityBase,IAggregateRoot
    {
        protected override void Validate()
        {

        }

        public void Register()
        {
            IsValidated();
            this.MaintanceStartTime = System.DateTime.Now;
            this.MiantanceOverTime = System.DateTime.Now;
            this.RState = (int)RStates.Added;
            this.MState = (int)MaintanceStates.INCOMPLETE;
        }
    }

    public interface IMaintanceRecordRepository : IRepository<MaintanceRecord, System.Guid>
    { }
}
