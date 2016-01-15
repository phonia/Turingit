using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TuringL.Models
{
    public partial class InstallInfo:EntityBase,IAggregateRoot
    {
        protected override void Validate()
        {
            if (string.IsNullOrEmpty(this.Principal)) AddBusinessRule(new BusinessRule() { Property = "业主负责人", Rule = "业主负责人不能为空！" });
            if (string.IsNullOrEmpty(this.Site)) AddBusinessRule(new BusinessRule() { Property = "安装地点", Rule = "安装地址不能为空" });
            if (string.IsNullOrEmpty(this.MaintancePeriod)) AddBusinessRule(new BusinessRule() { Property = "保修期", Rule = "保修期不能为空" });
            if (string.IsNullOrEmpty(this.InstallMethod)) AddBusinessRule(new BusinessRule() { Property = "安装方式", Rule = "安装方式不能为空" });
            if (string.IsNullOrEmpty(this.ProductId)) AddBusinessRule(new BusinessRule() { Property = "安装产品", Rule = "安装产品不能为空" });
        }

        public void Register()
        {
            IsValidated();
            this.Id = System.Guid.NewGuid();
            this.RState = (int)RStates.Added;
            this.IState = (int)InstallStates.INSTALLING;
            this.CheckTime = this.StartTime;
            this.OverTime = this.OverTime;
            if (string.IsNullOrEmpty(this.CNumber)) this.CNumber = "";
        }
    }

    public interface IInstallInfoRepository : IRepository<InstallInfo, System.Guid>
    { }
}
