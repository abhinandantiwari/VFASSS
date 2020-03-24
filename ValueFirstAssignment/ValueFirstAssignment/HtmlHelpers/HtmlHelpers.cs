using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace ValueFirstAssignment.HTMLHelper
{
    public static class HtmlHelpers 
    {
      
        public static MvcHtmlString MessageBoolToString<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string str = (bool)metadata.Model ? "Active" : "Inactive";
            return new MvcHtmlString(str);
        }
    }
}