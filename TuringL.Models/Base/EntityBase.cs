using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace TuringL.Models
{
    public abstract class EntityBase
    {
        private List<BusinessRule> _brokenRules = new List<BusinessRule>();

        protected abstract void Validate();

        public IEnumerable<BusinessRule> GetBrokenRules()
        {
            if (_brokenRules == null)
                _brokenRules = new List<BusinessRule>();
            _brokenRules.Clear();
            Validate();
            return _brokenRules;
        }

        public void IsValidated()
        {
            if (_brokenRules == null)
                _brokenRules = new List<BusinessRule>();
            _brokenRules.Clear();
            Validate();
            if (_brokenRules.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Clear();
                foreach (var item in _brokenRules)
                {
                    sb.Append(item.Property + ":" + item.Rule+" ");
                }
                if (sb.Length > 0) throw new Exception(sb.ToString());
            }
        }

        protected void AddBusinessRule(BusinessRule businessRules)
        {
            if (_brokenRules == null)
                _brokenRules = new List<BusinessRule>();
            _brokenRules.Add(businessRules);
        }

        /// <summary>
        /// check this is equal the obj
        /// and just check the property which is value type
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            
            if (this.GetType() != obj.GetType())
                return false;

            PropertyInfo[] properties = this.GetType().GetProperties();
            PropertyInfo[] objProperties = obj.GetType().GetProperties();
            if (properties == null && objProperties == null) return true;

            if (properties != null && objProperties != null && properties.Count() == 0 && objProperties.Count() == 0) return true;

            if (properties != null && objProperties != null && properties.Count() == objProperties.Count())
            {
                foreach (PropertyInfo item in properties)
                {
                    if (item.PropertyType.IsGenericType||item.PropertyType.IsClass)
                    {
                        continue;
                    }
                    PropertyInfo node = objProperties.Where(it => it.Name.Equals(item.Name)).FirstOrDefault();
                    if (node != null)
                    {
                        if (!node.GetValue(obj, null).Equals(item.GetValue(this, null)))
                            return false;
                    }
                    else
                        return false;
                }
                return true;
            }
            return false;
        }
    }
}
