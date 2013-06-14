using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eleooo.Common;
using Newtonsoft.Json;
using Eleooo.DAL;
using Eleooo.Web;

namespace Eleooo.BLL.Services
{
    class MansionHandler:IHandlerServices
    {

        #region IHandlerServices 成员

        public ServicesResult Query(System.Web.HttpContext context)
        {
            string word = context.Request.Params["q_word"];
            context.Response.ContentType = "text/html";

            int page_num = Utilities.ToInt(context.Request.Params["page_num"]);
            int page_size = Utilities.ToInt(context.Request.Params["per_page"]);
            int total;
            var result = MansionBLL.QueryMansionByWord(word, page_num, page_size, out total)
                     .Select(dr => new { id = dr[SysAreaMansion.IdColumn.ColumnName], fullname = dr[SysAreaMansion.NameColumn.ColumnName], name = Formatter.ReplaceWord(dr[SysAreaMansion.NameColumn.ColumnName].ToString( ), word, "<font color='red'>{0}</font>") }).ToArray( );
            var data = new
            {
                result = result,
                cnt_whole = total,
                info = total > 20 ? "满足条件的记录太多了，请继续缩小搜索范围吧^_^" : null
            };
            return new ServicesResult { data = data };
        }

        public ServicesResult Add(System.Web.HttpContext context)
        {
            throw new NotImplementedException( );
        }

        public ServicesResult Edit(System.Web.HttpContext context)
        {
            throw new NotImplementedException( );
        }

        public ServicesResult Delete(System.Web.HttpContext context)
        {
            throw new NotImplementedException( );
        }

        public ServicesResult Login(System.Web.HttpContext context)
        {
            throw new NotImplementedException( );
        }

        public ServicesResult Logout(System.Web.HttpContext context)
        {
            throw new NotImplementedException( );
        }

        #endregion
    }
}
