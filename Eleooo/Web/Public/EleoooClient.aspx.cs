using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.XPath;
using Eleooo.Common;

namespace Eleooo.Web.Public
{
    public partial class EleoooClient : ActionPage
    {
        const string CLIENT_PATH = "/Client/Eleooo.Client.application";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                XmlDocument doc = new XmlDocument( );
                doc.Load(PhyPath);
                XPathNavigator nav = doc.CreateNavigator( );
                nav.MoveToChild(XPathNodeType.Element);
                XPathNavigator assNav = GetAssemblyIdentityNav(nav);
                if (assNav != null)
                {
                    lblVersion.InnerHtml = GetAssemblyVersion(assNav);
                }
            }
            catch { }
        }
        XPathNavigator GetAssemblyIdentityNav(XPathNavigator nav)
        {
            do
            {
                if (Utilities.Compare(nav.LocalName, "assemblyIdentity"))
                    return nav;
            } while (nav.MoveToChild(XPathNodeType.Element));
            return null;
        }
        string GetAssemblyVersion(XPathNavigator nav)
        {
            if(nav.HasAttributes)
                nav.MoveToFirstAttribute();
            do
            {
                if (Utilities.Compare(nav.LocalName, "version"))
                    return nav.Value;
            } while (nav.MoveToNextAttribute( ));
            return string.Empty;
        }
        string PhyPath
        {
            get
            {
                return Server.MapPath(CLIENT_PATH);
            }
        }
    }
}