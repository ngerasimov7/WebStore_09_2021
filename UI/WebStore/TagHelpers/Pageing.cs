﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using WebStore.Domain.ViewModels;

namespace WebStore.TagHelpers
{
    public class Pageing : TagHelper
    {
        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public PageViewModel PageModel { get; set; }
        public string PageAction { get; set; }
        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new(StringComparer.OrdinalIgnoreCase);

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var ul = new TagBuilder("ul");
            ul.AddCssClass("pagination");

            for (var i = 1; i <= PageModel.TotalPages; i++)
                ul.InnerHtml.AppendHtml(CreateElement(i));

            output.Content.AppendHtml(ul);
        }

        private TagBuilder CreateElement(int PageNumber)
        {
            var li = new TagBuilder("li");
            var a = new TagBuilder("a");
            a.InnerHtml.AppendHtml(PageNumber.ToString());

            PageUrlValues["page"] = PageNumber;
            if (PageNumber == PageModel.Page)
                li.AddCssClass("active");
            else
            {
                a.Attributes["href"] = "#"; // Url.Action(PageAction, PageUrlValues);
            }

            foreach (var (key, value) in PageUrlValues.Where(v => v.Value is not null))
                a.MergeAttribute($"data-{key}", value.ToString());

            li.InnerHtml.AppendHtml(a);
            return li;
        }
    }
}