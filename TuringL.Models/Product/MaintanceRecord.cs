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
            if (string.IsNullOrEmpty(this.KeyWorld)) AddBusinessRule(new BusinessRule() { Property = "关键词", Rule = "关键词不能为空" });
            if (string.IsNullOrEmpty(this.MiantanceUser)) AddBusinessRule(new BusinessRule() { Property = "维护人员", Rule = "维护人员不能为空" });
            if (string.IsNullOrEmpty(this.ProductId)) AddBusinessRule(new BusinessRule() { Property = "维护产品", Rule = "维护产品不能为空" });
        }

        public void Register()
        {
            IsValidated();
            this.Id = System.Guid.NewGuid();
            this.MaintanceStartTime = System.DateTime.Now;
            this.MiantanceOverTime = System.DateTime.Now;
            this.RState = (int)RStates.Added;
            this.MState = (int)MaintanceStates.INCOMPLETE;
        }
    }

    public interface IMaintanceRecordRepository : IRepository<MaintanceRecord, System.Guid>
    { }
}
