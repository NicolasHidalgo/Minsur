using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace Presentacion
{
    public class ExtendedSelectListItem : SelectListItem
    {
        public object HtmlAttributes { get; set; }
    }


    public static class ExtendedSelectExtensions
    {
        internal static object GetModelStateValue(this HtmlHelper htmlHelper, string key, Type destinationType)
        {
            System.Web.Mvc.ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(key, out modelState))
            {
                if (modelState.Value != null)
                {
                    return modelState.Value.ConvertTo(destinationType, null /* culture */);
                }
            }
            return null;
        }

        public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<ExtendedSelectListItem> selectList)
        {
            return DropDownList(htmlHelper, name, selectList, null /* optionLabel */, null /* htmlAttributes */);
        }

        public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<ExtendedSelectListItem> selectList, object htmlAttributes)
        {
            return DropDownList(htmlHelper, name, selectList, null /* optionLabel */, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<ExtendedSelectListItem> selectList, IDictionary<string, object> htmlAttributes)
        {
            return DropDownList(htmlHelper, name, selectList, null /* optionLabel */, htmlAttributes);
        }

        public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<ExtendedSelectListItem> selectList, string optionLabel)
        {
            return DropDownList(htmlHelper, name, selectList, optionLabel, null /* htmlAttributes */);
        }

        public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<ExtendedSelectListItem> selectList, string optionLabel, object htmlAttributes)
        {
            return DropDownList(htmlHelper, name, selectList, optionLabel, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<ExtendedSelectListItem> selectList, string optionLabel, IDictionary<string, object> htmlAttributes)
        {
            return DropDownListHelper(htmlHelper, metadata: null, expression: name, selectList: selectList, optionLabel: optionLabel, htmlAttributes: htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<ExtendedSelectListItem> selectList)
        {
            return DropDownListFor(htmlHelper, expression, selectList, null /* optionLabel */, null /* htmlAttributes */);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<ExtendedSelectListItem> selectList, object htmlAttributes)
        {
            return DropDownListFor(htmlHelper, expression, selectList, null /* optionLabel */, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<ExtendedSelectListItem> selectList, IDictionary<string, object> htmlAttributes)
        {
            return DropDownListFor(htmlHelper, expression, selectList, null /* optionLabel */, htmlAttributes);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<ExtendedSelectListItem> selectList, string optionLabel)
        {
            return DropDownListFor(htmlHelper, expression, selectList, optionLabel, null /* htmlAttributes */);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<ExtendedSelectListItem> selectList, string optionLabel, object htmlAttributes)
        {
            return DropDownListFor(htmlHelper, expression, selectList, optionLabel, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Users cannot use anonymous methods with the LambdaExpression type")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<ExtendedSelectListItem> selectList, string optionLabel, IDictionary<string, object> htmlAttributes)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            return DropDownListHelper(htmlHelper, metadata, ExpressionHelper.GetExpressionText(expression), selectList, optionLabel, htmlAttributes);
        }

        private static MvcHtmlString DropDownListHelper(HtmlHelper htmlHelper, ModelMetadata metadata, string expression, IEnumerable<ExtendedSelectListItem> selectList, string optionLabel, IDictionary<string, object> htmlAttributes)
        {
            return SelectInternal(htmlHelper, metadata, optionLabel, expression, selectList, allowMultiple: false, htmlAttributes: htmlAttributes);
        }

        // Helper methods

        private static IEnumerable<ExtendedSelectListItem> GetSelectDataExtended(this HtmlHelper htmlHelper, string name)
        {
            object o = null;
            if (htmlHelper.ViewData != null)
            {
                o = htmlHelper.ViewData.Eval(name);
            }
            if (o == null)
            {
                throw new InvalidOperationException(
                    String.Format(
                        CultureInfo.CurrentCulture,
                        "MissingSelectData",
                        name,
                        "IEnumerable<ExtendedSelectListItem>"));
            }
            IEnumerable<ExtendedSelectListItem> selectList = o as IEnumerable<ExtendedSelectListItem>;
            if (selectList == null)
            {
                throw new InvalidOperationException(
                    String.Format(
                        CultureInfo.CurrentCulture,
                        "WrongSelectDataType",
                        name,
                        o.GetType().FullName,
                        "IEnumerable<ExtendedSelectListItem>"));
            }
            return selectList;
        }

        internal static string ListItemToOption(SelectListItem item)
        {
            TagBuilder builder = new TagBuilder("option")
            {
                InnerHtml = HttpUtility.HtmlEncode(item.Text)
            };
            if (item.Value != null)
            {
                builder.Attributes["value"] = item.Value;
            }
            if (item.Selected)
            {
                builder.Attributes["selected"] = "selected";
            }
            if (item.Disabled)
            {
                builder.Attributes["disabled"] = "disabled";
            }

            if (item is ExtendedSelectListItem)
            {
                var htmlAttributes = ((ExtendedSelectListItem)item).HtmlAttributes;
                var _htmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                builder.MergeAttributes(_htmlAttributes);
            }

            return builder.ToString(TagRenderMode.Normal);
        }
        private static IEnumerable<ExtendedSelectListItem> GetSelectListWithDefaultValue(IEnumerable<ExtendedSelectListItem> selectList, object defaultValue, bool allowMultiple)
        {
            IEnumerable defaultValues;

            if (allowMultiple)
            {
                defaultValues = defaultValue as IEnumerable;
                if (defaultValues == null || defaultValues is string)
                {
                    throw new InvalidOperationException(
                        String.Format(
                            CultureInfo.CurrentCulture,
                            "SelectExpressionNotEnumerable",
                            "expression"));
                }
            }
            else
            {
                defaultValues = new[] { defaultValue };
            }

            IEnumerable<string> values = from object value in defaultValues
                                         select Convert.ToString(value, CultureInfo.CurrentCulture);

            // ToString() by default returns an enum value's name.  But selectList may use numeric values.
            IEnumerable<string> enumValues = from Enum value in defaultValues.OfType<Enum>()
                                             select value.ToString("d");
            values = values.Concat(enumValues);

            HashSet<string> selectedValues = new HashSet<string>(values, StringComparer.OrdinalIgnoreCase);
            List<ExtendedSelectListItem> newSelectList = new List<ExtendedSelectListItem>();

            foreach (ExtendedSelectListItem item in selectList)
            {
                item.Selected = (item.Value != null) ? selectedValues.Contains(item.Value) : selectedValues.Contains(item.Text);
                newSelectList.Add(item);
            }
            return newSelectList;
        }

        private static MvcHtmlString SelectInternal(this HtmlHelper htmlHelper, ModelMetadata metadata,
            string optionLabel, string name, IEnumerable<ExtendedSelectListItem> selectList, bool allowMultiple,
            IDictionary<string, object> htmlAttributes)
        {
            string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (String.IsNullOrEmpty(fullName))
            {
                throw new ArgumentException("Null or empty", "name");
            }

            //bool usedViewData = false;

            // If we got a null selectList, try to use ViewData to get the list of items.
            //if (selectList == null)
            //{
            //    selectList = htmlHelper.GetSelectDataExtended(name);
            //    usedViewData = true;
            //}

            //object defaultValue = (allowMultiple) ? htmlHelper.GetModelStateValue(fullName, typeof(string[])) : htmlHelper.GetModelStateValue(fullName, typeof(string));

            // If we haven't already used ViewData to get the entire list of items then we need to
            // use the ViewData - supplied value before using the parameter-supplied value.
            //if (defaultValue == null && !String.IsNullOrEmpty(name))
            //{
            //    if (!usedViewData)
            //    {
            //        defaultValue = htmlHelper.ViewData.Eval(name);
            //    }
            //    else if (metadata != null)
            //    {
            //        defaultValue = metadata.Model;
            //    }
            //}

            //if (defaultValue != null)
            //{
            //    selectList = GetSelectListWithDefaultValue(selectList, defaultValue, allowMultiple);
            //}

            // Convert each ListItem to an <option> tag and wrap them with <optgroup> if requested.
            StringBuilder listItemBuilder = BuildItems(optionLabel, selectList);

            TagBuilder tagBuilder = new TagBuilder("select")
            {
                InnerHtml = listItemBuilder.ToString()
            };
            tagBuilder.MergeAttributes(htmlAttributes);
            tagBuilder.MergeAttribute("name", fullName, true /* replaceExisting */);
            tagBuilder.GenerateId(fullName);
            if (allowMultiple)
            {
                tagBuilder.MergeAttribute("multiple", "multiple");
            }

            // If there are any errors for a named field, we add the css attribute.
            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(fullName, out modelState))
            {
                if (modelState.Errors.Count > 0)
                {
                    tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
                }
            }

            tagBuilder.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(name, metadata));

            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal)); //tagBuilder.ToMvcHtmlString(TagRenderMode.Normal);
        }

        private static StringBuilder BuildItems(string optionLabel, IEnumerable<SelectListItem> selectList)
        {
            StringBuilder listItemBuilder = new StringBuilder();

            // Make optionLabel the first item that gets rendered.
            if (optionLabel != null)
            {
                listItemBuilder.AppendLine(ListItemToOption(new SelectListItem()
                {
                    Text = optionLabel,
                    Value = String.Empty,
                    Selected = false
                }));
            }

            // Group items in the SelectList if requested.
            // Treat each item with Group == null as a member of a unique group
            // so they are added according to the original order.
            IEnumerable<IGrouping<int, SelectListItem>> groupedSelectList = selectList.GroupBy<SelectListItem, int>(
                i => (i.Group == null) ? i.GetHashCode() : i.Group.GetHashCode());
            foreach (IGrouping<int, SelectListItem> group in groupedSelectList)
            {
                SelectListGroup optGroup = group.First().Group;

                // Wrap if requested.
                TagBuilder groupBuilder = null;
                if (optGroup != null)
                {
                    groupBuilder = new TagBuilder("optgroup");
                    if (optGroup.Name != null)
                    {
                        groupBuilder.MergeAttribute("label", optGroup.Name);
                    }
                    if (optGroup.Disabled)
                    {
                        groupBuilder.MergeAttribute("disabled", "disabled");
                    }
                    listItemBuilder.AppendLine(groupBuilder.ToString(TagRenderMode.StartTag));
                }

                foreach (SelectListItem item in group)
                {
                    listItemBuilder.AppendLine(ListItemToOption(item));
                }

                if (optGroup != null)
                {
                    listItemBuilder.AppendLine(groupBuilder.ToString(TagRenderMode.EndTag));
                }
            }

            return listItemBuilder;
        }
    }
}