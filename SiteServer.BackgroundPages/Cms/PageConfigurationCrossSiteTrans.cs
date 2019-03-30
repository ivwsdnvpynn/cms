﻿using System;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using SiteServer.Utils;
using SiteServer.BackgroundPages.Core;
using SiteServer.CMS.Caches;
using SiteServer.CMS.Core.Enumerations;
using SiteServer.CMS.Database.Core;
using SiteServer.CMS.Fx;
using SiteServer.Utils.Enumerations;

namespace SiteServer.BackgroundPages.Cms
{
    public class PageConfigurationCrossSiteTrans : BasePageCms
    {
        public RadioButtonList RblIsCrossSiteTransChecked;

        private int _currentChannelId;

        public static string GetRedirectUrl(int siteId, int currentChannelId)
        {
            return FxUtils.GetCmsUrl(siteId, nameof(PageConfigurationCrossSiteTrans), new NameValueCollection
            {
                {"CurrentChannelId", currentChannelId.ToString()}
            });
        }

		public void Page_Load(object sender, EventArgs e)
        {
            if (IsForbidden) return;

			FxUtils.CheckRequestParameter("siteId");

            if (IsPostBack) return;

            VerifySitePermissions(ConfigManager.WebSitePermissions.Configration);

            ClientScriptRegisterClientScriptBlock("NodeTreeScript", ChannelLoading.GetScript(SiteInfo, string.Empty, ELoadingType.ConfigurationCrossSiteTrans, null));

            if (AuthRequest.IsQueryExists("CurrentChannelId"))
            {
                _currentChannelId = AuthRequest.GetQueryInt("CurrentChannelId");
                var onLoadScript = ChannelLoading.GetScriptOnLoad(SiteId, _currentChannelId);
                if (!string.IsNullOrEmpty(onLoadScript))
                {
                    ClientScriptRegisterClientScriptBlock("NodeTreeScriptOnLoad", onLoadScript);
                }
            }

            FxUtils.AddListItems(RblIsCrossSiteTransChecked, "无需审核", "需要审核");
            ControlUtils.SelectSingleItem(RblIsCrossSiteTransChecked, SiteInfo.IsCrossSiteTransChecked.ToString());
        }

        public override void Submit_OnClick(object sender, EventArgs e)
		{
		    if (!Page.IsPostBack || !Page.IsValid) return;

		    SiteInfo.IsCrossSiteTransChecked = TranslateUtils.ToBool(RblIsCrossSiteTransChecked.SelectedValue);
				
		    try
		    {
		        DataProvider.Site.Update(SiteInfo);

		        AuthRequest.AddSiteLog(SiteId, "修改默认跨站转发设置");

		        SuccessMessage("默认跨站转发设置修改成功！");
		    }
		    catch(Exception ex)
		    {
		        FailMessage(ex, "默认跨站转发设置修改失败！");
		    }
		}
	}
}
