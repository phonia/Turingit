using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace TuringL.DServices.Base
{
    public class Mapping
    {
        //public static void ConvertToViewModel(object model, Type view)
        //{
        //    try
        //    {
        //        object viewObject = Activator.CreateInstance(view);
        //        PropertyInfo[] properties = view.GetProperties();
        //        if (properties != null)
        //        {
        //            foreach (var item in properties)
        //            {
        //                var value = model.GetType().GetProperties().Where(it => it.Name == item.Name).FirstOrDefault();
        //                if (value == null) continue;
        //                item.SetValue(viewObject, value.GetValue(model, null), null);
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        throw new Exception("error in ConvertToViewModel!");
        //    }
        //}
    }
}
