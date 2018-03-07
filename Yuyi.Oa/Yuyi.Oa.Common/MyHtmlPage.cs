using System.Text;
//命名空间换成引用它的类的同一个层次
namespace System.Web.Mvc
{
    public static class MyHtmlPage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="currentPage"></param>
        /// <param name="sizePage"></param>
        /// <param name="totalPage"></param>
        /// <returns></returns>
        public static HtmlString ShowPageNavigate(this HtmlHelper htmlHelper,int currentPage,int pageSize,int totalCount)
        {
            //获取重定向的url
            var redierctTo = htmlHelper.ViewContext.RequestContext.HttpContext.Request.Url.AbsolutePath;
            pageSize = pageSize == 0 ? 3 : pageSize;
            var totalPages = Math.Max((totalCount + pageSize - 1)/pageSize, 1);
            var output=new StringBuilder();
            if (currentPage > 1)
            {
                if (currentPage!=1)
                {
                    //处理首页链接
                    output.AppendFormat("<a class='pageLink' href='{0}?pageIndex=1&pageSize={1}'>首页</a>", redierctTo,
                        pageSize);
                }
                if (currentPage>1)
                {
                    //处理上一页链接
                    output.AppendFormat("<a class='pageLink' href='{0}?pageIndex={1}&pageSize={2}'>上一页</a>", redierctTo,currentPage-1,
                        pageSize);

                }
                else
                {
                    output.AppendFormat("<span class='pageLink'>上一页</span>");
                }
                output.Append(" ");
                int currint = 5;
                for (int i = 0; i < 10; i++)
                {
                    //一页最多显示10个页码 前五 后五
                    if ((currentPage+i-currint>=1&&(currentPage+1-currint<=totalPages)))
                    {
                        
                    }
                    else
                    {
                        if (currint==1)
                        {
                            //处理当前页
                            output.AppendFormat("<a class='cpb' href='{0}?pageIndex={1}&pageSize={2}'>{3}</a>",redierctTo,currentPage,pageSize,currentPage);

                        }
                        else
                        {
                            //一般页处理
                            output.AppendFormat("<a class='pageLink' href='{0}?pageIndex={1}&pageSize={2}'>{3}</a>", redierctTo, currentPage+i-currint, pageSize, currentPage+i-currint);

                        }
                    }
                    output.Append(" ");
                }
                if (currentPage<totalPages)
                {
                    //处理下一页链接
                    output.AppendFormat("<a class='pageLink' href='{0}?pageIndex={1}&pageSize={2}'>下一页</a>", redierctTo, currentPage+1,
                        pageSize);
                }
                else
                {
                    //为啥都注释了？
                    output.AppendFormat("<span class='pageLink'>下一页</span>");
                }
                output.Append(" ");

            }
            output.AppendFormat("第{0}页/第{1}页",currentPage,totalPages);//加不加这个统计工具都行
            return new HtmlString(output.ToString());

        }//分页样式代码搞定
    }
}
